using System;
using System.Linq;
using HyperOptions.Translators;

namespace HyperOptions
{
    public sealed class OptionSetup<TOptions, TTarget> where TOptions : class, new()
    {
        private readonly CommandLineParser<TOptions> _parser;
        private readonly OptionInfo _info;

        public OptionSetup(CommandLineParser<TOptions> parser, OptionInfo info)
        {
            _parser = parser;
            _info = info;
        }
        
        public CommandLineParser<TOptions> AsOptional(
            string description, 
            string shortOption, 
            string longOption, 
            TTarget defaultValue = default)
        {
            UpdateOption<DefaultTranslator<TTarget>>(description, shortOption, longOption, true, defaultValue);
            return _parser;
        }

        public CommandLineParser<TOptions> AsOptional<TTranslator>(
            string description,
            string shortOption,
            string longOption,
            TTarget defaultValue = default)
            where TTranslator : ITranslator<TTarget>
        {
            UpdateOption<TTranslator>(description, shortOption, longOption, true, defaultValue);
            return _parser;
        }

        public CommandLineParser<TOptions> AsRequired(
            string description, 
            string shortOption, 
            string longOption, 
            TTarget defaultValue = default)
        {
            UpdateOption<DefaultTranslator<TTarget>>(description, shortOption, longOption, false, defaultValue);
            return _parser;
        }
        
        public CommandLineParser<TOptions> AsRequired<TTranslator>(
            string description, 
            string shortOption, 
            string longOption, 
            TTarget defaultValue = default)
            where TTranslator : ITranslator<TTarget>
        {
            UpdateOption<TTranslator>(description, shortOption, longOption, false, defaultValue);
            return _parser;
        }

        private void UpdateOption<TTranslator>(
            string description, 
            string shortOption, 
            string longOption, 
            bool isOptional,
            TTarget defaultValue = default)
        where TTranslator : ITranslator<TTarget>
        {
            if (string.IsNullOrWhiteSpace(shortOption) && string.IsNullOrWhiteSpace(longOption))
                throw new ArgumentException(
                    "You must provide either a short and/or long option.");
            
            if (_parser.Options.Any(o => o.ShortOption == shortOption))
                throw new InvalidOperationException(
                    $"Short option {shortOption} for property {_info.Name} already exists.");
            
            if (_parser.Options.Any(o => o.LongOption == longOption))
                throw new InvalidOperationException(
                    $"Long option {longOption} for property {_info.Name} already exists.");
            
            _info.IsOptional = isOptional;
            _info.Description = description;
            _info.ShortOption = shortOption;
            _info.LongOption = longOption;
            _info.DefaultValue = defaultValue;
            _info.TranslatorType = typeof(TTranslator);
        }
    }
}
