using System;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
public PhotoService(IOptions<CloudinarySetting> config)
{
    var acc = new Account(config.Value.CloudName , config.Value.ApiKey , config.Value.ApiSecret);
    _cloudinary = new Cloudinary(acc);
}

    public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if(file.Length>0)
        {
            using var stream = file.OpenReadStream();
            var uploadParam = new ImageUploadParams{
                   File = new FileDescription(file.FileName ,stream),
                   Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                   Folder = "asp-net8" 
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParam);
        }
        return uploadResult;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
       var deletParam = new DeletionParams(publicId);

       return await _cloudinary.DestroyAsync(deletParam);
    }
}
