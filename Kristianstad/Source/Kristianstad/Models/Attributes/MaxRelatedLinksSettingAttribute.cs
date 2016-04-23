// <copyright file="MaxRelatedLinksSettingAttribute.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EPiCore.Content.Models.Pages;
    using EPiCore.Services.Content.Interfaces;
    using EPiServer.ServiceLocation;
    using EPiServer.SpecializedProperties;

    /// <summary>
    /// The <see cref="MaxRelatedLinksSettingAttribute" /> class. Validates a property of type
    /// <see cref="LinkItemCollection" /> by making sure the number of links contained is not greater than the limit
    /// set on the <see cref="SettingsPage" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxRelatedLinksSettingAttribute : MaxLinkItemsAttribute
    {
        private readonly Injected<IPagePropertyService> _pagePropertyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxRelatedLinksSettingAttribute" /> class.
        /// </summary>
        public MaxRelatedLinksSettingAttribute()
            : base()
        {
        }

        /// <summary>
        /// Determines whether the specified value is valid, i.e. the number of links is not greater than the limit
        /// set on the <see cref="SettingsPage" />.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, <c>false</c>.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            MaxLinks = _pagePropertyService.Service.GetSettingsPageProperty<int>("MaxRelatedLinks");
            return base.IsValid(value, validationContext);
        }
    }
}
