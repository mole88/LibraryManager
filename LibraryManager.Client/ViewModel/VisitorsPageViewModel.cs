using LibraryManager.Model;
using System.Collections.ObjectModel;

namespace LibraryManager.Client.ViewModel
{
    public class VisitorsPageViewModel
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<Visitor> Visitors => _manager.Visitors;
        public VisitorsPageViewModel()
        {
            _manager = ManagerInstance.Instance;
        }
    }
}
