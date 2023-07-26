using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using FluentFTP.Helpers;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

using static kb21_tools.KbLog;
using static kb21_tools.KbConf;


namespace kb21_tools
{
    public class Ftp
    {
        FtpClient ?client;

        ~Ftp()
        {
            client?.Disconnect();            
        }


        bool Connect()
        {
            client = new(GetSecret("ftp.address"), GetSecret("ftp.user"), GetSecret("ftp.pass"));
            client.Connect();
            return false;
        }
        public bool Send(string name,string code)
        {
            Connect();
                
//            try
  //          {
                byte[] byteArray = Encoding.ASCII.GetBytes("123");
                MemoryStream stream = new MemoryStream(byteArray);

                client?.UploadStream(stream, "/index.htm");

                return false;
    //        }
      //      catch
        //    {
          //      return true;
            //}
        }
        
        public void Test()
        {
            Connect();

            foreach (FtpListItem item in client.GetListing("/"))
            {

                // if this is a file
                if (item.Type == FtpObjectType.File)
                {
                    ok(item.FullName);
                    // get the file size
                    long size = client.GetFileSize(item.FullName);

                    // calculate a hash for the file on the server side (default algorithm)


                    
                }

                // get modified date/time of the file or folder
                DateTime time = client.GetModifiedTime(item.FullName);

            }
        }
    }
}
