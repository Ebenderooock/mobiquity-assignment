
// 81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9)
using com.mobiquity.packer.helpers;
using com.mobiquity.packer.resources;

namespace com.mobiquity.packer.models
{
    /// <summary>
    /// A Packing Request object that contains the overall package weight limit, and the items we need to fit into the package
    /// </summary>
    public class PackingRequest
    {
        private const string DATA_SEPARATOR = ":";
        private const string ITEM_SEPARATOR = " ";
        private decimal _weightLimit { get; set; }
        private IEnumerable<Item> _items { get; set; }

        /// <summary>
        /// The items that we have that we might fit into the package.
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException when trying to set the value to null</exception>
        /// <exception cref="APIException">Throws an APIException when trying to add more than 15 items to the source</exception>
        public IEnumerable<Item> Items
        {
            get { return _items; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value.Count() > 15) throw new APIException(ErrorMessage.ITEM_COUNT_EXCEEDED);
                _items = value;
            }
        }

        /// <summary>
        /// The weight limit of the package.
        /// </summary>
        /// <exception cref="APIException">Throws an APIException when the weight limit is set to more than 100</exception>
        public decimal WeightLimit
        {
            get
            {
                return _weightLimit;
            }
            set
            {
                if (value > 100) throw new APIException(ErrorMessage.WEIGHT_LIMIT_EXCEEDED);
                _weightLimit = value;
            }
        }

        /// <summary>
        /// This retrieves the list of items that weigh less than the weight limit of the package
        /// </summary>
        public IEnumerable<Item> ValidItems => _items.Where(item => item.Weight <= WeightLimit);

        /// <summary>
        /// Instantiates a new Packing Request
        /// </summary>
        public PackingRequest()
        {
            _items = new List<Item>();
        }

        /// <summary>
        /// Creates the packing request from a string value
        /// </summary>
        /// <param name="input">The input string to be converted.</param>
        /// <returns>The Packing Request</returns>
        public static PackingRequest Parse(string input)
        {
            PackingRequest packingRequest = new PackingRequest();

            string[] inputStringSections = ValidateInputString(input);

            // Get the weight limit
            packingRequest.WeightLimit = Parser.TryParseDecimal(InputString.CleanInput(inputStringSections[0]), ErrorMessage.INVALID_WEIGHT_LIMIT);

            // Get the items
            packingRequest.Items = InputString.CleanInput(inputStringSections[1]).Split(ITEM_SEPARATOR).ToList().Select(itemInput => Item.Parse(itemInput));

            return packingRequest;
        }

        /// <summary>
        /// Validates the input string
        /// </summary>
        /// <param name="input">The line for the packaging request</param>
        /// <returns>The weight limit and items of the packaging request</returns>
        /// <exception cref="APIException"></exception>
        private static string[] ValidateInputString(string input)
        {
            // if the input content is empty, throw an exception
            if (string.IsNullOrWhiteSpace(input)) throw new APIException(ErrorMessage.INVALID_INPUT);

            // Split the 2 parts of the input
            string[] inputStringSections = input.Split(DATA_SEPARATOR);

            // If there is less that 2 parts of the input, raise an invalid input exception
            if (inputStringSections.Length < 2) throw new APIException(ErrorMessage.INVALID_INPUT);

            return inputStringSections;
        }
    }
}