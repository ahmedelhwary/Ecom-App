using Ecom.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

public class ImageManagementService : IImageManagementService
{
    private readonly IFileProvider fileProvider;

    public async Task<List<string>> AddImageAsync(List<IFormFile> files, string src)
    {
        var savedImageSrc = new List<string>();

        if (files == null || files.Count == 0)
            return savedImageSrc;

        var imageDirectory = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "Images",
            src
        );

        if (!Directory.Exists(imageDirectory))
            Directory.CreateDirectory(imageDirectory);

        foreach (var file in files)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(imageDirectory, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            savedImageSrc.Add($"Images/{src}/{fileName}");
        }

        return savedImageSrc;
    }


    //public void DeleteImageAsync(string src)
    //{
    //    var fullPath = fileProvider.GetFileInfo(src);
    //    var root = fullPath.PhysicalPath;
    //        File.Delete(root);
    //}

    public async Task DeleteImageAsync(string src)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", src);
        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }
}
