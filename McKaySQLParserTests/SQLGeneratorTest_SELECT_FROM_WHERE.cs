using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;
using McKaySQLParser.Core.SQLGenerator;

namespace McKaySQLParserTests
{
    [TestClass]
    public class SQLGeneratorTest_SELECT_FROM_WHERE
    {
        [TestMethod]
        public void BasicTest_1_Condition()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" } });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NONE };

            stmtInfo.WhereCondition = new WhereCondition(WhereConditionNodeType.Condition);
            stmtInfo.WhereCondition.Condition_LeftExpression = "userId";
            stmtInfo.WhereCondition.Condition_Operator = ">";
            stmtInfo.WhereCondition.Condition_RightExpression = "10";

            stmtInfo.OrderBy.Add(new OrderByCondition() { Expression = "userId", Direction = OrderByDirection.ASC });

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId, userName FROM me WHERE userId>10 ORDER BY userId ASC", sql);
        }

        [TestMethod]
        public void BasicTest_2_Condition()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" } });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NONE };

            stmtInfo.WhereCondition = new WhereCondition(WhereConditionNodeType.Statement);

            stmtInfo.WhereCondition.Statement_Operator = "AND";

            stmtInfo.WhereCondition.Statement_LeftNode = new WhereCondition(WhereConditionNodeType.Condition);
            stmtInfo.WhereCondition.Statement_LeftNode.Condition_LeftExpression = "userId";
            stmtInfo.WhereCondition.Statement_LeftNode.Condition_Operator = ">";
            stmtInfo.WhereCondition.Statement_LeftNode.Condition_RightExpression = "5";

            stmtInfo.WhereCondition.Statement_RightNode = new WhereCondition(WhereConditionNodeType.Condition);
            stmtInfo.WhereCondition.Statement_RightNode.Condition_LeftExpression = "sex";
            stmtInfo.WhereCondition.Statement_RightNode.Condition_Operator = "=";
            stmtInfo.WhereCondition.Statement_RightNode.Condition_RightExpression = "1";

            stmtInfo.OrderBy.Add(new OrderByCondition() { Expression = "userId", Direction = OrderByDirection.ASC });

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId, userName FROM me WHERE (userId>5) AND (sex=1) ORDER BY userId ASC", sql);
        }
    }
}
