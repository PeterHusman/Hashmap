using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hashmap;

namespace HashmapUnitTests
{
    [TestClass]
    public class HashmapTests
    {
        [TestMethod]
        public void InsertTests()
        {
            Hashmap<string, int> hashmap = new Hashmap<string, int>();
            hashmap.Insert("Hello!", 1);
            Assert.ThrowsException<ArgumentException>(() => { hashmap.Insert("Hello!", 2); }, "Failed to throw exception when duplicate keys were added.");
        }
    }
}
