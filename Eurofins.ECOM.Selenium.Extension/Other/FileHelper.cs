using System;
using System.IO;

namespace Eurofins.ECOM.Selenium.Extension.Other
{
    public static class FileHelper
    {
        public static void CopyFile(string fileName, string sourcePath, string targetPath)
        {
            try
            {
                string sourceFile = Path.Combine(sourcePath, fileName);
                string destFile = Path.Combine(targetPath, fileName);

                // To copy a folder's contents to a new location:
                // Create a new target folder, if necessary.
                if (!System.IO.Directory.Exists(targetPath))
                {
                    System.IO.Directory.CreateDirectory(targetPath);
                }

                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch
            { throw; }
        }

        public static string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }
    }
}
