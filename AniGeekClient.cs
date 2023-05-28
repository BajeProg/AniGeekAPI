using AniGeekAPI.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AniGeekAPI
{
    public class AniGeekClient
    {
        public AniGeekClient(string apiToken) => RequestManager.SetToken(apiToken);

        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>В случае успешного входа возвращается User. В противном случае null</returns>
        public async Task<User?> LoginAsync(string username, string password)
        {
            var answer = await RequestManager.RequestAsync($"type=auth_user&login={username}&pass={password}");

            if (answer == null) return null;
            return await LoginAsync((string)answer.other);
        }

        /// <summary>
        /// Вход в аккаунт
        /// </summary>
        /// <param name="session">ID сессии</param>
        /// <returns>В случае успешного входа возвращается User. В противном случае null</returns>
        public async Task<User?> LoginAsync(string session)
        {
            var answer = await RequestManager.RequestAsync($"type=enter_session&session_token={session}");
            if (answer == null) return null;

            var user = JsonConvert.DeserializeObject<User>((string)answer.other);
            return user;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="username">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>В случае успеха операции true. В противном случае false</returns>
        public async Task<bool> RegisterAsync(string username, string password)
        {
            var answer = await RequestManager.RequestAsync($"type=register_user&login={username}&pass={password}");

            if (answer == null) return false;
            return !answer.error;
        }

        public async Task<List<Product>?> GetProductsAsync()
        {
            var answer = await RequestManager.RequestAsync("type=list_product");
            if(answer == null) return null;

            var products = ((JArray)answer.other).ToObject<List<Product>>();
            return products;
        }
        public async Task<Product?> GetProductAsync(int id)
        {
            var answer = await RequestManager.RequestAsync($"type=list_product&product_id={id}");
            if (answer == null) return null;

            var product = ((JArray)answer.other).ToObject<List<Product>>()?.First();
            return product;
        }
    }
}