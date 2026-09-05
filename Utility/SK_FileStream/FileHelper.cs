using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SK_FileStream
{    
    public class FileHelper
    {        
        public static byte[] FileReadAll(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static IEnumerable<ArraySegment<byte>> FileRead(string filePath,int bufferSize = 4 * 1024 * 1024)
        {
            byte[] buffer = new byte[bufferSize];

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
            {
                int bytesRead;
                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // 使用 ArraySegment 包装实际有效长度，避免切片复制
                    yield return new ArraySegment<byte>(buffer, 0, bytesRead);
                }
            }
        }

        public static string FileReadAllText(string filePath) 
        {
            return File.ReadAllText(filePath);
        }

        public static void FileReadLineText(string filePath,Action<string> fileTextHandler)
        {
            string line;
            using (var reader = new StreamReader(filePath))
            {                
                while ((line = reader.ReadLine()) != null)
                {
                    // 处理当前行
                    fileTextHandler(line);
                }
            }                
        }
        

        public static void FileWriteAll(string outputPath, byte[] fileBytes) 
        {
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllBytes(outputPath, fileBytes);
        }





    }
}
