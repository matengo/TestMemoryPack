using MemoryPack;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;

namespace TestMemoryPack.Client.Services
{
    public interface IMemoryPackClient
    {
        Task<T?> GetAsync<T>(string path);
        Task<T?> PostAsync<TIn, T>(string path, TIn model);
    }
    public class MemoryPackClient : IMemoryPackClient
    {
        private readonly HttpClient _httpClient;
        public MemoryPackClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T?> GetAsync<T>(string path)
        {
            var result = await _httpClient.GetAsync(path);
            if (result.IsSuccessStatusCode)
                return MemoryPackSerializer.Deserialize<T>(await result.Content.ReadAsByteArrayAsync());
            return default;
        }
        public async Task<T?> PostAsync<TIn, T>(string path, TIn model)
        {
            var item = MemoryPackSerializer.Serialize(model);

            var byteArrayContent = new ByteArrayContent(item);
            byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-memorypack");

            var result = await _httpClient.PostAsync(path, byteArrayContent);
            if (result.IsSuccessStatusCode)
                return MemoryPackSerializer.Deserialize<T>(await result.Content.ReadAsByteArrayAsync());
            return default;
        }
    }
}
