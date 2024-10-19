using LibraryManager.Client.ViewModel;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManager.Client.View
{
    /// <summary>
    /// Логика взаимодействия для BooksPageView.xaml
    /// </summary>
    public partial class BooksPageView : UserControl
    {
        public BooksPageView()
        {
            InitializeComponent();
        }
        private void DataGridRow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var row = sender as DataGridRow;
            if (row != null && row.DataContext is Book selectedBook)
            {
                // Вызов команды через ViewModel
                var viewModel = (BooksPageViewModel)this.DataContext;
                if (viewModel.RowSelectedCommand.CanExecute(selectedBook))
                {
                    viewModel.RowSelectedCommand.Execute(selectedBook);
                }
            }
        }
    }
}
