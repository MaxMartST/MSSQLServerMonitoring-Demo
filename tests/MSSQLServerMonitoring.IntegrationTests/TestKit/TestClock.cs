using System;
using MSSQLServerMonitoring.Infrastructure.Clock;

namespace MSSQLServerMonitoring.IntegrationTests.TestKit
{
    public class TestClock : IClock
    {
        public TestClock()
        {
            _dateTimeMock = DateTime.UtcNow;
            _firstDateTimeNow = _dateTimeMock;
        }

        private DateTime _dateTimeMock;
        private DateTime _firstDateTimeNow;

        public void SetDateTime( DateTime dateTime )
        {
            _dateTimeMock = dateTime;
            _firstDateTimeNow = DateTime.UtcNow;
        }

        public DateTime Now()
        {
            return _dateTimeMock.AddTicks( ( DateTime.UtcNow - _firstDateTimeNow ).Ticks );
        }

        public DateTime Today()
        {
            return Now().Date;
        }

        public void Delay( TimeSpan timeToDelay )
        {
            throw new NotImplementedException();
        }
    }
}
