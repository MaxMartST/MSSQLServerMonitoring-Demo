using Microsoft.AspNetCore.Mvc.RazorPages;
using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
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
        public void OnGet(int id)
        {
            Alert = _repositoryWrapper.Alert.GetOnCondition(a => a.Id == id).Result.First();
        }
    }
}
