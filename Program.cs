using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickBackUp
{
    class Program
    {
        static List<string> filePath = new List<string>();

        static void Main(string[] args)
        {
            GetAllTargetFilePath();
            CopyTargetFiles();
            Console.WriteLine("Complete!>>Press any key to exit!");
            Console.ReadKey();
        }

       static void GetAllTargetFilePath()    //获取目标文件路径
        {
            string rootPath = Directory.GetCurrentDirectory();
            SearchTargetFile(rootPath);
            string[] keyWord = { ".PRJ", ".$PJ", ".XLSX", ".XLS", ".DOC", ".DOCX", ".DLL", ".PJ", ".XML", ".PDF", ".XLSM", ".TXT", ".CSV", ".DWG", ".MSG", @"\POST\" };
            
            for(int i = 0; i< filePath.Count; i++)
            {
                int flag = 0;
                foreach(string s in keyWord)
                {
                    if (filePath[i].Contains(s) || filePath[i].Contains(s.ToLower()))
                        flag++;
                }
                if(flag == 0 || File.Exists(Path.GetDirectoryName(filePath[i]) + @"\dtbladed.in") && filePath[i].Contains("job_"))
                {
                    filePath.RemoveAt(i);
                    i--;
                }
            }
        }

        static void CopyTargetFiles() //复制目标文件
        {
            string backUpPath = Directory.GetCurrentDirectory() + @"\BackUp";
            if (!Directory.Exists(backUpPath))
            {
                Directory.CreateDirectory(backUpPath);
            }
            foreach(string s in filePath)
            {
                string tempFile = s.Replace(Directory.GetCurrentDirectory(), backUpPath);
                string tempDir = Path.GetDirectoryName(tempFile);
                if (!Directory.Exists(tempDir))
                {
                    Directory.CreateDirectory(tempDir);
                }
                if (File.Exists(tempFile))
                {
                    Console.WriteLine("Noting..." + tempFile + "已经存在~!");
                    continue;
                }
                File.Copy(s, tempFile, true);
                Console.WriteLine("BackUp...ING To.." + tempFile);
            }
        }

       static  void SearchTargetFile(string path)  //从当前目录开始搜索
        {          
            string[] filePath0 = Directory.GetFiles(path);
            foreach(string s in filePath0)
            {
                filePath.Add(s);
                Console.WriteLine("Searching.." + s);
            }
            string[] dirPath = Directory.GetDirectories(path);
            foreach(string s in dirPath)
            {
                SearchTargetFile(s);
            }
        }
    }
}
