using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer;

namespace Kristianstad.Models.Attributes
{
    /// <summary>
    /// LimitPageType Validation Attribute
    /// Add the attribute [LimitPageType(typeof(StandardPage))] to a EPiServer
    /// PageReference property to limit what pages can be selected to the
    /// chosen page type. Can also be used for inherited page types and
    /// interfaces.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class LimitPageType : ValidationAttribute
    {
        private readonly Type _pageType;
        private string _errorMsg = string.Empty;

        public Type PageType { get { return _pageType; } }

        public LimitPageType(Type pageType)
        {
            _pageType = pageType;
        }

        public override bool IsValid(object value)
        {
            // Check if page reference is a reference to a page of
            // the right page type
            PageReference pageRef = value as PageReference;
            if (pageRef != null)
            {
                PageData page = DataFactory.Instance.GetPage(pageRef);
                _errorMsg = page.PageTypeName;
                if (PageType.IsInstanceOfType(page))
                {
                    return true;
                }
            }

            /*
            else if (value as LinkItemCollection != null)
            {
                // Loop through and check if it is a link to an EPiServer page.
                // If it is add it to pages.
                LinkItemCollection linkItems = value as LinkItemCollection;
                List<PageData> pages = new List<PageData>();
                foreach (LinkItem linkItem in linkItems)
                {
                    string linkUrl;
                    Guid linkGuid = PermanentLinkUtility.GetGuid(linkItem.Href);

                    //if (!PermanentLinkMapStore.TryToMapped(linkItem.Href, out linkUrl))
                    //{
                     //   _errorMsg = linkItem.Text;
                     //   return false;
                    //}

                    if (linkGuid == null) // string.IsNullOrEmpty(linkUrl))
                    {
                        _errorMsg = linkItem.Text;
                        return false;
                    }

                    PageReference pageReference = PageReference.ParseUrl(linkUrl);
                    if (PageReference.IsNullOrEmpty(pageReference))
                    {
                        _errorMsg = linkItem.Text;
                        return false;
                    }

                    pages.Add(DataFactory.Instance.GetPage(pageReference));
                }

                if (pages.Count > 0)
                {
                    foreach (PageData page in pages)
                    {
                        if (!this.PageType.IsInstanceOfType(page))
                        {
                            _errorMsg = page.PageName;
                            return false;
                        }
                    }
                }

                return true;
            }
            */

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, PageType.Name, _errorMsg);
        }
    }
}