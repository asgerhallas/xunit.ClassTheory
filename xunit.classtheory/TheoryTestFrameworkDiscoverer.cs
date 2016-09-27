using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class TheoryTestFrameworkDiscoverer : XunitTestFrameworkDiscoverer
    {
        public TheoryTestFrameworkDiscoverer(IAssemblyInfo assemblyInfo, ISourceInformationProvider sourceProvider, IMessageSink diagnosticMessageSink, IXunitTestCollectionFactory collectionFactory = null) 
            : base(assemblyInfo, sourceProvider, diagnosticMessageSink, collectionFactory) {}

        protected override bool FindTestsForMethod(ITestMethod testMethod, bool includeSourceInformation, IMessageBus messageBus, ITestFrameworkDiscoveryOptions discoveryOptions)
        {
            return base.FindTestsForMethod(
                testMethod, includeSourceInformation, 
                new MessageBusDecorator(messageBus, DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault()), 
                discoveryOptions);
        }
    }
}