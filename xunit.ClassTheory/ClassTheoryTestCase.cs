using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class ClassTheoryTestCase : XunitTestCase
    {
        readonly Type factoryType;

        public ClassTheoryTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod, object[] testMethodArguments, Type factoryType) 
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments) 
        {
            this.factoryType = factoryType;
        }

        protected override string GetDisplayName(IAttributeInfo factAttribute, string displayName)
        {
            return $"{base.GetDisplayName(factAttribute, displayName)}: {factoryType}";
        }

        protected override string GetUniqueID()
        {
            return $"{base.GetUniqueID()}:{factoryType}";
        }

        public override Task<RunSummary> RunAsync(
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            var factory = Activator.CreateInstance(factoryType);

            // find the placeholder (typeof(object)) in the arguments and insert the factory. 
            var copyOfConstructorArguments = constructorArguments.ToArray();

            for (int i = 0; i < copyOfConstructorArguments.Length; i++)
            {
                if (copyOfConstructorArguments[i].GetType() == typeof (object))
                {
                    copyOfConstructorArguments[i] = factory;
                }
            }

            return new TheoryTestCaseRunner(this, DisplayName, SkipReason, copyOfConstructorArguments, TestMethodArguments, messageBus, aggregator, cancellationTokenSource, factory as IDisposable).RunAsync();
        }
    }
}