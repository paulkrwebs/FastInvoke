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
    public class FastInvokePropertyInfoExtensionsTest
    {
        #region // positive tests

        [Test]
        [TestCase(5000000)]
        public void SetAsFastInvoke_PropertyInfo_Performance(int iterations)
        {
            PropertyInfo pi = typeof(TestClass).GetProperty("Property");
            MethodInfo mi = pi.GetSetMethod();
            TestClass tc = new TestClass("injected");
            Action<object, object> fast = pi.SetAsFastInvoke();
            DateTime start;
            TimeSpan diff;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("SetAsFastInvoke_PropertyInfo_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                mi.Invoke(tc, new object[] { "set" });
            }
            diff = DateTime.Now - start;
            Assert.IsNotNull(tc.Property = "set");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            tc.Property = string.Empty;
            for (int i = 0; i < iterations; i++)
            {
                fast(tc, "set");
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tc.Property = "set");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tc.Property = "set";
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tc.Property = "set");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void SetAsFastInvoke_PropertyInfoValueTypeKnown_Performance(int iterations)
        {
            PropertyInfo pi = typeof(TestClass).GetProperty("Property");
            MethodInfo mi = pi.GetSetMethod();
            TestClass tc = new TestClass("injected");
            Action<TestClass, object> fast = pi.SetAsFastInvoke<TestClass>();
            DateTime start;
            TimeSpan diff;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("SetAsFastInvoke_PropertyInfoValueTypeKnown_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                mi.Invoke(tc, new object[] { "set" });
            }
            diff = DateTime.Now - start;
            Assert.IsNotNull(tc.Property = "set");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            tc.Property = string.Empty;
            for (int i = 0; i < iterations; i++)
            {
                fast(tc, "set");
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tc.Property = "set");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tc.Property = "set";
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tc.Property = "set");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void SetAsFastInvoke_PropertyInfoTypeSafe_Performance(int iterations)
        {
            PropertyInfo pi = typeof(TestClass).GetProperty("Property");
            MethodInfo mi = pi.GetSetMethod();
            TestClass tc = new TestClass("injected");
            Action<TestClass, string> fast = pi.SetAsFastInvoke<TestClass, string>();
            DateTime start;
            TimeSpan diff;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("SetAsFastInvoke_PropertyInfoTypeSafe_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                mi.Invoke(tc, new object[] { "set" });
            }
            diff = DateTime.Now - start;
            Assert.IsNotNull(tc.Property = "set");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            tc.Property = string.Empty;
            for (int i = 0; i < iterations; i++)
            {
                fast(tc, "set");
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tc.Property = "set");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tc.Property = "set";
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tc.Property = "set");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void GetAsFastInvoke_PropertyInfo_Performance(int iterations)
        {
            PropertyInfo pi = typeof(TestClass).GetProperty("Property");
            MethodInfo mi = pi.GetGetMethod();
            TestClass tc = new TestClass("injected");
            Func<object, object> fast = pi.GetAsFastInvoke();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("GetAsFastInvoke_PropertyInfo_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test = mi.Invoke(tc, new object[0]).ToString();
            }
            diff = DateTime.Now - start;
            Assert.IsTrue(test == "injected");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test2 = fast(tc).ToString();
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test2 == "injected");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test3 = tc.Property;
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test3 == "injected");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void GetAsFastInvoke_PropertyInfoReturnTypeNotKnown_Performance(int iterations)
        {
            PropertyInfo pi = typeof(TestClass).GetProperty("Property");
            MethodInfo mi = pi.GetGetMethod();
            TestClass tc = new TestClass("injected");
            Func<TestClass, object> fast = pi.GetAsFastInvoke<TestClass>();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("GetAsFastInvoke_PropertyInfoReturnTypeNotKnown_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test = mi.Invoke(tc, new object[0]).ToString();
            }
            diff = DateTime.Now - start;
            Assert.IsTrue(test == "injected");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test2 = fast(tc).ToString();
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test2 == "injected");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test3 = tc.Property;
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test3 == "injected");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void GetAsFastInvoke_PropertyInfoTypeSafe_Performance(int iterations)
        {
            PropertyInfo pi = typeof(TestClass).GetProperty("Property");
            MethodInfo mi = pi.GetGetMethod();
            TestClass tc = new TestClass("injected");
            Func<TestClass, string> fast = pi.GetAsFastInvoke<TestClass, string>();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("GetAsFastInvoke_PropertyInfoTypeSafe_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test = mi.Invoke(tc, new object[0]).ToString();
            }
            diff = DateTime.Now - start;
            Assert.IsTrue(test == "injected");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms",firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test2 = fast(tc);
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test2 == "injected");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms",secondTimePerCall));

            // native invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test3 = tc.Property;
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test3 == "injected");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms",thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        #endregion
    }
}
