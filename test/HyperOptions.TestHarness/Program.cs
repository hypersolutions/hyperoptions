using System;

namespace HyperOptions.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new CommandLineParser<TestOptions>("My Console Tool")
                .Set(o => o.Path).AsOptional("Path to files", "-p", "--path", @"c:\temp")
                .Set(o => o.Loggers).AsRequired<TestLoggerListTranslator>("Loggers to use", "-l", "--loggers")
                .Set(o => o.Help, true).AsOptional("Help options", "-?", "--help")
                .Parse(args);

            if (result.HasOptions)
            {
                Console.WriteLine($"{result.Options.Path}; {string.Join(',', result.Options.Loggers)}");
            }
        }
    }
}
