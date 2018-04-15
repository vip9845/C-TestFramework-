using Eurofins.ECOM.Selenium.Extension.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class StartFlow
    {
        public StartFlow()
        {

        }

        public TTFlow GoToPage<TTFlow, TPage>()
            where TTFlow : FlowBase, new()
            where TPage : IPage, new()
        {
            var firstPage = Navigator.Instance.FirstOpen<TPage>();
            return Activator.CreateInstance(typeof(TTFlow), new object[] { firstPage }) as TTFlow;
        }
    }
}
