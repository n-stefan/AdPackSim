using AdPackSimLib;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AdPackSimBlazor.Client.Pages
{
    public class Server : ComponentBase
    {
        protected HttpClient Http;

        protected string BaseUrl;

        protected bool Error;

        protected Data Data = Data.Default;

        protected async Task OptimalTotalDays() =>
            await SendRequest($"{BaseUrl}OptimalTotalDays");

        protected async Task OptimalReinvestingDays() =>
            await SendRequest($"{BaseUrl}OptimalReinvestingDays");

        protected async Task OptimalInitialPacks() =>
            await SendRequest($"{BaseUrl}OptimalInitialPacks");

        protected async Task Calculate() =>
            await SendRequest($"{BaseUrl}Calculate");

        protected async Task ToROL() =>
            await SendRequest($"{BaseUrl}ToROL");

        private async Task SendRequest(string url)
        {
            var response = await Http.PostAsJsonAsync(url, Data);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Data = await response.Content.ReadFromJsonAsync<Data>();
                Error = false;
            }
            else
            {
                Data = Data.Default;
                Error = true;
            }
        }
    }
}
