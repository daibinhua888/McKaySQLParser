﻿using Antlr4.Runtime.Tree;
using McKaySQLParser.Grammars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McKaySQLParser.Core
{
    public static class SQLParser
    {
        public static SelectStmtInfo ParseSQL(string sql)
        {
            Antlr4.Runtime.AntlrInputStream input = new Antlr4.Runtime.AntlrInputStream(sql);
            SelectSQLLexer lexer = new SelectSQLLexer(input);

            Antlr4.Runtime.UnbufferedTokenStream tokens = new Antlr4.Runtime.UnbufferedTokenStream(lexer);
            SelectSQLParser parser = new SelectSQLParser(tokens);

            var tree = parser.compileUnit();

            ParseTreeWalker walker = new ParseTreeWalker();

            SelectSQLTreeListener lsn = new SelectSQLTreeListener();

            walker.Walk(lsn, tree);

            return lsn.SelectStmt;
        }
    }
}
