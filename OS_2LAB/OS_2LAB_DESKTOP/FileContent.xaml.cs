using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OS_2LAB;

namespace OS_2LAB_DESKTOP
{
    public partial class FileContent : Window
    {
        public FileContent(FileView fileView)
        {
            InitializeComponent();

            using (FileStream fstream = File.OpenRead(fileView.Path))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);

                var bytes = array.Select(x => x.ToString("X2"));

                StringBuilder sb = new StringBuilder();

                long i = 1;
                foreach (var b in bytes)
                {
                    sb.Append(b);
                    sb.Append("  ");

                    if (i % 16 == 0)
                    {
                        sb.Append("\n");
                    }
                    
                    i++;
                }
                string outVAR = sb.ToString();
                
                TextBox_FileContent.Text = sb.ToString();
            }
        }
    }
}
