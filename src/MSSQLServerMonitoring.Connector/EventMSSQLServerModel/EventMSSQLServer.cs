using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Connector.Model
{
    public class EventMSSQLServer
    {
        public DateTime TimeStamp { get; set; }
        public string AttachActivityId { get; set; }
        public string EventName { get; set; }
        public string PackageName { get; set; }
        public string ClientHn { get; set; }
        public string ClientAppName { get; set; }
        public string NtUserName { get; set; }
        public int DatabaseId { get; set; }
        public string DatabaseName { get; set; }
        public string Statement { get; set; }
        public decimal Duration { get; set; }
        public long CpuTime { get; set; }
        public long PhysicalReads { get; set; }
        public long LogicalReads { get; set; }
        public long Writes { get; set; }
        public long RowCount { get; set; }
        public string SqlText { get; set; }
    }
}
