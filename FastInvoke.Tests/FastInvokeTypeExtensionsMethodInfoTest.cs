using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using System.Linq.Expressions;

namespace FastInvoke
{
    [TestFixture]
    public class FastInvokeTypeExtensionsMethodInfoTest
    {
        #region // non type safe

        [Test]
        public void GetMethodFastInvoke_MethodName_Performance()
        {
            Func<object, object[], object> fast = typeof(TestClass).GetMethodFastInvoke("TestMethodNoOverload");
            TestClass tc = new TestClass("injected");
            string test = string.Empty;
            
            test = fast(tc, new object[] { "param1", "param2" }).ToString();

            Assert.IsTrue(test == "param1-param2-injected");
        }

        [Test]
        public void GetMethodFastInvoke_MethodNameAndPrivateBindingFlags()
        {
            Func<object, object[], object> fast = typeof(TestClass).GetMethodFastInvoke("TestPrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[0]).ToString();

            Assert.IsTrue(test == "TestPrivateMethod");
        }

        [Test]
        public void GetMethodFastInvoke_MethodNameAndTypeArray()
        {
            Func<object, object[], object> fast = typeof(TestClass).GetMethodFastInvoke("TestMethod", new Type[] { typeof(string), typeof(string), typeof(string) });
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[] { "param1", "param2", "param3" }).ToString();

            Assert.IsTrue(test == "param1-param2-param3");
        }

        [Test]
        // not working!
        public void GetMethodFastInvoke_MethodNameAndTypeArrayAndParameterModifier()
        {
            // need to set this up to test
            // http://msdn.microsoft.com/en-us/library/4s2kzbw8.aspx
            // can do this in a better type safe way!
            ReferenceParams<TestClass, object> fast = typeof(TestClass).GetMethodFastInvokeAsRefenceCall<TestClass, object>("TestMethodReferenceParams", BindingFlags.Public | BindingFlags.Instance, null, CallingConventions.Any, new Type[] { typeof(string).MakeByRefType(), typeof(string).MakeByRefType() }, null);
            TestClass tc = new TestClass("injected");
            string test = string.Empty;
            string param1 = "param1";
            string param2 = "param2";
            object[] arr = new object[] { param1, param2 };

            //test = fast(tc, new object[] { param1, param2 }).ToString();
            fast(tc, ref arr);

            // Assert.AreEqual(test, "param1:end-param2:end-injected");
            Assert.AreEqual(arr[0], "param1:end");
            Assert.AreEqual(arr[1], "param2:end");
        }

        [Test]
        public void GetMethodFastInvoke_MethodNameWithVoidReturn()
        {
            Func<object, object[], object> fast = typeof(TestClass).GetMethodFastInvoke("TestVoid");
            TestClass tc = new TestClass("injected");

            fast(tc, new object[] { "param1", "param2" });

            Assert.IsTrue(tc.Property == "TestVoid Run");
        }

        #endregion

        #region // instance type safe

        [Test]
        public void GetMethodFastInvokeTypeSafeInstance_MethodName()
        {
            Func<TestClass, object[], object> fast = typeof(TestClass).GetMethodFastInvoke<TestClass>("TestMethodNoOverload");
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[] { "param1", "param2" }).ToString();

            Assert.IsTrue(test == "param1-param2-injected");
        }

        [Test]
        public void GetMethodFastInvokeTypeSafeInstance_MethodNameAndPrivateBindingFlags()
        {
            Func<TestClass, object[], object> fast = typeof(TestClass).GetMethodFastInvoke<TestClass>("TestPrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[0]).ToString();

            Assert.IsTrue(test == "TestPrivateMethod");
        }

        [Test]
        public void GetMethodFastInvokeTypeSafeInstance_MethodNameAndTypeArray()
        {
            Func<TestClass, object[], object> fast = typeof(TestClass).GetMethodFastInvoke<TestClass>("TestMethod", new Type[] { typeof(string), typeof(string), typeof(string) });
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[] { "param1", "param2", "param3" }).ToString();

            Assert.IsTrue(test == "param1-param2-param3");
        }

        [Test]
        public void GetMethodFastInvokeTypeSafe_MethodNameWithVoidReturn()
        {
            Func<TestClass, object[], object> fast = typeof(TestClass).GetMethodFastInvoke<TestClass>("TestVoid");
            TestClass tc = new TestClass("injected");

            fast(tc, new object[] { "param1", "param2" });

            Assert.IsTrue(tc.Property == "TestVoid Run");
        }

        #endregion

        #region // type safe

        [Test]
        public void GetMethodFastInvokeTypeSafe_MethodName()
        {
            Func<TestClass, object[], string> fast = typeof(TestClass).GetMethodFastInvoke<TestClass, string>("TestMethodNoOverload");
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[] { "param1", "param2" });

            Assert.IsTrue(test == "param1-param2-injected");
        }

        [Test]
        public void GetMethodFastInvokeTypeSafe_MethodNameAndPrivateBindingFlags()
        {
            Func<TestClass, object[], string> fast = typeof(TestClass).GetMethodFastInvoke<TestClass, string>("TestPrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[0]);

            Assert.IsTrue(test == "TestPrivateMethod");
        }
        

        [Test]
        public void GetMethodFastInvokeTypeSafe_MethodNameAndTypeArray()
        {
            Func<TestClass, object[], string> fast = typeof(TestClass).GetMethodFastInvoke<TestClass, string>("TestMethod", new Type[] { typeof(string), typeof(string), typeof(string) });
            TestClass tc = new TestClass("injected");
            string test = string.Empty;

            test = fast(tc, new object[] { "param1", "param2", "param3" });

            Assert.IsTrue(test == "param1-param2-param3");
        }

        #endregion
    }
}
