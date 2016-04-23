// <copyright file="MapService.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.Business.Map
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using EPiCore.Caching;
    using EPiServer.Framework.Cache;
    using EPiServer.ServiceLocation;
    using Newtonsoft.Json;

    /// <summary>
    /// The <see cref="MapService"/> class.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(IMapService), Lifecycle = ServiceInstanceScope.Singleton)]
    public class MapService : IMapService
    {
        private const string CacheKey = "MapService-";
        private const string AddressUrl = "http://193.17.67.229/cgi-bin/externt/sok/allting_json.php";
        private const string AutoCompleteUrl = "http://193.17.67.229/cgi-bin/externt/sok/allting.php";
        private readonly Injected<ICacheManager> _cacheManager;
        private readonly CacheEvictionPolicy _cacheEvictionPolicy = new CacheEvictionPolicy(TimeSpan.FromDays(1), CacheTimeoutType.Absolute);

        /// <inheritdoc/>
        public AddressResult GetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Cannot be null or white space.", nameof(address));
            }

            var url = $"{AddressUrl}?q={address}";

            var addressResult = _cacheManager.Service.Get<AddressResult>($"{CacheKey}{url}", () => null);

            if (addressResult != null)
            {
                return addressResult;
            }

            var request = WebRequest.Create(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                addressResult = JsonConvert.DeserializeObject<AddressResult>(streamReader.ReadToEnd());
                _cacheManager.Service.Add($"{CacheKey}{url}", addressResult, _cacheEvictionPolicy);

                return addressResult;
            }
        }

        /// <inheritdoc/>
        public string[] GetAddresses(string q)
        {
            if (string.IsNullOrWhiteSpace(q) || q.Length <= 2)
            {
                return new string[] { };
            }

            var url = $"{AutoCompleteUrl}?q={q}";

            var addresses = _cacheManager.Service.Get<string[]>($"{CacheKey}{url}", () => null);
            if (addresses != null)
            {
                return addresses;
            }

            var request = WebRequest.Create(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var resultString = streamReader.ReadToEnd();
                var result = resultString;

                // Normalize the response
                while (result.StartsWith("\t"))
                {
                    result = result.Substring(1);
                }

                addresses = result.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                _cacheManager.Service.Add($"{CacheKey}{url}", addresses, _cacheEvictionPolicy);

                return addresses;
            }
        }
    }
}