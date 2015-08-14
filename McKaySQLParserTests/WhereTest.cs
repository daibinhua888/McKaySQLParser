using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;
using McKaySQLParser.Core.Exceptions;

namespace McKaySQLParserTests
{
    [TestClass]
    public class WhereTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingWhereConditionException))]
        public void WhereClause_0()
        {
            string sql = @"select userId from me where";
            var info = SQLParser.ParseSQL(sql);
        }

        [TestMethod]
        public void WhereClause_1()
        {
            string sql = @"select userId from me where userId=100";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(1, info.WhereCondition.TotalCount);
        }

        [TestMethod]
        public void WhereClause_1_string()
        {
            string sql = @"select userId from me where userName='McKay'";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(1, info.WhereCondition.TotalCount);
            Assert.AreEqual("userName", info.WhereCondition.Condition_LeftExpression);
            Assert.AreEqual("=", info.WhereCondition.Condition_Operator);
            Assert.AreEqual("'McKay'", info.WhereCondition.Condition_RightExpression);
        }

        [TestMethod]
        public void WhereClause_2()
        {
            string sql = @"select userId from me where userId=100 and userId>1";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(3, info.WhereCondition.TotalCount);
        }

        [TestMethod]
        public void WhereClause_EqualOperator()
        {
            string sql = @"select userId from me where userId=100 and userId>1";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("=", info.WhereCondition.Statement_LeftNode.Condition_Operator);
        }

        [TestMethod]
        public void WhereClause_GreaterThen_Operator()
        {
            string sql = @"select userId from me where userId=100 and userId>1";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual(">", info.WhereCondition.Statement_RightNode.Condition_Operator);
        }
    }
}
