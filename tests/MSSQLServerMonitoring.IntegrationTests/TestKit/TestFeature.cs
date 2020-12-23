using System;
using NUnit.Framework;

namespace MSSQLServerMonitoring.IntegrationTests.TestKit
{
    [Category( "SkipWhenLiveUnitTesting" )]
    public abstract class TestFeature : IDisposable
    {
        protected ITestRunner Runner;
        protected ITestDriver Driver;

        [SetUp]
        public void Init()
        {
            SetUp();
        }

        [TearDown]
        public void Dispose()
        {
            TearDown();
            Driver.Dispose();
        }

        protected abstract void SetUp();

        protected abstract void TearDown();
    }
}
