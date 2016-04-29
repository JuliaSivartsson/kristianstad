using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kristianstad.CompareDomain.Models;
using Kristianstad.CompareDomain.WebServices.Models;

namespace Kristianstad.CompareDomain.Abstract
{
    public interface IService
    {
        OrganisationalUnit GetWebServiceOrganisationalUnit(string id);

        List<OrganisationalUnit> GetWebServiceOrganisationalUnits();

        List<PropertyQueryGroup> GetWebServicePropertyQueries();

        List<PropertyQueryWithResults> GetWebServicePropertyResults(List<PropertyQuery> queries, List<OrganisationalUnit> organisationalUnits); //(List<string> queryIds, List<string> organisationalUnitIds);

        /*
        List<GroupCategory> GetAllCategories();
        List<Category> GetAllCategoriesBasedOnAlphabet();

        GroupCategory GetGroupCategory(int id);
        Category GetCategory(int id);
        
        List<OrganisationalUnitInfo> GetOrganisationalUnitInfos();

        List<OrganisationalUnitInfo> GetOrganisationalUnitsInfo(string operatorsList);

        OrganisationalUnitInfo GetOrganisationalUnitInfo(int categoryId, string organisationalUnitId);
        
        PropertyQueryInfo GetPropertyQueryInfo(int categoryId, string queryId);

        List<Contact> GetContactsByOU(string organisationalUnitId);

        bool UpdateOrganisationalUnitInfo(OrganisationalUnitInfo ou);
        bool UpdatePropertyQueryInfo(PropertyQueryInfo propertyQuery);

        bool DeleteGroupCategory(GroupCategory groupCategory);
        bool DeleteCategory(Category category);
        
        bool UpdateGroupCategory(GroupCategory groupCategory);
        bool UpdateCategory(Category category, List<OrganisationalUnitInfo> earlierOrganisationalUnits = null, List<PropertyQueryInfo> earlierPropertyQueries = null);

        bool InsertGroupCategory(GroupCategory groupCategory);
        bool InsertCategory(Category category);
        */
    }
}
