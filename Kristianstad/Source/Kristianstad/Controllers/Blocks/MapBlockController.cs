// <copyright file="MapBlockController.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Controllers.Blocks
{
    using System;
    using System.Web.Mvc;
    using Business.Models.Blocks;
    using EPiCore.Controllers;
    using EPiCore.ViewModels.Blocks;
    using EPiServer;

    /// <summary>
    /// The <see cref="MapBlockController"/> class.
    /// </summary>
    public class MapBlockController : BaseBlockController<MapBlock>
    {
        /// <summary>
        /// Get the map for the current block.
        /// </summary>
        /// <param name="currentBlock">The current block.</param>
        /// <returns>The view for the map block.</returns>
        public override ActionResult Index(MapBlock currentBlock)
        {
            var type = typeof(BlockViewModel<>).MakeGenericType(currentBlock.GetOriginalType());

            var model = Activator.CreateInstance(type, currentBlock) as IBlockViewModel<MapBlock>;

            return PartialView("~/Views/MapBlock/Index.cshtml", model);
        }
    }
}