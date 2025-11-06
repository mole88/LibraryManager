using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManager.Client.View.AuthorsViews
{
    public partial class FindAuthorDialogView : UserControl
    {
        public FindAuthorDialogView()
        {
            InitializeComponent();
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
