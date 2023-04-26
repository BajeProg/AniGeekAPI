using Newtonsoft.Json;

namespace AniGeekAPI.Classes
{
    public class Product
    {
        [JsonProperty("id")] public long ID { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("price")] public int Price { get; set; }
        [JsonProperty("image")] public string? ImageSource { get; set; }

        public async Task<bool> Delete()
        {
            var answer = await RequestManager.RequestAsync($"type=delete_product&productid={ID}");

            if (answer == null) return false;
            return !answer.error;
        }
        public async Task<bool> LoadImageAsync(string ImageSource)
        {
            var answer = await RequestManager.RequestAsync($"type=add_image_product&productid={ID}&url={ImageSource}");

            if (answer == null) return false;
            return !answer.error;
        }
    }
}
