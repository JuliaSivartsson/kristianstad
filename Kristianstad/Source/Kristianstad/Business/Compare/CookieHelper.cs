using EPiServer;
using EPiServer.Core;
using Kristianstad.HtmlHelpers;
using Kristianstad.Models.Pages.Compare;
using Kristianstad.ViewModels.Compare;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.Business.Compare
{
    public class CookieHelper
    {
        private static readonly string COOKIENAME = "compare";

        public void AddOrganisationalUnitToCompare(ContentReference compareResultPageReference, OrganisationalUnitPage organisationalUnitPage)
        {
            var id = organisationalUnitPage.ContentLink.ID;
            var cookieCollection = GetOrganisationalUnitPageIdsInCompare(compareResultPageReference);
            if (cookieCollection.Any(x => x == id))
            {
                return;
            }

            // add the id
            cookieCollection.Add(id);

            // save new collection
            HttpContext.Current.Response.Cookies[COOKIENAME + compareResultPageReference.ID].Value = JsonConvert.SerializeObject(cookieCollection);
        }

        public void RemoveOrganisationalUnitFromCompare(ContentReference compareResultPageReference, OrganisationalUnitPage organisationalUnitPage)
        {
            var id = organisationalUnitPage.ContentLink.ID;
            var cookieCollection = GetOrganisationalUnitPageIdsInCompare(compareResultPageReference);
            if (cookieCollection.Any(x => x == id))
            {
                // remove the id
                cookieCollection.Remove(id);

                // save new collection
                HttpContext.Current.Response.Cookies[COOKIENAME + compareResultPageReference.ID].Value = JsonConvert.SerializeObject(cookieCollection);
            }
        }

        public List<OrganisationalUnitCompareModel> GetOrganisationalUnitsInCompare(IContentLoader contentLoader, ContentReference compareResultPageReference)
        {
            try
            {
                var organisationalUnitPageIds = GetOrganisationalUnitPageIdsInCompare(compareResultPageReference);

                List<OrganisationalUnitCompareModel> results = new List<OrganisationalUnitCompareModel>();
                foreach (var id in organisationalUnitPageIds)
                {
                    var page = contentLoader.Get<PageData>(new ContentReference(id));
                    if (page != null && page is OrganisationalUnitPage)
                    {
                        var organisationalUnitPage = page as OrganisationalUnitPage;
                        results.Add(new OrganisationalUnitCompareModel()
                        {
                            ID = organisationalUnitPage.ContentLink.ID,
                            Name = organisationalUnitPage.Name,
                            URL = CompareHelper.GetExternalUrl(organisationalUnitPage.ContentLink)
                        });
                    }
                }

                return results;
            }
            catch
            {
                return new List<OrganisationalUnitCompareModel>();
            }
        }

        private List<int> GetOrganisationalUnitPageIdsInCompare(ContentReference compareResultPageReference)
        {
            try
            {
                var cookieContents = HttpContext.Current.Request.Cookies[COOKIENAME + compareResultPageReference.ID].Value;
                JArray cookieArray = JArray.Parse(cookieContents);
                return cookieArray.Select(x => (int)x).ToList();
            }
            catch
            {
                return new List<int>();
            }
        }

        public void ClearCompare(ContentReference compareResultPageReference)
        {
            List<int> emptyList = new List<int>();
            HttpContext.Current.Response.Cookies[COOKIENAME + compareResultPageReference.ID].Value = JsonConvert.SerializeObject(emptyList);
        }

        /*
        public void AddOrganisationalUnitToCompare(ContentReference compareResultPageReference, OrganisationalUnitPage organisationalUnitPage)
        {
            var cookieCollection = GetOrganisationalUnitsInCompare(compareResultPageReference);

            bool alreadyExists = false;
            foreach (OrganisationalUnitCompareModel item in cookieCollection)
            {
                if (item.ID == organisationalUnitPage.ContentLink.ID)
                {
                    return;
                }
            }

            cookieCollection.Add(new OrganisationalUnitCompareModel()
            {
                ID = organisationalUnitPage.ContentLink.ID,
                Name = organisationalUnitPage.Name,
                URL = CompareHelper.GetExternalUrl(organisationalUnitPage.ContentLink)
            });

            HttpContext.Current.Response.Cookies[COOKIENAME + compareResultPageReference.ID].Value = JsonConvert.SerializeObject(cookieCollection);
        }

        public void RemoveOrganisationalUnitFromCompare(ContentReference compareResultPageReference, OrganisationalUnitPage organisationalUnitPage)
        {
            var cookieCollection = GetOrganisationalUnitsInCompare(compareResultPageReference);
            var existingItem = cookieCollection.Where(x => x.ID == organisationalUnitPage.ContentLink.ID).FirstOrDefault();
            if (existingItem != null)
            {
                cookieCollection.Remove(existingItem);
            }

            HttpContext.Current.Response.Cookies[COOKIENAME + compareResultPageReference.ID].Value = JsonConvert.SerializeObject(cookieCollection);
        }

        public List<OrganisationalUnitCompareModel> GetOrganisationalUnitsInCompare(ContentReference compareResultPageReference)
        {
            try
            {
                // get the cookie
                var cookieContents = HttpContext.Current.Request.Cookies[COOKIENAME + compareResultPageReference.ID].Value;
                JArray cookie = JArray.Parse(cookieContents);

                // parse the cookie to a list of models and return them
                return cookie.Select(o => new OrganisationalUnitCompareModel()
                {
                    ID = (int)o["ID"],
                    Name = (string)o["Name"],
                    URL = (string)o["URL"]
                }).ToList();
            }
            catch
            {
                return new List<OrganisationalUnitCompareModel>();
            }
        }
        */
    }
}