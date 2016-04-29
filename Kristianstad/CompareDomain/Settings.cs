using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Kristianstad.CompareDomain
{
    public class Settings : ISettings
    {
        private const string FILENAME = "compare-settings.json";
        private readonly string _filePath;

        ////Properties
        public string MunicipalityName { get; set; }
        public string MunicipalityId { get; set; } // id from Kolada API

        public string CountyName { get; set; }
        public string CountyId { get; set; } // id from Kolada API

        public int CacheSeconds_PropertyQueries { get; set; }
        public int CacheSeconds_OrganisationalUnits { get; set; }
        public int CacheSeconds_PropertyResult { get; set; }


        //Constructors
        [JsonConstructor]
        public Settings(bool load = false)
        {

            string pathAppData = AppDomain.CurrentDomain.BaseDirectory + "App_Data"; // HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data/");
            _filePath = Path.Combine(pathAppData, FILENAME);

            Initialize(load);
        }
        public Settings(string filepath, bool load = false)
        {
            _filePath = filepath;

            Initialize(load);
        }

        //Methods
        public void Initialize(bool load = false)
        {
            //default values:
            CacheSeconds_PropertyQueries = (60 * 60 * 24); // 1 day
            CacheSeconds_OrganisationalUnits = (60 * 60 * 24); // 1 day
            CacheSeconds_PropertyResult = (60 * 60 * 24); // 1 day

            if (load)
            {
                Load();
            }
        }
        public void Load()
        {
            if (System.IO.File.Exists(_filePath))
            {
                try
                {
                    using (StreamReader file = File.OpenText(_filePath))
                    {
                        string json = file.ReadToEnd();
                        Settings settings = JsonConvert.DeserializeObject<Settings>(json);

                        //set all properties
                        this.MunicipalityName = settings.MunicipalityName;
                        this.MunicipalityId = settings.MunicipalityId;
                        this.CountyName = settings.CountyName;
                        this.CountyId = settings.CountyId;
                        this.CacheSeconds_PropertyQueries = settings.CacheSeconds_PropertyQueries;
                        this.CacheSeconds_OrganisationalUnits = settings.CacheSeconds_OrganisationalUnits;
                    }
                }
                catch (Exception e)
                {
                    e = e;
                    //do what?
                }
            }
            else
            {
                Save();
            }
        }
        public void Save()
        {
            try
            {
                using (StreamWriter file = File.CreateText(_filePath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    string json = JsonConvert.SerializeObject(this);
                    writer.WriteRaw(json);
                    writer.Close();
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                ex = ex;
                //throw new ApplicationException("Could not write to settings file. " + ex);
            }
        }
    }
}