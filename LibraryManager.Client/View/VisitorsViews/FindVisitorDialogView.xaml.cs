using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.Client.View.VisitorsViews
{
    public partial class FindVisitorDialogView : UserControl
    {
        public FindVisitorDialogView()
        {
            InitializeComponent();
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
