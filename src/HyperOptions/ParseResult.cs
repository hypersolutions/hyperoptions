using System.Collections.Generic;
using System.Linq;

namespace HyperOptions
{
    /// <summary>
    /// Defines the parse result details.
    /// </summary>
    /// <typeparam name="TOptions">Target options</typeparam>
    public sealed class ParseResult<TOptions> where TOptions : class
    {
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="options">Target options - could be null</param>
        /// <param name="errors">List of any errors</param>
        public ParseResult(TOptions options, IEnumerable<string> errors)
        {
            Options = options;
            Errors = errors ?? new List<string>(0);
        }
        
        /// <summary>
        /// Gets the target options. Maybe null if the HasErrors or ShowHelp is set.
        /// </summary>
        public TOptions Options { get; }
        
        /// <summary>
        /// Gets a list of any errors that occurred during parsing.
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Gets a flag indicating that errors occurred during parsing.
        /// </summary>
        public bool HasErrors => Errors.Any();

        /// <summary>
        /// Gets a flag indicating that the target options exists.
        /// </summary>
        public bool HasOptions => Options != null;
    }
}
