using com.mobiquity.packer.resources;
using com.mobiquity.packer.helpers;
using System.Text.RegularExpressions;

namespace com.mobiquity.packer.models
{
    public class Item
    {
        private int _index { get; set; }
        private decimal _weight { get; set; }
        private decimal _price { get; set; }

        /// <summary>
        /// The index of the item.
        /// </summary>
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
            }
        }

        /// <summary>
        /// The weight of the item.
        /// </summary>
        /// <exception cref="APIException"></exception>
        public decimal Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (value > 100) throw new APIException(ErrorMessage.WEIGHT_LIMIT_EXCEEDED); 
                _weight = value;
            }
        }

        /// <summary>
        /// The price of the item.
        /// </summary>
        /// <exception cref="APIException"></exception>
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value > 100) throw new APIException(ErrorMessage.PRICE_LIMIT_EXCEEDED);
                _price = value;
            }
        }

        /// <summary>
        /// Create a new item object
        /// </summary>
        /// <param name="index">The index of the item in relation to its position in the list. This acts like the id.</param>
        /// <param name="weight">The weight of the item</param>
        /// <param name="price">The price of the item</param>
        public Item(int index, decimal weight, decimal price)
        {
            Index = index;
            Weight = weight;
            Price = price;
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
                Parser.TryParseDecimal(Regex.Replace(InputString.CleanInput(propertiesInput[2]), "[^0-9.]", ""), ErrorMessage.INVALID_PRICE)
              );
        }


    }
}