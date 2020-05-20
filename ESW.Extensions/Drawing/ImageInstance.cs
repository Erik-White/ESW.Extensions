using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ESW.Extensions.Drawing
{
    /// <summary>
    /// Extension methods for images stored as <see cref="Image"/> instances.
    /// </summary>
    public static class ImageInstance
    {
        /// <summary>
        /// Convert an <see cref="Image"/> instance to a specified format and encoding.
        /// </summary>
        /// <param name="value">An <see cref="Image"/> instance</param>
        /// <param name="format">The format for the image</param>
        /// <param name="encoderParameters">A collection of <see cref="EncoderParameter"/>s to set image quality, colour depth etc</param>
        /// <returns>An <see cref="Image"/> instance in the specified format and encoding</returns>
        public static Image AsFormat(this Image value, ImageFormat format, EncoderParameters encoderParameters = null)
        {
            using (var ms = value.ToMemoryStream(format, encoderParameters))
            {
                return Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Create a <see cref="byte"/> array from an <see cref="Image"/> instance.
        /// </summary>
        /// <param name="value">An <see cref="Image"/> instance</param>
        /// <returns>A <see cref="byte"/> array</returns>
        public static byte[] ToBytes(this Image value)
        {
            return value.ToBytes(value.RawFormat);
        }

        /// <summary>
        /// Create a <see cref="byte"/> array from an <see cref="Image"/> instance.
        /// </summary>
        /// <param name="value">An <see cref="Image"/> instance</param>
        /// <param name="format">The image format</param>
        /// <returns>An image <see cref="byte"/> array in the specified format</returns>
        public static byte[] ToBytes(this Image value, ImageFormat format)
        {
            return value.ToBytes(format);
        }

        /// <summary>
        /// Create a <see cref="byte"/> array from an <see cref="Image"/> instance.
        /// </summary>
        /// <param name="value">An <see cref="Image"/> instance</param>
        /// <param name="format">The image format</param>
        /// <param name="encoderParameters">A collection of <see cref="EncoderParameter"/>s to set image quality, colour depth etc</param>
        /// <returns>An image <see cref="byte"/> array in the specified format and encoding</returns>
        public static byte[] ToBytes(this Image value, ImageFormat format, EncoderParameters encoderParameters = null)
        {
            using (var ms = value.ToMemoryStream(format, encoderParameters))
            {
                return ms.ToArray();
            }
        }
    }
}
