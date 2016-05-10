using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kristianstad.CompareDomain.Abstract;
using Kristianstad.CompareDomain.Helpers;
using Kristianstad.CompareDomain.Models;
using Kristianstad.CompareDomain.WebServices;
using Kristianstad.CompareDomain.DAL;

namespace Kristianstad.CompareDomain
{
    public class Service : IService
    {
        private readonly ISettings _settings;
        private readonly List<ITownWebService> _townWebServices;
        private readonly ICacheManager _cache; // Waiting to implement this (not sure if okay to use MemoryCache?)

        //Constructors
        public Service()
            : this (new Settings(true), new List<ITownWebService>() { new KoladaTownWebService() }, new CacheManager())
        {
            // Empty
        }
        
        public Service(Settings settings, List<ITownWebService> townWebServices, ICacheManager cacheManager)
        {
            _settings = settings;
            _townWebServices = townWebServices;
            _cache = cacheManager;

            _settings.Save();
        }


        //Methods
        public string GetCustomSourceName()
        {
            return "Custom";
        }

        public OrganisationalUnit GetWebServiceOrganisationalUnit(string sourceName, string id)
        {
            string cacheKey = $"{"getOrganisationalUnitByID"}{sourceName}{id}";

            if (_cache.HasValue(cacheKey))
            {
                var value = (OrganisationalUnit)_cache.GetCache(cacheKey);
                if (value != null) { return value; }
            }

            ITownWebService source = FindSource(sourceName);
            if (source != null)
            {
                var ou = source.GetOrganisationalUnit(id);
                _cache.SetCache(cacheKey, ou, _settings.CacheSeconds_OrganisationalUnits);

                return ou;
            }

            return null;
        }

        public Dictionary<string, List<OrganisationalUnit>> GetWebServicesOrganisationalUnits()
        {
            if (!string.IsNullOrWhiteSpace(_settings.MunicipalityId))
            {
                string id = _settings.MunicipalityId;
                string cacheKey = $"{"getAllOrganisationalUnitsFromWebServices"}{id}";

                if (_cache.HasValue(cacheKey))
                {
                    var value = (Dictionary<string, List<OrganisationalUnit>>)_cache.GetCache(cacheKey);
                    if (value != null) { return value; }
                }

                Dictionary<string, List<OrganisationalUnit>> allSourceOU = new Dictionary<string, List<OrganisationalUnit>>();
                foreach (var source in _townWebServices)
                {
                    allSourceOU.Add(source.GetName(), source.GetAllOrganisationalUnits(_settings.MunicipalityId));
                }

                _cache.SetCache(cacheKey, allSourceOU, _settings.CacheSeconds_OrganisationalUnits);

                return allSourceOU;
            }

            return null;
        }

        public Dictionary<string, List<PropertyQueryGroup>> GetWebServicePropertyQueries()
        {
            string cacheKey = "getAllPropertyQueries";

            //returns from cache if value is present, else returns from Webservice
            if (_cache.HasValue(cacheKey))
            {
                var value = (Dictionary<string, List<PropertyQueryGroup>>)_cache.GetCache(cacheKey);
                if (value != null) { return value; }
            }

            //Get from webServices
            Dictionary<string, List<PropertyQueryGroup>> allSourcePQ = new Dictionary<string, List<PropertyQueryGroup>>();
            foreach (var source in _townWebServices)
            {
                allSourcePQ.Add(source.GetName(), source.GetAllPropertyQueries());
            }
            
            //Save to cache
            _cache.SetCache(cacheKey, allSourcePQ, _settings.CacheSeconds_PropertyQueries);

            return allSourcePQ;
        }

        public List<PropertyQueryWithResults> GetWebServicePropertyResults(List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits) //List<string> queryIds, List<string> organisationalUnitIds) //List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits)
        {
            //Get id's from All KpiQuestions and OrganisationalUnits in parameter
            //and compund to an unique cacheKey
            
            var queryIds = from q in queries
                           select q.SourceName + ":" + q.SourceId;
            var organisationalUnitIds = from ou in organisationalUnits
                                        select ou.SourceName + ":" + ou.SourceId;
            

            // Check if available in cache
            var cacheKey = "PropertyResults" + queryIds.Aggregate("", (current, kpi) => current + kpi);
            cacheKey = organisationalUnitIds.Aggregate(cacheKey, (current, ouId) => current + ouId);

            if (_cache.HasValue(cacheKey))
            {
                var value = (List<PropertyQueryWithResults>)_cache.GetCache(cacheKey);
                if (value != null) { return value; }
            }

            // Not in cache, load from web services
            // first sort the queries by source
            Dictionary<string, List<PropertyQuery>> queriesBySource = new Dictionary<string, List<PropertyQuery>>();
            foreach (var query in queries)
            {
                List<PropertyQuery> list = null;
                if (queriesBySource.ContainsKey(query.SourceName))
                {
                    list = queriesBySource[query.SourceName];
                }
                else
                {
                    list = new List<PropertyQuery>();
                    queriesBySource.Add(query.SourceName, list);
                }

                list.Add(query);
            }


            // Load info from sources
            List<PropertyQueryWithResults> results = new List<PropertyQueryWithResults>();
            foreach (var sourceQueries in queriesBySource)
            {
                var source = FindSource(sourceQueries.Key);
                if (source != null)
                {
                    var sourceResults = source.GetPropertyResults(sourceQueries.Value, organisationalUnits);
                    results.AddRange(sourceResults);
                }
            }

            // var returnValue = _townWebService.GetPropertyResults(queries, organisationalUnits);
            _cache.SetCache(cacheKey, results, _settings.CacheSeconds_PropertyResult);

            return results;
        }


        private ITownWebService FindSource(string sourceName)
        {
            foreach (var source in _townWebServices)
            {
                if (source.GetName().ToLower() == sourceName.ToLower())
                {
                    return source;
                }
            }

            return null;
        }
    }
}