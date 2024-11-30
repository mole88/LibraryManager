using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.Client.View.BooksViews
{
    public partial class FindBookDialogView : UserControl
    {
        public FindBookDialogView()
        {
            InitializeComponent();
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
