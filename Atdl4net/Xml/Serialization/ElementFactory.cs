#region Copyright (c) 2010, Cornerstone Technology Limited. http://atdl4net.org
//
//   This software is released under both commercial and open-source licenses.
//
//   If you received this software under the commercial license, the terms of that license can be found in the
//   Commercial.txt file in the Licenses folder.  If you received this software under the open-source license,
//   the following applies:
//
//      This file is part of Atdl4net.
//
//      Atdl4net is free software: you can redistribute it and/or modify it under the terms of the GNU Lesser General Public 
//      License as published by the Free Software Foundation, version 3.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using Atdl4net.Diagnostics;
using Atdl4net.Diagnostics.Exceptions;
using Atdl4net.Fix;
using Atdl4net.Resources;
using Atdl4net.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using ThrowHelper = Atdl4net.Diagnostics.ThrowHelper;

namespace Atdl4net.Xml.Serialization
{
    public class ElementFactory : INotifyClassDeserialized
    {
        private const string ExceptionContext = "ElementFactory";

        private Type _notifyCreationOfType;
        private ElementDefinition _elementDefinition;
        private Dictionary<string, object> _elementValueCache = new Dictionary<string, object>();

        public ElementFactory(ElementDefinition elementDefinition, Type notifyCreationOfType)
        {
            Logger.DebugFormat("ElementFactory created; root ElementName='{0}'.", elementDefinition.ElementName);

            _elementDefinition = elementDefinition;
            _notifyCreationOfType = notifyCreationOfType;
        }

        public object DeserializeElement(XElement element)
        {
            Logger.DebugFormat("DeserializeElement called; first 50 characters of XML='{0}'.", element.ToString().Substring(0, 50));

            return CreateObject(_elementDefinition, element, null);
        }

        private object CreateObject(ElementDefinition definition, XElement sourceElement, object parentObject)
        {
            Logger.DebugFormat("CreateObject(ElementDefinition, XElement) called; ElementName='{0}.'", definition.ElementName);

            Type[] constructorParameterTypes;
            object[] constructorParameterValues;

            GetConstructorParameters(definition, sourceElement, parentObject, out constructorParameterTypes, out constructorParameterValues);

            object newObject = CreateRawObject(definition.TargetType, constructorParameterTypes, constructorParameterValues);

            if (definition.CacheElementValueInstruction != null)
                _elementValueCache[definition.CacheElementValueInstruction.CacheKey] = newObject;

            IEnumerable<XAttribute> attributes = sourceElement.Attributes();

            try
            {
                ProcessAttributes(definition.TargetType, definition.Attributes, attributes, newObject);
            }
            catch (Atdl4netException ex)
            {
                throw ThrowHelper.Rethrow(this, ex, new ExceptionInfo(sourceElement), ErrorMessages.GeneralElementProcessingError, string.Empty);
            }

            ProcessChildren(definition, sourceElement, newObject);

            if (newObject.GetType() == _notifyCreationOfType)
                NotifyClassDeserialized(_notifyCreationOfType, newObject);

            return newObject;
        }

        /// <summary>
        /// Creates and populates a new instance of the generic type given in the supplied GenericTypeElementDefinition,
        /// using the supplied XML element as the source.
        /// </summary>
        /// <param name="genericTypeDefinition">The definition of the generic type to be created.</param>
        /// <param name="sourceElement">The source XML element for this object.</param>
        /// <param name="parentObject">The parent object of this object.</param>
        /// <returns>A new instance of the required type, typically of the form SomeType&lt;&gt;.</returns>
        /// <remarks>The inner type of the target type is specified via an attribute on the supplied input XML element.</remarks>
        /// <exception cref="MissingMandatoryValueException">Thrown when...<ul>
        /// <li></li>
        /// </ul></exception>
        private object CreateObject(GenericTypeElementDefinition genericTypeDefinition, XElement sourceElement, object parentObject)
        {
            Logger.DebugFormat("CreateObject(GenericTypeElementDefinition, XElement) called; ElementName='{0}'.", genericTypeDefinition.ElementName);

            object[] constructorParameterValues;
            Type[] constructorParameterTypes;

            GetConstructorParameters(genericTypeDefinition, sourceElement, parentObject, out constructorParameterTypes, out constructorParameterValues);

