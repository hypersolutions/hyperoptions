using System;
using HyperOptions.Translators;
using Shouldly;
using Xunit;

namespace HyperOptions.UnitTests
{
    public class TranslateActivatorTests
    {
        [Fact]
        public void DefaultTranslator_Activate_ReturnsValue()
        {
            var activator = new TranslateActivator();

            var value = activator.Activate(typeof(DefaultTranslator<string>), typeof(string), @"c:\temp");
            
            value.ShouldBe(@"c:\temp");
        }
        
        [Fact]
        public void CustomTranslator_Activate_ReturnsValue()
        {
            var activator = new TranslateActivator();

            var value = (string[])activator.Activate(
                typeof(TestLoggerListTranslator), typeof(string[]), "Console,File");
            
            value[0].ShouldBe("Console");
            value[1].ShouldBe("File");
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
