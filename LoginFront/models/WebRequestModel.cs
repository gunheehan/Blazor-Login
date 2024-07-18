using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace LoginFront.models;

public class WebRequestModel(HttpClient client)
{
    public async Task Get<T>(string url, Action<T> completedCallbak)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            T branches = await JsonSerializer.DeserializeAsync<T>(responseStream);
            completedCallbak?.Invoke(branches);
        }
        else
        {
            ShowConsoleLog(false, url);
        }
    }
    
    public async Task Get<T1, T2>(string url, T1 data, Action<T2> completedCallback)
    {
        FieldInfo[] fields = data.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

        url += "?";

        string[] queryStrings = new string[fields.Length];

        for (int i = 0; i < fields.Length; i++)
            queryStrings[i] = string.Format("{0}={1}", fields[i].Name, fields[i].GetValue(data));

        url += string.Join("&", queryStrings);
        
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBody))
            {
                ShowConsoleLog(false, $"Response body is empty: {url}");
                return;
            }

            T2 result = JsonSerializer.Deserialize<T2>(responseBody);
            if (result is null)
            {
                ShowConsoleLog(false, $"JsonParsing Error : " + responseBody);
                return;
            }

            completedCallback?.Invoke(result);
        }
        else
        {
            ShowConsoleLog(false, $"Error: {response.ReasonPhrase}\nURL: {url}");
        }
    }
    
    public async Task Put(string url, string data)
    {
        var content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            
            ShowConsoleLog(true, responseData);
        }
        else
        {
            ShowConsoleLog(false, $"Error: {response.ReasonPhrase}\nURL: {url}");
        }
    }
    
    public async Task Put(string url, string fileName, byte[] data, string contentType)
    {
        using var content = new MultipartFormDataContent();
        using var imageContent = new ByteArrayContent(data);
        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        content.Add(imageContent, "file", fileName);

        var response = await client.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            ShowConsoleLog(true, responseData);
        }
        else
        {
            ShowConsoleLog(false, $"Error: {response.ReasonPhrase}\nURL: {url}");
        }
    }

    public async Task Put<T>(string url, T data)
    {
        string json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PutAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            ShowConsoleLog(true, responseData);
        }
        else
        {
            ShowConsoleLog(false, $"Error: {response.ReasonPhrase}\nURL: {url}");
        }
    }

    public async Task Post<T1, T2>(string url, T1 data, Action<T2> completedCallback)
    {
        var fields =
            typeof(T1).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        var formData = new Dictionary<string, string>();

        foreach (var field in fields)
        {
            var value = field.GetValue(data)?.ToString();
            if (value != null)
            {
                formData.Add(field.Name, value);
            }
        }

        var content = new FormUrlEncodedContent(formData);
        var response = await client.PostAsync(url, content);


        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            T2 result = JsonSerializer.Deserialize<T2>(responseBody);
            completedCallback?.Invoke(result);
        }
        else
        {
            ShowConsoleLog(false, $"Error: {response.ReasonPhrase}\nURL: {url}");
        }
    }

    public async Task Post<T1, T2>(string url, T1 data, Dictionary<string, string> headers,
        Action<T2> completedCallback)
    {
        string jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        foreach (var header in headers)
        {
            content.Headers.Add(header.Key, header.Value);
        }

        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            T2 result = JsonSerializer.Deserialize<T2>(responseBody);
            completedCallback?.Invoke(result);
        }
        else
        {
            ShowConsoleLog(false, $"Error: {response.ReasonPhrase}\nURL: {url}");
        }
    }

    private void ShowConsoleLog(bool isSuccess, string message)
    {
        if (isSuccess)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[WebRequest Success] ");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[WebRequest Failed] ");
        }
        
        Console.ResetColor();
        Console.WriteLine(message);
    }
}