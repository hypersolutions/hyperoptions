using System;
using Shouldly;
using Xunit;

namespace HyperOptions.UnitTests
{
    public class PropertyExpressionParserTests
    {
        [Fact]
        public void FromReadonly_GetOptionInfo_ThrowsException()
        {
            var error = Should.Throw<ArgumentException>(
                () => PropertyExpressionParser.GetOptionInfo<TestOptions, bool>(p => p.IsValid));
            
            error.Message.ShouldContain("Property IsValid is readonly.");
        }
        
        [Fact]
        public void FromPath_GetOptionInfo_SetsName()
        {
            var info = PropertyExpressionParser.GetOptionInfo<TestOptions, string>(p => p.Path);
            
            info.Name.ShouldBe("Path");
        }
        
        [Fact]
        public void FromPath_GetOptionInfo_SetsTargetType()
        {
            var info = PropertyExpressionParser.GetOptionInfo<TestOptions, string>(p => p.Path);
            
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
