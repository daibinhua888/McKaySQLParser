using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McKaySQLParser.Core.SQLGenerator
{
    public interface IGenerator
    {
        string Generate(SelectStmtInfo stmtInfo);
    }
}
