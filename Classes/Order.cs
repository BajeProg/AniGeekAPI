using Newtonsoft.Json;

namespace AniGeekAPI.Classes
{
    public class Order
    {
        [JsonProperty("id")] public long ID { get; set; }
        [JsonProperty("product")] public Product? Product { get; set; }
        [JsonProperty("adress")] public string Address { get; set; }
        [JsonProperty("status")] public string? Status { get; set; }
        [JsonProperty("date")] public DateTime Date { get; set; }

        public async Task<bool> Delete()
        {
            var answer = await RequestManager.RequestAsync($"type=cancel_order&orderid={ID}");

            if (answer == null) return false;
            return !answer.error;
        }
    }
}
