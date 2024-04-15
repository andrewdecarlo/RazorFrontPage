﻿using RazorCRUDUI.Models;
using System;

namespace UI.Utilities
{
    public static class FileHelper
    {
        public static string UploadNewImage(IWebHostEnvironment environment, IFormFile file)
        {
            // this creates a random unique id
            string guid = Guid.NewGuid().ToString();

            // we get what the extension of the file we chose was
            // it should proably be a jpeg, png, or gif
            // but we aren't doing any validation of the file type
            string ext = Path.GetExtension(file.FileName);

            // get the short path
            // which is the relative path to our image folder plus
            // "guid.ext"
            // so something like
            // "images\\Ietms\\08a783ba4567-4f3c-8e7d-4f3c-8e7d.jpg"
            string shortPath = Path.Combine("images\\Items", guid + ext);

            // we need the full path not just the relative path to save the file
            // that is what we get from the environment variable
            string path = Path.Combine(environment.WebRootPath, shortPath);

            // copy the file to our images folder
            // with the "guid.ext" file name
            using (var fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }

            return shortPath;
        }

        public static void DeleteOldImage(IWebHostEnvironment environment, ItemModel item)
        {
            if (string.IsNullOrEmpty(item.PictureUrl))
                return;
            string path = Path.Combine(environment.WebRootPath, item.PictureUrl);
            if (!System.IO.File.Exists(path))
                return;
            System.IO.File.Delete(path);
        }
    }
}
