using Microsoft.IdentityModel.Tokens;
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Data.product = null;
            AddEdit f = new AddEdit();
            f.Owner = this;
            f.ShowDialog();
            LoadBDInDataGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduct.SelectedItem != null)
            {
                Data.product = (Product)dgProduct.SelectedItem;
                AddEdit f = new AddEdit();
                f.Owner = this;
                f.ShowDialog();
                LoadBDInDataGrid();
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Product row = (Product)dgProduct.SelectedItem;

                    if (row != null)
                        using (OptStoreContext _db = new OptStoreContext())
                        {
                            _db.Products.Remove(row);
                            _db.SaveChanges();
                        }
                    LoadBDInDataGrid();
                }
                catch
                {
                    MessageBox.Show("Ошибка удаления!");
                }
            }
            else dgProduct.Focus();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Authorization f = new Authorization();
            f.ShowDialog();

            if (Data.Login == false) Close();
            if (Data.Right == "Администратор");
            else
            {
                btnAdd.IsEnabled = false;
                btnDel.IsEnabled = false;
                btnEdit.IsEnabled = false;
                MainWin.Title = MainWin.Title + " " + Data.UserSurName + " " + Data.UserName + "( " + Data.Right + " )";
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            List<Product> listItem = (List<Product>)dgProduct.ItemsSource;
            var filtred = listItem.Where(p=>p.NameTovar.Contains(tbFind.Text));
            if(filtred.Count()>0)
            {
                var item = filtred.First();
                dgProduct.SelectedItem = item;  
                dgProduct.ScrollIntoView(item);
                dgProduct.Focus();
            }
        }

        private void btnFiltred_Click(object sender, RoutedEventArgs e)
        {
            if (tbFiltr.Text.IsNullOrEmpty() == false)
            {
                using (OptStoreContext _db = new OptStoreContext())
                {
                    //var filtred = _db.Products.Where(p => p.DateStart == tbFiltr.Text);
                }
            }
        }
    }
}