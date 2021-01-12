using Hangfire;
using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.HangFire.HangFire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.AdminApi.Controllers
{
    [Route("/Api/HangFire")]
    public class HangFireController : Controller
    {
        private IHangFireService _hangFireService;
        public HangFireController(IHangFireService hangFireService)
        {
            this._hangFireService = hangFireService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //RecurringJob.AddOrUpdate("demo-jod", () => this._hangFireService.RunDemoTask(), Cron.Minutely);

            return Ok();
        }
    }
}
