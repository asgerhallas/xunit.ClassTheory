using System;
using xunit.ClassTheory;
using Xunit;

[assembly: UseClassTheoryTestFramework]

namespace xunit.ClassTheory.Tests
{
    [ClassTheory(typeof (IMyInterface))]
    public class Class1
    {
        readonly IMyInterface subject;

        public Class1(IMyInterface subject)
        {
            this.subject = subject;
        }

        [Fact]
        public void GetItIsA()
        {
            Assert.Equal("A", subject.GetIt());
        }
    }

    public interface IMyInterface : IDisposable
    {
        string GetIt();
    }

    public class MyInterface1 : IMyInterface
    {
        public string GetIt()
        {
            return "A";
        }

        public void Dispose()
        {
        }
    }

    public class MyInterface2 : IMyInterface
    {
        public string GetIt()
        {
            return "A";
        }

        public void Dispose()
        {
        }
    }
}
