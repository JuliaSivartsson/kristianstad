// <copyright file="IMapService.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Map
{
    /// <summary>
    /// The <see cref="IMapService"/> interface.
    /// </summary>
    public interface IMapService
    {
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <returns>Array of possible addresses</returns>
        string[] GetAddresses(string q);

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        AddressResult GetAddress(string address);
    }
}
