using CrudPersoanajesExamenAGC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CrudPersoanajesExamenAGC.Services
{
    public class PersonajesService
    {

        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public PersonajesService(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:Personajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajes()
        {
            string request = "/api/personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonaje(int id)
        {
            string request = "/api/FindPersonaje/" + id;
            Personaje personaje =
                await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task DeletePersonaje(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/DeletePersonaje/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }

        public async Task CreatePersonaje
            (Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/CreatePersonaje/"+personaje;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonaje
            (Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/UpdatePersonaje/"+personaje;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
    
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }





    }
}
