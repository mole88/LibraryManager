using LibraryManager.Client.Core;
using LibraryManager.Client.Reports;
using LibraryManager.Client.SupportClasses;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel
{
    public class StatisticsPageViewModel : ObservableObject
    {
        private Manager _manager;

        public StatisticsPageViewModel()
        {
            _manager = ManagerInstance.Instance;

            GenerateReportCommand = new RelayCommand((o) =>
            {
                var reportGenerator = new PdfReportGenerator();
                reportGenerator.GenerateReport();
            });
        }

        public RelayCommand GenerateReportCommand { get; set; }
    }
    

}
