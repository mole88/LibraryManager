using LibraryManager.Client.Core;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Client.ViewModel.VisitorsViewModels
{
    internal class DeleteVisitorDialogViewModel
    {
        private Manager _manager;

        public DeleteVisitorDialogViewModel(Visitor visitor)
        {
            _manager = ManagerInstance.Instance;
            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (visitor != null)
                {
                    await _manager.RemoveVisitorAsync(visitor);
                    CancelCommand.Execute(o);
                }
            });
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
