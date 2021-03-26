using System.IO;

namespace FromFarmer.Utilities.Operations
{
    public class FileOperation
    {
        public static void CreateFolder(string path)
        {
            bool exists = Directory.Exists(path);
            if (!exists)
                Directory.CreateDirectory(path);
        }

        public static bool CreatePng(byte[] bytes, string path, ref string filename)
        {
            try
            {
                filename += ".png";
                var fullpath = path + filename;
                if (bytes.Length > 0)
                {
                    using (var fileStream = new FileStream(fullpath, FileMode.Create))
                    {
                        fileStream.Write(bytes, 0, bytes.Length);
                        fileStream.Flush();
                    }
                }
            }
            catch
            {
                filename = "";
                return false;
            }
            return true;
        }

        public static bool Delete(string path, string filename)
        {
            try
            {
                var fullpath = path + filename;
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }

    }
}
