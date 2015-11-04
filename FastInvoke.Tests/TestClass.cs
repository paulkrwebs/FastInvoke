using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastInvoke
{
    public class TestClass
    {
        public string Property { get; set; }

        public TestClass(string property)
        {
            Property = property;
        }

        public string TestMethod(string param1, string param2)
        {
            return string.Format("{0}-{1}-{2}", param1, param2, Property);
        }

        public string TestMethod(string param1, string param2, string param3)
        {
            return string.Format("{0}-{1}-{2}", param1, param2, param3);
        }

        public string TestMethodNoOverload(string param1, string param2)
        {
            return string.Format("{0}-{1}-{2}", param1, param2, Property);
        }

        public void TestMethodReferenceParams(ref string param1, ref string param2)
        {
            param1 = param1 + ":end";
            param2 = param2 + ":end";
            //return string.Format("{0}-{1}-{2}", param1, param2, Property);
        }

        public void TestVoid(string param1, string param2)
        {
            Property = "TestVoid Run";
        }

        private string TestPrivateMethod()
        {
            return "TestPrivateMethod";
        }
    }
}
