using System;
using System.IO.Ports;

namespace Reader_Agrident
{
	class Program
	{
		static void Main(string[] args)
		{
			SerialPort _serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;

			_serialPort.Open();

			_serialPort.Write("[CSW]");
			
			System.Threading.Thread.Sleep(300);

			while (_serialPort.BytesToRead > 0) {
				string response = _serialPort.ReadLine();

				Console.WriteLine(response);
			}
			

			_serialPort.Close();

		}
	}
}
