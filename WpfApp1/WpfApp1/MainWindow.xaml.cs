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
using System.Windows.Forms;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timer1;
        public MainWindow()
        {
            InitializeComponent();
            inicializarTimer();
        }

        public void inicializarTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 7000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtReceptor.Text))
            {
                ObtenerMensajes();
            }
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            WSChat.ChatWS ws = new WSChat.ChatWS();
            WSChat.MensajeDTO mensaje = new WSChat.MensajeDTO();

            mensaje.Emisor = txtEmisor.Text;
            mensaje.Receptor = txtReceptor.Text;
            mensaje.Mensaje = txtMensaje.Text;

            ws.GuardaMensaje(mensaje);
            System.Windows.Forms.MessageBox.Show("Mensaje enviado!!!");
            ObtenerMensajes();
        }

        public void ObtenerMensajes()
        {
            txtLista.Clear();
            WSChat.ChatWS ws = new WSChat.ChatWS();
            var lista = ws.ObtenerMensajes(txtReceptor.Text, txtEmisor.Text);
            foreach (var item in lista)
            {
                txtLista.AppendText(item.Emisor + ": " + item.Mensaje + " ||\n" 
/*                                    + item.Receptor + ": " + item.Mensaje + " ||\n"*/);
            }
        }
    }
}
