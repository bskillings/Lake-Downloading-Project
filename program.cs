using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace LakeDownloader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Downloader downloader = new Downloader(args[0]);
            downloader.DownloadAllFiles();
            downloader.PrintDataStatements();
            Console.ReadKey();
        }
        
    }
}
