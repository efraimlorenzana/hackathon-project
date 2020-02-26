using System;
using Application.Interfaces;
using Application.Photos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySetting> config)
        {
            var account = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            
            _cloudinary = new Cloudinary(account);
        }

        public PhotoUploadResult AddPhoto(IFormFile file)
        {
            var UploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(200).Height(200).Crop("fill").Gravity("face")
                    };

                    UploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            if(UploadResult.Error != null) throw new Exception("Error uploading photo");

            return new PhotoUploadResult
            {
                PublicId = UploadResult.PublicId,
                Url = UploadResult.SecureUri.AbsoluteUri
            };
        }

        public string DeletePhoto(string publicId)
        {
            var deletePhotoParams = new DeletionParams(publicId);

            var delete = _cloudinary.Destroy(deletePhotoParams);

            return delete.Result == "ok" ? delete.Result : null;
        }
    }
}