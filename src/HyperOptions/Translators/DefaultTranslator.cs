using System;

namespace HyperOptions.Translators
{
    public sealed class DefaultTranslator<TTarget> : ITranslator<TTarget>
    {
        public TTarget Translate(string value)
        {
            return (TTarget)Convert.ChangeType(value, Type.GetTypeCode(typeof(TTarget)));
        }
    }
}
