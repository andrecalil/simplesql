using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleSQL.Domain;

namespace SimpleSQL.ServerRequest
{
    public class SelectSplitThread
    {
        public Select SelectToSplit { get; set; }
        public string TargetTable { get; set; }
        public List<Select> SelectStore { get; set; }

        public SelectSplitThread(Select pSelectToSplit, string pTargetTable, List<Select> pSelectStore)
        {
            this.SelectToSplit = pSelectToSplit;
            this.TargetTable = pTargetTable;
            this.SelectStore = pSelectStore;
        }
    }
}