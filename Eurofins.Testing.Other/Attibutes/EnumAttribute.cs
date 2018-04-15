using System;

namespace Eurofins.Testing.Other
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumAttribute : Attribute
    {
        private string[] title;
        public string[] Titles
        {
            get
            {
                return title;
            }
        }
        public EnumAttribute(string[] title)
        {
            this.title = title;
        }
    }
}
