// <copyright file="Global.asax.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    /// <summary>
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
    }
}