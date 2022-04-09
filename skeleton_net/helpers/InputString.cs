using System.Globalization;
using com.mobiquity.packer.models;

namespace com.mobiquity.packer.helpers {

    /// <summary>
    /// Central parsing class that will handle the parsing off errors, in case of an invalid input, it will throw an API exception
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