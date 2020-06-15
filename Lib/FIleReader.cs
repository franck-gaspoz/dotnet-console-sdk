﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace DotNetConsoleAppToolkit.Lib
{
    public static class FIleReader
    {
        public static (string[] lines,OSPlatform eol) ReadAllLines(string path)
        {
            string[] r = null;
            var txt = File.ReadAllText(path);

            var sep_crcrlf = "\r\r\n";
            var sep_linux = "\n";
            var sep_osx = "\r";
            var sep_windows = "\r\n";
            var sep_qnx_pre_posix = ""+(char)30;
            var sep_IBM_mainframe = ""+(char)21;
            var sep_unicode_LF = "\u000D";
            var sep_unicode_VT = "\u000B";
            var sep_unicode_FF = "\u000C";
            var sep_unicode_NEL = "\u0085";
            var sep_unicode_LS = "\u2028";
            var sep_unicode_PS = "\u2029";
            var eols = new List<(string separator, OSPlatform eol)>
            {
                (sep_windows,OSPlatform.Windows),
                (sep_linux,OSPlatform.Linux),
                (sep_osx,OSPlatform.OSX),
                (sep_osx, OSPlatform.Create("QNX pre-POSIX")),
                (sep_IBM_mainframe, OSPlatform.Create("IBM mainframe")),
                (sep_unicode_LF,OSPlatform.Create("Unicode LF")),
                (sep_unicode_VT,OSPlatform.Create("Unicode VT")),
                (sep_unicode_FF,OSPlatform.Create("Unicode FF")),
                (sep_unicode_NEL,OSPlatform.Create("Unicode NEL")),
                (sep_unicode_LS,OSPlatform.Create("Unicode LS")),
                (sep_unicode_PS,OSPlatform.Create("Unicode PS")),
            };

            static (int count,string separator,OSPlatform eol) Count(string searched,string txt,OSPlatform eol)
            {
                var i = 0;
                var cnt=0;
                while (i < txt.Length && i > -1)
                    if ((i = txt.IndexOf(searched, i)) > -1)
                    {
                        cnt++;
                        i++;
                    }
                return (cnt,searched,eol);
            }
            OSPlatform? detectedOspl = null;
            var counts = new List<(int count,string separator,OSPlatform eol)>();

            var cnt_doublecrlf = Count(sep_crcrlf, txt, OSPlatform.Create("CRCRLF"));
            if (cnt_doublecrlf.count>0)
            {
                txt = txt.Replace(cnt_doublecrlf.separator, sep_windows);                
                detectedOspl = cnt_doublecrlf.eol;
            }
            foreach (var eol in eols)
                counts.Add(Count(eol.separator, txt, eol.eol));

            var max = counts.Max((x) => x.count);

            // check by order which encoding has the most count
            foreach ( var eol in eols )
            {
                var check = counts.Where(x => x.separator == eol.separator).First();
                if (check.count==max)
                {
                    r = txt.Split(check.separator);
                    if (!detectedOspl.HasValue) detectedOspl = check.eol;
                    break;
                }
            }

            if (!detectedOspl.HasValue)
            {
                // eol not detected, returns whole file in a single line
                detectedOspl = OSPlatform.Create("?");
                r = new string[] { txt };
            }
            return (r,detectedOspl.Value);
        }
    }
}