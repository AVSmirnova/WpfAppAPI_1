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
using System.Windows.Shapes;
using WpfAppAPI_1.Infrastructure;
using WpfAppAPI_1.Model;

namespace WpfAppAPI_1.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        ApiClient apiClient;
        public AuthWindow()
        {
            InitializeComponent();
            apiClient = new ApiClient(App.httpClient);
        }

        private async void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            LoginUserRequestDto userRequestDto = new LoginUserRequestDto()
            {
                Login = login.Text,
                Password=password.Text
            };

            var result = await apiClient.UserLogin(userRequestDto);
            MessageBox.Show(result);


        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrWindow registrWindow = new RegistrWindow();
            registrWindow.Show();
        }
    }
}
