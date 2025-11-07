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
using WpfAppAPI_1.Model;
using WpfAppAPI_1.Infrastructure;

namespace WpfAppAPI_1.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class RegistrWindow : Window
    {
        ApiClient apiClient;
        public RegistrWindow()
        {
            InitializeComponent();
            apiClient = new ApiClient(App.httpClient);
        }

        private async void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            User newUser = new User()
            {
                FirstName = firsName.Text,
                LastName = lastName.Text,
                DateOfBirth = dpDateOfBirth.DisplayDate,
                Login=login.Text,
                Password=password.Text,
                RoleId=3
            };

            string result = await UserRegistration(newUser);
            MessageBox.Show(result);

        }

       private async Task<string> UserRegistration(User newUser)
        {
            return await apiClient.RegistrUserAsync(newUser);  
        }
    }
}
