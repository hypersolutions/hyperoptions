using System.Collections.Generic;

namespace HyperOptions.Setters
{
    public sealed class RequiredOptionSetter<TOptions> : OptionSetter<TOptions>
    {
        public RequiredOptionSetter(List<string> argList, TOptions options) : base(argList, options)
        {
        }

        public override void Set(OptionInfo info, string arg)
        {
            var argIndex = ArgList.IndexOf(arg);
            var argValue = ArgList[argIndex + 1];
            var value = TranslateActivator.Activate(info.TranslatorType, info.TargetType, argValue);
            var property = typeof(TOptions).GetProperty(info.Name);
            property?.SetValue(Options, value);
        }
    }
}
