namespace LibraryManager.Model
{
    public class Statistics
    {
        private Manager _manager = ManagerInstance.Instance;
        public int NewBooksMonth { get; set; }
        public int NewVisitorsMonth { get; set; }
        public int NewAuthorsMonth { get; set; }
        public int NewTransactionsMonth { get; set; }

        public int NewBooksAllTime { get; set; }
        public int NewVisitorsAllTime { get; set; }
        public int NewAuthorsAllTime { get; set; }
        public int NewTransactionsAllTime { get; set; }

        public Statistics()
        {
            GetMonthlyStat();
            GetAllTimeStat();
        }
        private void GetMonthlyStat()
        {
            DateTime current = DateTime.Now;

            NewVisitorsMonth = _manager.Visitors.Count(v => v.CreationDate.Month == current.Month && v.CreationDate.Year == current.Year);
            NewAuthorsMonth = _manager.Authors.Count(v => v.CreationDate.Month == current.Month && v.CreationDate.Year == current.Year);
            NewBooksMonth = _manager.Books.Count(v => v.CreationDate.Month == current.Month && v.CreationDate.Year == current.Year);
            NewTransactionsMonth = _manager.Transactions.Count(v => v.DateTaken.Month == current.Month && v.DateTaken.Year == current.Year);
        }

        private void GetAllTimeStat()
        {
            NewVisitorsAllTime = _manager.Visitors.Count;
            NewAuthorsAllTime = _manager.Authors.Count;
            NewBooksAllTime = _manager.Books.Count;
            NewTransactionsAllTime = _manager.Transactions.Count;
        }
    }
}
