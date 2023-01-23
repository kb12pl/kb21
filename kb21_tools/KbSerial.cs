using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using static kb21_tools.KbLog;

namespace kb21_tools
{    
    public class Serial
    {
        
        public Serial()
        {            
        }
        public string Read()
        {
            //37 00 35 DA 59
            //24  24  16  01  37  00  35  DA  59  00  00  00  96
            SerialPort sp =new SerialPort("com5",9600,Parity.None, 8, StopBits.One);            

                

            //sp.ReadTimeout = 5000;
            //sp.WriteTimeout = 5000;
            sp.Open();
            //sp.DiscardOutBuffer();
            //sp.DiscardInBuffer();
            //sp.DataReceived += OnScan;


            byte[] c = new byte[15];
            while (sp.BytesToRead < 13)
                Thread.Sleep(5000);

            { 
                
                    

                var x = sp.BytesToRead;
                
                var a = sp.Read(c, 0, 15);
                ok(a);
                ok(BitConverter.ToString(c));
                sp.Close();
                sp.Close();
            }
            return "";

        }
        void OnScan(object sender, SerialDataReceivedEventArgs args)
        {
            SerialPort port = sender as SerialPort;

            string line = port.ReadExisting();
            ok(line);
            // etc
        }
    }
}
