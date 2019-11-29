using System;
using System.Collections.Generic;
using System.IO.Ports;
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
			SerialPort _serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			_serialPort.Open();

			_serialPort.Write("[CSW]");
			System.Threading.Thread.Sleep(300);

			// Clear the current buffer
			_serialPort.DiscardInBuffer();

			_serialPort.Write("[CSW]");

			System.Threading.Thread.Sleep(300);

			string response = "";
			while (_serialPort.BytesToRead > 0)
			{
				response += _serialPort.ReadLine();
			}

			txtBox.Text = response;

			_serialPort.Close();
		}
	}
}
