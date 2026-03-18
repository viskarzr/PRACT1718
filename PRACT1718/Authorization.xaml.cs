using Microsoft.EntityFrameworkCore;
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
using System.Windows.Threading;

namespace PRACT1718
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }
        DispatcherTimer _timer;
        int _countLogin = 1;

        void GetCaptcha()
        {
            string masChar = "QWERTYUIOPLKJHGFDSAZXCVBNMmnbvcxzasdfghjk" + "lpoiuytrewq1234567890";
            string captcha = "";
            Random rnd = new Random();
            for (int i =1; i<=6; i++)
            {
                captcha += captcha + masChar[rnd.Next(0, masChar.Length)];
            }
            Grid.Visibility = Visibility.Visible;
            txtCaptcha.Text = captcha;
            tbCaptcha.Text = null;
            txtCaptcha.LayoutTransform = new RotateTransform(rnd.Next(-15,15));
            Line.X1 = 10;
            Line.Y1 = rnd.Next(10,40);
            Line.X2 = 270;
            Line.Y2 = rnd.Next(10, 40);
        }

        
        private void Window_Activated(object sender, EventArgs e)
        {
            tbUser.Focus();
            Data.Login = false;
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            stackPanel.IsEnabled = true;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            using (OptStoreContext _db = new OptStoreContext())
            {
                var user = _db.Users.Where(user => user.UserLogin == tbUser.Text && user.UserPassword == pbPassword.Password);

                if (user.Count() == 1 && txtCaptcha.Text == tbCaptcha.Text)
                {
                    Data.Login = true;
                    Data.UserSurName = user.First().UserSurName;
                    Data.UserName = user.First().UserName;
                    Data.UserPatronymic = user.First().UserPatronymic;

                    _db.Roles.Load();
                    Data.Right = user.First().UserRoleNavigation.RoleName;
                    Close();
                }
                else
                {
                    if (user.Count() == 1)
                    {
                        MessageBox.Show("Капча неверна! Попробуйте ввод.");
                    }
                    else
                    {
                        MessageBox.Show("Логин, пароль неверны! Повторите попытку ввода!");
                    }
                    GetCaptcha();

                    if (_countLogin >=2)
                    {
                        stackPanel.IsEnabled = false;
                        _timer.Start();
                    }
                    _countLogin++;
                    tbUser.Focus();
                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            Data.Login = true;
            Data.UserSurName = "Гость";
            Data.UserPatronymic = "";
            Data.UserName = "";
            Data.Right = "Клиент";
            Close();
        }
    }
}
