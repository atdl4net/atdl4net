#region Copyright (c) 2010-2011, Cornerstone Technology Limited. http://atdl4net.org
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
//      License as published by the Free Software Foundation, either version 2.1 of the License, or (at your option) any later version.
// 
//      Atdl4net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
//      of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.
//
//      You should have received a copy of the GNU Lesser General Public License along with Atdl4net.  If not, see
//      http://www.gnu.org/licenses/.
//
#endregion

using System;
using System.Reflection;
using Common.Logging;

namespace Atdl4net.Diagnostics
{
    /// <summary>
    /// Helper class for generating new instances of exceptions using the provided parameters
    /// </summary>
    public class ThrowHelper
    {
        private static readonly ILog _log = LogManager.GetLogger("ExceptionManagement");

        private ThrowHelper() { }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, innerException, null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), innerException, null);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="info">The info.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, ExceptionInfo info, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="info">The info.</param>
        /// <param name="message">The message.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, ExceptionInfo info, string message) where T : System.Exception
        {
            T ex = CreateException<T>(source, message, innerException, info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="info">The info.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, ExceptionInfo info, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Creates an exception of the specified type and initializes it using the values supplied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="info">The info.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static T New<T>(object source, Exception innerException, ExceptionInfo info, string format, params object[] args) where T : System.Exception
        {
            T ex = CreateException<T>(source, string.Format(format, args), innerException, info);

            _log.Error("Exception created by ThrowHelper", ex);

            return ex;
        }

        /// <summary>
        /// Wraps the supplied exception in a new exception of the same type as that supplied, in order to get a
        /// decent error message back to the end-user.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static Exception Rethrow(object source, Exception ex, string format, object arg)
        {
            Exception newException = Rethrow(source, ex, null, format, arg);

            _log.Error("Exception rethrown by ThrowHelper", newException);

            return newException;
        }

        /// <summary>
        /// Wraps the supplied exception in a new exception of the same type as that supplied, in order to get a
        /// decent error message back to the end-user.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="info">The info.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg">The arg.</param>
        /// <returns>A new exception of the specified type.</returns>
        public static Exception Rethrow(object source, Exception ex, ExceptionInfo info, string format, object arg)
        {
            Type classType = ex.GetType();

            ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string), typeof(Exception) });

            Exception exception = (Exception)classConstructor.Invoke(new object[] { string.Format(format, arg, ex.Message), ex });

            exception.Source = source.ToString();

            if (info != null)
                info.PopulateExceptionData(exception.Data);

            return exception;
        }

        /// <summary>
        /// Workaround limitation in C# 2.0/4.0 - can't create an instance of a generic type with parameters.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static T CreateException<T>(object source, string message, ExceptionInfo info) where T : System.Exception
        {
            Type classType = typeof(T);

            switch (classType.Name)
            {
                // Special treatment is needed for ArgumentOutOfRangeException and ArgumentNullException because the constructor that takes 
                // a single string for these types makes its own message.
                case "ArgumentOutOfRangeException":
                case "ArgumentNullException":
                    {
                        ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string), typeof(string) });

                        T exception = (T)classConstructor.Invoke(new object[] { "Value", message });

                        exception.Source = source.ToString();

                        if (info != null)
                            info.PopulateExceptionData(exception.Data);

                        return exception;
                    }

                default:
                    {
                        ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string) });

                        T exception = (T)classConstructor.Invoke(new object[] { message });

                        exception.Source = source.ToString();

                        if (info != null)
                            info.PopulateExceptionData(exception.Data);

                        return exception;
                    }
            }
        }

        /// <summary>
        /// Workaround limitation in C# 2.0/4.0 - can't create an instance of a generic type with parameters.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static T CreateException<T>(object source, string message, Exception innerException, ExceptionInfo info) where T : System.Exception
        {
            Type classType = typeof(T);

            ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { typeof(string), typeof(Exception) });

            T exception = (T)classConstructor.Invoke(new object[] { message, innerException });

            exception.Source = source.ToString();

            if (info != null)
                info.PopulateExceptionData(exception.Data);

            return exception;
        }
    }
}