
namespace LoginFront.Model;

public class ImageTokenRequest()
{
    public string projectName;
}
[Serializable]
public class ImagTokenResponse()
{
    public string data { get; set; }
}

public class ImageInfo()
{
    public string Name;
    public string base64;
}

public class ImageModel(HttpClient client)
{
    public async Task<HttpResponseMessage> UploadIamge(ImageInfo imageInfo)
        => await client.PostAsJsonAsync("Logins/PutImage", imageInfo);
}