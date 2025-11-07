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
using WpfAppAPI_1.Model.Dto;

namespace WpfAppAPI_1.Views
{
    /// <summary>
    /// Логика взаимодействия для DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : Window
    {
        ApiClient apiClient;
        List<DeliveryResponseDto> deliveryResponses;
        UserResponseDto _currientUser = null;
        public DeliveryWindow( UserResponseDto currientUser)
        {
            InitializeComponent();
            apiClient = new ApiClient(App.httpClient);
            _currientUser= currientUser;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
           try
           {
                Mouse.OverrideCursor = Cursors.Wait;
                if (_currientUser != null)
                {

                    // MessageBox.Show(_currientUser.Role.Id.ToString());
                    if (_currientUser.Role.Id == 1)
                    {

                        deliveryResponses = await apiClient.GetDeliveriesAsync();
                    }
                    else
                    {
                        deliveryResponses = await apiClient.GetUserDeliveriesAsync();
                    }


                }
                    
                } catch (Exception ex)
           {
                    MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                    deliveryResponses=new List<DeliveryResponseDto>();
            }
            finally
            {
                   Mouse.OverrideCursor = null;
                   dgDelivery.ItemsSource = deliveryResponses;
             
            }



        }

        private void dgDelivery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

        }
    }
}
