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

        public static List<OrganisationalUnitModel> GetOrganisationalUnitsFromWebservice(List<OrganisationalUnitPage> exclude = null)
        {
            if (exclude == null) { exclude = new List<OrganisationalUnitPage>(); } 
            return CompareServiceFactory.Instance.GetWebServiceOrganisationalUnits().Where(x => !exclude.Any(x2 => x2.EntityId == x.OrganisationalUnitId)).Select(x => new OrganisationalUnitModel() { Name = x.Name,  }).ToList();
        }

        public static List<SelectItem> GetSelectItems(IEnumerable<OrganisationalUnit> organisationalUnits)
        {
            List<SelectItem> items = new List<SelectItem>();
            foreach (var organisationalUnit in organisationalUnits)
            {
                items.Add(new SelectItem
                {
                    Text = organisationalUnit.Name,
                    Value = organisationalUnit.WebServiceName + SEPARATOR + organisationalUnit.OrganisationalUnitId
                });
            }

            return items;
        }

        public static List<OrganisationalUnit> GetModelsFromCheckboxValues(IEnumerable<string> values)
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
                            WebServiceName = separated[0],
                            OrganisationalUnitId = separated[1]
                        });
                    }
                }
            }

            return items;
        }
    }
}