using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System.Collections.Generic;
using System.Linq;

namespace MSSQLServerMonitoring.WebApp.Pages.Alerts
{
    public class DetailsModel : PageModel
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public string textQuery { get; private set; }
        public string individualId { get; private set; }

        public DetailsModel(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IActionResult OnGet(int id)
        {
            //List<Alert> alerts = _repositoryWrapper.Alert.GetOnCondition(a => a.Id == id).Result;
            Alert alert = _repositoryWrapper.Alert.GetOnCondition(a => a.Id == id).Result.FirstOrDefault();

            if (alert == null)
            {
                return RedirectToPage("/NotFound");
            }

            individualId = alert.QueryId;

            if (alert.EventName == "rpc_completed")
            {
                textQuery = alert.Statement;
            }

            if (alert.EventName == "sql_statement_completed")
            {
                textQuery = alert.SqlText;
            }

            return Page();
        }
    }
}
