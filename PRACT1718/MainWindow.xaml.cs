using PRACT1718.ModelsBD;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRACT1718
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBDInDataGrid();
        }

        void LoadBDInDataGrid()
        {
            using (OptStoreContext _db = new OptStoreContext())
            {
                int SelectedIndex = dgProduct.SelectedIndex;
                dgProduct.ItemsSource = _db.Products.ToList();
                if (SelectedIndex !=-1)
                {
                    if(SelectedIndex>=dgProduct.Items.Count) SelectedIndex = dgProduct.Items.Count - 1;
                    dgProduct.SelectedIndex = SelectedIndex;
                    dgProduct.ScrollIntoView(dgProduct.SelectedItem);
                }
                dgProduct.Focus();
            }
        }
    }
}