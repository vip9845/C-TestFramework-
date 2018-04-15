using Eurofins.Testing.Other;
using OpenQA.Selenium;
using Eurofins.Selenium.Extension.Other;
using System.Collections.Generic;
using System.Reflection;
using System;


namespace Eurofins.ECOM.Selenium.Extension.Control
{
    public class OfferMenu : ControlBase
    {
        public OfferMenu()
        {

        }

        public OfferMenuItem this[object menuType]
        {
            get
            {
                var q = GetByQueue(menuType);
                var dequeueInfo = q.Dequeue();
                return new OfferMenuItem(By.XPath(dequeueInfo), q);
            }
        }

        private Queue<string> GetByQueue(object obj)
        {
            //Enum Type
            var objType = obj.GetType();
            //Class Type
            var objDeclaringType = objType.DeclaringType;

            //1.Add Class Attr  
            Queue<string> q = new Queue<string>();
            //ClassAttribute attrClass = objDeclaringType.GetCustomAttributes(typeof(ClassAttribute), true)[0] as ClassAttribute;
            //q.Enqueue(attrClass.Title);

            //2.Add Enum Attr
            EnumAttribute attrEnum = objType.GetCustomAttributes(typeof (EnumAttribute), true)[0] as EnumAttribute;
            foreach (var item in attrEnum.Titles)
                q.Enqueue(item);

            //3. Add Enum field Attr
            FieldAttribute attrEnumField = Attribute.GetCustomAttribute(obj.GetType().GetMember(obj.ToString())[0], typeof (FieldAttribute)) as FieldAttribute;

            foreach (string item in attrEnumField.Titles)
                q.Enqueue(item);

            //q.Enqueue(attrEnumField.Titles[0]);

            return q;
        }
    }
}