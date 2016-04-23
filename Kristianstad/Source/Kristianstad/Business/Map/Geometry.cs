// <copyright file="Geometry.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Map
{
    /// <summary>
    /// The <see cref="Geometry"/> class.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public double[] Coordinates { get; set; }
    }
}