using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HyperOptions.Formatters;
using HyperOptions.Setters;

namespace HyperOptions
{
    /// <summary>
    /// Defines the parser that extracts command line options into a class.
    /// </summary>
    /// <typeparam name="TOptions">Class to instantiate and hydrate from the command line args.</typeparam>
    public sealed class CommandLineParser<TOptions> where TOptions : class, new()
    {
        private readonly string[] _messages;
        private readonly List<OptionInfo> _options = new List<OptionInfo>();
        private readonly PropertyExpressionParser _propertyExpressionParser = new PropertyExpressionParser();
        private readonly List<string> _errors = new List<string>();

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="messages">
        /// List of messages to appear in the output if required e.g. title and/or description etc.
        /// </param>
        public CommandLineParser(params string[] messages)
        {
            _messages = messages;
        }
        
        /// <summary>
        /// List of configured options.
        /// </summary>
        public IEnumerable<OptionInfo> Options => _options;
        
        /// <summary>
        /// Adds a mapping of an option to a command line arg.
        /// </summary>
        /// <param name="value">Property expression for the mapping</param>
        /// <param name="isHelp">Whether the option is a help option</param>
        /// <typeparam name="TTarget">Target class</typeparam>
        /// <returns>Setup extensions to add further details to the mapping</returns>
        /// <exception cref="InvalidOperationException">Duplicate mapping already exists</exception>
        public OptionSetup<TOptions, TTarget> Set<TTarget>(
            Expression<Func<TOptions, TTarget>> value, 
            bool isHelp = false)
        {
            var info = _propertyExpressionParser.GetOptionInfo(value);
            
            if (_options.Any(o => o.Name == info.Name))
                throw new InvalidOperationException($"The property {info.Name} already exists.");

            info.IsHelp = isHelp;
            _options.Add(info);
            return new OptionSetup<TOptions, TTarget>(this, info);
        }

        /// <summary>
        /// Parses the args into the target class.
        /// </summary>
        /// <param name="args">Command line args</param>
        /// <param name="outputFormatter">Optional output formatter - none proved then the default is used</param>
        /// <returns>Parsing result</returns>
        public ParseResult<TOptions> Parse(string[] args, IOutputFormatter outputFormatter = null)
        {
            var argList = args.ToList();

            var result = CheckForHelpRequest(argList, outputFormatter ?? new DefaultOutputFormatter());

            if (result != default) return result;
            
            var options = new TOptions();
            var optionalSetter = new OptionalOptionSetter<TOptions>(argList, options);
            var requiredSetter = new RequiredOptionSetter<TOptions>(argList, options);
            
            foreach (var info in _options)
            {
                var arg = FindArg(argList, info);

                try
                {
                    if (info.IsOptional)
                    {
                        optionalSetter.Set(info, arg);
                    }
                    else
                    {
                        requiredSetter.Set(info, arg);
                    }
                }
                catch (Exception error)
                {
                    _errors.Add($"Unexpected error occurred processing {info.Name}: {error.Message}");
                }
            }

            return HandleResult(options, outputFormatter ?? new DefaultOutputFormatter());
        }
        
        private string FindArg(List<string> argList, OptionInfo info)
        {
            var arg = argList.FirstOrDefault(a => a == info.ShortOption || a == info.LongOption);

            if (arg == null && !info.IsOptional)
            {
                _errors.Add($"Required option {info.Name} is missing.");
            }

            return arg;
        }

        private ParseResult<TOptions> CheckForHelpRequest(List<string> argList, IOutputFormatter outputFormatter)
        {
            foreach (var info in _options.Where(o => o.IsHelp))
            {
                var arg = argList.FirstOrDefault(a => a == info.ShortOption || a == info.LongOption);

                if (arg != null)
                {
                    outputFormatter.Format(new FormatInfo(_options, _messages, _errors));
                    return new ParseResult<TOptions>(null, new string[0]);
                }
            }

            return default;
        }

        private ParseResult<TOptions> HandleResult(TOptions options, IOutputFormatter outputFormatter)
        {
            if (_errors.Any())
            {
                outputFormatter.Format(new FormatInfo(_options, _messages, _errors));
                return new ParseResult<TOptions>(options, _errors);
            }
            
            return new ParseResult<TOptions>(options, null);
        }
    }
}
