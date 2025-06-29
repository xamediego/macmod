using Azure.Storage.Blobs;

namespace macmod.services.interfaces;

public interface IBlobService
{
    Task<BlobClient> DownloadAsync(string innerUri, string containerName);
    
    Task<string[]> GetLinks(string container);

    Task<BlobContainerClient> CreateBlobContainerClient(string containerName);
}