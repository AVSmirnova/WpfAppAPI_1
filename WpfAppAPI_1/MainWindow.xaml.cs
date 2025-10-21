using System.Collections.ObjectModel;
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
using WpfAppAPI_1.Infrastructure;
using WpfAppAPI_1.Models;

namespace WpfAppAPI_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApiClient apiClient;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Product> Products { get; set; } 
        public MainWindow()
        {
            InitializeComponent();
            apiClient = new ApiClient(App.httpClient);
            DataContext=this;
        }

       
        public async  Task LoadAllAsync()
        {
            var categories = await apiClient.GetCategoriesAsync();
            Categories = new ObservableCollection<Category>(categories);
            var products = await apiClient.GetProductsAsync();
            Products = new ObservableCollection<Product>(products);
           
        }

      
        private async  void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAllAsync();
            cmbCategoriya.ItemsSource = Categories;
            cmbCategoriya.DisplayMemberPath = "Title";
            dgProducts.ItemsSource = Products;
        }

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            await LoadAllAsync();
            cmbCategoriya.ItemsSource = Categories;
            cmbCategoriya.DisplayMemberPath = "Title";
            dgProducts.ItemsSource = Products;
        }
    }
}