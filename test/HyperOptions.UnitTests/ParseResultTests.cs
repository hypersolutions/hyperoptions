using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace HyperOptions.UnitTests
{
    public class ParseResultTests
    {
        [Fact]
        public void NullOptions_HasOptions_ReturnsTrue()
        {
            var result = new ParseResult<TestOptions>(new TestOptions(), null);
            
            result.HasOptions.ShouldBeTrue();
        }
        
        [Fact]
        public void NullErrors_HasErrors_ReturnsFalse()
        {
            var result = new ParseResult<TestOptions>(new TestOptions(), null);
            
            result.HasErrors.ShouldBeFalse();
        }
        
        [Fact]
        public void EmptyErrors_HasErrors_ReturnsFalse()
        {
            var result = new ParseResult<TestOptions>(new TestOptions(), new List<string>());
            
            result.HasErrors.ShouldBeFalse();
        }
        
        [Fact]
        public void Errors_HasErrors_ReturnsTrue()
        {
            var result = new ParseResult<TestOptions>(new TestOptions(), new[] {"Oops"});
            
            result.HasErrors.ShouldBeTrue();
        }

        [Fact]
        public void FromOptions_Options_ReturnsValue()
        {
            var options = new TestOptions();
            var result = new ParseResult<TestOptions>(options, null);
            
            result.Options.ShouldBe(options);
        }
        
        [Fact]
        public void FromOptions_HasOptions_ReturnsFalse()
        {
            var result = new ParseResult<TestOptions>(null, null);
            
            result.HasOptions.ShouldBeFalse();
        }
        
        [Fact]
        public void WithErrors_Errors_ReturnsTrue()
        {
            var errors = new[] {"Oops"};
            var result = new ParseResult<TestOptions>(new TestOptions(), errors);
            
            result.Errors.ShouldContain("Oops");
        }
        
        private class TestOptions
        {
        }
    }
}
