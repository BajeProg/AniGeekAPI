using Newtonsoft.Json;

namespace AniGeekAPI
{
    public static class RequestManager
    {
        private const string _domain = "https://dev.rea.hrel.ru/BVV/?";
        private static string? _token;

        public static void SetToken(string token)
        {
            _token = token;
        }

        public static async Task<ServerAnswer?> RequestAsync(string GET)
        {
            if (_token == null) throw new Exception("Токен отсутствует");

            using (var client = new HttpClient())
            {
                string json = await client.GetStringAsync(_domain + $"token={_token}&" + GET);
                var answer = JsonConvert.DeserializeObject<ServerAnswer>(json);

                if (answer!.error) throw new Exception(answer.desc);
                return answer;
            }
        }
    }
}
