# dotnet-console-app-toolkit
<b>Dot Net Console App Toolkit</b> helps build fastly nice multi-plateforms (windows,linux,macos) console applications using C# and .NET Core 3.1 and .NET Standard 2.1
<hr/>

## Example : shell

<img src="Doc/2020-06-13 02_34_57-Window-github.png"/>

This is a view of what is done with the C# project <a href="https://github.com/franck-gaspoz/dotnet-console-app-toolkit-shell"><b>dotnet-console-app-toolkit-shell</b></a>. The <b>Dot Net Console App Toolkit</b> integrates anything needed to run a complete shell, writes shell commands using C# and use console UI components.

> ### &nbsp;&nbsp;:information_source: How this exemple is coded ?
> This shell example runs with just a few lines of code:

``` csharp
    var commandLineReader = new CommandLineReader();
    InitializeCommandProcessor(args,commandLineReader);
    var returnCode = commandLineReader.ReadCommandLine();
    Environment.Exit(returnCode);
```

## packages dependencies:

Microsoft.CodeAnalysis.CSharp.Scripting 3.7.0-1.final
