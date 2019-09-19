﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Math source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using OBeautifulCode.Validation.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// Provides useful methods related to reflection.
    /// </summary>
#if !OBeautifulCodeReflectionRecipesProject
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Reflection", "See package version number")]
    internal
#else
    public
#endif
    static class TypeHelper
    {
        private static readonly Type[] CollectionTypes =
        {
            typeof(Collection<>),
            typeof(ICollection<>),
            typeof(ReadOnlyCollection<>),
            typeof(IReadOnlyCollection<>),
            typeof(List<>),
            typeof(IList<>),
            typeof(IReadOnlyList<>)
        };

        private static readonly Type[] DictionaryTypes =
        {
            typeof(Dictionary<,>),
            typeof(IDictionary<,>),
            typeof(ReadOnlyDictionary<,>),
            typeof(IReadOnlyDictionary<,>),
            typeof(ConcurrentDictionary<,>),
        };

        /// <summary>
        /// Determines if a type is an anonymous type.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>A value indicating whether or not the type provided is anonymous.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsAnonymous(
            this Type type)
        {
            new { type }.Must().NotBeNull();

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
            new { type }.Must().NotBeNull();

            var result = type.Namespace == null;

            return result;
        }

        /// <summary>
        /// Determines if a type if assignable to another type.
        /// </summary>
        /// <remarks>
        /// Adapted from: <a href="https://stackoverflow.com/questions/74616/how-to-detect-if-type-is-another-generic-type/1075059#1075059" />.
        /// </remarks>
        /// <param name="type">The current type.</param>
        /// <param name="otherType">The type to check for ability to assign to.</param>
        /// <param name="treatUnboundGenericAsAssignableTo">Treats an unbound generic as a type that can be assigned to (e.g. IsAssignableTo(List&lt;int&gt;, List&lt;&gt;)).</param>
        /// <returns>
        /// true if <paramref name="type"/> can be assigned to <paramref name="otherType"/>; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="otherType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="type"/>.<see cref="Type.IsGenericTypeDefinition"/> is true.</exception>
        public static bool IsAssignableTo(
            this Type type,
            Type otherType,
            bool treatUnboundGenericAsAssignableTo = false)
        {
            // A copy of this method exists in OBC.Validation.
            // Any bug fixes made here should also be applied to OBC.Validation.
            // OBC.Validation cannot take a reference to OBC.Reflection because it creates a circular reference
            // since OBC.Reflection itself depends on OBC.Validation.
            // We considered converting all usages of OBC.Validation in OBC.Reflection to vanilla if..then..throw
            // but decided against because it was going to be too much work and we like the way OBC.Validation reads (e.g. Must().NotBeNull()) in OBC.Reflection.
            // The other option was to create a third package that OBC.Validation and OBC.Reflection could both depend on, but
            // that didn't feel right because this method naturally fits with TypeHelper.

            new { type }.Must().NotBeNull();
            new { otherType }.Must().NotBeNull();
            type.IsGenericTypeDefinition.Named(Invariant($"{nameof(type)}.{nameof(Type.IsGenericTypeDefinition)}")).Must().BeFalse();

            // type is equal to the other type
            if (type == otherType)
            {
                return true;
            }

            // type is assignable to the other type
            if (otherType.IsAssignableFrom(type))
            {
                return true;
            }

            // type is generic and other type is an unbounded generic type
            if (treatUnboundGenericAsAssignableTo && otherType.IsGenericTypeDefinition)
            {
                // type's unbounded generic version is the other type
                if (type.IsGenericType && type.GetGenericTypeDefinition() == otherType)
                {
                    return true;
                }

                // type implements an interface who's unbounded generic version is the other type
                if (type.GetInterfaces().Any(_ => _.IsGenericType && (_.GetGenericTypeDefinition() == otherType)))
                {
                    return true;
                }

                var baseType = type.BaseType;
                if (baseType == null)
                {
                    return false;
                }

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                var result = baseType.IsAssignableTo(otherType, treatUnboundGenericAsAssignableTo);
                return result;
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified type is a class type, that's not anonymous, and is closed.
        /// </summary>
        /// <remarks>
        /// This is basically asking, "Is this a class type that can be constructed/new-ed up?"
        /// </remarks>
        /// <param name="type">The type.</param>
        /// <returns>
        /// true if the specified type is a class type, non-anonymous, and closed.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsNonAnonymousClosedClassType(
            this Type type)
        {
            new { type }.Must().NotBeNull();

            var result =
                type.IsClass &&
                (!type.IsAnonymous()) &&
                (!type.IsGenericTypeDefinition); // can't do an IsAssignableTo check on generic type definitions

            return result;
        }

        /// <summary>
        /// Determines if the specified type is <see cref="Nullable{T}"/>.
        /// </summary>
        /// <remarks>Adapted from: <a href="https://stackoverflow.com/a/41281601/356790" />.</remarks>
        /// <param name="type">The type.</param>
        /// <returns>
        /// true if the specified type is <see cref="Nullable{T}"/>, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsNullableType(
            this Type type)
        {
            new { type }.Must().NotBeNull();

            var result = Nullable.GetUnderlyingType(type) != null;

            return result;
        }
        
        /// <summary>
        /// Determines if the specified type is one of the following <see cref="System"/> collection types: <see cref="CollectionTypes"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// true if the specified type is a <see cref="System"/> collection type; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsSystemCollectionType(
            this Type type)
        {
            new { type }.Must().NotBeNull();

            if (!type.IsGenericType)
            {
                return false;
            }

            var genericType = type.GetGenericTypeDefinition();

            var result = CollectionTypes.Any(_ => genericType == _);
            return result;
        }

        /// <summary>
        /// Determines if the specified type is one of the following <see cref="System"/> dictionary types: <see cref="DictionaryTypes"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// true if the specified type is a <see cref="System"/> dictionary type; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static bool IsSystemDictionaryType(
            this Type type)
        {
            new { type }.Must().NotBeNull();

            if (!type.IsGenericType)
            {
                return false;
            }

            var genericType = type.GetGenericTypeDefinition();

            var result = DictionaryTypes.Any(_ => genericType == _);

            return result;
        }
    }
}
