namespace JeweleryAPI.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public bool IsPrivilegedUser { get; set; }
        public decimal GoldPriceInGrams { get; set; }
        public decimal GoldWeightInGrams { get; set; }
        public decimal TotalPrice { get; set; }
        public int Discount { get; set; }
    }
}
