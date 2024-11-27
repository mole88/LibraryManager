using LibraryManager.Client.Core;
using LibraryManager.Client.Reports;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel
{
    public class StatisticsPageViewModel : ObservableObject
    {
        private Manager _manager;
        private Statistics _stat;

        public StatisticsPageViewModel()
        {
            _manager = ManagerInstance.Instance;

            GenerateReportCommand = new RelayCommand((o) =>
            {
                var reportGenerator = new PdfReportGenerator();
                reportGenerator.GenerateReport();
            });
        }
        public string NewBooksMonth {  get; set; }
        public string NewVisitorsMonth { get; set; }
        public string NewAuthorsMonth { get; set; }
        public string NewTransactionsMonth { get; set; }

        public string NewBooksAllTime { get; set; }
        public string NewVisitorsAllTime { get; set; }
        public string NewAuthorsAllTime { get; set; }
        public string NewTransactionsAllTime { get; set; }

        public RelayCommand GenerateReportCommand { get; set; }

        public void UpdateStatistics()
        {
            _stat = new Statistics();

            NewBooksMonth = _stat.NewBooksMonth.ToString();
            NewAuthorsMonth = _stat.NewAuthorsMonth.ToString();
            NewVisitorsMonth = _stat.NewVisitorsMonth.ToString();
            NewTransactionsMonth = _stat.NewTransactionsMonth.ToString();

            NewBooksAllTime = _stat.NewBooksAllTime.ToString();
            NewAuthorsAllTime = _stat.NewAuthorsAllTime.ToString();
            NewVisitorsAllTime = _stat.NewVisitorsAllTime.ToString();
            NewTransactionsAllTime = _stat.NewTransactionsAllTime.ToString();
        }
    }
}
