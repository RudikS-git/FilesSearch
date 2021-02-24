using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace OS_2LAB
{
    public class DateCreation : IComparable
    {
        public DateTime? Value { get; set; }

        public DateCreation(DateTime? dateCreation)
        {
            Value = dateCreation;
        }

        public int CompareTo(object obj)
        {
            FileInfo fileInfo = (obj as FileInfo);
            if (fileInfo == null)
                throw new ArgumentException(nameof(fileInfo));
            
            DateTime? dateTime = fileInfo.CreationTime;
            if (dateTime == null)
                throw new ArgumentException(nameof(dateTime));

            if (Value.Value.Day == dateTime.Value.Day)
            {
                return 0;
            }

            return 1;
        }
    }

    public class DateModification : IComparable
    {
        public DateTime? Value { get; set; }

        public DateModification(DateTime? dateModification)
        {
            Value = dateModification;
        }
        
        public int CompareTo(object obj)
        {
            FileInfo fileInfo = (obj as FileInfo);
            if (fileInfo == null)
                throw new ArgumentException(nameof(fileInfo));

            DateTime? dateTime = fileInfo.LastWriteTime;
            if (dateTime == null)
                throw new ArgumentException(nameof(dateTime));

            if (Value.Value.Day == dateTime.Value.Day)
            {
                return 0;
            }

            return 1;
        }
    }

    public class MinSize : IComparable
    {
        public long Value { get; set; }

        public MinSize(long minSize)
        {
            Value = minSize;
        }

        public int CompareTo(object obj)
        {
            FileInfo fileInfo = (obj as FileInfo);
            if (fileInfo == null)
                throw new ArgumentException(nameof(fileInfo));

            long size = fileInfo.Length;

            if (Value <= size)
            {
                return 0;
            }

            return 1;
        }
    }

    public class MaxSize : IComparable
    {
        public long Value { get; set; }

        public MaxSize(long maxSize)
        {
            Value = maxSize;
        }

        public int CompareTo(object obj)
        {
            FileInfo fileInfo = (obj as FileInfo);
            if (fileInfo == null)
                throw new ArgumentException(nameof(fileInfo));

            long size = fileInfo.Length;
            
            if (Value >= size)
            {
                return 0;
            }

            return 1;
        }
    }

    public class OccurrenceSymbols : IComparable
    {
        public string Value { get; set; }

        public OccurrenceSymbols(string text)
        {
            Value = text;
        }

        public int CompareTo(object obj)
        {
            FileInfo fileInfo = (obj as FileInfo);
            if (fileInfo == null)
                throw new ArgumentException(nameof(fileInfo));

            using (StreamReader sr = new StreamReader(fileInfo.FullName))
            {
                string text = sr.ReadToEnd();
                if(text.Contains(Value))
                {
                    return 0;
                }
                
                return 1;
            }
        }
    }


    public class FileSearchOptions
    {
        public string Name { get; set; }
        public IComparable IComparer { get; set; }
        
        public FileSearchOptions(IComparable iComparer)
        {
            IComparer = iComparer;
        }

        // public string Name { get; set; }
       // public DateTime? DateCreation { get; set; }
       // public DateTime? DateModification { get; set; }
      //  public long? MinSize { get; set; }
       // public long? MaxSize { get; set; }
      // public char[] Symbols { get; set; }

      /*public bool CompareAll(FileInfo fileInfo)
        {
            bool state = false;

            if (DateCreation != null)
            {
                state = DateCreation == fileInfo.CreationTime;

                if (state == false)
                    return state;
            }

            if (DateModification != null)
            {
                state = DateModification == fileInfo.LastWriteTime;

                if (state == false)
                    return state;
            }

            if (MinSize != 0)
            {
                state = MinSize <= fileInfo.Length;

                if (state == false)
                    return state;
            }

            if (MaxSize != null)
            {
                state = MaxSize >= fileInfo.Length;

                if (state == false)
                    return state;
            }

            return state;
        }*/
    }
}