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
    public class FastInvokeMethodInfoExtensionsTest
    {
        #region // positive tests

        [Test]
        [TestCase(5000000)]
        public void CreateFastInvoke_MethodInfo_Performance(int iterations)
        {
            MethodInfo mi = typeof(TestClass).GetMethod("TestMethod", new Type[] { typeof(string),typeof(string) });
            TestClass tc = new TestClass("injected");
            Func<object, object[], object> fast = mi.CreateFastInvoke();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateFastInvoke_MethodInfo_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test = mi.Invoke(tc, new object[] { "param1", "param2" }).ToString();
            }
            diff = DateTime.Now - start;
            Assert.IsTrue(test == "param1-param2-injected");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms",firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test2 = fast(tc, new object[] { "param1", "param2" }).ToString();
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test2 == "param1-param2-injected");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms",secondTimePerCall));

            // native invoke
            start = DateTime.Now;
            test3 = tc.TestMethod("param1", "param2");
            for (int i = 0; i < iterations; i++)
            {
                test3 = tc.TestMethod("param1", "param2");
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test3 == "param1-param2-injected");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms",thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void CreateFastInvokeTypeSafeInstance_MethodInfo_Performance(int iterations)
        {
            MethodInfo mi = typeof(TestClass).GetMethod("TestMethod", new Type[] { typeof(string),typeof(string) });
            TestClass tc = new TestClass("injected");
            Func<TestClass, object[], object> fast = mi.CreateFastInvoke<TestClass>();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateFastInvokeTypeSafeInstance_MethodInfo_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test = mi.Invoke(tc, new object[] { "param1", "param2" }).ToString();
            }
            diff = DateTime.Now - start;
            Assert.IsTrue(test == "param1-param2-injected");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test2 = fast(tc, new object[] { "param1", "param2" }).ToString();
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test2 == "param1-param2-injected");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            start = DateTime.Now;
            test3 = tc.TestMethod("param1", "param2");
            for (int i = 0; i < iterations; i++)
            {
                test3 = tc.TestMethod("param1", "param2");
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test3 == "param1-param2-injected");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void CreateFastInvokeTypeSafe_MethodInfo_Performance(int iterations)
        {
            MethodInfo mi = typeof(TestClass).GetMethod("TestMethod", new Type[] { typeof(string),typeof(string) });
            TestClass tc = new TestClass("injected");
            Func<TestClass, object[], string> fast = mi.CreateFastInvoke<TestClass, string>();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateFastInvokeTypeSafe_MethodInfo_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test = mi.Invoke(tc, new object[] { "param1", "param2" }).ToString();
            }
            diff = DateTime.Now - start;
            Assert.IsTrue(test == "param1-param2-injected");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                test2 = fast(tc, new object[] { "param1", "param2" });
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test2 == "param1-param2-injected");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            start = DateTime.Now;
            test3 = tc.TestMethod("param1", "param2");
            for (int i = 0; i < iterations; i++)
            {
                test3 = tc.TestMethod("param1", "param2");
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(test3 == "param1-param2-injected");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void CreateFastInvoke_MethodInfoWithVoidReturn_Performance(int iterations)
        {
            MethodInfo mi = typeof(TestClass).GetMethod("TestVoid");
            TestClass tc = new TestClass("injected");
            Func<object, object[], object> fast = mi.CreateFastInvoke();
            DateTime start;
            TimeSpan diff;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateFastInvoke_MethodInfoWithVoidReturn_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                mi.Invoke(tc, new object[] { "param1", "param2" });
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(tc.Property == "TestVoid Run");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms",firstTimePerCall));

            // fast invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                fast(tc, new object[] { "param1", "param2" });
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(tc.Property == "TestVoid Run");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms",secondTimePerCall));

            // native invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            // The reason of the anomaly is the jit compiler.
            // In the first case the jit compiler has to compile the calling command.
            //In the second case it is compiled before the timer started
            tc.TestVoid("param1", "param2");
            for (int i = 0; i < iterations; i++)
            {
                tc.TestVoid("param1", "param2");
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(tc.Property == "TestVoid Run");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native): {0} ms",thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void CreateFastInvokeTypeSafe_MethodInfoVoidReturn_Performance(int iterations)
        {
            MethodInfo mi = typeof(TestClass).GetMethod("TestVoid");
            TestClass tc = new TestClass("injected");
            Func<TestClass, object[], object> fast = mi.CreateFastInvoke<TestClass>();
            DateTime start;
            TimeSpan diff;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateFastInvokeTypeSafe_MethodInfoVoidReturn_Performance() Performance Test");

            // reflection
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                mi.Invoke(tc, new object[] { "param1", "param2" });
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(tc.Property == "TestVoid Run");
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                fast(tc, new object[] { "param1", "param2" });
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(tc.Property == "TestVoid Run");
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            tc.Property = string.Empty;
            start = DateTime.Now;
            // The reason of the anomaly is the jit compiler.
            // In the first case the jit compiler has to compile the calling command.
            //In the second case it is compiled before the timer started
            tc.TestVoid("param1", "param2");
            for (int i = 0; i < iterations; i++)
            {
                tc.TestVoid("param1", "param2");
            }
            diff = DateTime.Now - start;

            Assert.IsTrue(tc.Property == "TestVoid Run");
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        #endregion
    }
}
