using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class TheoryTestFramework : XunitTestFramework
    {
        public TheoryTestFramework(IMessageSink messageSink)
            : base(messageSink)
        { }

        protected override ITestFrameworkDiscoverer CreateDiscoverer(IAssemblyInfo assemblyInfo) => 
            new TheoryTestFrameworkDiscoverer(assemblyInfo, SourceInformationProvider, DiagnosticMessageSink, null);

        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName) => 
            new TheoryTestFrameworkExecutor(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
    }
}