using com.mobiquity.packer.resources;
using com.mobiquity.packer.helpers;
using System.Text.RegularExpressions;

namespace com.mobiquity.packer.models
{
    public class Item
    {
        public int Index { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }

        public Item(int index, decimal weight, decimal price)
        {
            Index = index;
            Weight = weight;
            Price = price;
        }

        public Item()
        {

        }

        /// <summary>
        /// Takes an input string and parses it into an Item object
        /// </summary>
        /// <see cref="string" />
        public static Item Parse(string input)
        {
            // Clean the input, then split it up into separate strings to process
            string[] propertiesInput = InputString.CleanInput(input).Split(",");

            // Validate the input, if the input does not contain 3 separate inputs, then it is deemed invalid
            if (propertiesInput.Length != 3) throw new APIException(ErrorMessage.INVALID_INPUT);

            // Now, we can parse each individual property and create the Item
            return new Item(
                Parser.TryParseInt(InputString.CleanInput(propertiesInput[0]), ErrorMessage.INVALID_INDEX),
                Parser.TryParseDecimal(InputString.CleanInput(propertiesInput[1]), ErrorMessage.INVALID_WEIGHT),
                Parser.TryParseDecimal(Regex.Replace(InputString.CleanInput(propertiesInput[2]), "[^0-9]", ""), ErrorMessage.INVALID_PRICE)
              );
        }


    }
}