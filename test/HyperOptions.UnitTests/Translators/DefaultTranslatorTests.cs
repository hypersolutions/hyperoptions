using HyperOptions.Translators;
using Shouldly;
using Xunit;

namespace HyperOptions.UnitTests.Translators
{
    public class DefaultTranslatorTests
    {
        [Fact]
        public void WithString_Translate_ReturnsString()
        {
            var translator = new DefaultTranslator<string>();

            var value = translator.Translate("hello");
            
            value.ShouldBe("hello");
        }
        
        [Fact]
        public void WithInt_Translate_ReturnsInt()
        {
            var translator = new DefaultTranslator<int>();

            var value = translator.Translate("100");
            
            value.ShouldBe(100);
        }
        
        [Theory]
        [InlineData("True")]
        [InlineData("true")]
        [InlineData("TRUE")]
        public void WithBool_Translate_ReturnsBool(string arg)
        {
            var translator = new DefaultTranslator<bool>();

            var value = translator.Translate(arg);
            
            value.ShouldBeTrue();
        }
    }
}
