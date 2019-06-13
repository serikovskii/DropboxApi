using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Dropbox_ControlWork
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

        }

        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() => AddToDatabase());
        }

        public async void AddToDatabase()
        {
            string login="";
            string password = "";
            string confirmedPassword = "";
            Dispatcher.Invoke(() => login = loginTextBox.Text);
            Dispatcher.Invoke(() => password = passwordBox.Password);
            Dispatcher.Invoke(() => confirmedPassword = confirmedPasswordBox.Password);
            
            var loginLengt = 5;
            var passwordLengt = 8;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmedPassword))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }
            else if (login.Length < loginLengt)
            {
                MessageBox.Show("Логин должен содержать не меньше 6 символов!");
                return;
            }
            else if (password != confirmedPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            else if (password.Length < passwordLengt)
            {
                MessageBox.Show("Пароль должен содержать не меньше 8 символов!");
                return;
            }

            using (var context = new DataContext())
            {
                var user = context.Users.SingleOrDefault(searchingUser => searchingUser.Login == login);

                if (user != null)
                {
                    MessageBox.Show("Такой пользователь уже существует!");
                    return;
                }
                else
                {
                    context.Users.Add(new User
                    {
                        Login = login,
                        Password = EncryptionService.HashPassword(password)
                    });
                    await context.SaveChangesAsync();

                    MessageBox.Show("Успешно добавлено!");

                    MainWindow main = new MainWindow();
                    main.Show();

                    Close();

                }
            }
        }

        private void WindowClosing(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();

            Close();
        }
    }  
}
