using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Ookii.Dialogs.Wpf;
using OS_2LAB;

namespace OS_2LAB_DESKTOP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FileSearch.logPath = AppDomain.CurrentDomain.BaseDirectory + "log_threads.txt";
            StartLog(FileSearch.logPath);
        }

        public string GetPath()
        {
            var dialog = new VistaFolderBrowserDialog();
            
            if (dialog.ShowDialog() == true)
            {
                return dialog.SelectedPath;
            }

            return null;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string path = GetPath();

            if (path != null)
            {
                textBox_PathSearch.Text = path;
            }
        }
        
        private void ButtonClick_Search(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(textBox_PathSearch.Text))
            {
                MessageBox.Show("Данной директории не существует!");
                return;
            }

            StartLog(FileSearch.logPath);

            List<FileSearch> fileSearches = new List<FileSearch>();
            List<Thread> threads = new List<Thread>();

            string fileName = "*.*";
            if (!string.IsNullOrEmpty(TextBox_FileName.Text) && fileName.Contains('.'))
            {
                fileName = TextBox_FileName.Text;
            }

            if (DatePicker_FileCreation.SelectedDate != null)
            {
                var fileSearch = new FileSearch(textBox_PathSearch.Text, fileName, new DateCreation(DatePicker_FileCreation.SelectedDate));
                fileSearches.Add(fileSearch);
                Thread thread = new Thread(fileSearch.StartThread);
                thread.Name = "DateCreation";
                threads.Add(thread);
            }

            if (DatePicker_FileModification.SelectedDate != null)
            {
                var fileSearch = new FileSearch(textBox_PathSearch.Text, fileName, new DateModification(DatePicker_FileModification.SelectedDate));
                fileSearches.Add(fileSearch);
                Thread thread = new Thread(fileSearch.StartThread);
                thread.Name = "DateModification";
                threads.Add(thread);
            }

            if (!string.IsNullOrEmpty(TextBox_MinValue.Text))
            {
                if (long.TryParse(TextBox_MinValue.Text, out long result))
                {
                    if (result != 0)
                    {
                        result = GetTypeSize(result, (TypeSize)ComboBox_MinSize.SelectedIndex);

                        var fileSearch = new FileSearch(textBox_PathSearch.Text, fileName, new MinSize(result));
                        fileSearches.Add(fileSearch);
                        Thread thread = new Thread(fileSearch.StartThread);
                        thread.Name = "MinValue";
                        threads.Add(thread);
                    }
                }
                else
                {
                    MessageBox.Show("Минимальный размер должен иметь корректное число!");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(TextBox_MaxValue.Text))
            {
                if (long.TryParse(TextBox_MaxValue.Text, out long result))
                {
                    result = GetTypeSize(result, (TypeSize) ComboBox_MaxSize.SelectedIndex);

                    var fileSearch = new FileSearch(textBox_PathSearch.Text, fileName, new MaxSize(result));
                    fileSearches.Add(fileSearch);
                    Thread thread = new Thread(fileSearch.StartThread);
                    thread.Name = "MaxValue";
                    threads.Add(thread);
                }
                else
                {
                    MessageBox.Show("Максимальный размер должен иметь корректное число!");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(TextBox_OccurenceSymbols.Text))
            {
                var fileSearch = new FileSearch(textBox_PathSearch.Text, fileName, new OccurrenceSymbols(TextBox_OccurenceSymbols.Text));
                fileSearches.Add(fileSearch);
                Thread thread = new Thread(fileSearch.StartThread);
                thread.Name = "OccurenceSymbols";
                threads.Add(thread);
            }

            List<FileView> fileViews = new List<FileView>();
            if (threads.Count == 0)
            {
                var fileSearch = new FileSearch(textBox_PathSearch.Text, fileName, new MinSize(0));
                fileSearches.Add(fileSearch);
                fileSearch.Start();

                foreach (var it in fileSearch.list)
                {
                    fileViews.Add(new FileView(it));
                }
            }
            else
            {
                foreach (var thread in threads)
                {
                    thread.Start();
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }

                List<FileView> totalFileInfos = new List<FileView>();
                foreach (var fileSearch in fileSearches)
                {
                    foreach (var fileInfo in fileSearch.list)
                    {
                        totalFileInfos.Add(new FileView(fileInfo));
                    }
                }

                fileViews = totalFileInfos.GroupBy(it => it.Path)
                    .Where(pair => pair.Count() == threads.Count || threads.Count == 1)
                    .Select(it => it.First())
                    .ToList();
            }
            
            TableFiles.ItemsSource = fileViews;

            if (fileViews.Count() == 0)
            {
                MessageBox.Show("Файлов не найдено!");
            }

        }

        private void DoubleClick_DataGridItem(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            FileView fileView = (FileView) dataGrid.SelectedItem;
            
            FileContent fileContent = new FileContent(fileView);
            fileContent.ShowDialog();
        }
        
        
        enum TypeSize
        {
            Byte,
            Kb,
            Mb,
            Gb
        }
        
        // возврат в байтах
        private long GetTypeSize(long bytes, TypeSize typeSize)
        {
            switch (typeSize)
            {
                case TypeSize.Byte:
                    return bytes;

                case TypeSize.Kb:
                    return bytes * 1024;

                case TypeSize.Mb:
                    return bytes * 1024 * 1024;

                case TypeSize.Gb:
                    return bytes * 1024 * 1024 * 1024;
            }

            return bytes;
        }

        private void StartLog(string path)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }

        private void ButtonClick_CheckLog(object sender, RoutedEventArgs e)
        {
            LogContent fileContent = new LogContent(FileSearch.logPath);
            fileContent.ShowDialog();
        }
    }
}
