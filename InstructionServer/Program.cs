using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace InstructionServer
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
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          
            Application.Run(new EBMMain());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                LogHelper.WriteLog(typeof(Program), ex.StackTrace);
            }
            catch (Exception)
            {
                
                throw;
            }
         
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                LogHelper.WriteLog(typeof(Program), e.Exception.StackTrace);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
