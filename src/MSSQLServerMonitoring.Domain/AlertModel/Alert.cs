using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;

namespace MSSQLServerMonitoring.Domain.AlertModel
{
    public class Alert : Entity, IAggregateRoot
    {
        public DateTime TimeStamp { get; set; }
        public string AttachActivityId { get; set; }
        public string SqlText { get; set; }
        public decimal Duration { get; set; }
        public long LogicalReads { get; set; }
        public long Writes { get; set; }
        public string Message { get; set; }
        public Alert(string sqlText, string message, string attachActivityId, decimal duration, long logicalReads, long writes)
        {
            AttachActivityId = attachActivityId;
            SqlText = sqlText;
            Duration = duration;
            LogicalReads = logicalReads;
            Writes = writes;
            Message = message;
        }

        public DateTime RegDate
        {
            get
            {
                return TimeStamp;
            }

            set
            {
                TimeStamp = value;
            }
        }

        protected Alert()
        { 
        }
    }
}
