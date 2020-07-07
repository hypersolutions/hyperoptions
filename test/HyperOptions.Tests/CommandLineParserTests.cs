using System;
using HyperOptions.Translators;
using Shouldly;
using Xunit;

namespace HyperOptions.Tests
{
    public class CommandLineParserTests
    {
        [Fact]
        public void OptionExists_Set_ThrowsException()
        {
            var parser = new CommandLineParser<TestOptions>();
            parser.Set(o => o.Path);

            var error = Should.Throw<InvalidOperationException>(() => parser.Set(o => o.Path));
            
            error.Message.ShouldBe("The property Path already exists.");
        }
        
        [Fact]
        public void ShortOptionExists_AsOptional_ThrowsException()
        {
            var parser = new CommandLineParser<TestOptions>();
            parser.Set(o => o.Path).AsOptional("Path to files", "-p", "--path", @"c:\temp");

            var error = Should.Throw<InvalidOperationException>(
                () => parser.Set(o => o.Loggers).AsOptional("List of loggers", "-p", "--loggers"));
            
            error.Message.ShouldBe("Short option -p for property Loggers already exists.");
        }
        
        [Fact]
        public void LongOptionExists_AsOptional_ThrowsException()
        {
            var parser = new CommandLineParser<TestOptions>();
            parser.Set(o => o.Path).AsOptional("Path to files", "-p", "--path", @"c:\temp");

            var error = Should.Throw<InvalidOperationException>(
                () => parser.Set(o => o.Loggers).AsOptional("List of loggers", "-l", "--path"));
            
            error.Message.ShouldBe("Long option --path for property Loggers already exists.");
        }
        
        [Fact]
        public void ShortOptionExists_AsRequired_ThrowsException()
        {
            var parser = new CommandLineParser<TestOptions>();
            parser.Set(o => o.Path).AsRequired("Path to files", "-p", "--path", @"c:\temp");

            var error = Should.Throw<InvalidOperationException>(
                () => parser.Set(o => o.Loggers).AsRequired("List of loggers", "-p", "--loggers"));
            
            error.Message.ShouldBe("Short option -p for property Loggers already exists.");
        }
        
        [Fact]
        public void LongOptionExists_AsRequired_ThrowsException()
        {
            var parser = new CommandLineParser<TestOptions>();
            parser.Set(o => o.Path).AsRequired("Path to files", "-p", "--path", @"c:\temp");

            var error = Should.Throw<InvalidOperationException>(
                () => parser.Set(o => o.Loggers).AsRequired("List of loggers", "-l", "--path"));
            
            error.Message.ShouldBe("Long option --path for property Loggers already exists.");
        }
        
        [Fact]
        public void OptionalNotSet_Parse_ReturnsDefault()
        {
            var result = new CommandLineParser<TestOptions>()
                .Set(o => o.Path).AsOptional("Path to files", "-p", "--path", @"c:\temp")
                .Parse(new []{"-l", "Console"});
            
            result.Options.Path.ShouldBe(@"c:\temp");
        }
        
        [Fact]
        public void OptionalSet_Parse_ReturnsValue()
        {
            var result = new CommandLineParser<TestOptions>()
                .Set(o => o.Path).AsOptional("Path to files", "-p", "--path", @"c:\temp")
                .Parse(new []{"--path", @"c:\temp\pkg"});
            
            result.Options.Path.ShouldBe(@"c:\temp\pkg");
        }
        
        [Fact]
        public void RequiredSet_Parse_ReturnsValue()
        {
            var result = new CommandLineParser<TestOptions>()
                .Set(o => o.Path).AsRequired("Path to files", "-p", "--path", @"c:\temp")
                .Parse(new []{"--path", @"c:\temp\pkg"});
            
            result.Options.Path.ShouldBe(@"c:\temp\pkg");
        }
        
        [Fact]
        public void WithLoggerTranslator_Parse_ReturnsLogList()
        {
            var result = new CommandLineParser<TestOptions>()
                .Set(o => o.Loggers).AsRequired<TestLoggerListTranslator>("Loggers to use", "-l", "--loggers")
                .Parse(new []{"-l", @"Console,File"});
            
            result.Options.Loggers[0].ShouldBe("Console");
            result.Options.Loggers[1].ShouldBe("File");
        }

        private class TestOptions
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Path { get; set; }
        
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string[] Loggers { get; set; }
        }

        private sealed class TestLoggerListTranslator : ITranslator<string[]>
        {
            public string[] Translate(string value)
            {
                return value.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
