using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McKaySQLParser.Core
{
    public enum TableReadType
    { 
        NONE,
        NOLOCK,
        READPAST
    }

    public struct TableDescriptor
    {
        public string TableName { get; set; }

        public TableReadType TableReadType { get; set; }
    }
}
