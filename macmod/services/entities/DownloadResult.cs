namespace macmod.services.entities;

public class DownloadResult
{
    public string FileName { get; set; } = "";
    public string ContentType { get; set; } = "";
    public Stream FileStream { get; set; }
}