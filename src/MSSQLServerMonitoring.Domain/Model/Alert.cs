using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Domain.Model
{
    public class Alert : Entity, IAggregateRoot
    {
        public DateTime TimeStamp { get; set; }
        public string AttachActivityId { get; set; }
        public string SqlText { get; set; }
        public string Message { get; set; }
    }
}
