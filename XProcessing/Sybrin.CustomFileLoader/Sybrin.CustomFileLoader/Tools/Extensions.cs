using Sybrin10.Extensions;
using Sybrin10.Kernel;
using System;

namespace Sybrin.CustomFileLoader.Tools {
    public static class Extensions {
        public static ILogger Logger { get; set; }
        public static string Name { get; set; } = "Sybrin.CustomFileLoader";

        public static void CustomLog(this string message, EntryType entryType = EntryType.Debug, Exception ex = null) {
            if (Logger == null) Core.GetObjectInstance<ILogger>(); // Create an instance of the logger if it is not specified - this should rather be retrieved from the XProcess instead.
            Logger.Write(Name, message, entryType, ex);
        }

        public static void CustomLog(this Exception ex, string message = "", EntryType entryType = EntryType.Error) {
            if (Logger == null) Core.GetObjectInstance<ILogger>(); // Create an instance of the logger if it is not specified - this should rather be retrieved from the XProcess instead.
            Logger.Write(Name, message, entryType, ex);
        }
    }
}
