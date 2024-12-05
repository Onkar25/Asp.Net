using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO.Image;
using NZWalkAPI.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        // Post to upload Image
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            if(ModelState.IsValid)
            {
                // Convert DTO to Domain Model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileName = imageUploadRequestDto.Filename,
                    FileExtension = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeInBytes = imageUploadRequestDto.File.Length,
                    FileDescription = imageUploadRequestDto.FileDescription
                };

                // User repository to upload Image
                await imageRepository.UploadImage(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowExtension = new string[] { ".jpg", ".jpeg", ".png"};

            if (!allowExtension.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            if(imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Filesize is more that 10 Mb");
            }
        }
    }
}

