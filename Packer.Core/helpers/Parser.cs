using System.Globalization;
using com.mobiquity.packer.models;

namespace com.mobiquity.packer.helpers {

    /// <summary>
    /// Central parsing class that will handle the parsing off errors, in case of an invalid input, it will throw an API exception
    /// <summary>
    public static class Parser {

        private const string INPUT_DECIMAL_IDENTIFIER = ".";
        public static decimal TryParseDecimal(string input, string errorMessage) {
            // We need to use this parse, because different environments might have different decimal identifiers.
            if(string.IsNullOrEmpty(input)) throw new APIException(errorMessage);

            if(decimal.TryParse(input.Replace(INPUT_DECIMAL_IDENTIFIER, NumberFormatInfo.CurrentInfo.NumberDecimalSeparator), out decimal value)) {
                return value;
            } else {
                throw new APIException(errorMessage);
            }
        }
        public static int TryParseInt(string input, string errorMessage) {
            if(string.IsNullOrEmpty(input)) throw new APIException(errorMessage);

            if(int.TryParse(input, out int value)) {
                 return value;
            } else {
                throw new APIException(errorMessage);
            }
        }


    }
}