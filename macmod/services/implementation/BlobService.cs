using Azure.Storage.Blobs;

using macmod.services.interfaces;

namespace macmod.services.implementation;

public class BlobService(IConfiguration configuration) : IBlobService
{
    private string _blobConnectionString = configuration["BLOBCONNECTION"] ?? "";

    public async Task<BlobClient> DownloadAsync(string innerUri, string containerName)
    {
        BlobContainerClient containerClient = await CreateBlobContainerClient(containerName);
        
        BlobClient blobClient = containerClient.GetBlobClient(innerUri);

        if (!await blobClient.ExistsAsync()) throw new FileNotFoundException("File not found.");

        return blobClient;
    }
    
    public async Task<string> UploadAsync(IFormFile file, string containerName)
    {
        if (file == null || file.Length == 0) throw new ArgumentException("No file uploaded.");
        
        BlobContainerClient containerClient = await CreateBlobContainerClient(containerName);
        
        await containerClient.CreateIfNotExistsAsync();

        BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

        await using var stream = file.OpenReadStream();
        
        await blobClient.UploadAsync(stream, overwrite: true);

        return blobClient.Uri.ToString();
    }

    public async Task<string[]> GetLinks(string container)
    {
        BlobContainerClient containerClient = await CreateBlobContainerClient(container);

        var r = containerClient.GetBlobs();
        return r.Select(a => a.Name).ToArray();
    }
    
    public Task<BlobContainerClient> CreateBlobContainerClient(string containerName)
    {
        Console.WriteLine($"Blobconnection exists?: {_blobConnectionString != ""}");
        if(_blobConnectionString == "") _blobConnectionString = Environment.GetEnvironmentVariable("BLOBCONNECTION") ?? "";
        
        BlobServiceClient blobServiceClient = new BlobServiceClient(_blobConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        return Task.FromResult(containerClient);
    }
}