            string innerTypeName = ReadAttribute(sourceElement.Attributes(), genericTypeDefinition.AttributeForInnerType, typeof(string)) as string;

            if (string.IsNullOrEmpty(innerTypeName))
                throw ThrowHelper.New<MissingMandatoryValueException>(this, new ExceptionInfo(sourceElement), ErrorMessages.MissingMandatoryAttribute,
                    genericTypeDefinition.AttributeForInnerType.LocalName, genericTypeDefinition.ElementName.LocalName);

            Type innerType;

            if (string.IsNullOrEmpty(genericTypeDefinition.InnerTypeNamespace))
                innerType = Type.GetType(innerTypeName);
            else
            {
                innerType = Type.GetType(string.Format("{0}.{1}", genericTypeDefinition.InnerTypeNamespace, innerTypeName));
            }

            if (innerType == null)
                throw ThrowHelper.New<InvalidFieldValueException>(this, new ExceptionInfo(sourceElement), ErrorMessages.UnrecognisedTypeError, innerTypeName,
                    genericTypeDefinition.AttributeForInnerType.LocalName, genericTypeDefinition.ElementName.LocalName);

            object newObject = CreateRawObject(genericTypeDefinition.TargetType, new Type[] { innerType }, constructorParameterTypes, constructorParameterValues);

            IEnumerable<XAttribute> attributes = sourceElement.Attributes();

            try
            {
                ProcessAttributes(newObject.GetType(), genericTypeDefinition.Attributes, attributes, newObject);
                ProcessAttributes(newObject.GetType(), genericTypeDefinition.InnerTypeToAttributesMap[innerType], attributes, newObject);
            }
            catch (MissingMandatoryValueException ex)
            {
                throw ThrowHelper.Rethrow(this, ex, new ExceptionInfo(sourceElement), ErrorMessages.GeneralElementProcessingError, string.Empty);
            }

            ProcessChildren(genericTypeDefinition, sourceElement, newObject);

            if (newObject.GetType() == _notifyCreationOfType)
                NotifyClassDeserialized(_notifyCreationOfType, newObject);

            return newObject;
        }

        private object CreateObject(MultiTypeElementDefinition multiTypeDefinition, XElement sourceElement, object parentObject)
        {
            Logger.DebugFormat("CreateObject(MultiTypeElementDefinition, XElement) called; ElementName='{0}'.", multiTypeDefinition.ElementName);

            object[] constructorParameterValues;
            Type[] constructorParameterTypes;

            GetConstructorParameters(multiTypeDefinition, sourceElement, parentObject, out constructorParameterTypes, out constructorParameterValues);

            string typeName = ReadAttribute(sourceElement.Attributes(), multiTypeDefinition.AttributeForType, typeof(string)) as string;

            if (string.IsNullOrEmpty(typeName))
                throw ThrowHelper.New<MissingMandatoryValueException>(this, new ExceptionInfo(sourceElement), ErrorMessages.MissingMandatoryAttribute,
                    multiTypeDefinition.AttributeForType.LocalName, multiTypeDefinition.ElementName.LocalName);
            
            // If the value for the typename is in an XML namespace, remove it.
            if (typeName.Contains(':') && typeName.IndexOf(':') < typeName.Length - 1)
                typeName = typeName.Substring(typeName.IndexOf(':') + 1);

            Type targetType;

            if (string.IsNullOrEmpty(multiTypeDefinition.TypeNamespace))
                targetType = Type.GetType(typeName);
            else
            {
                targetType = Type.GetType(string.Format("{0}.{1}", multiTypeDefinition.TypeNamespace, typeName));
            }

            if (targetType == null)
                throw ThrowHelper.New<InvalidFieldValueException>(this, new ExceptionInfo(sourceElement), ErrorMessages.UnrecognisedTypeError, typeName,
                    multiTypeDefinition.AttributeForType.LocalName, multiTypeDefinition.ElementName.LocalName);

            object newObject = CreateRawObject(targetType, constructorParameterTypes, constructorParameterValues);

            IEnumerable<XAttribute> attributes = sourceElement.Attributes();

