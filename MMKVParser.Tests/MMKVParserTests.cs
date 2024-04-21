using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMKVParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMKVParser.Tests
{
    [TestClass()]
    public class MMKVParserTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            MMKVParser parser = new("mmkv.default");

            var map = parser.Load();

            Assert.IsNotNull(MMKVParser.ReadUTF8StringValue(map["TOKEN"][1]));
        }
    }
}
