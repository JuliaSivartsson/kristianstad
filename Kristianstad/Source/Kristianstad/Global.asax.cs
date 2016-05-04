// <copyright file="Global.asax.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad
{
    using EPiServer;
    using EPiServer.ServiceLocation;
    using EPiServer.Web.Routing.Segments;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;    /// <summary>
                                 /// The <see cref="Global" /> class.
                                 /// </summary>
    public class Global : EPiCore.Global
    {
        /// <summary>
        /// On application start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterBundles(BundleCollection bundles)
        {
        }

        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            IContentLoader contentLoader;
            ServiceLocator.Current.TryGetExistingInstance(out contentLoader);

            IRoutingSegmentLoader routingSegmentLoader;
            ServiceLocator.Current.TryGetExistingInstance(out routingSegmentLoader);

            if (contentLoader != null && routingSegmentLoader != null)
            {
                routes.MapRoute("CompareAdminPlugin", "CompareAdminPlugin/{action}",
                                new { controller = "CompareAdminPlugin", action = "Index" });
            }
        }

    }
}