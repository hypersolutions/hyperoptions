using System;
using HyperOptions.Translators;

namespace HyperOptions.TestHarness
{
    public sealed class TestLoggerListTranslator : ITranslator<string[]>
    {
        public string[] Translate(string value)
        {
            return value.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
