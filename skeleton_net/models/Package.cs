namespace com.mobiquity.packer.models
{
    public class Package
    {
        public decimal WeightLimit { get; set; }
        public IEnumerable<int> Contents {get;set;}

        public Package (decimal weightLimit) {
            WeightLimit = weightLimit;
            Contents = new List<int>();
        }
    }
}