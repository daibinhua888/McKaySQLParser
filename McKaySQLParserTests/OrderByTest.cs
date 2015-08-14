using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;
using McKaySQLParser.Core.Exceptions;

namespace McKaySQLParserTests
{
    [TestClass]
    public class OrderByTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingOrderByException))]
        public void OrderByNotExists()
        {
            string sql = @"select userId from [me] order by";
            var info = SQLParser.ParseSQL(sql);
        }

        [TestMethod]
        public void OneColumn_NO_Direction()
        {
            string sql = @"select userId from [me] order by userId";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy.First().Direction);
        }

        [TestMethod]
        public void OneColumn_NO_Direction_WITH_Underline()
        {
            string sql = @"select userId from [me] order by user_Id";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy.First().Direction);
            Assert.AreEqual("user_Id", info.OrderBy.First().Expression);
        }

        [TestMethod]
        public void TwoColumn_NO_Direction()
        {
            string sql = @"select userId from [me] order by userId, userName";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy[0].Direction);
            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy[1].Direction);
        }

        [TestMethod]
        public void TwoColumn_OneDirection()
        {
            string sql = @"select userId from [me] order by userId desc, userName";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(OrderByDirection.DESC, info.OrderBy[0].Direction);
            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy[1].Direction);
        }

        [TestMethod]
        public void ThreeColumn_OneDirection()
        {
            string sql = @"select userId from [me] order by userId,         userName DESC,           sex";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy[0].Direction);
            Assert.AreEqual(OrderByDirection.DESC, info.OrderBy[1].Direction);
            Assert.AreEqual(OrderByDirection.ASC, info.OrderBy[2].Direction);
        }
    }
}
