using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using Selenium.Internal.SeleniumEmulation;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public class ActionEx:Actions
    {
        private IWebDriver _webDriver;
        private readonly IJavaScriptExecutor _scriptExecutor;

        public IJavaScriptExecutor ScriptExecutor
        {
            get
            {
                return _scriptExecutor;
            }
        }

        public ActionEx(IWebDriver webDriver):base(webDriver)
        {
            this._webDriver = webDriver;
        }

        public void Scroll(int x, int y)
        {
            var jsscript = "javascript:window.scroll(" + x + "," + y + ")";
            JavaScriptLibrary.ExecuteScript(this._webDriver, jsscript, "");
        }

        public void HoverOn(IWebElement wrappedElement)
        {
            var fire = "return (" + JavaScriptLibrary.GetSeleniumScript("fireEvent.js") + ").apply(null,arguments);";
            ScriptExecutor.ExecuteScript(fire, wrappedElement, "mouseover");
            System.Threading.Thread.Sleep(1000);
        }

        public void MouseHoverOn(IWebElement wrappedElement)
        {
            System.Threading.Thread.Sleep(500);
            MoveToElement(wrappedElement).Perform();
            System.Threading.Thread.Sleep(500);
        }

        public long GetElementIndex(IWebElement wrappedElement)
        {
            String script = "var _isCommentOrEmptyTextNode = function(node) {\n" +
                            "    return node.nodeType == 8 || ((node.nodeType == 3) && !(/[^\\t\\n\\r ]/.test(node.data)));\n" +
                            "}\n" +
                            "    var element = arguments[0];\n" +
                            "    var previousSibling;\n" +
                            "    var index = 0;\n" +
                            "    while ((previousSibling = element.previousSibling) != null) {\n" +
                            "        if (!_isCommentOrEmptyTextNode(previousSibling)) {\n" +
                            "            index++;\n" +
                            "        }\n" +
                            "        element = previousSibling;\n" +
                            "    }\n" +
                            "    return index;";
            return (long)JavaScriptLibrary.ExecuteScript(this._webDriver, script, wrappedElement);
        }
    }
}
