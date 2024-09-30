using Blazored.LocalStorage;

namespace Dashboard.Application.Implementations
{

    public class ProtectionStorage
    {

        private readonly ILocalStorageService _protectionStorage;

        public ProtectionStorage(ILocalStorageService protectionStorage)
        {
            _protectionStorage = protectionStorage;
        }

        public async Task SaveDataAsync<T>(string key, T data)
        {
            await _protectionStorage.SetItemAsync(key, data);
        }

        public async Task<T?> LoadDataAsync<T>(string key)
        {
            return await _protectionStorage.GetItemAsync<T>(key);
        }

        public async Task EraseDataAsync(string key)
        {
            await _protectionStorage.RemoveItemAsync(key);


        }

        public async Task<bool> ContainKey(string key)
        {
            return await _protectionStorage.ContainKeyAsync(key);
        }

        
    }

}
