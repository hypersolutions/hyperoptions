using System.Collections.Generic;
using HyperOptions.Setters;
using HyperOptions.Translators;
using Shouldly;
using Xunit;

namespace HyperOptions.UnitTests.Setters
{
    public class OptionalOptionSetterTests
    {
        [Fact]
        public void ArgExists_Set_UpdatesOptionsPath()
        {
            var argList = new List<string>(new []{"-p", @"c:\temp"});
            var options = new TestOptions();
            var info = new OptionInfo("Path", typeof(string)) {TranslatorType = typeof(DefaultTranslator<string>)};
            var setter = new OptionalOptionSetter<TestOptions>(argList, options);
            
            setter.Set(info, "-p");
            
            options.Path.ShouldBe(@"c:\temp");
        }
        
        [Fact]
        public void ArgExistsNoValue_Set_UpdatesOptionsPathWithDefault()
        {
            var argList = new List<string>(new []{"-p", null});
            var options = new TestOptions();
            var info = new OptionInfo("Path", typeof(string))
            {
                TranslatorType = typeof(DefaultTranslator<string>), DefaultValue = @"c:\temp\pkg"
            };
            var setter = new OptionalOptionSetter<TestOptions>(argList, options);
            
            setter.Set(info, "-p");
            
            options.Path.ShouldBe(@"c:\temp\pkg");
        }
        
        [Fact]
        public void NullArg_Set_UpdatesOptionsPathWithDefault()
        {
            var argList = new List<string>(new []{"-p", @"c:\temp"});
            var options = new TestOptions();
            var info = new OptionInfo("Path", typeof(string)) {DefaultValue = @"c:\temp\pkg"};
            var setter = new OptionalOptionSetter<TestOptions>(argList, options);
            
            setter.Set(info, null);
            
            options.Path.ShouldBe(@"c:\temp\pkg");
        }
        
        private class TestOptions
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Path { get; set; }
        }
    }
}
