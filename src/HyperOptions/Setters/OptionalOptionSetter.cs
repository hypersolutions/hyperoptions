using System.Collections.Generic;

namespace HyperOptions.Setters
{
    public sealed class OptionalOptionSetter<TOptions> : OptionSetter<TOptions>
    {
        public OptionalOptionSetter(List<string> argList, TOptions options) : base(argList, options)
        {
        }

        public override void Set(OptionInfo info, string arg)
        {
            object value = null;

            if (arg != null)
            {
                var argIndex = ArgList.IndexOf(arg);
                var argValue = ArgList[argIndex + 1];
                value = TranslateActivator.Activate(info.TranslatorType, info.TargetType, argValue);
            }

            value = value ?? info.DefaultValue;
            
            if (value == null) return;
            
            var property = typeof(TOptions).GetProperty(info.Name);
            property?.SetValue(Options, value);
        }
    }
}
