using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace WpfAppAPI_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      // объявляем статическое поле, чтобы далее использовать
      // именно этот созданный экземпляр
        public static HttpClient httpClient;

        // перепишем метод базового класса добавив в него инициализацию HttpClient 
        // и пропишем базвый путь к API
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            httpClient = new System.Net.Http.HttpClient
            {
                BaseAddress = new Uri("https://localhost:7255")
            };

           
        }
    }

}
