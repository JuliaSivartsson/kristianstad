// <copyright file="TabNames.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Models.Misc
{
    using EPiServer.DataAnnotations;

    /// <summary>
    /// The <see cref="TabNames" /> class. Definitions of the tabs used to group properties together in the EPiServer UI.
    /// using the <see cref="ContentTypeAttribute" /> class.
    /// </summary>
    public class TabNames // TODO: Some of these tab names shall be moved to EPiCore
    {
        /// <summary>
        /// The header tab.
        /// </summary>
        public const string Header = "Sidhuvud";

        /// <summary>
        /// The footer tab.
        /// </summary>
        public const string Footer = "Sidfot";

        /// <summary>
        /// Tab for main menu properties.
        /// </summary>
        public const string MenuSettings = "Huvudmeny inställningar";
    }
}