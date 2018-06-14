using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    class Person : IComparable
    {

        String name;
        int age;
        String ssn;

        public Person(String name, int age, String ssn)
        {
            this.name = name;
            this.age = age;
            this.ssn = ssn;

        }

        public int CompareTo(object obj)
        {
            return age.CompareTo(obj);
        }
    }

}
