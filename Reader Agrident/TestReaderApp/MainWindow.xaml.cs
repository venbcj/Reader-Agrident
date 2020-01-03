using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;

namespace TestReaderApp
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

		private void Button_Click(object sender, RoutedEventArgs e)
		{

            string[] test = SerialPort.GetPortNames();

           

			SerialPort _serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			_serialPort.Open();

			//_serialPort.Write("[CSW]");
			//System.Threading.Thread.Sleep(300);

			// Clear the current buffer
			_serialPort.DiscardInBuffer();

			_serialPort.Write("[CSW|0]");

			System.Threading.Thread.Sleep(800);

            string[] kolommen = {"", "moeder", "diernrMdr", "datum", "verloop", "geborenen", "levend", "registratie", "lam_1", "diernrLam_1", "geslacht_1", "gewicht_1", "status_1", "lam_2", "diernrLam_2", "geslacht_2", "gewicht_2", "status_2", "lam_3", "diernrLam_3", "geslacht_3", "gewicht_3", "status_3", "lam_4", "diernrLam_4", "geslacht_4", "gewicht_4", "status_4", "lam_5", "diernrLam_5", "geslacht_5", "gewicht_5", "status_5", "lam_6", "diernrLam_6", "geslacht_6", "gewicht_6", "status_6", "lam_7", "diernrLam_7", "geslacht_7", "gewicht_7", "status_7",};
            var response = "";
            string[] regels;

            while (_serialPort.BytesToRead > 0)
			{
				response += _serialPort.ReadLine();
            }
            _serialPort.Close();

            CultureInfo provider = CultureInfo.InvariantCulture;
            regels = response.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<Geboorte> geboortes = new List<Geboorte>();
            foreach (string regel in regels) {

                string[] velden = regel.Split('|');

                if (velden.Length < 43)
                {
                    continue;
                }

                Geboorte geboorte = new Geboorte();
                geboorte.Levensnummer = velden[1];
                geboorte.diernrMdr = velden[2];
                geboorte.datum = DateTime.ParseExact(velden[3], "ddMMyyyy", provider);
                geboorte.verloop = velden[4];
                geboorte.geborenen = velden[5];
                geboorte.levend = velden[6];
                geboorte.registratie = velden[7];

                int numberOfLamFields = 5;
                for (int l = 0; l < 7; l++) {
                    Lam lam = new Lam();
                    int fieldIndex = 8 + l * numberOfLamFields;

                    Decimal gewicht;
                    Decimal.TryParse(velden[fieldIndex + 3], out gewicht);

                    lam.Levensnummer = velden[fieldIndex];
                    lam.diernrLam = velden[fieldIndex + 1];
                    lam.geslacht = velden[fieldIndex + 2];
                    lam.gewicht = gewicht;
                    lam.status = velden[fieldIndex + 4];

                    if (!lam.isEmpty())
                    {
                        geboorte.Lammeren.Add(lam);
                    }
                }

                geboortes.Add(geboorte);
            }

            var json = JsonConvert.SerializeObject(geboortes, Formatting.Indented);
            txtBox.Text = json;

            // Send to server
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://test.oervanovis.nl");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }
            string result;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            { 
                result = streamReader.ReadToEnd();
            }

            MessageBox.Show(result, "Error Title");

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }
    }
}
