using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Threading;

namespace OS_2LAB
{
    public class FileSearch
    {
        static public object locker = new object();

        public static string logPath;
        
        private IComparable _iComparable;
        private string _name;

        public List<FileInfo> list;
        private DirectoryInfo _directoryInfo;

        public FileSearch(string path)
        {
            list = new List<FileInfo>();
            _directoryInfo = new DirectoryInfo(path);
            _name = "*.*";
        }
        
        public FileSearch(string path, string name, IComparable iComparable)
        {
            list = new List<FileInfo>();
            _directoryInfo = new DirectoryInfo(path);
            _iComparable = iComparable;
            _name = name;
        }

        public FileSearch(DirectoryInfo directoryInfo, string name, IComparable iComparable)
        {
            list = new List<FileInfo>();
            _directoryInfo = directoryInfo;
            _iComparable = iComparable;
            _name = name;
        }

        public List<FileInfo> SearchFiles(DirectoryInfo directoryInfo)
        {
            List<FileInfo> fileInfos = new List<FileInfo>();
            var files = directoryInfo.GetFiles(_name, SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                if (file.Attributes != FileAttributes.System && _iComparable.CompareTo(file) == 0)
                {
                    fileInfos.Add(file);

                    LogWriteFileAdded(file);
                }
            }

            return fileInfos;
        }

        public void Start()
        {
            try
            {
                var directoryInfos = _directoryInfo.GetDirectories();

                foreach (var item in directoryInfos)
                {
                    var fileSearch = new FileSearch(item, _name, _iComparable);
                    fileSearch.Start();
                    list.AddRange(fileSearch.list);
                }

                list.AddRange(SearchFiles(_directoryInfo));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void StartThread()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            LogWriteThreadStart();
            
            Start();

            stopwatch.Stop();
            LogWriteThreadEnd(stopwatch);
        }

        private void LogWriteThreadStart()
        {
            lock (locker)
            {
                using (StreamWriter sw = new StreamWriter(FileSearch.logPath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(
                        $"[{DateTime.Now}] Поток({Thread.CurrentThread.ManagedThreadId}) \"{Thread.CurrentThread.Name}\" начал свою работу");
                }
            }
        }
        
        private void LogWriteThreadEnd(Stopwatch stopwatch)
        {
            lock (locker)
            {
                using (StreamWriter sw = new StreamWriter(logPath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(
                        $"[{DateTime.Now}] Поток({Thread.CurrentThread.ManagedThreadId}) \"{Thread.CurrentThread.Name}\" завершил свою работу за {stopwatch.ElapsedMilliseconds} мс(тиков: {stopwatch.ElapsedTicks})");
                }
            }
        }

        private void LogWriteFileAdded(FileInfo file)
        {
            lock (locker)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(logPath, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine($"[{DateTime.Now}] Поток({Thread.CurrentThread.ManagedThreadId}) \"{Thread.CurrentThread.Name}\" записал в буфер файл {file.Name}, Size - {file.Length} b, C - {file.CreationTime}, M - {file.LastWriteTime}");
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
