using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Domain.HangFireModel
{
    public class HangFireCounter
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public int Limit { get; set; }
    }
}
