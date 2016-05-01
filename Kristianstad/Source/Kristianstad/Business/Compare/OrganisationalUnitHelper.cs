using EPiServer.Shell.ObjectEditing;
using Kristianstad.CompareDomain;
using Kristianstad.CompareDomain.Models;
using Kristianstad.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.Business.Compare
{
    public static class OrganisationalUnitHelper
    {
        private static readonly string SEPARATOR = "|";
        private static readonly string INFO_READ_AT_STRING = " (från {0}, ID: {1})"; // {0} = WebServiceName, {1} Id, {2} = Date/Time

        /*
        public static List<OrganisationalUnitModel> GetOrganisationalUnitsFromWebservice(List<OrganisationalUnitPage> exclude = null)
        {
            return CompareServiceFactory.Instance.GetWebServiceOrganisationalUnits().Where(x => exclude == null || !exclude.Any(x2 => x2.EntityId == x.OrganisationalUnitId)).Select(x => new OrganisationalUnitModel() { Name = x.Name,  }).ToList();
        }
        */

        public static List<SelectItem> GetSelectItems(IEnumerable<OrganisationalUnit> organisationalUnits)
        {
            List<SelectItem> items = new List<SelectItem>();
            if (organisationalUnits != null)
            {
                foreach (var organisationalUnit in organisationalUnits)
                {
                    items.Add(new SelectItem
                    {
                        Text = organisationalUnit.Title + string.Format(INFO_READ_AT_STRING, organisationalUnit.SourceName, organisationalUnit.SourceId, organisationalUnit.InfoReadAt),
                        Value = organisationalUnit.SourceName + SEPARATOR + organisationalUnit.SourceId
                    });
                }
            }

            return items;
        }

        public static List<OrganisationalUnit> GetModelsFromCheckboxValues(string[] values)
        {
            List<OrganisationalUnit> items = new List<OrganisationalUnit>();
            foreach (var value in values)
            {
                if (value.Contains(SEPARATOR))
                {
                    var separated = value.Split(new string[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                    if (separated.Length == 2)
                    {
                        items.Add(new OrganisationalUnit
                        {
                            SourceName = separated[0],
                            SourceId = separated[1]
                        });
                    }
                }
            }

            return items;
        }
    }
}