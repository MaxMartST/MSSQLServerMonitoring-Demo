using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;

namespace MSSQLServerMonitoring.Domain.AlertModel
{
    public class Alert : Entity, IAggregateRoot
    {
        public DateTime TimeStamp { get; set; }
        public string QueryId { get; set; }
        public string EventName { get; set; }
        public string SqlText { get; set; }
        public string Statement { get; set; }
        public decimal Duration { get; set; }
        public long LogicalReads { get; set; }
        public long Writes { get; set; }
        public string Message { get; set; }

        public DateTime RegDate
        {
            get{ return TimeStamp; }
            set{ TimeStamp = value; }
        }
    }
}
