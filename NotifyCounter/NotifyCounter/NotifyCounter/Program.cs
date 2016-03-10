using System;
using System.Threading;
using System.Windows.Forms;

namespace NotifyCounter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isFirstInstance;
            using (Mutex mtx = new Mutex(true, "CounterPrj", out isFirstInstance))
            {
                if(isFirstInstance)
                {
                    Application.Run(new Form1());
                }
            }
        }
    }
}
