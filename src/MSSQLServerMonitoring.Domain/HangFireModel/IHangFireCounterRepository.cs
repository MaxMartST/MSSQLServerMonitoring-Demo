using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Domain.HangFireModel
{
    public interface IHangFireCounterRepository
    {
        Task<HangFireCounter> GetHangFireCounter();
        Task UpdateHangFireCounter(HangFireCounter hangFireCounter);
    }
}
