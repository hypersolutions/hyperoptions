# HyperOptions

## Getting Started

You can find this packages via NuGet: 

[**HyperOptions**](https://www.nuget.org/packages/HyperOptions)

## Overview

Provides a simple API for parsing command line arguments into an object.

## Basic Setup

Given the following options class:

```c#
public class TestOptions
{
    public string Path { get; set; }
    
    public string[] Loggers { get; set; }
    
    public string Help { get; set; }
}
```

You can configure the APi to hydrate an instance of this class using:

```c#
var result = new CommandLineParser<TestOptions>("My Console Tool")
    .Set(o => o.Path).AsOptional("Path to files", "-p", "--path", @"c:\temp")
    .Set(o => o.Loggers).AsRequired<TestLoggerListTranslator>("Loggers to use", "-l", "--loggers")
    .Set(o => o.Help, true).AsOptional("Help options", "-?", "--help")
    .Parse(args);
```

**Note** the following:

* The constructor allows you to provide messages that can appear at the start of any output.
* You can add both optional and required property mappings.
* If you require custom translation between the argument and the target property, you can use a custom translator.
* You can flag an option as a help option.

The _result_ contains the _options_ if the parsing was successful. If the command line requested the help option then it will be null as 
the output formatter will provide details of the configured options to the user.

When you parse, you can use an output formatter to display details to the user. There is a default class _DefaultOutputFormatter_ which is 
used if none is provided.

Any errors or help are displayed via this output formatter.

## Translators

By default, data from the command line is converted to the target type via a built-in translator. From the above example, you can see that 
the _loggers_ option has a custom translator _TestLoggerListTranslator_ that will convert the string comma-delimited list of loggers into a string array:

```c#
public sealed class TestLoggerListTranslator : ITranslator<string[]>
{
    public string[] Translate(string value)
    {
        return value.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }
}
```

## Custom Output Formatter

You can provide your own output formatter by implementing the _IOutputFormatter_ interface. It has a single method:

```c#
void Format(FormatInfo info);
```

where the _FormatInfo_ contains the configured options, any errors and initial display messages.

The _CommandLineParser.Parse_ method has an optional parameter for the output formatter.

## Developer Notes

### Building and Publishing

From the root, to build, run:

```bash
dotnet build --configuration Release 
```

To run all the unit tests, run:

```bash
dotnet test --no-build --configuration Release
```

To create a package for the tool, run:
 
```bash
cd src/HyperOptions
dotnet pack --no-build --configuration Release 
```

To publish the package to the nuget feed on nuget.org:

```bash
dotnet nuget push ./bin/Release/HyperOptions.1.0.0.nupkg -k [THE API KEY] -s https://api.nuget.org/v3/index.json
```

## Links

* **GitFlow** https://datasift.github.io/gitflow/IntroducingGitFlow.html
