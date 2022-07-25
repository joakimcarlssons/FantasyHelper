using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FH.UI.Blazor.Components.Helpers
{
    public static class LocalStorageKeys
    {
        public const string ChosenFantasy = "ChosenFantasy";
    }

    public static class StorageHelpers
    {
        #region JS Function Names

        private const string SaveToLocal = "SaveToLocalStorage";
        private const string LoadFromLocal = "LoadFromLocalStorage";
        private const string RemoveFromLocal = "RemoveFromLocalStorage";

        #endregion

        #region Local Storage

        public static async Task SaveToLocalStorage<T>(this IJSRuntime js, string key, T data)
        {
            try
            {
                Console.WriteLine("Saving to local storage...");
                await js.InvokeVoidAsync(SaveToLocal, key, JsonSerializer.Serialize(data));
                Console.WriteLine($"{ key } was saved to local storage!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error when saving to local storage: { ex.Message }");
                throw;
            }
        }

        public static async Task<T> LoadFromLocalStorage<T>(this IJSRuntime js, string key)
        {
            try
            {
                var result = await js.InvokeAsync<T>(LoadFromLocal, key);
                Console.WriteLine($"{ key } loaded from local storage: { result }");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error when loading from local storage: { ex.Message }");
                throw;
            }
        }

        public static async Task RemoveFromLocalStorage<T>(this IJSRuntime js, string key)
        {
            try
            {
                await js.InvokeVoidAsync(RemoveFromLocal, key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error when removing from local storage: { ex.Message }");
                throw;
            }
        }

        #endregion

        #region Data Handling

        public static string TrimStorageStrings(this string value)
            => value.TrimStart('\"').TrimEnd('\"');

        #endregion
    }
}
