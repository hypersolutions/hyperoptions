using System.Collections.Generic;

namespace HyperOptions.Formatters
{
    public sealed class FormatInfo
    {
        public FormatInfo(IEnumerable<OptionInfo> options, IEnumerable<string> messages, IEnumerable<string> errors)
        {
            Options = options ?? new List<OptionInfo>(0);
            Messages = messages ?? new List<string>(0);
            Errors = errors ?? new List<string>(0);
        }
        
        public IEnumerable<OptionInfo> Options { get; }
        
        public IEnumerable<string> Messages { get; }
        
        public IEnumerable<string> Errors { get; }
    }
}
