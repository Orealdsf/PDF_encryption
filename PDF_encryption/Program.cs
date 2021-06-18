using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "";
            string src = $@"C:/PDF/{filename}.pdf"; //來源位置
            string dest = $@"C:/PDF/{filename}加密.pdf"; //目的位置
            string pwd = "";

            if (string.IsNullOrEmpty(src) || string.IsNullOrEmpty(dest))
                Console.WriteLine("原文件或目標文件不能爲空");

            PdfReader reader = new PdfReader(src); //讀取要加密的PDF文件
            int n = reader.NumberOfPages;     //獲取PDF文件的頁數
            Rectangle pagesize = reader.GetPageSize(1);
            Document document = new Document(pagesize);
            FileStream stream = new FileStream(dest, FileMode.Create);
            PdfCopy copy = new PdfCopy(document, stream);
            copy.SetEncryption(PdfWriter.STRENGTH128BITS, pwd, null, PdfWriter.AllowCopy | PdfWriter.AllowPrinting);
            //加密必須放在文檔打開之前
            document.Open();
            //寫文件
            for (int i = 1; i <= n; i++)
            {

                PdfImportedPage page = copy.GetImportedPage(reader, i);
                copy.AddPage(page);
            }
            document.Close();
            reader.Close();
            File.Delete(src); //加密完成後刪除原檔
            Console.WriteLine("文檔加密完成");
        }
    }
}
