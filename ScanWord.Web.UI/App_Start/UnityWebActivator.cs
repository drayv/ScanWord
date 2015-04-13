// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityWebActivator.cs" company="Maksym Shchyhol">
//   Copyright (c) Maksym Shchyhol. All Rights Reserved
// </copyright>
// <summary>
//   Provides the bootstrapping for integrating Unity with ASP.NET MVC.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using ScanWord.Web.UI;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(UnityWebActivator), "Shutdown")]

namespace ScanWord.Web.UI
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity.Mvc;

    using ScanWord.DependencyResolution;

    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityWebActivator
    {
        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }

        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            var container = UnityConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}