namespace MewtonGames.Payments
{
    public class Product
    {
        public string id { get; private set; }
        public string price { get; private set; }

        public Product(string id, string price)
        {
            this.id = id;
            this.price = price;
        }
    }
}