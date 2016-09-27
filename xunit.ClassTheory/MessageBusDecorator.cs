using System;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace xunit.ClassTheory
{
    public class MessageBusDecorator : IMessageBus
    {
        readonly IMessageBus bus;
        readonly IMessageSink diagnosticMessageSink;
        readonly TestMethodDisplay defaultMethodDisplay;

        public MessageBusDecorator(IMessageBus bus, IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay)
        {
            this.bus = bus;
            this.diagnosticMessageSink = diagnosticMessageSink;
            this.defaultMethodDisplay = defaultMethodDisplay;
        }

        public bool QueueMessage(IMessageSinkMessage message)
        {
            var discoveryMessage = message as TestCaseDiscoveryMessage;
            var xunitTestCase = discoveryMessage?.TestCase as XunitTestCase;

            if (xunitTestCase == null)
                return bus.QueueMessage(message);

            var classTheoryAttribute = discoveryMessage.TestClass.Class
                .GetCustomAttributes(typeof(ClassTheoryAttribute)).SingleOrDefault();

            if (classTheoryAttribute == null)
                return bus.QueueMessage(message);

            var factoryInterfaceType = (Type)classTheoryAttribute.GetConstructorArguments().Single();
            var testCases = discoveryMessage.TestClass.Class.Assembly.GetTypes(true)
                .Select(x => x.ToRuntimeType())
                .Where(x => !x.IsAbstract && factoryInterfaceType.IsAssignableFrom(x))
                .Select(factoryType => new ClassTheoryTestCase(
                    diagnosticMessageSink, defaultMethodDisplay, xunitTestCase.TestMethod, 
                    xunitTestCase.TestMethodArguments, factoryType))
                .ToList();

            return testCases.Aggregate(true, (current, testCase) => 
                current && bus.QueueMessage(new TestCaseDiscoveryMessage(testCase)));
        }

        public void Dispose()
        {
            bus.Dispose();
        }
    }
}