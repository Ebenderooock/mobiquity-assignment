using System.Globalization;
using com.mobiquity.packer.models;

namespace com.mobiquity.packer.helpers {

    /// <summary>
    /// Central helper class that will clean the input strings
    /// <summary>
    public static class InputString {
        public static string CleanInput(string input)
        {
            return input
                        .Trim()
                        .Replace("(", "")
                        .Replace(")", "");
        }

    }
}