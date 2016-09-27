using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class TheoryTestFrameworkExecutor : XunitTestFrameworkExecutor
    {
        public TheoryTestFrameworkExecutor(AssemblyName assemblyName, ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyName, sourceInformationProvider, diagnosticMessageSink)
        { }

        protected override ITestFrameworkDiscoverer CreateDiscoverer() => 
            new TheoryTestFrameworkDiscoverer(AssemblyInfo, SourceInformationProvider, DiagnosticMessageSink, null);

        protected override async void RunTestCases(IEnumerable<IXunitTestCase> testCases, IMessageSink executionMessageSink, ITestFrameworkExecutionOptions executionOptions)
        {
            using (var assemblyRunner = new TheoryTestAssemblyRunner(TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions))
                await assemblyRunner.RunAsync();
        }
    }
}