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
using System.Data.OleDb;
using System.Data;

namespace MuMaps
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyTravels mt;
        private Registration r;
        private BitmapImage imagCheck = new BitmapImage(new Uri("/src/correct.png", UriKind.Relative));
        private BitmapImage imagCross = new BitmapImage(new Uri("/src/wrong.png", UriKind.Relative));
        private String usuario = "admin";
        private String password = "root";


        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            passContrasena.IsEnabled = false;
            txtUsuario.Background = Brushes.White;
            if (e.Key == Key.Enter)
            {
                if (ComprobarEntrada(txtUsuario.Text, usuario, txtUsuario, checkUser))
                {
                    passContrasena.IsEnabled = true;
                    passContrasena.Focus();
                }

            }

        }

        private Boolean ComprobarEntrada(string valorIntroducido, string valorValido,
Control componenteEntrada, Image imagenFeedBack)
        {
            Boolean valido = false;
            componenteEntrada.BorderThickness = new Thickness(2);
            if (valorIntroducido.Equals(valorValido))
            {
                // borde y background en verde
                componenteEntrada.BorderBrush = Brushes.Green;
                componenteEntrada.Background = Brushes.LightGreen;
                // imagen al lado de la entrada de usuario --> check
                imagenFeedBack.Source = imagCheck;
                imagenFeedBack.ToolTip = "Credencial Correcta";
                valido = true;
            }
            else
            {
                // marcamos borde en rojo
                componenteEntrada.BorderBrush = Brushes.Red;
                componenteEntrada.Background = Brushes.IndianRed;
                // imagen al lado de la entrada de usuario --> cross
                imagenFeedBack.Source = imagCross;
                imagenFeedBack.ToolTip = "Credencial No encontrada";
                valido = false;
            }
            return valido;
        }

        private void passContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            btnLogin.IsEnabled = false;
            passContrasena.Background = Brushes.White;

            {
                if (e.Key == Key.Enter)
                {
                    if (ComprobarEntrada(passContrasena.Password, password, passContrasena, checkPassword))
                    {
                        btnLogin.IsEnabled = true;
                    }
                }

            }
        }
      private void btnRecuperar_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarEntrada(txtUsuario.Text, usuario, txtUsuario, checkUser))
            {
                MessageBox.Show("La contraseña es raíz en inglés");
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            mt = new MyTravels(txtUsuario.Text);
            mt.Show();
            this.Close();
            
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            r = new Registration();
            r.Show();
            this.Close();
        }
    }
}
