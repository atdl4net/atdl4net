#region Copyright (c) 2010-2011, Steve Wilkinson (author)
//
//   This software is released under the MIT License..
//
#endregion

using System;
using System.Collections.Generic;
using Atdl4net.Model.Enumerations;
using Atdl4net.Xml.Serialization;

namespace Atdl4net.Xml
{
    /// <summary>
    /// Provides the string representation of all the enumerated types within FIXatdl.
    /// </summary>
    public static class EnumDefinitions
    {
        /// <summary>Border_t enumeration.</summary>
        public static readonly EnumDefinition Border_t = new EnumDefinition(typeof(Border_t), new Dictionary<string, Enum>
            {
                { "None", Model.Enumerations.Border_t.None },
                { "Line", Model.Enumerations.Border_t.Line}
            });

        /// <summary>Inclusion_t enumeration.</summary>
        public static readonly EnumDefinition Inclusion_t = new EnumDefinition(typeof(Inclusion_t), new Dictionary<string, Enum>
            {
                { "Include", Model.Enumerations.Inclusion_t.Include },
                { "Exclude", Model.Enumerations.Inclusion_t.Exclude}
            });

        /// <summary>IncrementPolicy_t enumeration.</summary>
        public static readonly EnumDefinition IncrementPolicy_t = new EnumDefinition(typeof(IncrementPolicy_t), new Dictionary<string, Enum>
            {
                { "Static", Model.Enumerations.IncrementPolicy_t.Static },
                { "LotSize", Model.Enumerations.IncrementPolicy_t.LotSize },
                { "Tick", Model.Enumerations.IncrementPolicy_t.Tick}
            });

        /// <summary>InitPolicy_t enumeration.</summary>
        public static readonly EnumDefinition InitPolicy_t = new EnumDefinition(typeof(InitPolicy_t), new Dictionary<string, Enum>
            {
                { "UseValue", Model.Enumerations.InitPolicy_t.UseValue },
                { "UseFixField", Model.Enumerations.InitPolicy_t.UseFixField}
            });

        /// <summary>LogicOperator_t enumeration.</summary>
        public static readonly EnumDefinition LogicOperator_t = new EnumDefinition(typeof(LogicOperator_t), new Dictionary<string, Enum>
            {
                { "AND", Model.Enumerations.LogicOperator_t.And },
                { "OR", Model.Enumerations.LogicOperator_t.Or},
                { "XOR", Model.Enumerations.LogicOperator_t.Xor },
                { "NOT", Model.Enumerations.LogicOperator_t.Not}
            });

        /// <summary>Operator_t enumeration.</summary>
        public static readonly EnumDefinition Operator_t = new EnumDefinition(typeof(Operator_t), new Dictionary<string, Enum>
            {
                { "EX", Model.Enumerations.Operator_t.Exist },
                { "NX", Model.Enumerations.Operator_t.NotExist},
                { "EQ", Model.Enumerations.Operator_t.Equal },
                { "LT", Model.Enumerations.Operator_t.LessThan},
                { "GT", Model.Enumerations.Operator_t.GreaterThan },
                { "NE", Model.Enumerations.Operator_t.NotEqual},
                { "LE", Model.Enumerations.Operator_t.LessThanOrEqual },
                { "GE", Model.Enumerations.Operator_t.GreaterThanOrEqual}
            });

        /// <summary>Orientation_t enumeration.</summary>
        public static readonly EnumDefinition Orientation_t = new EnumDefinition(typeof(Orientation_t), new Dictionary<string, Enum>
            {
                { "HORIZONTAL", Model.Enumerations.Orientation_t.Horizontal },
                { "VERTICAL", Model.Enumerations.Orientation_t.Vertical}
            });

        /// <summary>Region enumeration. The underlying enumeration has the [Flags] attribute.</summary>
        public static readonly EnumDefinition Region = new EnumDefinition(typeof(Region), new Dictionary<string, Enum>
            {
                { string.Empty, Model.Enumerations.Region.None },
                { "TheAmericas", Model.Enumerations.Region.TheAmericas},
                { "EuropeMiddleEastAfrica", Model.Enumerations.Region.EuropeMiddleEastAfrica },
                { "AsiaPacificJapan", Model.Enumerations.Region.AsiaPacificJapan}
            });

        /// <summary>Use_t enumeration.</summary>
        public static readonly EnumDefinition Use_t = new EnumDefinition(typeof(Use_t), new Dictionary<string, Enum>
            {
                { "optional", Model.Enumerations.Use_t.Optional },
                { "required", Model.Enumerations.Use_t.Required }
            });
    }
}