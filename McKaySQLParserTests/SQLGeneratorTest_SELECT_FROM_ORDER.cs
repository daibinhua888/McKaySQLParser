using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;
using McKaySQLParser.Core.SQLGenerator;

namespace McKaySQLParserTests
{
    [TestClass]
    public class SQLGeneratorTest_SELECT_FROM_ORDER
    {
        [TestMethod]
        public void BasicTest_ORDERBY()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" } });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NONE };

            stmtInfo.OrderBy.Add(new OrderByCondition() {  Expression="userId", Direction= OrderByDirection.ASC});

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId, userName FROM me ORDER BY userId ASC", sql);
        }

        [TestMethod]
        public void BasicTest_ORDERBY2()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" } });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NONE };

            stmtInfo.OrderBy.Add(new OrderByCondition() { Expression = "userId", Direction = OrderByDirection.ASC });
            stmtInfo.OrderBy.Add(new OrderByCondition() { Expression = "userName", Direction = OrderByDirection.DESC });

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId, userName FROM me ORDER BY userId ASC, userName DESC", sql);
        }
    }
}
