// <copyright file="MapAddressSelectionQuery.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Map
{
    using System.Collections.Generic;
    using System.Linq;
    using EPiServer.ServiceLocation;
    using EPiServer.Shell.ObjectEditing;

    /// <summary>
    /// The <see cref="MapAddressSelectionQuery"/> class.
    /// </summary>
    [ServiceConfiguration(typeof(ISelectionQuery))]
    public class MapAddressSelectionQuery : ISelectionQuery
    {
        /// <summary>
        /// The _map service
        /// </summary>
        private readonly Injected<IMapService> _mapService;

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The addresses that match the query.</returns>
        public IEnumerable<ISelectItem> GetItems(string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length <= 2)
            {
                return new SelectItem[] { };
            }

            var addresses = _mapService.Service.GetAddresses(query);

            return addresses.Select(x => new SelectItem() { Text = x, Value = x });
        }

        /// <summary>
        /// Gets the item by value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The address that match.</returns>
        public ISelectItem GetItemByValue(string value)
        {
            return new SelectItem() { Text = value, Value = value };
        }
    }
}