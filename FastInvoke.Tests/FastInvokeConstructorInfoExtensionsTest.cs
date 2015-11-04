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
    public class FastInvokeConstructorInfoExtensionsTest
    {
        #region // positive test

        [Test]
        [TestCase(5000000)]
        public void CreateAsFunc_ConstructorInfo_Performance(int iterations)
        {
            ConstructorInfo ci = typeof(TestClass).GetConstructor(new Type[] { typeof(string) });
            TestClass tc = new TestClass("injected");
            TestClass tcResult;
            Func<object[], object> fast = ci.CreateAsFastInvoke();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateAsFunc_ConstructorInfo_Performance() Performance Test");

            // reflection
            tcResult = null;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tcResult = ci.Invoke(new object[] { "injected" }) as TestClass;
            }
            diff = DateTime.Now - start;
            Assert.IsNotNull(tcResult);
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            tcResult = null;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tcResult = fast(new object[] { "injected" }) as TestClass;
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tcResult);
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            tcResult = null;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tcResult = new TestClass("injected");
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tcResult);
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        [Test]
        [TestCase(5000000)]
        public void CreateAsFuncTypeSafe_ConstructorInfo_Performance(int iterations)
        {
            ConstructorInfo ci = typeof(TestClass).GetConstructor(new Type[] { typeof(string) });
            TestClass tc = new TestClass("injected");
            TestClass tcResult;
            Func<object[], TestClass> fast = ci.CreateAsFastInvoke<TestClass>();
            DateTime start;
            TimeSpan diff;
            string test = string.Empty;
            string test2 = string.Empty;
            string test3 = string.Empty;
            double firstTimePerCall;
            double secondTimePerCall;
            double thirdTimePerCall;

            // start
            Console.WriteLine("CreateAsFuncTypeSafe_ConstructorInfo_Performance() Performance Test");

            // reflection
            tcResult = null;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tcResult = ci.Invoke(new object[] { "injected" }) as TestClass;
            }
            diff = DateTime.Now - start;
            Assert.IsNotNull(tcResult);
            firstTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (reflection): {0} ms", firstTimePerCall));

            // fast invoke
            tcResult = null;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tcResult = fast(new object[] { "injected" });
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tcResult);
            secondTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (fast invoke): {0} ms", secondTimePerCall));

            // native invoke
            tcResult = null;
            start = DateTime.Now;
            for (int i = 0; i < iterations; i++)
            {
                tcResult = new TestClass("injected");
            }
            diff = DateTime.Now - start;

            Assert.IsNotNull(tcResult);
            thirdTimePerCall = diff.TotalMilliseconds / (double)iterations;
            Console.WriteLine(String.Format("Per Call (native call): {0} ms", thirdTimePerCall));

            // ratios
            Console.WriteLine(string.Format("Reflection/Fast Invoke Ratio: {0}", (firstTimePerCall / secondTimePerCall)));
            Console.WriteLine(string.Format("Fast Invoke/Native Invoke Ratio: {0}", (secondTimePerCall / thirdTimePerCall)));
        }

        #endregion
    }
}
