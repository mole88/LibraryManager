using LibraryManager.Client.Core;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel
{
    public class StatisticsPageViewModel : ObservableObject
    {
        private Manager _manager;

        public StatisticsPageViewModel()
        {
            _manager = ManagerInstance.Instance;
        }
    }
    

}
