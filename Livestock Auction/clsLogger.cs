using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Livestock_Auction
{
    static class clsLogger
    {
        static public void WriteLog(string Message)
        {
            FileInfo fiLogFile = new FileInfo(string.Format("{0}\\AuctionLog_{1}.log", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.Threading.Thread.CurrentThread.Name));
            StreamWriter swConfig = new StreamWriter(fiLogFile.Open(FileMode.Append, FileAccess.Write));

            swConfig.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "------");
            swConfig.WriteLine(Message);
            swConfig.WriteLine("");

            swConfig.Close();
        }
    }
}
