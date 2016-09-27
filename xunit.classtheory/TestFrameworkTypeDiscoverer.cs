using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class TestFrameworkTypeDiscoverer : ITestFrameworkTypeDiscoverer
    {
        public Type GetTestFrameworkType(IAttributeInfo attribute)
        {
            return typeof (TheoryTestFramework);
        }
    }
}