using Kristianstad.CompareDomain;
using Kristianstad.CompareDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compare
{
    public class ServiceFactory
    {
        private IService _service;

        public IService Instance
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
