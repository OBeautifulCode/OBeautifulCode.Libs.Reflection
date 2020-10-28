﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncludeConstructorsWith.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Reflection.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    /// <summary>
    /// Specifies which constructors to include when finding constructors with parameters that match a set of properties.
    /// </summary>
#if !OBeautifulCodeReflectionSolution
    [global::System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Reflection.Recipes", "See package version number")]
    internal
#else
    public
#endif
    enum IncludeConstructorsWith
    {
        /// <summary>
        /// Invalid (default).
        /// </summary>
        Invalid,

        /// <summary>
        /// Include constructors where every property matches a constructor parameter.
        /// If there are any extra parameters, the constructor is included.
        /// </summary>
        ExtraParametersIfAny,

        /// <summary>
        /// Include constructors where every property matches a constructor parameter
        /// and all extra constructor parameters, if there are any, have a default value.
        /// </summary>
        ExtraParametersIfAnyHavingDefaultValues,

        /// <summary>
        /// Include constructors where every constructor parameter matches a property
        /// and every property matches a constructor parameter.
        /// </summary>
        NoExtraParameters,
    }
}