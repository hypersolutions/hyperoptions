using System;
using Shouldly;
using Xunit;

namespace HyperOptions.Tests
{
    public class PropertyExpressionParserTests
    {
        [Fact]
        public void FromReadonly_GetOptionInfo_ThrowsException()
        {
            var parser = new PropertyExpressionParser();

            var error = Should.Throw<ArgumentException>(() => parser.GetOptionInfo<TestOptions, bool>(p => p.IsValid));
            
            error.Message.ShouldContain("Property IsValid is readonly.");
        }
        
        [Fact]
        public void FromPath_GetOptionInfo_SetsName()
        {
            var parser = new PropertyExpressionParser();
            
            var info = parser.GetOptionInfo<TestOptions, string>(p => p.Path);
            
            info.Name.ShouldBe("Path");
        }
        
        [Fact]
        public void FromPath_GetOptionInfo_SetsTargetType()
        {
            var parser = new PropertyExpressionParser();
            
            var info = parser.GetOptionInfo<TestOptions, string>(p => p.Path);
            
            info.TargetType.ShouldBe(typeof(string));
        }
        
        private class TestOptions
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Path { get; set; }
            
            // ReSharper disable once UnassignedGetOnlyAutoProperty
            public bool IsValid { get; }
        }
    }
}
