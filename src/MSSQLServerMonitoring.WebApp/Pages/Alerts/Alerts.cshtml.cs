using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;

namespace MSSQLServerMonitoring.WebApp.Pages.Alerts
{
    public class AlertsModel : PageModel
    {
        IRepositoryWrapper _repositoryWrapper;
        public AlertsModel(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        //public IEnumerable<Alert> Alerts { get; set; }
        public List<Alert> Alerts { get; set; }
        public void OnGet()
        {
            Alerts = _repositoryWrapper.Alert.GetAll().Result;
            //Alerts = (IEnumerable<Alert>)_repositoryWrapper.Alert.GetAll();
        }
    }
}
