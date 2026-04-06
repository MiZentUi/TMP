using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using NbrbAPI.Models;
using Splat;

public class RateService(HttpClient? httpClient = null) : IRateService
{
    private readonly HttpClient _httpClient = httpClient ?? Locator.Current.GetService<HttpClient>()!;

    public IEnumerable<Rate> GetRates(DateTime date)
    {
        var response = _httpClient.GetAsync($"https://api.nbrb.by/exrates/rates?periodicity=0&ondate={date.ToString("yyyy-MM-dd")}").Result;
        response.EnsureSuccessStatusCode();
        List<Rate> rates = [new() {
            Cur_Abbreviation = "BYN",
            Cur_Name = "Белорусский рубль",
            Cur_OfficialRate = 1
        }];
        rates.AddRange(JsonSerializer.Deserialize<List<Rate>>(response.Content.ReadAsStream())!);
        rates.Sort((x, y) => x.Cur_Name!.CompareTo(y.Cur_Name));
        return rates;
    }
}