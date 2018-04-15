using System;

namespace Eurofins.Testing.Other
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldAttribute : Attribute
    {
        private string[] titles;
        private string title;
        private object[] objTitles;
        public string[] Titles
        {
            get { return titles; }
        }

        public object[] ObjectTitles
        {
            get { return objTitles; }
        }

        public string Title
        {
            get { return title; }
        }

        public FieldAttribute(object[] titles)
        {
            this.objTitles = titles;
        }

        public FieldAttribute(string[] titles)
        {
            this.titles = titles;
        }

        public FieldAttribute(string title)
        {
            this.title = title;
        }

        public static string[] Gets(object enm)
        {
            if (enm != null)
            {
                var mi = enm.GetType().GetMember(enm.ToString());
                if (mi.Length > 0)
                {
                    var attr = GetCustomAttribute(mi[0], typeof(FieldAttribute)) as FieldAttribute;
                    if (attr != null)
                    {
                        return attr.titles;
                    }
                }
            }
            return null;
        }

        public static TT[] Gets<TT>(object enm)
        {
            if (enm != null)
            {
                var mi = enm.GetType().GetMember(enm.ToString());
                if (mi.Length > 0)
                {
                    var attr = GetCustomAttribute(mi[0], typeof(FieldAttribute)) as FieldAttribute;
                    if (attr != null)
                    {
                        return attr.objTitles as TT[];
                    }
                }
            }
            return null;
        }

        public static string Get(object enm)
        {
            if (enm != null)
            {
                var mi = enm.GetType().GetMember(enm.ToString());
                if (mi.Length > 0)
                {
                    var attr = GetCustomAttribute(mi[0], typeof(FieldAttribute)) as FieldAttribute;
                    if (attr != null)
                    {
                        return attr.title;
                    }
                }
            }
            return null;
        }
    }
}
