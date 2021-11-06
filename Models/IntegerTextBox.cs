using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TravelAcrossRussiaMVVM.Models
{
    public class IntegerTextBox : TextBox
    {
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = "0";
            }
            base.OnLostFocus(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _) || int.Parse(e.Text) < 0 || int.Parse(e.Text) > 5)
            {
                e.Handled = true;
            }
            base.OnPreviewTextInput(e);
        }
    }
}
