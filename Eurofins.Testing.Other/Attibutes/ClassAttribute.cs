using System;

namespace Eurofins.Testing.Other
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassAttribute : Attribute
    {
        private string title;
        public string Title
        {
            get
            {
                return title;
            }
        }

        public ClassAttribute(string title)
        {
            this.title = title;
        }

        public static string Get(Type tp)
        {
            var attr = Attribute.GetCustomAttribute(tp, typeof(ClassAttribute)) as ClassAttribute;
            if (attr != null)
                return attr.title;
            return null;
        }
    }
}
