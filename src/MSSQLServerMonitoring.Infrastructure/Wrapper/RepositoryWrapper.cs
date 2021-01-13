﻿using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Infrastructure.Data;
using MSSQLServerMonitoring.Infrastructure.Data.AlertModel;
using MSSQLServerMonitoring.Infrastructure.Data.QueryModel;

namespace MSSQLServerMonitoring.Infrastructure.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ExampleContext _ctx;
        private IQueryRepository _query;
        private IAlertRepository _alert;
        public RepositoryWrapper(ExampleContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryRepository Query
        {
            get
            {
                if (_query == null)
                {
                    _query = new QueryRepository(_ctx);
                }

                return _query;
            }
        }

        public IAlertRepository Alert
        {
            get
            {
                if (_alert == null)
                {
                    _alert = new AlertRepository(_ctx);
                }

                return _alert;
            }
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
