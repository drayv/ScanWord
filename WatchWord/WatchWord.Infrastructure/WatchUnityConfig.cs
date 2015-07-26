using System;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using ScanWord.Core.Abstract;
using ScanWord.Core.Concrete;
using ScanWord.Core.Data;
using ScanWord.Data.Sql;
using WatchWord.Data.Sql;
using WatchWord.Domain.Common;
using WatchWord.Domain.Data;
using WatchWord.Service.EntityServices;

namespace WatchWord.Infrastructure
{
    /// <summary>Specifies the Unity configuration for the main container.</summary>
    public class WatchUnityConfig
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
            container.RegisterType<IScanWordParser, ScanWordParser>();
            container.RegisterType<IScanDataUnitOfWork, ScanDataUnitOfWork>();

            container.RegisterType<IMaterialsService, MaterialsService>();

            container.RegisterType<DbContext, WatchDataContainer>();
            container.RegisterType<IWatchDataRepository, WatchDataRepository>();
        }
    }
}