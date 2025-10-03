namespace FinabitAPI.Finabit.Items.dto
{
    public class ItemsGjirafa
    {
        public string ProductCode { get; set; } // unique,required
        public string GTIN { get; set; } // barcode , required
        public string Title { get; set; } // Emertimi , qysh shfaqet ne web, required
        public string Description { get; set; } // Optional, detailed description of the product
        public string Brand { get; set; } // required
        public List<string> ImageUrls { get; set; } // required
        public List<string> Categories { get; set; }//kategorite
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public int StoreStockQuantity { get; set; }
        public List<Specification> Specifications { get; set; }
     
    }
    public class Specification
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
