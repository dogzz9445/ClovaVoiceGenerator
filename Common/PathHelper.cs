using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class PathHelper
    {
        private static string numberPattern = "_{0:00000}";

        public static string NextAvailableFilename(string path)
        {
            // Short-cut if already available
            if (!File.Exists(path))
                return path;

            return NextAvailableIndexFilename(path);
        }

        public static string NextAvailableIndexFilename(string path, string pattern = null)
        {
            // If path has extension then insert the number pattern just before the extension and return next filename
            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), pattern ?? numberPattern));

            // Otherwise just append the pattern to the path and return next filename
            return GetNextFilename(path + (pattern ?? numberPattern));
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp; // short-circuit if no matches

            int min = 1, max = 2; // min is inclusive, max is exclusive/untested

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }

        public static void CreateDirectory(string directoryName)
        {
            if (string.IsNullOrEmpty(directoryName))
            {
                return;
            }
            if (Directory.Exists(directoryName))
            {
                return;
            }

            Directory.CreateDirectory(directoryName);
        }

        public static Stream OpenWriteOverride(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), $"Argument null exception PathHelper::OpenWrite {nameof(fileName)}");
            }

            CreateDirectory(Path.GetDirectoryName(fileName));

            return File.OpenWrite(fileName);
        }

        public static Stream OpenWriteNextAvailableIndex(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), $"Argument null exception PathHelper::OpenWrite {nameof(fileName)}");
            }

            CreateDirectory(Path.GetDirectoryName(fileName));

            return File.OpenWrite(NextAvailableIndexFilename(fileName));
        }
    }
}
