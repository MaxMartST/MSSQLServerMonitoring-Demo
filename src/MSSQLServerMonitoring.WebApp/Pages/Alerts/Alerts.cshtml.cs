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
        public string MessageSort { get; set; }
        public string StatusSort { get; set; }

        public void OnGet(string sortOrder)
        {
            List<Alert> alerts = Alerts;

            IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            MessageSort = sortOrder == "Message" ? "message_desc" : "Message";
            StatusSort = sortOrder == "Status" ? "status_desc" : "Status";

            IEnumerable<Alert> alertsGroup = from ag in alerts
                                             select ag;

            switch (sortOrder)
            {
                case "id_desc":
                    alertsGroup = alertsGroup.OrderByDescending(ag => ag.Id);
                    break;
                case "Date":
                    alertsGroup = alertsGroup.OrderBy(ag => ag.TimeStamp);
                    break;
                case "date_desc":
                    alertsGroup = alertsGroup.OrderByDescending(ag => ag.TimeStamp);
                    break;
                case "Massage":
                    alertsGroup = alertsGroup.OrderBy(ag => ag.Message);
                    break;
                case "message_desc":
                    alertsGroup = alertsGroup.OrderByDescending(ag => ag.Message);
                    break;
                case "Status":
                    alertsGroup = alertsGroup.OrderBy(ag => ag.Status);
                    break;
                case "status_desc":
                    alertsGroup = alertsGroup.OrderByDescending(ag => ag.Status);
                    break;
                default:
                    alertsGroup = alertsGroup.OrderBy(ag => ag.Id);
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
