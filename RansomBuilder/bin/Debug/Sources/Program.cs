using System;
using System.Windows.Forms;

namespace crypt_engine
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
			Form1 frm1 = new Form1();
            Application.Run();
        }
    }
}
