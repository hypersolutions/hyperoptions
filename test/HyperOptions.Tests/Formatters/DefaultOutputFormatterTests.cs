using System;
using System.Collections.Generic;
using System.IO;
using HyperOptions.Formatters;
using Shouldly;
using Xunit;

namespace HyperOptions.Tests.Formatters
{
    public class DefaultOutputFormatterTests
    {
        private readonly DefaultOutputFormatter _formatter;
        private readonly FormatInfo _info;

        public DefaultOutputFormatterTests()
        {
            _formatter = new DefaultOutputFormatter();
            var options = new List<OptionInfo>(new []
            {
                new OptionInfo("Path", typeof(string))
                {
                    ShortOption = "-p", LongOption = "--path", Description = "Path to files"
                },
                new OptionInfo("Loggers", typeof(string))
                {
                    ShortOption = "-l", LongOption = "--loggers", Description = "Comma-delimited list of loggers"
                },
                new OptionInfo("OutputPath", typeof(string))
                {
                    ShortOption = "-o", LongOption = "--output-path", IsOptional = true, DefaultValue = @"c:\temp\out"
                },
                new OptionInfo("Assembly", typeof(string))
                {
                    LongOption = "--assembly", Description = "Assembly path"
                },
                new OptionInfo("Quiet", typeof(string))
                {
                    LongOption = "-q", Description = "Quiet mode"
                }
            });
            var messages = new List<string>(new []{"Test File Processor", "Processes files in a folder."});
            var errors = new List<string>(new []{"Unknown arg", "Translator failed"});
            _info = new FormatInfo(options, messages, errors);
        }
        
        [Fact]
        public void WithMessage1_Format_DisplaysMessage1First()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[1].ShouldBe("Test File Processor");
        }

        [Fact]
        public void WithMessage2_Format_DisplaysMessage2Second()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[2].ShouldBe("Processes files in a folder.");
        }
        
        [Fact]
        public void WithGeneralError_Format_DisplaysMessage1First()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[4].ShouldBe("Error(s) has been detected:");
        }
        
        [Fact]
        public void WithError1_Format_DisplaysMessage1First()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[5].ShouldBe("Unknown arg");
        }

        [Fact]
        public void WithError2_Format_DisplaysMessage2Second()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[6].ShouldBe("Translator failed");
        }
        
        [Fact]
        public void FirstOption_Format_DisplaysShortAndLongOptions()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[8].ShouldStartWith("-p|--path         ");
        }
        
        [Fact]
        public void FirstOption_Format_DisplaysDescription()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[8].ShouldContain("Path to files                       ");
        }
        
        [Fact]
        public void FirstOption_Format_DisplaysRequired()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[8].ShouldContain("Required     ");
        }
        
        [Fact]
        public void FirstOption_Format_DisplaysDefault()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[8].ShouldEndWith("[No default]");
        }
        
        [Fact]
        public void SecondOption_Format_DisplaysShortAndLongOptions()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[9].ShouldStartWith("-l|--loggers      ");
        }
        
        [Fact]
        public void SecondOption_Format_DisplaysDescription()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[9].ShouldContain("Comma-delimited list of loggers     ");
        }
        
        [Fact]
        public void SecondOption_Format_DisplaysRequired()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[9].ShouldContain("Required     ");
        }
        
        [Fact]
        public void SecondOption_Format_DisplaysDefault()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[8].ShouldEndWith("[No default]");
        }
        
        [Fact]
        public void ThirdOption_Format_DisplaysShortAndLongOptions()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[10].ShouldStartWith("-o|--output-path  ");
        }
        
        [Fact]
        public void ThirdOption_Format_DisplaysDescription()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[10].ShouldContain("[No description]                    ");
        }
        
        [Fact]
        public void ThirdOption_Format_DisplaysRequired()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[10].ShouldContain("Optional     ");
        }
        
        [Fact]
        public void ThirdOption_Format_DisplaysDefault()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[10].ShouldEndWith(@"c:\temp\out");
        }
        
        [Fact]
        public void ForthOption_Format_DisplaysShortAndLongOptions()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[11].ShouldStartWith("--assembly        ");
        }
        
        [Fact]
        public void ForthOption_Format_DisplaysDescription()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[11].ShouldContain("Assembly path                       ");
        }
        
        [Fact]
        public void ForthOption_Format_DisplaysRequired()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[11].ShouldContain("Required     ");
        }
        
        [Fact]
        public void ForthOption_Format_DisplaysDefault()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[11].ShouldEndWith("[No default]");
        }
        
        [Fact]
        public void FifthOption_Format_DisplaysShortAndLongOptions()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[12].ShouldStartWith("-q                ");
        }
        
        [Fact]
        public void FifthOption_Format_DisplaysDescription()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[12].ShouldContain("Quiet mode                          ");
        }
        
        [Fact]
        public void FifthOption_Format_DisplaysRequired()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[12].ShouldContain("Required     ");
        }
        
        [Fact]
        public void FifthOption_Format_DisplaysDefault()
        {
            using var output = new OutputHelper();
            
            _formatter.Format(_info);

            var text = GetSplitOutput(output);
            text[12].ShouldEndWith("[No default]");
        }
        
        private static string[] GetSplitOutput(OutputHelper output)
        {
            var text = output.Writer.ToString();
            return text.Split(Environment.NewLine);
        }
        
        private class OutputHelper : IDisposable
        {
            public OutputHelper()
            {
                Writer = new StringWriter();
                Console.SetOut(Writer);
            }
            
            public StringWriter Writer { get; }

            public void Dispose()
            {
                var standardOutput = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = true};
                Console.SetOut(standardOutput);
                Writer?.Dispose();
            }
        }
    }
}
