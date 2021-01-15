using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;

namespace MSSQLServerMonitoring.WebApp.Pages.Alerts
{
    public class AlertsModel : PageModel
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public AlertsModel(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        [BindProperty(SupportsGet = true), DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [BindProperty(SupportsGet = true), DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<Alert> Alerts { get; set; }
        public string IdSort { get; set; }
        public string DateSort { get; set; }

        public void OnGet()
        {
            Alerts = _repositoryWrapper.Alert.GetAll().Result;
        }

        public void OnGetGroupAlerts(string sortOrder)
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

        public void OnPost()
        {
            Alerts = _repositoryWrapper.Alert.GetOnCondition(a => (a.TimeStamp <= StartDate) && (a.TimeStamp >= EndDate)).Result;
        }
    }
}
