using System;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    [TestFrameworkDiscoverer("xunit.ClassTheory.TestFrameworkTypeDiscoverer", "xunit.ClassTheory")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class UseClassTheoryTestFramework : Attribute, ITestFrameworkAttribute
    {
        
    }
}