using System;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
	public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalkDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment , IHttpContextAccessor httpContextAccessor , NZWalkDbContext dbContext)
		{
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> UploadImage(Image image)
        {
            // Get local file path
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            // Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // https://localhost:1234/Images/image.png
            // Scheme = https
            // Host = localhost:
            // PathBase = 1234

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}

