using Eurofins.ECOM.Selenium.Extension.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class FlowBase
    {
        private IPage _page;

        public FlowBase()
        {

        }
        public FlowBase(IPage page)
        {
            _page = page;
        }

        protected IPage Page
        {
            get
            {
                return _page;
            }
        }
    }
}
