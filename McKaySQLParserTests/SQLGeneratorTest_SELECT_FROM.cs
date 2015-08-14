using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core.SQLGenerator;
using McKaySQLParser.Core;

namespace McKaySQLParserTests
{
    [TestClass]
    public class SQLGeneratorTest_SELECT_FROM
    {
        [TestMethod]
        public void BasicTest_NO_WHERE_NO_ORDERBY()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression=new ColumnExpression(ExpressionType.ColumnName){ ColumnName="userId"}});
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType= TableReadType.NONE };

            IGenerator g = new DefaultSqlGenerator();

            var sql=g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId, userName FROM me", sql);
        }

        [TestMethod]
        public void BasicTest_PREDICT_NO_WHERE_NO_ORDERBY()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Column_PredictExists = true;
            stmtInfo.Column_PredictWord = "DISTINCT";

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" } });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NONE };

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT DISTINCT userId, userName FROM me", sql);
        }

        [TestMethod]
        public void BasicTest_Alias()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" }, HasAlias=true, Alias="UID" });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NONE };

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId AS UID, userName FROM me", sql);
        }

        [TestMethod]
        public void BasicTest_Alias_NOLOCK()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" }, HasAlias = true, Alias = "UID" });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.NOLOCK };

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId AS UID, userName FROM me(NOLOCK)", sql);
        }

        [TestMethod]
        public void BasicTest_Alias_READPAST()
        {
            SelectStmtInfo stmtInfo = new SelectStmtInfo();

            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userId" }, HasAlias = true, Alias = "UID" });
            stmtInfo.Columns.Add(new Column() { Expression = new ColumnExpression(ExpressionType.ColumnName) { ColumnName = "userName" } });

            stmtInfo.TableDescriptor = new TableDescriptor() { TableName = "me", TableReadType = TableReadType.READPAST };

            IGenerator g = new DefaultSqlGenerator();

            var sql = g.Generate(stmtInfo);

            Assert.AreEqual("SELECT userId AS UID, userName FROM me(READPAST)", sql);
        }
    }
}
