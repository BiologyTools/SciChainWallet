using Gtk;
namespace SciChain
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Init();
            MainForm main = MainForm.Create();
            main.Show();
            Application.Run();
        }
    }
}