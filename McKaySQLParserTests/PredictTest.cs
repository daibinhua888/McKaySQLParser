using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;

namespace McKaySQLParserTests
{
    [TestClass]
    public class PredictTest
    {
        [TestMethod]
        public void PredictWordNotExists()
        {
            string sql = @"select userId from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(false, info.Column_PredictExists);
        }

        [TestMethod]
        public void PredictWordExists()
        {
            string sql = @"select distinct userId from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(true, info.Column_PredictExists);
            Assert.AreEqual("distinct", info.Column_PredictWord);
        }
    }
}
