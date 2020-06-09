﻿using System.IO;

namespace DotNetConsoleSdk.Component.CommandLine.Commands.FileSystem
{
    public class WildcardFilePath : DirectoryPath
    {
        public readonly string WildCardFileName;

        public WildcardFilePath(string path) : base(path) { 
            if (ContainsWildcardFileName(path))
            {
                var basepath = Path.GetDirectoryName(path);
                FileSystemInfo = new DirectoryInfo(basepath);
                DirectoryInfo = (DirectoryInfo)FileSystemInfo;
                WildCardFileName = Path.GetFileName(path);
            }
        }

        public static bool ContainsWildcardFileName(string path) {
            var ext = Path.GetFileName(path);
            return ext.Contains('*') || ext.Contains('?');
        }
    }
}
