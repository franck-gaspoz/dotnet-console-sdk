﻿using DotNetConsoleSdk.Component.CommandLine.CommandModel;
using System.IO;
using static DotNetConsoleSdk.Lib.Str;
using static DotNetConsoleSdk.DotNetConsole;
using System.Globalization;

namespace DotNetConsoleSdk.Component.CommandLine.Commands.FileSystem
{
    [CustomParamaterType]
    public abstract class FileSystemPath
    {
        public static string ErrorColorization = $"{Red}";
        public static string NormalDirectoryColorization = $"{Blue}";
        public static string WritableDirectoryColorization = $"{Bdarkgreen}{White}";
        public static string SystemWritableDirectoryColorization = $"{Bdarkgreen}{Yellow}";
        public static string SystemColorization = $"{Red}";
        public static string FileColorization = $"";
        public static string ReadOnlyFileColorization = $"{Green}";
        public FileSystemInfo FileSystemInfo { get; protected set; }

        public string Error;

        public FileSystemPath(FileSystemInfo fileSystemInfo)
        {
            FileSystemInfo = fileSystemInfo;
        }

        public abstract bool CheckExists(bool dumpError = true);

        public bool IsDirectory => FileSystemInfo.Attributes.HasFlag(FileAttributes.Directory);
        public bool IsFile => !IsDirectory;
        public bool HasError => Error != null;
        public bool IsReadOnly => FileSystemInfo.Attributes.HasFlag(FileAttributes.ReadOnly);
        public bool IsSystem => FileSystemInfo.Attributes.HasFlag(FileAttributes.System);
        public bool IsHidden => FileSystemInfo.Attributes.HasFlag(FileAttributes.Hidden);
        public bool IsArchive => FileSystemInfo.Attributes.HasFlag(FileAttributes.Archive);
        public bool IsCompressed => FileSystemInfo.Attributes.HasFlag(FileAttributes.Compressed);

        public string GetError() => $"{ErrorColorization}{Error}";

        public static FileSystemPath Get(FileSystemInfo fsinf)
        {
            if (fsinf.Attributes.HasFlag(FileAttributes.Directory))
                return new DirectoryPath(fsinf.FullName);
            else
                return new FilePath(fsinf.FullName);
        }

        public void Print(bool printAttributes=false,bool shortPath=false,string prefix="",string postfix="")
        {
            var bg = GetCmd(KeyWords.b + "", DefaultBackground.ToString().ToLower());
            var color = (IsDirectory) ? NormalDirectoryColorization : FileColorization;
            if (!IsSystem && IsDirectory && !IsReadOnly) color += WritableDirectoryColorization;
            if (IsSystem && !IsDirectory) color += SystemColorization + bg;
            if (IsSystem && IsDirectory && !IsReadOnly) color += SystemWritableDirectoryColorization;
            if (IsFile && IsReadOnly) color += ReadOnlyFileColorization;
            var r = "";
            var attr = "";
            string hidden = "";
            if (printAttributes)
            {
                var dir = IsDirectory ? "d" : "-";
                var ro = IsReadOnly ? "r-" : "rw";
                var sys = IsSystem ? "s" : "-";
                var h = IsHidden ? "h" : "-";
                //var c = IsCompressed ? "c" : "-";
                var a = IsArchive ? "a" : "-";
                var size = (IsDirectory) ? "" : HumanFormatOfSize(((FileInfo)FileSystemInfo).Length, 2);
                var moddat = FileSystemInfo.LastWriteTime;
                hidden = IsHidden ? "*" : "";
                var smoddat = $"{moddat.ToString("MMM", CultureInfo.InvariantCulture),-3} {moddat.Day,-2} {moddat.Hour.ToString().PadLeft(2,'0')}:{moddat.Minute.ToString().PadLeft(2,'0')}";
                attr = $" {dir}{ro}{sys}{h}{a} {size,10} {smoddat}  ";
            }
            var name = shortPath ? FileSystemInfo.Name : FileSystemInfo.FullName;
            var quote =name.Contains(' ') ? "\"" : "";
            r += $"{attr}{color}{prefix}{quote}{name}{quote}{hidden}{postfix}";
            DotNetConsole.Print(r);
            if (HasError)
                DotNetConsole.Print($" {ErrorColorization}{GetError()}");
        }
    }
}
