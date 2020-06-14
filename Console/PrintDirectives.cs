﻿namespace DotNetConsoleAppToolkit.Console
{
    /*
    * print directives global syntax:
    *      commandBlockBegin command commandValueAssignationChar value (commandSeparatorChar command commandValueAssignationChar value)* commandBlockEnd
    *      commandBlockBegin := (
    *      commandBlockEnd := )
    *      commandValueAssignationChar := =
    *      commandSeparatorChar := ,
    *      value := string_without_CommandBlockBegin_and_CommandBlockEnd) | ( codeBlockBegin any codeBlockEnd )
    *      any := string
    *      codeBlockBegin ::= [[
    *      codeBlockEnd ::= ]]
    *      syntactic elements can be changed for convenience & personal preference
    * colors: 
    *      set foreground:     f=consoleColor
    *      set background:     b=consoleColor
    *      set default foreground: df=consoleColor
    *      set default background: db=consoleColor
    *      backup foreground:  bkf
    *      backup background:  bkb
    *      restore foreground: rsf
    *      restore background: rsb
    *      set colors to defaults: rdc
    *      consoleColor (ignoreCase) := Black | DarkBlue | DarkGreen | DarkCyan | DarkRed  | DarkMagenta | DarkYellow | Gray | DarkGray  | Blue | Green | Cyan  | Red  | Magenta  | Yellow  | White
    * print flow control:
    *      clear console: cl
    *      line break: br
    *      backup cursor pos: bkcr
    *      restore cursor pos: rscr
    *      set cursor left: crx=
    *      set cursor top: cry=
    * app control:
    *      exit: exit
    * scripts engines:
    *      exec: exec csharp from text
    * text decoration (vt100):
    *      underline on: uon
    *      inverted text on: invon
    *      text decoration off: tdoff
    */
    public enum PrintDirectives
    {
        bkf,
        bkb,
        rsf,
        rsb,
        rdc,
        cl,
        f,
        b,
        df,
        db,
        br,
        inf,
        bkcr,
        rscr,
        crh,
        crs,
        crx,
        cry,
        exit,
        exec,

        // VT100
        uon,
        //bon,
        //blon,
        invon,
        //novon,
        //lion,

        tdoff,
        clrlcright,
        clrlcleft,
        clrl,
        cup,
        cdown,
        cleft,
        cright,
        cnup,
        cndown,
        cnleft,
        cnright
    }
}
