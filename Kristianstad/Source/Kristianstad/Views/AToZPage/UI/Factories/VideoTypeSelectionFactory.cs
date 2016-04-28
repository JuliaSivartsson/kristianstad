// <copyright file="VideoTypeSelectionFactory.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.UI.Factories
{
    using System.Collections.Generic;
    using EPiServer.Framework.Localization;
    using EPiServer.ServiceLocation;
    using EPiServer.Shell.ObjectEditing;

    /// <summary>
    /// The <see cref="VideoTypeSelectionFactory"/> class. Produces items representing video types.
    /// </summary>
    [SelectionFactoryRegistration]
    public class VideoTypeSelectionFactory : ISelectionFactory
    {
        private readonly Injected<LocalizationService> _localizationService;

        /// <summary>
        /// VideoTypes
        /// </summary>
        public enum VideoType
        {
            /// <summary>
            /// Local file
            /// </summary>
            LOCAL = 0,

            /// <summary>
            /// Youtube id
            /// </summary>
            YOUTUBE = 1,

            /// <summary>
            /// Vimeo id
            /// </summary>
            VIMEO = 2
        }

        /// <summary>
        /// Gets the selections.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>The selections.</returns>
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            var selectItems = new List<SelectItem>
            {
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/videotypes/localfile"),
                    Value = VideoType.LOCAL
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/videotypes/youtube"),
                    Value = VideoType.YOUTUBE
                },
                new SelectItem
                {
                    Text = _localizationService.Service.GetString("/videotypes/vimeo"),
                    Value = VideoType.VIMEO
                }
            };

            return selectItems;
        }
    }
}