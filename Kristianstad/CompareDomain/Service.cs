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
        private readonly ITownWebService _townWebService;
        private readonly ICacheManager _cache; // Waiting to implement this (not sure if okay to use MemoryCache?)

        //Constructors
        public Service()
            : this (new Settings(true), new KoladaTownWebService(), new CacheManager())
        {
            // Empty
        }
        
        public Service(Settings settings, ITownWebService townWebService, ICacheManager cacheManager)
        {
            _settings = settings;
            _townWebService = townWebService;
            _cache = cacheManager;

            _settings.Save();
        }


        //Methods

        #region WebService functionality

        public OrganisationalUnit GetWebServiceOrganisationalUnit(string id)
        {
            string cacheKey = $"{"getOrganisationalUnitByID"}{id}";

            if (_cache.HasValue(cacheKey))
            {
                return (OrganisationalUnit)_cache.GetCache(cacheKey);
            }

            var ou = _townWebService.GetOrganisationalUnit(id);
            _cache.SetCache(cacheKey, ou, _settings.CacheSeconds_OrganisationalUnits);

            return ou;
        }

        public List<OrganisationalUnit> GetWebServiceOrganisationalUnits()
        {
            string id = _settings.MunicipalityId;
            string cacheKey = $"{"getAllOrganisationalUnitsFromWebService"}{id}";

            if (_cache.HasValue(cacheKey))
            {
                return (List<OrganisationalUnit>)_cache.GetCache(cacheKey);
            }

            var allOU = _townWebService.GetAllOrganisationalUnits(_settings.MunicipalityId);
            _cache.SetCache(cacheKey, allOU, _settings.CacheSeconds_OrganisationalUnits);

            return allOU;
        }

        public List<PropertyQueryGroup> GetWebServicePropertyQueries()
        {
            var list = new List<PropertyQueryGroup>();
            string cacheKey = "getAllPropertyQueries";

            //returns from cache if value is present, else returns from Webservice
            if (_cache.HasValue(cacheKey))
            {
                return (List<PropertyQueryGroup>)_cache.GetCache(cacheKey);
            }

            //Get from webService
            list = _townWebService.GetAllPropertyQueries();
            //Save to cache
            _cache.SetCache(cacheKey, list, _settings.CacheSeconds_PropertyQueries);

            return list;
        }

        public List<PropertyQueryWithResults> GetWebServicePropertyResults(List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits) //List<string> queryIds, List<string> organisationalUnitIds) //List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits)
        {
            //Get id's from All KpiQuestions and OrganisationalUnits in parameter
            //and compund to an unique cacheKey
            
            var queryIds = from q in queries
                           select q.QueryId;
            var organisationalUnitIds = from ou in organisationalUnits
                                        select ou.OrganisationalUnitId;
            

            //adding all KPIQuestionId + ouIds 
            var cacheKey = "PropertyResults" + queryIds.Aggregate("", (current, kpi) => current + kpi);
            cacheKey = organisationalUnitIds.Aggregate(cacheKey, (current, ouId) => current + ouId);

            if (_cache.HasValue(cacheKey))
            {
                return (List<PropertyQueryWithResults>)_cache.GetCache(cacheKey);
            }

            var returnValue = _townWebService.GetPropertyResults(queries, organisationalUnits); //queryIds.ToList(), organisationalUnitIds.ToList());
            _cache.SetCache(cacheKey, returnValue, _settings.CacheSeconds_PropertyResult);

            return returnValue;
        }

        #endregion
        
    }
}