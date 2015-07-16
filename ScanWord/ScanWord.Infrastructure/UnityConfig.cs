using System;
using Microsoft.Practices.Unity;
using ScanWord.Data.Sql;
using ScanWord.Domain;
using ScanWord.Domain.Common;
using ScanWord.Domain.Data;
using ScanWord.Service;

namespace ScanWord.Infrastructure
{
    /// <summary>Specifies the Unity configuration for the main container.</summary>
    public class UnityConfig
    {
        /// <summary>Unity container.</summary>
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>Gets the configured Unity container.</summary>
        /// <returns>The Unity container.<see cref="IUnityContainer"/>.</returns>
        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IProjectSettings, ProjectSettings>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c => new ProjectSettings()));
            container.RegisterType<IScanWordParser, ScanWordParser>();
            container.RegisterType<IScanDataRepository, ScanDataRepository>();
            container.RegisterType<IScanWordDataHandler, ScanWordDataHandler>();
        }
    }
}