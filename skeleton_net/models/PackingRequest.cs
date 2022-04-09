
// 81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9)
using com.mobiquity.packer.helpers;
using com.mobiquity.packer.resources;

namespace com.mobiquity.packer.models
{
    public class PackingRequest
    {
        public decimal WeightLimit { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Item> ValidItems => Items.Where(item => item.Weight <= WeightLimit);

        public PackingRequest()
        {
            Items = new List<Item>();
        }

        public static PackingRequest Parse(string input)
        {
            PackingRequest packingRequest = new PackingRequest();
            string[] inputStringSections = ValidateInputString(input);

            packingRequest.WeightLimit = ProcessWeight(inputStringSections[0]);
            packingRequest.Items = ProcessItems(inputStringSections[1]);
            return packingRequest;
        }

        private static string[] ValidateInputString(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new APIException("Invalid input string");
            string[] inputStringSections = input.Split(':');
            if (input.Split(':').Length < 2) throw new APIException("Invalid input string");

            return inputStringSections;
        }

        private static decimal ProcessWeight(string input) => Parser.TryParseDecimal(InputString.CleanInput(input), ErrorMessage.INVALID_WEIGHT_LIMIT);

        private static IEnumerable<Item> ProcessItems(string input) => InputString.CleanInput(input).Split(" ").ToList().Select(itemInput => Item.Parse(itemInput));
    }
}