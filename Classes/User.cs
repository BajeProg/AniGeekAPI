using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AniGeekAPI.Classes
{
    public class User
    {
        [JsonProperty("id")] public long ID { get; set; }
        [JsonProperty("login")] public string Login { get; set; }
        [JsonProperty("image")] public string ImageSource { get; set; }
        [JsonProperty("cart")] public long? CartID { get; set; }
        [JsonProperty("favorites")] public long? FavoritesID { get; set; }
        [JsonProperty("access_rights")] public string? Access { get; set; }

        public async Task<bool> LoadImageAsync(string ImageSource)
        {
            var answer = await RequestManager.RequestAsync($"type=load_user_image&userid={ID}&url={ImageSource}");

            if (answer == null) return false;
            return !answer.error;
        }
        public async Task<bool> AddToCartAsync(Product product)
        {
            var answer = await RequestManager.RequestAsync($"type=addTo_cart&userid={ID}&productid={product.ID}");

            if(answer == null) return false;
            return !answer.error;
        }
        public async Task<bool> RemoveFromCartAsync(Product product)
        {
            var answer = await RequestManager.RequestAsync($"type=remove_from_cart&userid={ID}&productid={product.ID}");

            if (answer == null) return false;
            return !answer.error;
        }
        public async Task<List<Product>?> GetProductsFromCartAsync()
        {
            var answer = await RequestManager.RequestAsync($"type=list_cart&userid={ID}");
            if (answer == null) return null;

            var products = ((JArray)answer.other).ToObject<List<Product>>();
            return products;
        }
        public async Task<bool> AddToFavoriteAsync(Product product)
        {
            var answer = await RequestManager.RequestAsync($"type=addTo_favorite&userid={ID}&productid={product.ID}");

            if (answer == null) return false;
            return !answer.error;
        }
        public async Task<bool> RemoveFromFavoriteAsync(Product product)
        {
            var answer = await RequestManager.RequestAsync($"type=remove_from_favorite&userid={ID}&productid={product.ID}");

            if (answer == null) return false;
            return !answer.error;
        }
        public async Task<List<Product>?> GetProductsFromFavoriteAsync()
        {
            var answer = await RequestManager.RequestAsync($"type=list_favorite&userid={ID}");
            if (answer == null) return null;

            var products = ((JArray)answer.other).ToObject<List<Product>>();
            return products;
        }
        public async Task<bool> MakeOrderAsync(Product product, string address)
        {
            var answer = await RequestManager.RequestAsync($"type=set_order&userid={ID}&productid={product.ID}&address={address}");
            
            if (answer == null) return false;
            return !answer.error;
        }
        public async Task<List<Order>?> GetOrdersAsync()
        {
            var answer = await RequestManager.RequestAsync($"type=list_orders&userid={ID}");
            if (answer == null) return null;

            var orders = ((JArray)answer.other).ToObject<List<Order>>();
            return orders;
        }
        public async Task<bool> AddProductAsync(string name, string description, int price)
        {
            if(Access == null) return false;
            if (Access != "admin") throw new InvalidOperationException("У вас нет прав на добавление товаров!");

            var answer = await RequestManager.RequestAsync($"type=add_product&name={name}&desc={description}&price={price}");

            if (answer == null) return false;
            return !answer.error;
        }
    }
}
