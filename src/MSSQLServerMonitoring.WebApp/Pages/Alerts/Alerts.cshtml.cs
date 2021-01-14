using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public List<Alert> Alerts { get; set; }
        public string IdSort { get; set; }
        public string DateSort { get; set; }

        public void OnGet(string sortOrder)
        {
            List<Alert> alerts = _repositoryWrapper.Alert.GetAll().Result;

            DateSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            IEnumerable<Alert> alertsGroup = from ag in alerts
                                             select ag;

            switch (sortOrder)
            {
                case "date_desc":
                    alertsGroup = alertsGroup.OrderByDescending(ag => ag.TimeStamp);
                    break;
                default:
                    alertsGroup = alertsGroup.OrderBy(ag => ag.TimeStamp);
                    break;
            }

            Alerts = alertsGroup.ToList();
        }

        public void OnPost(DateTime startDate, DateTime endDate)
        {
            Alerts = _repositoryWrapper.Alert.GetOnCondition(a => (a.TimeStamp <= startDate) && (a.TimeStamp >= endDate)).Result;
        }
    }
}
