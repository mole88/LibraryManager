using LibraryManager.Model;
using System.Collections.ObjectModel;
using LibraryManager.Client.Core;

namespace LibraryManager.Client.ViewModel
{
    public class VisitorsPageViewModel : ObservsbleObject
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
