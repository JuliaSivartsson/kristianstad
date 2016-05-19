using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kristianstad.CompareDomain.Abstract;
using System.Net;
using System.IO;
using Kristianstad.CompareDomain.WebServices.Models;
using Kristianstad.CompareDomain.Models;
using Newtonsoft.Json;

namespace Kristianstad.CompareDomain.WebServices
{
    /// <summary>
    /// Set of functions that calls KoladaAPI and returns different type of data
    /// </summary>
    public class KoladaTownWebService : TownWebServiceBase
    {
        private static readonly string BaseUrl = "http://api.kolada.se/v2/";

        public override string GetName()
        {
            return "Kolada"; 
        }
        
        public override OrganisationalUnit GetOrganisationalUnit(string id)
        {
            var rawJson = string.Empty;

            var apiRequest = "ou/" + id;
            rawJson = RawJson(BaseUrl + apiRequest);
            var ou = JsonConvert.DeserializeObject<OUs>(rawJson).Values;
            OU theOu = ou.First();

            return new OrganisationalUnit() { SourceName = this.GetName(), SourceId = theOu.Id, Name = theOu.Title, InfoReadAt = DateTime.Now };
        }
        
        public override List<PropertyQueryWithResults> GetPropertyResults(List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits) //List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits)
        {
            List<PropertyQueryWithResults> results = new List<PropertyQueryWithResults>();
            
            //Set the Kolada url
            var apiRequest = "oudata/kpi/";
            apiRequest += string.Join(",", queries.Select(q => q.SourceId).ToList());
            apiRequest += "/ou/" + string.Join(",", organisationalUnits.Select(o => o.SourceId).ToList());

            //Load the data from Kolada
            var rawJson = RawJson(BaseUrl + apiRequest);

            if (!string.IsNullOrWhiteSpace(rawJson))
            {
                //serialize the json data
                var kpiAnswers = JsonConvert.DeserializeObject<KpiAnswers>(rawJson).Values;

                //create correct models
                foreach (var query in queries)
                {
                    PropertyQueryWithResults queryWithResults = new PropertyQueryWithResults(query);
                    foreach (var ou in organisationalUnits)
                    {
                        queryWithResults.Results.Add(new PropertyQueryResult(ou.SourceId, kpiAnswers.Where(a => a.Kpi == query.SourceId && a.Ou == ou.SourceId)
                                                                                                                    .Select(a => new PropertyQueryResultForPeriod(a.Period,
                                                                                                                                                                            a.Values.Select(v => new PropertyQueryResultValue(v.Gender, v.Status, v.Value)).ToList()))
                                                                                                                    .ToList(), query.Period));
                    }
                    results.Add(queryWithResults);
                }
            }

            return results;
        }

        public override List<PropertyQueryGroup> GetAllPropertyQueries()
        {
            var rawJson = string.Empty;
            var apiRequest= "kpi_groups";
            rawJson = RawJson(BaseUrl + apiRequest);
            var kpi = JsonConvert.DeserializeObject<KpiGroups>(rawJson).Values;

            DateTime infoReadAt = DateTime.Now;
            return kpi.Select(k => new PropertyQueryGroup() { SourceName = this.GetName(), SourceId = k.Id, Name = k.Title, InfoReadAt = infoReadAt, Queries = k.Members.Select(m => new PropertyQuery() { SourceName = this.GetName(), SourceId = m.Member_id, Name = m.Member_title, InfoReadAt = infoReadAt, Type = GuessPropertyQueryType(m.Member_title) }).ToList() }).ToList();
        }

        private string GuessPropertyQueryType(string title)
        {
            if (title.ToLower().Contains("(%)"))
            {
                return PropertyQuery.TYPE_PERCENT;
            }
            else if (title.ToLower().Contains("procentenheter"))
            {
                return PropertyQuery.TYPE_PERCENTAGE;
            }
            else if (title.ToLower().Contains("ja=1") && title.ToLower().Contains("nej=0")) 
            {
                return PropertyQuery.TYPE_YESNO;
            }

            return PropertyQuery.TYPE_STANDARD;
        }

        public override List<OrganisationalUnit> GetAllOrganisationalUnits(string municipalityId)
        {
            var rawJson = string.Empty;
            var apiRequest = "ou?municipality=" + municipalityId;

            rawJson = RawJson(BaseUrl + apiRequest);

            var currentTime = DateTime.Now;

            var OUs = JsonConvert.DeserializeObject<OUs>(rawJson).Values;
            return OUs.Select(o => new OrganisationalUnit() { SourceName = this.GetName(), SourceId = o.Id, Name = o.Title, InfoReadAt = currentTime }).ToList();
        }

        /// <summary>
        /// Function:
        /// 1. Takes apiRequest as argument, and builds valid URI
        /// 2. Makes HttpWebRequest
        /// 3. Reads data from request and sends it back in raw format...
        /// 
        /// </summary>
        /// <param name="apiRequest"></param>
        /// <returns>rawJson, JSON in string format</returns>
        private string RawJson(string apiRequest)
        {
            var rawJson = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(apiRequest);

            try
            {
                using (var response = request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    rawJson = reader.ReadToEnd();
                }
            }
            catch { }

            return rawJson;
        }
        
    }
}
