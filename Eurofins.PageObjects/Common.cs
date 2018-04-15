using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eurofins.PageObjects
{
    public static class Common
    {
        static Random rnd = new Random();

        public enum ArrayType
        {
            DNA,
            TheAminoAcidSequence
        }

        public static string GetXpathStringForIdEndsWith(string endStringOfControlId)
        {
            return "//*[substring(@id, string-length(@id)- string-length(\"" + endStringOfControlId + "\") + 1 )=\"" + endStringOfControlId + "\"]";
        }

        internal static int GetRandomNumber(int startNmbr, int endNmbr)
        {
            return rnd.Next(startNmbr, endNmbr);
        }

        internal static int GetRandomNumber()
        {
            return rnd.Next(1, 999);
        }

        public static class RandomValue
        {
            public static string RandomString
            {
                get
                {
                    string[] rndString = { "Horse", "Elephant", "John", "David", "Singh", "Ambani", "Matrix", "Neo", "Sunil", "Test", "Auto", "Selenium", "Eurofins", "Hello", "India", "Sachin", "Sourav", "Hrithik", "Hawking", "Wrigth", "Sky", "Virat", "Dhoni", "Anushka", "Sunny", "Kim" };
                    return rndString[rnd.Next(0, rndString.Length)] + GetRandomNumber(1000, 99999);
                }
            }

            public static string RandomCity
            {
                get
                {
                    return "Bangalore";
                }
            }

            public static string RandomState
            {
                get
                {
                    return "Andhra Pradesh";
                }
            }

            public static string RandomCountry
            {
                get
                {
                    return "India";
                }
            }

            public static string RandomTimeZone
            {
                get
                {
                    return "(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi";
                }
            }

            public static int RandomNumber
            {
                get
                {
                    return GetRandomNumber();
                }
            }

            public static string Email(string firstName)
            {
                return firstName + "@mailinator.com";
            }
        }
    }
}
