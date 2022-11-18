using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace PamyatajkaUI.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]{2}:[^0-9]{2}");
            e.Handled = regex.IsMatch(e.Text);
        }
        
    }
}