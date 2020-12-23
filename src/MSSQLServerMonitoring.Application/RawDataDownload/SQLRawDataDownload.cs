using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public class SQLRawDataDownload
    {
        // логика приложения получить сырые данные по событиям и сохранить анамальные запросы
        ISQLServerServic _sQLServerServic;
        public SQLRawDataDownload(ISQLServerServic sQLServerServic)
        {
            _sQLServerServic = sQLServerServic;
        }
    }
}