            try
            {
                ProcessAttributes(newObject.GetType(), multiTypeDefinition.Attributes, attributes, newObject);
                ProcessAttributes(newObject.GetType(), multiTypeDefinition.TypeToAttributesMap[targetType], attributes, newObject);
            }
            catch (Atdl4netException ex)
            {
                throw ThrowHelper.Rethrow(this, ex, new ExceptionInfo(sourceElement), ErrorMessages.GeneralElementProcessingError, string.Empty);
            }

            ProcessChildren(multiTypeDefinition, sourceElement, newObject);

            if (newObject.GetType() == _notifyCreationOfType)
                NotifyClassDeserialized(_notifyCreationOfType, newObject);

            return newObject;
        }

        private static object CreateRawObject(Type outerType, Type[] innerTypes, Type[] argTypes, params object[] args)
        {
            Logger.DebugFormat("CreateObject(Type, Type[], Type[], params object[]) called (creating generic type); Outer type={0}.", outerType.FullName);

            Type specificType = outerType.MakeGenericType(innerTypes);

            ConstructorInfo classConstructor = specificType.GetConstructor(argTypes);

            if (classConstructor == null)
                throw ThrowHelper.New<InternalErrorException>(ExceptionContext, InternalErrors.NoConstructorFoundForSpecifiedArgumentTypes, outerType.FullName);

            return classConstructor.Invoke(args);
        }

        private static object CreateRawObject(Type targetType, Type[] argTypes, params object[] args)
        {
            Logger.DebugFormat("CreateObject(Type, Type[], params object[]) called; Type={0}.", targetType.FullName);

            ConstructorInfo classConstructor = targetType.GetConstructor(argTypes);

            if (classConstructor == null)
                throw ThrowHelper.New<InternalErrorException>(ExceptionContext, InternalErrors.NoConstructorFoundForSpecifiedArgumentTypes, targetType.FullName);

            return classConstructor.Invoke(args);
        }

        private void GetConstructorParameters(ElementDefinition elementDefinition, XElement sourceElement, object parentObject,
            out Type[] constructorParameterTypes, out object[] constructorParameterValues)
        {
            Logger.DebugFormat("GetConstructorParameters called; ElementName='{0}'.", elementDefinition.ElementName);

            if (elementDefinition.ConstructorParameters != null)
            {
                constructorParameterValues = new object[elementDefinition.ConstructorParameters.Length];
                constructorParameterTypes = new Type[elementDefinition.ConstructorParameters.Length];

                for (int n = 0; n < elementDefinition.ConstructorParameters.Length; n++)
                {
                    switch (elementDefinition.ConstructorParameters[n].SourceType)
                    {
                        case SourceType.ElementAttribute:
                            constructorParameterValues[n] = ReadAttribute(sourceElement.Attributes(), elementDefinition.ConstructorParameters[n].Source, elementDefinition.ConstructorParameters[n].Type);
                            break;

                        case SourceType.ParentObject:
                            constructorParameterValues[n] = parentObject;
                            break;

                        case SourceType.NamedPredecessor:
                            {
                                object value;

                                if (_elementValueCache.TryGetValue(elementDefinition.ConstructorParameters[n].Source, out value))
                                    constructorParameterValues[n] = value;
                            }
                            break;
                    }

                    constructorParameterTypes[n] = elementDefinition.ConstructorParameters[n].Type;
                }
            }
            else
            {
                constructorParameterValues = new ConstructorParameter[] { };
                constructorParameterTypes = new Type[] { };
            }
        }

        private void ProcessAttributes(Type targetType, ElementAttribute[] attributeDefinitions, IEnumerable<XAttribute> attributes, object target)
        {
            Logger.DebugFormat("ProcessAttributes called; Target type={0}.", targetType.FullName);

            foreach (ElementAttribute attrDefn in attributeDefinitions)
            {
                object value = null;

                if (attrDefn.Type.IsEnum && attrDefn.EnumValues != null)
                    value = ReadAttribute(attributes, attrDefn.XmlName, attrDefn.Type, attrDefn.EnumValues);
                else
                    value = ReadAttribute(attributes, attrDefn.XmlName, attrDefn.Type);

                if (attrDefn.Required == Required.Mandatory && value == null)
                    throw ThrowHelper.New<MissingMandatoryValueException>(this, ErrorMessages.MissingMandatoryAttribute,
                        attrDefn.XmlName.LocalName, targetType.Name);

                if (value == null)
                    continue;

                // Process indirect properties (only one level of indirect is supported).
                if (attrDefn.Property.Contains("."))
                {
                    string[] names = attrDefn.Property.Split(new char[] { '.' });

                    if (names.Length != 2)
                        throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.InvalidPropertyIndirection, attrDefn.Property);

                    PropertyInfo outerProperty = targetType.GetProperty(names[0]);

                    if (outerProperty == null)
                        throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.PropertyNotFoundOnObjectInternal, names[0], targetType.FullName);

                    object innerObject = outerProperty.GetValue(target, null);

                    if (innerObject == null)
                        throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.UnableToRetrievePropertyValueOnObject, attrDefn.Property, targetType.FullName);

