// <copyright file="MapBlockInitializationModule.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Initialization
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using EPiCore.Initialization;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using Map;
    using Models.Blocks;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="MapBlockInitializationModule"/> class.
    /// </summary>
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class MapBlockInitializationModule : BaseInitializationModule
    {
        private readonly Injected<IContentEvents> _contentEvents;
        private readonly Injected<IMapService> _mapService;

        /// <summary>
        /// Initializes if not installed.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void InitializeIfNotInstalled(InitializationEngine context)
        {
            _contentEvents.Service.SavingContent += SavingContent;
        }

        /// <summary>
        /// Uninitializes if installed.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void UninitializeIfInstalled(InitializationEngine context)
        {
            base.UninitializeIfInstalled(context);

            _contentEvents.Service.SavingContent -= SavingContent;
        }

        /// <summary>
        /// Add the cordinates to the block.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ContentEventArgs"/> instance containing the event data.</param>
        private void SavingContent(object sender, ContentEventArgs e)
        {
            if (!(e.Content is MapBlock))
            {
                return;
            }

            var block = e.Content as MapBlock;
            var address = block.Address;

            if (string.IsNullOrWhiteSpace(address) || address.Length <= 2)
            {
                return;
            }

            var addressResult = _mapService.Service.GetAddress(address);

            if (!addressResult.Features.Any())
            {
                e.CancelAction = true;
                e.CancelReason = "Not a valid address.";
                return;
            }

            block.Latitude = addressResult.Features[0].Geometry.Coordinates[0];
            block.Longitude = addressResult.Features[0].Geometry.Coordinates[1];
        }
    }
}