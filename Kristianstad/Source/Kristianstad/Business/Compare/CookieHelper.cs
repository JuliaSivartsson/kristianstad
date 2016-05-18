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

        public void AddCookie(int categoryId, int organisationalUnitId)
        {
            List<int> cookieCollection = GetCookie(categoryId);

            bool isAlreadyInCookie = false;
            foreach (int item in cookieCollection)
            {
                if (item == organisationalUnitId)
                {
                    isAlreadyInCookie = true;
                    break;
                }
            }

            List<int> newCookieCollection = new List<int>();
            if (isAlreadyInCookie)
            {
                foreach (int item in cookieCollection)
                {
                    if (item != organisationalUnitId)
                    {
                        newCookieCollection.Add(item);
                    }
                }
            }
            else
            {
                newCookieCollection = cookieCollection;
                newCookieCollection.Add(organisationalUnitId);
            }

            HttpContext.Current.Response.Cookies[COOKIENAME + categoryId].Value = JsonConvert.SerializeObject(newCookieCollection);
        }

        public List<int> GetCookie(int categoryId)
        {
            JArray cookie;
            try
            {
                cookie = JArray.Parse(HttpContext.Current.Request.Cookies[COOKIENAME + categoryId].Value);
            }
            catch
            {
                cookie = new JArray();
            }

            return cookie.Select(o => (int)o).ToList();
        }

        public void ClearCookie(int categoryId)
        {
            List<int> newCookieCollection = new List<int>();
            HttpContext.Current.Response.Cookies[COOKIENAME + categoryId].Value = JsonConvert.SerializeObject(newCookieCollection);
        }
    }
}