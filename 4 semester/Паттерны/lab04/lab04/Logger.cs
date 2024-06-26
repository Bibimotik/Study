using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab04
{
    public partial class Logger : ILogger
    {
        private static Logger logger;
        private string FileName;
        private int ID;
        private List<string> Namespaces;

        private Logger()
        {
            FileName = $"../../../LOG_{DateTime.Now.ToString("yyyyMMdd-hhmmss")}.txt";
            ID = 0;
            Namespaces = new List<string>();
        }

        public static ILogger Create()
        {
            if (logger is not null)
            {
                return logger;
            }

            logger = new Logger();

            return logger;
        }

        public void Start(string title)
        {
            ID++;

            Namespaces.Add(title);

            WriteToLogFile("STRT", "");
        }

        public void Stop()
        {
            ID++;

            string removedNamespace = string.Empty;

            if (Namespaces.Count > 0)
            {
                removedNamespace = Namespaces.Last();

                Namespaces.RemoveAt(Namespaces.Count - 1);

                WriteToLogFile("STOP", "");

                return;
            }

            WriteToLogFile("STOP", $"No namespaces to remove");
        }

        public void Log(string message)
        {
            ID++;

            WriteToLogFile("INFO", message);
        }

        private void WriteToLogFile(string logTypeName, string message)
        {
            string logID = ID.ToString("D6");
            string logNamespace = string.Empty;
            foreach (var namespc in Namespaces)
            {
                logNamespace += $"{namespc}:";
            }

            string log = $"{logID} - {DateTime.Now} - {logTypeName} {logNamespace}   {message}";

            if (File.Exists(FileName))
            {
                using (StreamWriter sw = File.AppendText(FileName))
                {
                    sw.WriteLine(log);
                }

                return;
            }

            using (StreamWriter sw = File.CreateText(FileName))
            {
                sw.WriteLine($"{logID} - {DateTime.Now} - INIT");
            }

            ID++;

            WriteToLogFile(logTypeName, message);
        }
    }
}