using System;
using HyperOptions.Translators;

namespace HyperOptions
{
    public sealed class OptionInfo
    {
        public OptionInfo(string name, Type targetType)
        {
            Name = name;
            TargetType = targetType;
            TranslatorType = typeof(DefaultTranslator<>);
        }
        
        public string Name { get; }
        
        public Type TargetType { get; }
        
        public string Description { get; set; }
        
        public bool IsOptional { get; set; }
        
        public string ShortOption { get; set; }
        
        public string LongOption { get; set; }
        
        public object DefaultValue { get; set; }
        
        public bool IsHelp { get; set; }
        
        public Type TranslatorType { get; set; }
    }
}
