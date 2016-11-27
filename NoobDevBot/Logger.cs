using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobDevBot
{
    public static class Logger
    {
        private static StreamWriter logfile;
       
        static Logger()
        {
            logfile = new StreamWriter(File.OpenWrite("log.txt"));
            logfile.AutoFlush = true;
            logfile.BaseStream.Position = logfile.BaseStream.Length;

        }

        public static void StartLog()
        {
            appendLine("started");
        }

        public static void Log(string text, byte warningLevel = 0) { return; }//appendLine(text, warningLevel);

        public static void EndLog()
        {
            appendLine("end");
            logfile.Close();
            
        }

        private static void appendLine(string text, byte warningLevel = 0) => logfile.WriteLine($"[{DateTime.Now}]({warningLevel}) {text}");
        
    }
}
