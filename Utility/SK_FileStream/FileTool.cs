using SaveLog;
using System;
using System.IO;

namespace SK_FileStream
{
    public class FileTool
    {

        /// <summary>
        /// 删除指定时间以前的文件
        /// </summary>
        /// <param name="directoryPath">要清理的文件夹路径</param>
        /// <param name="cutoffTime">截止时间（在此时间之前的会被删除）</param>
        /// <param name="recursive">是否递归清理子文件夹中的文件（默认 false）</param>
        public static void DeleteFiles(string directoryPath, DateTime cutoffTime, bool recursive = false)
        {
            if (!Directory.Exists(directoryPath))
            {
                StaticSink.SaveLog($"路径不存在: {directoryPath}");
                return;
            }

            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            // 设置查找选项：只查找当前目录还是包括所有子目录
            SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            try
            {
                FileInfo[] files = directory.GetFiles("*.*", searchOption);

                foreach (FileInfo file in files)
                {
                    // 使用 LastWriteTime（最后修改时间），也可以改用 CreationTime（创建时间）
                    if (file.LastWriteTime < cutoffTime)
                    {
                        try
                        {
                            // 尝试移除可能存在的只读属性
                            if (file.IsReadOnly)
                            {
                                file.IsReadOnly = false;
                            }

                            file.Delete();
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            FileLogHelper.Warn($"没有权限删除文件 {file.Name}: {ex.Message}");
                        }
                        catch (IOException ex)
                        {
                            FileLogHelper.Warn($"文件可能正在被使用或占用 {file.Name}: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            FileLogHelper.Warn($"删除文件 {file.Name} 时出错: {ex.Message}");
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                FileLogHelper.Warn($"无法读取目录 {directoryPath} 中的内容（权限不足）: {ex.Message}");
            }
            catch (Exception ex)
            {
                FileLogHelper.Warn($"处理过程遇到错误: {ex.Message}");
            }
        }



        public static void writeTxtToFile(string filePath,string textValue) 
        {
            try
            {
                // 确保目录存在
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 第二个参数 append: true 表示追加，false 表示覆盖
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine($"{textValue}");
                }

                //FileLogHelper.Warn("写入成功！");

            }
            catch (Exception ex)
            {
                FileLogHelper.Warn($"写入失败: {ex.Message}");
            }

        }
        


    }
}
