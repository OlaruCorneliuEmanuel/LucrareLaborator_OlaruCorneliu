using System.Windows;

namespace InterfataWPF
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "admin" && txtPass.Password == "admin")
            {
                MainWindow main = new MainWindow();
                main.Show();

                this.Close();
            }
            else
            {
                tbEroare.Text = "Utilizator sau parolă incorecte!";
            }
        }
    }
}