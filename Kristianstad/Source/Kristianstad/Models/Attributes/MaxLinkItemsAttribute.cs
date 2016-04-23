// <copyright file="MaxLinkItemsAttribute.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using EPiCore.Models.Attributes.Validation;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.Framework.Localization;
    using EPiServer.ServiceLocation;
    using EPiServer.SpecializedProperties;

    /// <summary>
    /// The <see cref="LinkItemCollectionAllowedTypesAttribute" /> class. Validates a property of type <see cref="LinkItemCollection" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLinkItemsAttribute : ValidationAttribute
    {
        private readonly Injected<LocalizationService> _localizationService;
        private readonly Injected<IContentTypeRepository> _contentTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxLinkItemsAttribute" /> class.
        /// </summary>
        /// <param name="maxLinks">The maximum allowed number of links.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="maxLinks" /> is not a natural number.</exception>
        public MaxLinkItemsAttribute(int maxLinks)
        {
            if (maxLinks < 0)
            {
                throw new ArgumentException("Natural number required", nameof(maxLinks));
            }

            MaxLinks = maxLinks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxLinkItemsAttribute"/> class, which allows 0 links.
        /// </summary>
        protected MaxLinkItemsAttribute()
        {
            MaxLinks = 0;
        }

        /// <summary>
        /// Whether this validation attribute requires a context.
        /// </summary>
        public override bool RequiresValidationContext => true;

        /// <summary>
        /// Gets or sets the maximum allowed number of links.
        /// </summary>
        protected int MaxLinks { get; set; }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns><c>true</c> if the specified value is valid; otherwise, <c>false</c>.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var contentData = validationContext.ObjectInstance as IContentData;

            if (contentData == null)
            {
                return ValidationResult.Success;
            }

            var typedValue = value as LinkItemCollection;

            if (typedValue == null)
            {
                return ValidationResult.Success;
            }

            if (typedValue.Count > MaxLinks)
            {
                var propertyName = _contentTypeRepository.Service.Load(validationContext.ObjectType.BaseType).PropertyDefinitions
                    .Where(x => x.Name == validationContext.MemberName)
                    .FirstOrDefault()
                    .TranslateDisplayName();
                var message = string.Format(_localizationService.Service.GetString("/errors/validation/linkItemCountAttribute/badCount", ErrorMessage), MaxLinks, typedValue.Count);

                return new ValidationResult(message, new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
