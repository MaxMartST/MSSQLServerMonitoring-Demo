using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System.Collections.Generic;
using System.Linq;

namespace MSSQLServerMonitoring.WebApp.Pages.Alerts
{
    public class DetailsModel : PageModel
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public Alert Alert { get; private set; }

        public DetailsModel(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public IActionResult OnGet(int id)
        {
            List<Alert> alerts = _repositoryWrapper.Alert.GetOnCondition(a => a.Id == id).Result;

            if (alerts.Count == 0)
            {
                return RedirectToPage("/NotFound");
            }
            else
            {
                Alert = alerts.First();
            }

            return Page();
        }
    }
}
