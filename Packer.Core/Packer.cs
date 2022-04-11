//using com.mobiquity.packer.abstraction;
using com.mobiquity.packer.models;
using com.mobiquity.packer.resources;
using System.Text;

namespace com.mobiquity.packer
{
    /// <summary>
    /// The Packer class is used to determine what the most efficient packaging contents is for a given package with a given weight limit
    /// </summary>
    public static class Packer
    {
        private const string DEFAULT_SEPARATOR = ",";
        private const string NO_VALUE_INDICATOR = "-";
        /// <summary>
        /// Process the given packing request file. The file should contain the weight limit and the items to be packaged.
        /// </summary>
        /// <exception cref="APIException"></exception>
        public static string Pack (string filePath) {

            if (!File.Exists(filePath)) throw new APIException(ErrorMessage.FILE_NOT_FOUND);
            
            return string.Join(Environment.NewLine, File.ReadAllLines(filePath, Encoding.UTF8)
                    .Select(line => Packer.DetermineContents(PackingRequest.Parse(line))).ToList());
        }
        
        /// <summary>
        ///     Determine the most efficient package contents for the given weigh limit and items.
        /// </summary>
        /// <see cref="PackingRequest" />
        private static string DetermineContents(PackingRequest packingRequest)
        {
            IEnumerable<Item> itemsInPackage = new List<Item>();

            // First, we need to order the items by heighest price / lowest weight
            IEnumerable<Item> orderedItems = packingRequest.ValidItems.OrderByDescending(item => item.Price).ThenBy(item => item.Weight);

            // Next, we need to loop through the items and start adding them to the list
            // As part of this loop, we will first see when adding the next item, if it will exceed the weight limit.
            // If we do exceed the weight limit, we won't add the next item and continue down the list.
            // If we don't exceed the weight limit, we add the item and continue down the list            
            for(int index = 0; index < orderedItems.Count(); index++) {
                if(itemsInPackage.Sum(item => item.Weight) + orderedItems.ElementAt(index).Weight <= packingRequest.WeightLimit) {
                    itemsInPackage = itemsInPackage.Append(orderedItems.ElementAt(index));
                } 
            }

            // Before returning the list, we first check to see if the list has any items, if it does, we return the list of indexes used to fill the package
            // if there are not items in the list, we simply return "-" to indicate no items.
            if(itemsInPackage.Count() > 0) {
                return string.Join(DEFAULT_SEPARATOR, itemsInPackage.Select(item => item.Index));
            } else {
                return NO_VALUE_INDICATOR;
            }
        }
    }
}