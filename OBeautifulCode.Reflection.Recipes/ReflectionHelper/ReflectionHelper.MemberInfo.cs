﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.MemberInfo.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Reflection.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using OBeautifulCode.Assertion.Recipes;

    /// <summary>
    /// Provides useful methods related to reflection.
    /// </summary>
#if !OBeautifulCodeReflectionRecipesProject
    internal
#else
    public
#endif
    static partial class ReflectionHelper
    {
        /// <summary>
        /// Determines if the specified member is compiler-generated.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns>
        /// True if the member is compiler-generated, otherwise false.
        /// </returns>
        public static bool IsCompilerGenerated(
            this MemberInfo memberInfo)
        {
            var result = memberInfo.CustomAttributes.Select(s => s.AttributeType).Contains(typeof(CompilerGeneratedAttribute));

            return result;
        }

        /// <summary>
        /// Gets the underlying type of the specified <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns>
        /// The underlying type of the specified <see cref="MemberInfo"/>.
        /// </returns>
        public static Type GetUnderlyingType(
            this MemberInfo memberInfo)
        {
            new { memberInfo }.AsArg().Must().NotBeNull();

            switch (memberInfo.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)memberInfo).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)memberInfo).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).PropertyType;
                default:
                    throw new ArgumentException("Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo");
            }
        }
    }
}
