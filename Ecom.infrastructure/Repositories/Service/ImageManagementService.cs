using Ecom.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

public class ImageManagementService : IImageManagementService
{
    private readonly IFileProvider fileProvider;

    public ImageManagementService(IFileProvider fileProvider)
    {
        this.fileProvider = fileProvider;
    }

    public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
    {
        var savedImageSrc = new List<string>();
        var imageDirectory = Path.Combine("wwwroot", "Images", src);

        if (!Directory.Exists(imageDirectory))
            Directory.CreateDirectory(imageDirectory);

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var fullPath = Path.Combine(imageDirectory, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);

                savedImageSrc.Add($"Images/{src}/{fileName}");
            }
        }

        return savedImageSrc;
    }

    public void DeleteImageAsync(string src)
    {
        var fullPath = fileProvider.GetFileInfo(src);
        var root = fullPath.PhysicalPath;
            File.Delete(root);
    }
}
