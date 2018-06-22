using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace EBMTest
{
    static class Program
    {
        //public static bool IsCopyProcess { get; private set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string fileName = @"Log\EBM" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".log";
            if (!Directory.Exists(@"Log")) Directory.CreateDirectory(@"Log");
            TextWriterTraceListener ebmListener = new TextWriterTraceListener(fileName);
            ebmListener.Name = "ebmListener";
            ebmListener.IndentSize = 0;
            Trace.AutoFlush = true;
            Trace.IndentSize = 0;
            Trace.Listeners.Add(ebmListener);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var processes = Process.GetProcessesByName("EBMTest");
            //if (processes.Length <= 1)
            //{
            //    IsCopyProcess = false;
            //}
            //else if (processes.Length == 2)
            //{
            //    IsCopyProcess = true;
            //}
            //else
            //{
            //    MessageBox.Show("最多只可运行两次该软件", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            Application.Run(new EBMMain());
        }
    }
}
