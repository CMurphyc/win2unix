using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace win2unix
{
    class Program
    {
        //public static string configDataPath = @"wildsII_proj2\Assets\Resources\ConfigData";
        public static void Main(string[] args)
        {
            string prefix = Environment.CurrentDirectory.Replace("\\", "//").Replace("//", "/").TrimEnd('/');
            string configDataPath = prefix + @"\..\..\wildsII_proj2\Assets\Resources\ConfigData";
            Console.WriteLine(prefix);
            DirectoryInfo fileInfos = new DirectoryInfo(configDataPath);
            foreach (FileInfo info in fileInfos.GetFiles("*.txt", SearchOption.AllDirectories))
            {

                Console.WriteLine(info.Name);
                Console.WriteLine(info.DirectoryName);
                try
                {
                    FileConverter.Dos2Unix(info.FullName);
                    FileConverter.RemoveBOM(info.FullName);
                    Console.WriteLine(string.Format("{0} 已转换为UNIX格式行尾符号", info.FullName));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("{0} 转换失败！", info.FullName));
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Solve(DirectoryInfo info)
        {

        }
    }
}
