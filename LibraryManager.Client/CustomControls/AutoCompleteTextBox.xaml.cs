using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.Client.CustomControls
{
    public partial class AutoCompleteTextBox : UserControl
    {
        public AutoCompleteTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(ObservableCollection<string>), typeof(AutoCompleteTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register(nameof(SearchText), typeof(string), typeof(AutoCompleteTextBox), new PropertyMetadata(string.Empty, OnSearchTextChanged));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(string), typeof(AutoCompleteTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(nameof(DisplayMemberPath), typeof(string), typeof(AutoCompleteTextBox), new PropertyMetadata(null));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(AutoCompleteTextBox), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsPopupOpenProperty =
            DependencyProperty.Register(nameof(IsPopupOpen), typeof(bool), typeof(AutoCompleteTextBox), new PropertyMetadata(false));

        public bool IsPopupOpen
        {
            get => (bool)GetValue(IsPopupOpenProperty);
            set => SetValue(IsPopupOpenProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public ObservableCollection<string> ItemsSource
        {
            get => (ObservableCollection<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public string SelectedItem
        {
            get => (string)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        public ObservableCollection<string> FilteredItems { get; } = new();

        private static void OnSearchTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AutoCompleteTextBox control && e.NewValue is string newValue)
            {
                control.UpdateFilteredItems(newValue);
            }
        }

        private void UpdateFilteredItems(string query)
        {
            FilteredItems.Clear();
            if (!string.IsNullOrWhiteSpace(query) && ItemsSource != null)
            {
                foreach (var item in ItemsSource)
                {
                    var propertyValue = item;
                    if (!string.IsNullOrWhiteSpace(propertyValue) &&
                        propertyValue.Contains(query, StringComparison.OrdinalIgnoreCase) &&
                        !propertyValue.Equals(query, StringComparison.OrdinalIgnoreCase))
                    {
                        FilteredItems.Add(item);
                    }
                }
            }

            IsPopupOpen = FilteredItems.Any();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem != null)
            {

                if (SelectedItem != null)
                {
                    SearchText = SelectedItem;
                }

                IsPopupOpen = false;
            }
        }
    }
}
