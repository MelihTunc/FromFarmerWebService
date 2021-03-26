using System;
using System.IO;

namespace FromFarmer.Utilities.Helpers
{
    public class FileLogHelper
    {
        public void WriteLog(string title, string value)
        {
            string path = "./Log.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(DateTime.Now + " : " + title + " ==> " + value);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(DateTime.Now + " : " + title + " ==> " + value);
                }
            }
        }

        public void WriteLog(string title, Exception ex)
        {
            string path = "./Log.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(DateTime.Now + " : " + title + " ==> " + ex.ToString());
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(DateTime.Now + " : " + title + " ==> " + ex.ToString());
                }
            }
        }
    }
}
