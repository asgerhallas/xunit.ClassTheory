using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class TheoryTestCaseRunner : XunitTestCaseRunner
    {
        readonly IDisposable disposable;

        public TheoryTestCaseRunner(
            IXunitTestCase testCase, 
            string displayName, 
            string skipReason, 
            object[] constructorArguments, 
            object[] testMethodArguments, 
            IMessageBus messageBus, 
            ExceptionAggregator aggregator, 
            CancellationTokenSource cancellationTokenSource, 
            IDisposable disposable) 
            : base(testCase, displayName, skipReason, constructorArguments, testMethodArguments, messageBus, aggregator, cancellationTokenSource)
        {
            this.disposable = disposable;
        }

        protected override Task BeforeTestCaseFinishedAsync()
        {
            Aggregator.Run(() =>
            {
                disposable?.Dispose();
            });

            return base.BeforeTestCaseFinishedAsync();
        }
    }
}