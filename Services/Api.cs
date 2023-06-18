using System.Web;
using System;
using FastChat.Models;
using System.Net.Http.Json;

namespace FastChat;

public class Api
{
    private readonly HttpClient _httpClient;

    public Api(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Deputat> GetDeputat(string name)
    {
        var response = await _httpClient.PostAsJsonAsync("http://192.168.162.115:8080/api/deputat/name", new { name = name});
        return await response.Content.ReadFromJsonAsync<Deputat>();
    }

    public async Task<IList<Deputat>> GetAllDeputats()
    {
        var response = await _httpClient.GetAsync("http://192.168.162.115:8080/api/deputat?pageNo=0&pageSize=1000&sortBy=id");
        return await response.Content.ReadFromJsonAsync<IList<Deputat>>();
    }
}