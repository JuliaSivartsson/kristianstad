using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.Collections.Generic;
using EPiCore.Models.Blocks;
using EPiCore.Content.Models.Misc;

namespace Kristianstad.Business.Models.Blocks
{
    /// <summary>
    /// The <see cref="CategoryBlock"/> class.
    /// </summary>
    [ContentType(DisplayName = "Category block", GUID = "88853808-e40f-4a77-8857-053de23eb118", GroupName = GroupNames.Content)]
    public class CategoryBlock : BaseBlock
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Display(
            Name = "Name",
            Description = "This is the name of the category",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the organisational units.
        /// </summary>
        /// <value>
        /// The organisational units.
        /// </value>
        [CultureSpecific]
        [Display(
            Name = "Organisational units",
            Description = "This should contain all the organisational units in the category",
            GroupName = SystemTabNames.Content,
            Order = 2)]
        [ScaffoldColumn(false)]
        public virtual ContentArea OrganisationalUnits { get; set; }
    }
}