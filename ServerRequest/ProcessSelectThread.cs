using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleSQL.Domain;
using System.Data;

namespace SimpleSQL.ServerRequest
{
    public class ProcessSelectThread
    {
        public ProcessRequest ProcessRequest { get; set; }
        public Select SelectToProcess { get; set; }
        public List<DataTable> ResultStore { get; set; }

        public ProcessSelectThread(Select pSelectToProcess, List<DataTable> pResultStore, ProcessRequest pProcessRequest)
        {
            this.SelectToProcess = pSelectToProcess;
            this.ResultStore = pResultStore;
            this.ProcessRequest = pProcessRequest;
        }
    }
}