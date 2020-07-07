using System;
using HyperOptions.Translators;
using Shouldly;
using Xunit;

namespace HyperOptions.Tests
{
    public class OptionSetupTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public void InvalidOptions_AsOptional_ThrowsException(string shortOption, string longOption)
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            var error = Should.Throw<ArgumentException>(
                () => setup.AsOptional("Path to files", shortOption, longOption));
            
            error.Message.ShouldContain("You must provide either a short and/or long option.");
        }
        
        [Fact]
        public void ForSetup_AsOptional_SetsDescription()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path");
            
            info.Description.ShouldBe("Path to files");
        }
        
        [Fact]
        public void ForSetup_AsOptional_SetsShortOption()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path");
            
            info.ShortOption.ShouldBe("-p");
        }
        
        [Fact]
        public void ForSetup_AsOptional_SetsLongOption()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path");
            
            info.LongOption.ShouldBe("--path");
        }
        
        [Fact]
        public void ForSetup_AsOptional_SetsDefaultValueNull()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path");
            
            info.DefaultValue.ShouldBeNull();
        }
        
        [Fact]
        public void ForSetupWithDefault_AsOptional_SetsDefaultValue()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path", @"c:\temp");
            
            info.DefaultValue.ShouldBe(@"c:\temp");
        }
        
        [Fact]
        public void ForSetup_AsOptional_SetsIsOptionalTrue()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path");
            
            info.IsOptional.ShouldBeTrue();
        }
        
        [Fact]
        public void ForSetup_AsOptional_SetsDefaultTranslator()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsOptional("Path to files", "-p", "--path");

            info.TranslatorType.Name.ShouldContain("DefaultTranslator");
        }
        
        [Fact]
        public void ForSetupWithTranslator_AsOptional_SetsTranslator()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Loggers", typeof(string[]));
            var setup = new OptionSetup<TestOptions, string[]>(parser, info);

            setup.AsOptional<TestLoggerListTranslator>("List of loggers", "-l", "--loggers");

            info.TranslatorType.Name.ShouldContain("TestLoggerListTranslator");
        }
        
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        public void InvalidOptions_AsRequired_ThrowsException(string shortOption, string longOption)
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            var error = Should.Throw<ArgumentException>(
                () => setup.AsRequired("Path to files", shortOption, longOption));
            
            error.Message.ShouldContain("You must provide either a short and/or long option.");
        }
        
        [Fact]
        public void ForSetup_AsRequired_SetsDescription()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path");
            
            info.Description.ShouldBe("Path to files");
        }
        
        [Fact]
        public void ForSetup_AsRequired_SetsShortOption()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path");
            
            info.ShortOption.ShouldBe("-p");
        }
        
        [Fact]
        public void ForSetup_AsRequired_SetsLongOption()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path");
            
            info.LongOption.ShouldBe("--path");
        }
        
        [Fact]
        public void ForSetup_AsRequired_SetsDefaultValueNull()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path");
            
            info.DefaultValue.ShouldBeNull();
        }
        
        [Fact]
        public void ForSetupWithDefault_AsRequired_SetsDefaultValue()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path", @"c:\temp");
            
            info.DefaultValue.ShouldBe(@"c:\temp");
        }
        
        [Fact]
        public void ForSetup_AsRequired_SetsIsOptionalFalse()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path");
            
            info.IsOptional.ShouldBeFalse();
        }
        
        [Fact]
        public void ForSetup_AsRequired_SetsDefaultTranslator()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Path", typeof(string));
            var setup = new OptionSetup<TestOptions, string>(parser, info);

            setup.AsRequired("Path to files", "-p", "--path");

            info.TranslatorType.Name.ShouldContain("DefaultTranslator");
        }
        
        [Fact]
        public void ForSetupWithTranslator_AsRequired_SetsTranslator()
        {
            var parser = new CommandLineParser<TestOptions>();
            var info = new OptionInfo("Loggers", typeof(string[]));
            var setup = new OptionSetup<TestOptions, string[]>(parser, info);

            setup.AsRequired<TestLoggerListTranslator>("List of loggers", "-l", "--loggers");

            info.TranslatorType.Name.ShouldContain("TestLoggerListTranslator");
        }
        
        private class TestOptions
        {
            // ReSharper disable once UnusedMember.Local
            public string Path { get; set; }
            
            // ReSharper disable once UnusedMember.Local
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
