// <copyright file="FileHelper.cs" company="Sigma AB">
// Copyright (c) Sigma AB 2015
// </copyright>

namespace Kristianstad.HtmlHelpers
{
    using System.IO;
    using System.Web.Mvc;
    using EPiCore.Models.Media;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.ServiceLocation;

    /// <summary>
    /// The <see cref="FileHelper"/> class, with utility methods for files.
    /// </summary>
    public static class FileHelper
    {
        private static readonly Injected<IContentLoader> _contentLoader;

        /// <summary>
        /// Constructs a file name without file extension.
        /// </summary>
        /// <param name="helper">The helper</param>
        /// <param name="media"><see cref="ContentReference"/> representing EPiServer media (file)</param>
        /// <returns>The name, without file extension, of the given media (file), or an empty string if file or file name does not exist</returns>
        public static string GetFileName(this HtmlHelper helper, ContentReference media)
        {
            var mediaData = _contentLoader.Service.Get<MediaData>(media);
            if (mediaData == null || string.IsNullOrEmpty(mediaData.Name))
            {
                return string.Empty;
            }

            return Path.GetFileNameWithoutExtension(mediaData.Name);
        }

        /// <summary>
        /// Constructs a file size string.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="media"><see cref="ContentReference"/> representing EPiServer media (file)</param>
        /// <returns>The size of the given media (file)</returns>
        public static string GetFileSize(this HtmlHelper helper, ContentReference media)
        {
            var mediaData = _contentLoader.Service.Get<MediaData>(media) as IMediaData;
            if (mediaData == null)
            {
                return "?? KB";
            }

            return GetReadableFileSize(mediaData.FileSize);
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <remarks>
        /// Source: http://stackoverflow.com/a/11124118 
        /// </remarks>
        /// <param name="fileSize">Size of the file.</param>
        /// <returns>file size expressed in KB/MB/GB/etc</returns>
        private static string GetReadableFileSize(double fileSize)
        {
            if (fileSize == 0)
            {
                return "0 KB";
            }

            var size = (long)fileSize;
            // Get absolute value
            double absoluteSize = (fileSize < 0 ? -fileSize : fileSize);
            // Determine the suffix and readable value
            string suffix;
            double readable;

            if (absoluteSize >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (size >> 50);
            }
            else if (absoluteSize >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (size >> 40);
            }
            else if (absoluteSize >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (size >> 30);
            }
            else if (absoluteSize >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (size >> 20);
            }
            else if (absoluteSize >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (size >> 10);
            }
            else if (absoluteSize >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = size;
            }
            else
            {
                // If the file size is counted in bytes we return 1 KB
                return "1 KB";
            }

            // Divide by 1024 to get fractional value
            readable = (readable / 1024);

            // Return formatted number with suffix
            if (suffix == "KB")
            {
                return readable.ToString("0 ") + suffix;
            }
            else
            {
                return readable.ToString("0.## ") + suffix;
            }
        }
    }
}