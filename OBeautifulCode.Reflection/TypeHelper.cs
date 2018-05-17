﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Math source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Spritely.Recipes;

    /// <summary>
    /// Provides useful methods related to reflection.
    /// </summary>
#if !OBeautifulCodeReflectionRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Reflection", "See package version number")]
#endif
#if !OBeautifulCodeReflectionRecipesProject
    internal
#else
    public
#endif
    static class TypeHelper
    {
        /// <summary>
        /// Determines if a type is an anonymous type.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>A value indicating whether or not the type provided is anonymous.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsAnonymous(
            this Type type)
        {
            new { type }.Must().NotBeNull().OrThrow();

            var result = Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                             && type.Namespace == null
                             && type.IsGenericType && type.Name.Contains("AnonymousType")
                             && (type.Name.StartsWith("<>", StringComparison.Ordinal) || type.Name.StartsWith("VB$", StringComparison.Ordinal))
                             && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;

            return result;
        }

        /// <summary>
        /// Determines if a type is an anonymous type using a faster, but potentially
        /// less accurate heuristic than <see cref="IsAnonymous(Type)"/>.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>A value indicating whether or not the type provided is anonymous.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsAnonymousFastCheck(this Type type)
        {
            new { type }.Must().NotBeNull().OrThrow();

            var result = type.Namespace == null;

            return result;
        }
    }
}
