﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.Constructor.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Reflection.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Reflection;

#if !OBeautifulCodeReflectionSolution
    internal
#else
    public
#endif
    static partial class ReflectionHelper
    {
        /// <summary>
        /// Gets the constructors of the specified type,
        /// with various options to control the scope of constructors included and optionally order the constructors.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="memberRelationships">OPTIONAL value that scopes the search for members based on their relationship to <paramref name="type"/>.  DEFAULT is to include the members declared in or inherited by the specified type.</param>
        /// <param name="memberOwners">OPTIONAL value that scopes the search for members based on who owns the member.  DEFAULT is to include members owned by an object or owned by the type itself.</param>
        /// <param name="memberAccessModifiers">OPTIONAL value that scopes the search for members based on access modifiers.  DEFAULT is to include members having any supported access modifier.</param>
        /// <param name="memberAttributes">OPTIONAL value that scopes the search for members based on the presence or absence of certain attributes on those members.  DEFAULT is to include members having or not having all special attributes.</param>
        /// <param name="orderMembersBy">OPTIONAL value that specifies how to the members.  DEFAULT is return the members in no particular order.</param>
        /// <returns>
        /// The constructors in the specified order.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        public static IReadOnlyList<ConstructorInfo> GetConstructorsFiltered(
            this Type type,
            MemberRelationships memberRelationships = MemberRelationships.DeclaredOrInherited,
            MemberOwners memberOwners = MemberOwners.All,
            MemberAccessModifiers memberAccessModifiers = MemberAccessModifiers.All,
            MemberAttributes memberAttributes = MemberAttributes.All,
            OrderMembersBy orderMembersBy = OrderMembersBy.None)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var result = type
                .GetMembersFiltered(memberRelationships, memberOwners, MemberMutability.All, memberAccessModifiers, MemberKinds.Constructor, memberAttributes, orderMembersBy)
                .Cast<ConstructorInfo>()
                .ToList();

            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <param name="type">The type of object to construct.</param>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameters of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newly created object.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="Exception">Various exceptions thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        public static object Construct(
            this Type type,
            params object[] parameters)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var result = Activator.CreateInstance(type, parameters);

            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameters of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newly created object.
        /// </returns>
        /// <exception cref="Exception">Any exception thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        public static T Construct<T>(
            params object[] parameters)
        {
            var result = typeof(T).Construct<T>(parameters);

            return result;
        }

        /// <summary>
        /// Constructs an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="type">The type of object to construct.</param>
        /// <param name="parameters">
        /// An array of arguments that match in number, order, and type the parameters of the constructor to invoke.
        /// If an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.
        /// </param>
        /// <returns>
        /// A reference to the newly created object.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="Exception">Any exception thrown by <see cref="Activator.CreateInstance(Type, object[])"/>.</exception>
        /// <exception cref="InvalidCastException">The created object could not be cast to a <typeparamref name="T"/>.</exception>
        public static T Construct<T>(
            this Type type,
            params object[] parameters)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var objectResult = type.Construct(parameters);

            var result = (T)objectResult;

            return result;
        }
    }
}
