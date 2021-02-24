using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace OS_2LAB
{
    public class FileView
    {
        public FileView(FileInfo fileInfo)
        {
            Path = fileInfo.FullName;
            Length = fileInfo.Length;
            DateCreation = fileInfo.CreationTime;
            DateModification = fileInfo.LastWriteTime;
        }

        [DisplayName("Путь")]
        public string Path { get; set; }

        [DisplayName("Размер(байты)")]
        public long Length { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreation { get; set; }

        [DisplayName("Дата модификации")]
        public DateTime DateModification { get; set; }
    }
}
