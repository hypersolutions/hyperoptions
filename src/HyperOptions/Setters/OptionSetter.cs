using System.Collections.Generic;

namespace HyperOptions.Setters
{
    public abstract class OptionSetter<TOptions>
    {
        protected OptionSetter(List<string> argList, TOptions options)
        {
            ArgList = argList;
            Options = options;
            TranslateActivator = new TranslateActivator();
        }
        
        protected List<string> ArgList { get; }
        
        protected TOptions Options { get; }

        protected TranslateActivator TranslateActivator { get; }
        
        public abstract void Set(OptionInfo info, string arg);
    }
}
