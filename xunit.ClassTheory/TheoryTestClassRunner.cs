using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class TheoryTestClassRunner : XunitTestClassRunner
    {
        public TheoryTestClassRunner(ITestClass testClass, IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases, IMessageSink diagnosticMessageSink, IMessageBus messageBus, ITestCaseOrderer testCaseOrderer, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, IDictionary<Type, object> collectionFixtureMappings) 
            : base(testClass, @class, testCases, diagnosticMessageSink, messageBus, testCaseOrderer, aggregator, cancellationTokenSource, collectionFixtureMappings) {}

        protected override bool TryGetConstructorArgument(ConstructorInfo constructor, int index, ParameterInfo parameter, out object argumentValue)
        {
            if (base.TryGetConstructorArgument(constructor, index, parameter, out argumentValue))
                return true;

            argumentValue = new object(); // placeholder until we get the right test case
            return true;
        }
    }
}