﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoFakeItEasyBootstrapper.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.AutoFakeItEasy source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.AutoFakeItEasy.Recipes
{
    using System.Collections.Generic;

    /// <summary>
    /// Bootstraps AutoFakeItEasy.
    /// </summary>
    [System.Diagnostics.DebuggerStepThrough]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.AutoFakeItEasy", "See package version number")]
    public class AutoFakeItEasyBootstrapper : FakeItEasy.DefaultBootstrapper
    {
        /// <summary>
        /// Scans for FakeItEasy extension points.
        /// </summary>
        /// <returns>
        /// Returns base implementation after loading <see cref="AutoFixtureBackedDummyFactory"/> into the app domain.
        /// </returns>
        public override IEnumerable<string> GetAssemblyFileNamesToScanForExtensions()
        {
            // calling a static method on the factory loads it into the app domain, 
            // exposing it as an extension point during FakeItEasy's scan.
            // That's a lot easier than trying to find the path to the assembly, which
            // is typically what you would do here.
            AutoFixtureBackedDummyFactory.LoadInAppDomain();
            return base.GetAssemblyFileNamesToScanForExtensions();
        }
    }
}