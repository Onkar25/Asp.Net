using System;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repository
{
	public interface IImageRepository
	{
		Task<Image> UploadImage(Image image);
	}
}

