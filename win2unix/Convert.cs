using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace win2unix
{
    class Convert
    {

        static Convert() {}

        public static void Win2Unix(string path)
        {
            const byte CR = 0x0D; // \r
            const byte LF = 0x0A; // \n
            byte[] data = File.ReadAllBytes(path);

            using (FileStream fs = File.OpenWrite(path))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                int LFPos = 0;
                int index = 0;
                // 移除行内所有的CRLF中的CR
                do
                {
                    index = Array.IndexOf<byte>(data, CR, LFPos);
                    if ((index >= 0) && (data[index + 1] == LF))
                    {
                        bw.Write(data, LFPos, index - LFPos);
                        LFPos = index + 1;
                    }
                }
                while (index > 0);
                bw.Write(data, LFPos, data.Length - LFPos);
                fs.SetLength(fs.Position);
            }

            RemoveBOM(path);
        }

        /// <summary>
        /// 移除字节顺序标记
        /// </summary>
        /// <param name="path"></param>
        public static void RemoveBOM(string path)
        {
            string content = File.ReadAllText(path);
            byte[] bom = Encoding.UTF8.GetPreamble();
            string sBOM = Encoding.UTF8.GetString(bom);

            if (content.StartsWith(sBOM)) content.Remove(0, sBOM.Length);
            File.WriteAllText(path, content);
        }
    }
}
