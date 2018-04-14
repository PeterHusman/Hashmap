using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hashmap;
using System.Collections.Generic;

namespace HashmapUnitTests
{
    [TestClass]
    public class HashmapTests
    {
        [TestMethod]
        public void InsertTest()
        {
            Hashmap<string, int> hashmap = new Hashmap<string, int>();
            hashmap.Insert("Hello!", 1);
            Assert.ThrowsException<ArgumentException>(() => { hashmap.Insert("Hello!", 2); }, "Failed to throw exception when duplicate keys were added.");
        }

        [TestMethod]
        public void DeleteTest()
        {
            Hashmap<string, int> hashmap = new Hashmap<string, int>();
            Assert.IsFalse(hashmap.Delete("Hello!"));
        }

        [TestMethod]
        public void AccessTest()
        {
            Hashmap<string, int> hashmap = new Hashmap<string, int>();
            Assert.ThrowsException<KeyNotFoundException>(()=>hashmap["hey!"], "Failed to throw exception when nonexistant key was accessed.");
        }
    }
}
