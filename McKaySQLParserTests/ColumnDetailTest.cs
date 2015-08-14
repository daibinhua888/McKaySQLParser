using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;

namespace McKaySQLParserTests
{
    [TestClass]
    public class ColumnDetailTest
    {
        [TestMethod]
        public void ColumnDetail_1_STAR()
        {
            string sql = @"select * from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("*", info.Columns.First().Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns.First().Expression.ExpressionType);
        }

        [TestMethod]
        public void ColumnDetail_1_FIX_NUMBER()
        {
            string sql = @"Select 1 from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("1", info.Columns.First().Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns.First().Expression.ExpressionType);
        }

        [TestMethod]
        public void ColumnDetail_1_Has_UnderLine()
        {
            string sql = @"Select user_Id from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("user_Id", info.Columns.First().Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns.First().Expression.ExpressionType);
        }

        [TestMethod]
        public void ColumnDetail_2()
        {
            string sql = @"Select userId, 1 from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("userId", info.Columns[0].Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns[0].Expression.ExpressionType);

            Assert.AreEqual("1", info.Columns[1].Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns[1].Expression.ExpressionType);
        }

        [TestMethod]
        public void ColumnDetail_2_With_Alias()
        {
            string sql = @"Select userId, 1 as fid from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("userId", info.Columns[0].Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns[0].Expression.ExpressionType);

            Assert.AreEqual("1", info.Columns[1].Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns[1].Expression.ExpressionType);

            Assert.AreEqual(true, info.Columns[1].HasAlias);
            Assert.AreEqual("fid", info.Columns[1].Alias);
        }

        [TestMethod]
        public void ColumnDetail_10()
        {
            string sql = @"Select userId, 1 as fid, userId2, userId3, userId4, userId5, userId6, userId7, userId8, userId9 from [me]";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("userId", info.Columns[0].Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns[0].Expression.ExpressionType);

            Assert.AreEqual("1", info.Columns[1].Expression.ColumnName);
            Assert.AreEqual(ExpressionType.ColumnName, info.Columns[1].Expression.ExpressionType);

            Assert.AreEqual(true, info.Columns[1].HasAlias);
            Assert.AreEqual("fid", info.Columns[1].Alias);

            Assert.AreEqual("userId9", info.Columns.Last().Expression.ColumnName);

            Assert.AreEqual(10, info.Columns.Count);
        }
    }
}