                    PropertyInfo property = outerProperty.PropertyType.GetProperty(names[1]);

                    if (property == null)
                        throw ThrowHelper.New<InvalidPropertyOnObjectException>(this, ErrorMessages.PropertyNotFoundOnObject, attrDefn.Property, targetType.Name);

                    SetPropertyValue(property, innerObject, value);
                }
                else
                {
                    PropertyInfo property = targetType.GetProperty(attrDefn.Property);

                    if (property == null)
                        throw ThrowHelper.New<InvalidPropertyOnObjectException>(this, ErrorMessages.PropertyNotFoundOnObject, attrDefn.Property, targetType.Name);

                    SetPropertyValue(property, target, value);
                }
            }
        }

        private void ProcessChildren(ElementDefinition definition, XElement sourceElement, object target)
        {
            Logger.DebugFormat("ProcessChildren called; ElementName='{0}'", definition.ElementName);

            // We have to reflect the target type as we can't rely on the Definition to contain it (e.g. MultiTypeElementDefinition).
            Type targetType = target.GetType();

            foreach (ChildElementDefinition childDefinition in definition.ChildElements)
            {
                bool isRecursiveDefinition = childDefinition.ElementDefinition is RecursiveTypeElementDefinition;
                bool hasContainerElement = !isRecursiveDefinition && childDefinition.ContainerElementName != null;

                IEnumerable<XElement> matchingChildElements;

                ElementDefinition targetDefinition = isRecursiveDefinition ? definition : childDefinition.ElementDefinition;

                if (hasContainerElement)
                {
                    XElement containerElement = (from e in sourceElement.Elements(childDefinition.ContainerElementName) select e).FirstOrDefault();

                    if (containerElement == null)
                        return;

                    matchingChildElements = from e in containerElement.Elements(childDefinition.ElementDefinition.ElementName) select e;
                }
                else
                    matchingChildElements = from e in sourceElement.Elements(targetDefinition.ElementName) select e;

                foreach (XElement childElement in matchingChildElements)
                {
                    object childObject;

                    if (targetDefinition is GenericTypeElementDefinition)
                        childObject = CreateObject(targetDefinition as GenericTypeElementDefinition, childElement, target);
                    else if (targetDefinition is MultiTypeElementDefinition)
                        childObject = CreateObject(targetDefinition as MultiTypeElementDefinition, childElement, target);
                    else
                        childObject = CreateObject(targetDefinition, childElement, target);

                    PropertyInfo property = targetType.GetProperty(childDefinition.ContainerProperty);

                    if (property == null)
                        throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.PropertyNotFoundOnObjectInternal, 
                            childDefinition.ContainerProperty, targetType.FullName);
                    try
                    {
                        // For the case of MultiTypeElementDefinition we must use the reflected type 
                        ProcessChildProperty(childDefinition, property, targetDefinition.TargetType ?? childObject.GetType(), target, childObject);
                    }
                    catch (Atdl4netException ex)
                    {
                        throw ThrowHelper.Rethrow(this, ex, new ExceptionInfo(childElement), ErrorMessages.GeneralElementProcessingError,
                            definition.ElementName.LocalName);
                    }
                    catch (ArgumentException ex)
                    {
                        throw ThrowHelper.New<Atdl4netException>(this, ex, new ExceptionInfo(childElement), ErrorMessages.GeneralElementProcessingError,
                            definition.ElementName.LocalName, ex.Message);
                    }
                }
            }
        }

        private void ProcessChildProperty(ChildElementDefinition childDefinition, PropertyInfo property, Type targetType, object target, object childObject)
        {
            Logger.DebugFormat("ProcessChildProperty called; ElementName='{0}', Property={1}.", childDefinition.ElementDefinition.ElementName, property.Name);

            string containerMethod;

            if (childDefinition.ContainerMethod is StandardContainerMethod)
            {
                StandardContainerMethod method = (StandardContainerMethod)childDefinition.ContainerMethod;

                if (method == StandardContainerMethod.Assign)
                {
                    SetPropertyValue(property, target, childObject);

                    return;
                }
                else
                    containerMethod = Enum.GetName(typeof(StandardContainerMethod), childDefinition.ContainerMethod);
            }
            else
                containerMethod = childDefinition.ContainerMethod as string;

            MethodInfo targetMethod = property.PropertyType.GetMethod(containerMethod, new Type[] { targetType });

            if (targetMethod == null)
                throw ThrowHelper.New<InternalErrorException>(this, InternalErrors.ContainerMethodNotFoundOnObject,
                    containerMethod, targetType.FullName);
            try
            {
                targetMethod.Invoke(property.GetValue(target, null), new object[] { childObject });
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                    throw ThrowHelper.Rethrow(this, ex.InnerException, ErrorMessages.UnableToInvokeMethodError, 
                        string.Format("the {0} method on the {1} property", containerMethod, property.Name));
                else
                    throw;
            }
        }

        private static object ReadAttribute(IEnumerable<XAttribute> attributes, XName attributeName, Type type)
        {
            Logger.DebugFormat("ReadAttribute(IEnumerable<XAttribute>, XName, Type) called; Attribute name='{0}'", attributeName);

            XAttribute attribute = attributes.FirstOrDefault(a => a.Name == attributeName);

            if (attribute == null)
                return null;

            // NB Most simple enums are dealt with in the other overload of ReadAttribute.
            if (type.IsEnum)
            {
                try
                {
                    return Enum.Parse(type, attribute.Value);
                }
                catch (ArgumentException ex)
                {
                    throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ex, ErrorMessages.InvalidValueEnumParseFailure, attribute.Value, type.Name);
                }
            }
            else
            {
                try
                {
                    return ValueConverter.ConvertTo(attribute.Value, type);
                }
                catch (FormatException ex)
                {
                    throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ex, ErrorMessages.DataConversionError,
                        attribute.Value, type.Name, attributeName.LocalName);
                }
            }
        }

        private static object ReadAttribute(IEnumerable<XAttribute> attributes, XName attributeName, Type enumType, Dictionary<string, Enum> enumValues)
        {
            Logger.DebugFormat("ReadAttribute(IEnumerable<XAttribute>, XName, Type, Dictionary<string, Enum>) called; Attribute name='{0}'", attributeName);

            XAttribute attribute = attributes.FirstOrDefault(a => a.Name == attributeName);

            if (attribute == null)
                return null;

            if (!enumValues.ContainsKey(attribute.Value))
                throw ThrowHelper.New<InvalidFieldValueException>(ExceptionContext, ErrorMessages.InvalidValueEnumParseFailure, attribute.Value, enumType.Name);

            return Enum.ToObject(enumType, enumValues[attribute.Value]);
        }

        private static void SetPropertyValue(PropertyInfo property, object target, object value)
        {
            Logger.DebugFormat("SetPropertyValue called; Target object type={0}, property={1}, value='{2}'.", target.GetType().FullName, property.Name, value);

            try
            {
                if (property.PropertyType == value.GetType())
                    property.SetValue(target, value, null);
                else
                {
                    object newValue = CreateRawObject(property.PropertyType, new Type[] { value.GetType() }, value);

                    property.SetValue(target, newValue, null);
                }
            }
            catch (ArgumentException ex)
            {
                throw ThrowHelper.New<InternalErrorException>(ExceptionContext, ex, InternalErrors.UnableToSetPropertyValueOnObject,
                    property.Name, value, target.GetType().FullName);
            }
        }

        #region INotifyClassDeserialized Members & Support Methods

        private void NotifyClassDeserialized(Type classType, object extraInfo)
        {
            EventHandler<ClassDeserializedEventArgs> classDeserialized = ClassDeserialized;

            if (classDeserialized!=null)
                classDeserialized(this, new ClassDeserializedEventArgs(classType, extraInfo));
        }

        public event EventHandler<ClassDeserializedEventArgs> ClassDeserialized;

        #endregion
    }
}