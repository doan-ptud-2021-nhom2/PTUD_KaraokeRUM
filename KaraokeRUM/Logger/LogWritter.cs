using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace KaraokeRUM.Logger
{
    class LogWritter
    {
        public static string _path = @"..\..\Logger";
        public static string LogPath = Path.Combine(_path, "ActionLogs.txt");
        public static string Admin = string.Empty;
        public static void Write(string message)
        {
            if (!File.Exists(Logger.LogWritter.LogPath))
                File.Create(Logger.LogWritter.LogPath).Close();

            string logContent = string.Format("[{0}]({1}){2}\n", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),Admin, message);
            File.AppendAllText(Logger.LogWritter.LogPath, logContent);
        }
    }
}
