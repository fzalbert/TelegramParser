using Client.ViewModel;
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

namespace Client.VIew
{
    /// <summary>
    /// Interaction logic for AuthorizationControl.xaml
    /// </summary>
    public partial class AuthorizationControl : UserControl
    {
        public AuthorizationControl()
        {
            InitializeComponent();

            var dataContext = this.DataContext as AuthViewModel;

            dataContext.AuthIsStarted += ShowPhonePannel;
            dataContext.CodeIsSended += ShowCodePannel;

        }

        private void ReturnPhonePannel_Click(object sender, RoutedEventArgs e)
            => ShowPhonePannel();

        private void ShowPhonePannel()
        {
            StartBtn.Visibility = Visibility.Collapsed;
            CodeEnteringPanel.Visibility = Visibility.Collapsed;

            PhoneNumberPanel.Visibility = Visibility.Visible;
        }

        private void ShowCodePannel()
        {
            PhoneNumberPanel.Visibility = Visibility.Collapsed;

            CodeEnteringPanel.Visibility = Visibility.Visible;
        }
    }
}
