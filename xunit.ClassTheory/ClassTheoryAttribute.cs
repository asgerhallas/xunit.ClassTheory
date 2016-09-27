using System;
using Xunit;

namespace xunit.ClassTheory
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ClassTheoryAttribute : TheoryAttribute
    {
        public ClassTheoryAttribute(Type @interface)
        {
            Interface = @interface;
        }

        public Type Interface { get; set; }
    }
}