using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
                Data.num = 1;
                AddEdit f = new AddEdit();
                f.Owner = this;
                f.ShowDialog();
                LoadBDInDataGrid();
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (dgProduct.SelectedItem!=null)
            {
                Data.product = (Product)dgProduct.SelectedItem;
                Data.num = 0;
                AddEdit f = new AddEdit();
                f.Owner = this;
                f.ShowDialog();
            }
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
            if (DateTime.TryParse(tbFind.Text, out DateTime searchDate))
            {
                DateOnly searchDateOnly = DateOnly.FromDateTime(searchDate);

                
                var filtred = listItem.Where(p => p.DateStart == searchDateOnly);

                if (filtred.Count() > 0)
                {
                    var item = filtred.First();
                    dgProduct.SelectedItem = item;
                    dgProduct.ScrollIntoView(item);
                    dgProduct.Focus();
                }
            }
        }

        private void btnFiltred_Click(object sender, RoutedEventArgs e)
        {
            if (tbFiltr.Text.IsNullOrEmpty() == false)
            {
                using (OptStoreContext _db = new OptStoreContext())
                {
                    var filtred = _db.Products.Where(p =>p.NameTovar.Contains(tbFiltr.Text));
                    dgProduct.ItemsSource = filtred.ToList();
                }
            }
            else
            {
                LoadBDInDataGrid();
            }
        }

        private void btnDateStartWith_Click(object sender, RoutedEventArgs e)
        {
            using (OptStoreContext _db = new OptStoreContext())
            {
                    var dateStart = tbStartDate.Text;
                    var start = _db.Products.FromSql($"SpisokDate {dateStart}");
                dgProduct.ItemsSource = start.ToList();
            }
        
        }

        private void btnUnitCountStart_Click(object sender, RoutedEventArgs e)
        {
            using (OptStoreContext _db = new OptStoreContext())
            {
                var unit = tbUnitCol.Text;
                var count = _db.Products.FromSql($"SpisokSizePart {unit}");
                dgProduct.ItemsSource = count.ToList();

            }
        }

        private void btnFirmChange_Click(object sender, RoutedEventArgs e)
        {
            using (OptStoreContext _db = new OptStoreContext())
            {
                var row = (Product)dgProduct.SelectedItem;
                if (row != null)
                {
                    var id = new SqlParameter("@Id", row.Id);
                    var firm = new SqlParameter("@Company", tbNewFirm.Text);
                    var text = new SqlParameter();
                    text.ParameterName = "@text";
                    text.SqlDbType = System.Data.SqlDbType.NVarChar;
                    text.Size = 200;
                    text.Direction = System.Data.ParameterDirection.Output;
                    _db.Database.ExecuteSqlRaw($"ChangeFirm @Id, @Company, @text output", id, firm, text);
                    LoadBDInDataGrid();

                }
            }
        }

        private void btnDelId_Click(object sender, RoutedEventArgs e)
        {
            //var row = (Product)dgProduct.SelectedItem;
            //if (row != null)
            //{
            //    using (OptStoreContext _db = new OptStoreContext())
            //    {
            //        SqlParameter idDel = new SqlParameter("@Id", row.Id);
            //        var Del = _db.Database.ExecuteSqlRaw($"Delete from Product where Id={idDel}");
            //    }
            //}
        }
    }
}