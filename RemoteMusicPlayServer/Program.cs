using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class Program
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static void Hide()
        {
            IntPtr hwnd = GetConsoleWindow();
            ShowWindow(hwnd, SW_HIDE);
        }

        public static void Show()
        {
            IntPtr hwnd = GetConsoleWindow();
            ShowWindow(hwnd, SW_SHOW);
        }

        public static void Main(string[] args)
        {
            var path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Constant.ContentRootPath = Path.Combine(path,"rmp");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Constant.ContentRootPath)
                .UseKestrel()
                .UseUrls("http://*:9000/")
                .UseStartup<Startup>();
        }
    }
}
