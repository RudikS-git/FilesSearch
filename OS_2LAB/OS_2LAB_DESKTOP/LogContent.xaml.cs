using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OS_2LAB_DESKTOP
{
    public partial class LogContent : Window
    {
        public LogContent(string logPath)
        {
            InitializeComponent();

            try
            {
                using (StreamReader sr = new StreamReader(logPath))
                {
                    TextBox_LogThreads.Text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            
        }
    }
}
