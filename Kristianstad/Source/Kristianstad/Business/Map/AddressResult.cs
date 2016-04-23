// <copyright file="AddressResult.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Map
{
    /// <summary>
    /// The <see cref="AddressResult"/> class.
    /// </summary>
    public class AddressResult
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        /// <value>
        /// The features.
        /// </value>
        public Feature[] Features { get; set; }
    }
}