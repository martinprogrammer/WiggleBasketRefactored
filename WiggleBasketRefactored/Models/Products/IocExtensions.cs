using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Mvc5;

namespace WiggleBasketRefactored.Models.Products
{
    public static class IocExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1,T2>(new ContainerControlledLifetimeManager());
        }
    }

    public class UnityMvc5
    {
        public static void Start()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.BindInRequestScope<IWebDbContext, WebDbContext>();
            container.BindInRequestScope<IUnitOfWorkManager, UnitOfWorkManager>();
            container.BindInRequestScope<IProductRepository, ProductRepository>();

            return container;
        }
    }
}