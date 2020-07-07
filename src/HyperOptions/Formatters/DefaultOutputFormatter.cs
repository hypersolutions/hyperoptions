using System;
using System.Collections.Generic;
using System.Linq;

namespace HyperOptions.Formatters
{
    public sealed class DefaultOutputFormatter : IOutputFormatter
    {
        private const string NoDescription = "[No description]";
        private const string NoDefaultValue = "[No default]";

        public void Format(FormatInfo info)
        {
            Console.WriteLine();
            
            WriteMessages(info.Messages.ToList());
            WriteErrors(info.Errors.ToList());
            
            var optionList = info.Options.ToList();
            var maxOptionLength = GetMaxOptionLength(optionList);
            var optionFormat = BuildFormatString(maxOptionLength);
            var maxDescriptionLength = GetMaxDescriptionLength(optionList);
            var descriptionFormat = BuildFormatString(maxDescriptionLength);
            
            foreach (var option in optionList)
            {
                WriteOptions(option, optionFormat);
                WriteDescription(option, descriptionFormat);
                WriteOptional(option);
                WriteDefaultValue(option);
            }
        }

        private static void WriteMessages(List<string> messages)
        {
            if (messages.Any())
            {
                foreach (var message in messages)
                {
                    Console.WriteLine(message);
                }
                
                Console.WriteLine();
            }
        }
        
        private static void WriteErrors(List<string> errors)
        {
            if (errors.Any())
            {
                Console.WriteLine("Error(s) has been detected:");
                
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                
                Console.WriteLine();
            }
        }

        private static void WriteOptions(OptionInfo option, string optionFormat)
        {
            if (string.IsNullOrWhiteSpace(option.ShortOption))
            {
                Console.Write(optionFormat, $"{option.LongOption}");
            }
            else if (string.IsNullOrWhiteSpace(option.LongOption))
            {
                Console.Write(optionFormat, $"{option.ShortOption}");
            }
            else
            {
                Console.Write(optionFormat, $"{option.ShortOption}|{option.LongOption}");
            }
        }

        private static void WriteDescription(OptionInfo option, string descriptionFormat)
        {
            var description = !string.IsNullOrWhiteSpace(option.Description) ? option.Description : NoDescription;
            Console.Write(descriptionFormat, description);
        }

        private static void WriteOptional(OptionInfo option)
        {
            var optional = option.IsOptional ? "Optional" : "Required";
            Console.Write("{0,-13}", optional);
        }

        private static void WriteDefaultValue(OptionInfo option)
        {
            Console.WriteLine(option.DefaultValue ?? NoDefaultValue);
        }
        
        private static int GetMaxOptionLength(List<OptionInfo> options)
        {
            var shortOptions = options.Select(o => new {Text = o.ShortOption ?? string.Empty});
            var longOptions = options.Select(o => new {Text = o.LongOption ?? string.Empty});
            return shortOptions.Union(longOptions).Max(o => o.Text.Length);
        }

        private static int GetMaxDescriptionLength(List<OptionInfo> options)
        {
            return options.Max(o => string.IsNullOrWhiteSpace(o.Description) 
                ? NoDescription.Length : o.Description.Length);
        }
        
        private static string BuildFormatString(int length)
        {
            return $"{{0,-{length + 5}}}";
        }
    }
}
