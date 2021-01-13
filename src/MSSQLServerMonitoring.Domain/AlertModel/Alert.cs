using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Domain.AlertModel
{
    public class Alert : Entity, IAggregateRoot
    {
        public DateTime TimeStamp { get; set; }
        public AlertStatus? Status { get; set; }
        public string AttachActivityId { get; set; }
        public string SqlText { get; set; }
        public string Message { get; set; }
        public Alert(string sqlText, string message, string attachActivityId)
        {
            AttachActivityId = attachActivityId;
            SqlText = sqlText;
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
    }
}
