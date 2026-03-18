using PRACT1718.ModelsBD;
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

namespace PRACT1718
{
    /// <summary>
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Window
    {
        public AddEdit()
        {
            InitializeComponent();
        }
        OptStoreContext _db = new OptStoreContext();
        Product _product;

        private void EditAddWin_Loaded(object sender, RoutedEventArgs e)
        {
            if (Data.product == null)
            {
                EditAddWin.Title = "Добавление записи";
                btnAddEdit.Content = "Добавить";
                _product = new Product();
            }
            else
            {
                EditAddWin.Title = "Редактирование записи";
                btnAddEdit.Content = "Сохранить";
                _product = _db.Products.Find(Data.product.Id);
            }
        }

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (tbNameTov.Text.Length ==0) errors.AppendLine("Укажите название товара");
            if (tbPriceEd.Text.Length == 0) errors.AppendLine("Укажите цену за единицу"); //доработать что нельзя меньше 0
            // придумать как сделать что нельзя вводить дату появления на складе больше текущей и даты продажи и наоборот
            if(tbNumberPart.Text.Length == 0) errors.AppendLine("Укажите номер партии");
            if(tbSizePart.Text.Length == 0) errors.AppendLine("Укажите размер партии");
            // Придумать для даты продажи и цены продажи то же самое

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (Data.product == null)
                {
                    _db.Products.Add(_product);
                    _db.SaveChanges();
                }
                else
                {
                    _db.SaveChanges();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                _db.Products.Remove(_product);
                MessageBox.Show(ex.Message.ToString());
            } 
        }
    }
}
