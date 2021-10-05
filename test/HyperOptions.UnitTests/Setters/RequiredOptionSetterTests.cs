using System.Collections.Generic;
using HyperOptions.Setters;
using HyperOptions.Translators;
using Shouldly;
using Xunit;

namespace HyperOptions.UnitTests.Setters
{
    public class RequiredOptionSetterTests
    {
        [Fact]
        public void ArgExists_Set_UpdatesOptionsPath()
        {
            var argList = new List<string>(new []{"-p", @"c:\temp"});
            var options = new TestOptions();
            var info = new OptionInfo("Path", typeof(string)) {TranslatorType = typeof(DefaultTranslator<string>)};
            var setter = new RequiredOptionSetter<TestOptions>(argList, options);
            
            setter.Set(info, "-p");
            
            options.Path.ShouldBe(@"c:\temp");
        }
        
        private class TestOptions
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Path { get; set; }
        }
    }
}
