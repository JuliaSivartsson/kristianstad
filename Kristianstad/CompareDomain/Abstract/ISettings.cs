using System;
using System.IO;
using Kristianstad.CompareDomain.Abstract;

namespace Kristianstad.CompareDomain
{
    public interface ISettings
    {
        string MunicipalityName { get; set; }
        string MunicipalityId { get; set; }
        string CountyName { get; set; }
        string CountyId { get; set; }
        int CacheSeconds_PropertyQueries { get; set; }
        int CacheSeconds_OrganisationalUnits { get; set; }
        int CacheSeconds_PropertyResult { get; set; }

        void Initialize(bool load = false);
        void Load();
        void Save();
    }
}
