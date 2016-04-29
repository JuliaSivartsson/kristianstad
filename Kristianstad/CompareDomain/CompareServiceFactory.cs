using Kristianstad.CompareDomain;
using Kristianstad.CompareDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain
{
    public static class CompareServiceFactory
    {
        private static IService _service;

        public static IService Instance
        {
            get
            {
                if (_service == null)
                {
                    _service = new Service();
                }

                return _service;
            }
        }
    }
}
