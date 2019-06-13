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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dropbox_ControlWork
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SignInButtonClick(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            using (var context = new DataContext())
            {
                var user = context
                    .Users
                    .SingleOrDefault(searchingUser => searchingUser.Login == login);

                if (user == null || !EncryptionService.VerifyPassword(password, user.Password))
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
                else
                {
                    DropboxWindow dropbox = new DropboxWindow();
                    dropbox.Show();

                    Close();
                }
            }
        }

        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();

            Close();
        }

        private void WindowClosing(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DropboxClick(object sender, RoutedEventArgs e)
        {

            DropboxWindow dropboxWindow = new DropboxWindow();
            dropboxWindow.Show();

            Close();
        }
    }
}
