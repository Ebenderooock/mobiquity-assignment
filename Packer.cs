//using com.mobiquity.packer.abstraction;
using com.mobiquity.packer.models;

namespace com.mobiquity.packer
{
    public class Packer
    {
        /// <summary>
        /// Process the given packing request file. The file should contain the weight limit and the items to be packaged.
        /// </summary>
        /// <example>"81 : (1,3.87,€32) (2,14.55,€74)"</example>
        /// <see cref="string" />
        public static string Pack (string filePath) {
            return string.Join(System.Environment.NewLine, File.ReadAllLines(filePath)
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
                return string.Join(",", itemsInPackage.Select(item => item.Index));
            } else {
                return "-";
            }
        }
    }
}