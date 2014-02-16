﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Download_MThread.Core
{
    public static class DOwnloadLoader
    {

        public static List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            var partitions = new List<T>[totalPartitions];

            var maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            var k = 0;

            for (var i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (var j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }
        public static bool IsCache(string path, string cardName)
        {
            return File.Exists(path + cardName.ToLower() + ".jpg");
        }

        public static void DeleteAllCache(string path)
        {
            var files = Directory.GetFiles(path);

            foreach (var filename in files)
            {
                File.Delete(filename);
            }
        }
    }
    public class DownloadWorker
    {
        public event EventHandler<EventArgs> Progressed;

        protected virtual void OnProgressed()
        {
            var handler = Progressed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public bool DownloadeImage(string url, string path )
        {
            OnProgressed();

            var fileName = path + "/"+ url +".jpg";
            WizardsImageHandler.DownloadRemoteImageFile(url, fileName);

            if (DOwnloadLoader.IsCache(fileName, url + ".jpg"))
            {
                return true;
            }
            return false;
        }
    }
}
