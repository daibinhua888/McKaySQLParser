using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McKaySQLParser.Core;
using McKaySQLParser.Core.Exceptions;

namespace McKaySQLParserTests
{
    [TestClass]
    public class TableNameTest
    {
        [TestMethod]
        public void TableName_Equal_To_me()
        {
            string sql = @"select userId from me";
            var info=SQLParser.ParseSQL(sql);

            Assert.AreEqual("me", info.TableDescriptor.TableName);
        }

        [TestMethod]
        public void TableName_Equal_To_me_NOLOCK()
        {
            string sql = @"select userId from me(NOLOCK)";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("me", info.TableDescriptor.TableName);
            Assert.AreEqual(TableReadType.NOLOCK, info.TableDescriptor.TableReadType);
        }

        [TestMethod]
        public void TableName_Equal_To_me_READPAST()
        {
            string sql = @"select userId from me(READPAST)";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("me", info.TableDescriptor.TableName);
            Assert.AreEqual(TableReadType.READPAST, info.TableDescriptor.TableReadType);
        }

        [TestMethod]
        public void TableName_Equal_To_Me()
        {
            string sql = @"select userId from Me";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("Me", info.TableDescriptor.TableName);
        }

        [TestMethod]
        public void TableName_Equal_To_ME()
        {
            string sql = @"select userId from ME";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("ME", info.TableDescriptor.TableName);
        }

        [TestMethod]
        public void TableName_Equal_To_mE()
        {
            string sql = @"select userId from mE";
            var info = SQLParser.ParseSQL(sql);

            Assert.AreEqual("mE", info.TableDescriptor.TableName);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingTableNameException))]
        public void TableName_DoesNot_Equal_To_ME4()
        {
            string sql = @"select userId from";
            var info = SQLParser.ParseSQL(sql);
        }
    }
}
