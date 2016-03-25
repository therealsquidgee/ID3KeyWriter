using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ID3KeyWriter
{
    public class Program
    {
        public string path;

        [STAThread]
        public static void Main(string[] args)
        {
            Console.BufferHeight = Int16.MaxValue - 1;

            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.ShowDialog();

            var keyWriter = new ID3KeyWriter(dlg.SelectedPath);
            keyWriter.writeTags();
            Console.ReadLine();
        }
    }
}
