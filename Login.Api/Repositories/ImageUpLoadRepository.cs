using System.Data;
using Dapper;
using Login.Api.Model;
using Microsoft.Data.SqlClient;

namespace Login.Api.Repositories;

public interface IImageUpLoadRepository
{
    Task UploadImageToDb(ImageFile image);
    Task<IEnumerable<ImageFile>> GetImages();
}

public class ImageUpLoadRepository : IImageUpLoadRepository
{
    private readonly IConfiguration config;
    private const string CONN_KEY = "default";

    public ImageUpLoadRepository(IConfiguration config)
    {
        this.config = config;
    }

    public async Task UploadImageToDb(ImageFile image)
    {
        var connectionString = config.GetConnectionString(CONN_KEY);
        using IDbConnection connection = new SqlConnection(connectionString);
        string sql = "insert into ImageFile (ImageName) values (@ImageName)";
        await connection.ExecuteAsync(sql, new { ImageName = image.ImageName });
    }

    public async Task<IEnumerable<ImageFile>> GetImages()
    {
        var connectionString = config.GetConnectionString( CONN_KEY);
        using IDbConnection connection = new SqlConnection(connectionString);
        string sql = "select * from ImageFile";
        var images = await connection.QueryAsync<ImageFile>(sql);
        return images;
    }
}