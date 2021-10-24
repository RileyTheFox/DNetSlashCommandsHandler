using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DNetSlashCommandsHandler
{
    /// <summary>
    /// Console class that allows for XML formatted strings.
    /// </summary>
    public static class XConsole
    {
        private const ConsoleColor _errorColor = ConsoleColor.Red;
        private const ConsoleColor _warningColor = ConsoleColor.DarkYellow;
        private const ConsoleColor _successColor = ConsoleColor.Green;
        private const ConsoleColor _infoColor = ConsoleColor.Blue;
        private const ConsoleColor _logColor = ConsoleColor.Gray;

        private static readonly Dictionary<string, Action> _actions =
            new Dictionary<string, Action>()
        {
            { "black",       () => Console.ForegroundColor = ConsoleColor.Black },
            { "blue",        () => Console.ForegroundColor = ConsoleColor.Blue },
            { "cyan",        () => Console.ForegroundColor = ConsoleColor.Cyan },
            { "darkblue",    () => Console.ForegroundColor = ConsoleColor.DarkBlue },
            { "darkcyan",    () => Console.ForegroundColor = ConsoleColor.DarkCyan },
            { "darkgray",    () => Console.ForegroundColor = ConsoleColor.DarkGray },
            { "darkgreen",   () => Console.ForegroundColor = ConsoleColor.DarkGreen },
            { "darkmagenta", () => Console.ForegroundColor = ConsoleColor.DarkMagenta },
            { "darkred",     () => Console.ForegroundColor = ConsoleColor.DarkRed },
            { "darkyellow",  () => Console.ForegroundColor = ConsoleColor.DarkYellow },
            { "gray",        () => Console.ForegroundColor = ConsoleColor.Gray },
            { "green",       () => Console.ForegroundColor = ConsoleColor.Green },
            { "magenta",     () => Console.ForegroundColor = ConsoleColor.Magenta },
            { "red",         () => Console.ForegroundColor = ConsoleColor.Red },
            { "white",       () => Console.ForegroundColor = ConsoleColor.White },
            { "yellow",      () => Console.ForegroundColor = ConsoleColor.Yellow },
            { "error",       () => Console.ForegroundColor = _errorColor },
            { "warning",     () => Console.ForegroundColor = _warningColor },
            { "success",     () => Console.ForegroundColor = _successColor },
            { "info",        () => Console.ForegroundColor = _infoColor },
            { "log",         () => Console.ForegroundColor = _logColor },
        };

        private static readonly XmlReaderSettings _settings = new XmlReaderSettings
        {
            ConformanceLevel = ConformanceLevel.Fragment,
            IgnoreWhitespace = false,
            IgnoreComments = true
        };

        /// <summary>
        /// Parses the given string with XML tags, 
        /// followed by the current line terminator.<br/>
        /// </summary>
        /// <param name="value">A string</param>
        public static void WriteLine(string value)
        {
            Write(value);
            Console.WriteLine();
        }

        /// <summary>
        /// Parses the given string with XML tags.
        /// </summary>
        /// <param name="value">A string</param>
        public static void Write(string value)
        {
            XmlReader reader = XmlReader.Create(new StringReader(value), _settings);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (_actions.ContainsKey(reader.Name))
                        {
                            _actions[reader.Name]();
                        }
                        break;
                    case XmlNodeType.Text:
                        Console.Write(reader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        Console.ResetColor();
                        break;
                    default:
                        Console.Write(' ');
                        break;
                }
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object, 
        /// followed by the current line terminator.<br/>
        /// This does the same as Console.WriteLine();
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteLine(object value)
        {
            Console.WriteLine(value);
        }

        /// <summary>
        /// Writes the text representation of the specified object with the specified foreground color and background color, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        /// <param name="foregroundColor">The color of the text</param>
        /// <param name="backgroundColor">The color of the background</param>
        public static void WriteColorLine(object value, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the text representation of the specified object with the specified foreground color and background color.
        /// </summary>
        /// <param name="value">Any object</param>
        /// <param name="foregroundColor">The color of the text</param>
        /// <param name="backgroundColor">The color of the background</param>
        public static void WriteColor(object value, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the text representation of the specified object with the specified color, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        /// <param name="foregroundColor">The color of the text</param>
        public static void WriteColorLine(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the text representation of the specified object with the specified color.
        /// </summary>
        /// <param name="value">Any object</param>
        /// <param name="foregroundColor">The color of the text</param>
        public static void WriteColor(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the text representation of the specified object colored as an error, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteErrorLine(object value)
            => WriteColorLine(value, _errorColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as an error.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteError(object value)
            => WriteColor(value, _errorColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as a warning, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteWarningLine(object value)
            => WriteColorLine(value, _warningColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as a warning.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteWarning(object value)
            => WriteColor(value, _warningColor);


        /// <summary>
        /// Writes the text representation of the specified object colored as a success, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteSuccessLine(object value)
            => WriteColorLine(value, _successColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as a success.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteSuccess(object value)
            => WriteColor(value, _successColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as info, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteInfoLine(object value)
            => WriteColorLine(value, _infoColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as info.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteInfo(object value)
            => WriteColor(value, _infoColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as a log, 
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteLogLine(object value)
            => WriteColorLine(value, _logColor);

        /// <summary>
        /// Writes the text representation of the specified object colored as a log.
        /// </summary>
        /// <param name="value">Any object</param>
        public static void WriteLog(object value)
            => WriteColor(value, _logColor);
    }
}
