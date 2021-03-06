﻿using System;
using System.Linq;
using System.Drawing.Imaging;

namespace ESW.Extensions.Drawing
{
    public static class Encoding
    {
        /// <summary>
        /// Find the <see cref="ImageCodecInfo"/> for an <see cref="ImageFormat"/> instance.
        /// </summary>
        /// <param name="value">An image format instance</param>
        /// <returns>An <see cref="ImageCodecInfo"/> instance corresponding to the <see cref="ImageFormat"/></returns>
        public static ImageCodecInfo CodecInfo(this ImageFormat value)
        {
            return ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == value.Guid);
        }

        /// <summary>
        /// Add optional <see cref="EncoderParameter"/>s to a <see cref="EncoderParameters"/> collection.
        /// </summary>
        /// <param name="value">An <see cref="EncoderParameters"/> collection</param>
        /// <param name="compression">The algorithm used for compressing the image</param>
        /// <param name="quality">Expressed as a percentage, with 100 being original image quality</param>
        /// <param name="colorDepth">Colour range in bits per pixel</param>
        /// <param name="transform">A TransformFlip or TransformRotate <see cref="EncoderValue"/></param>
        /// <param name="frame">Specifies that the image has more than one frame (page). Can be passed to the TIFF encoder</param>
        /// <returns>A <see cref="EncoderParameters"/> collection containing the specified <see cref="EncoderParameter"/>s</returns>
        public static EncoderParameters AddParams(this EncoderParameters value, EncoderValue? compression = null, long? quality = null, long? colorDepth = null, EncoderValue? transform = null, EncoderValue? frame = null)
        {
            if (quality < 0 || quality > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(quality), quality.Value, $"{nameof(quality)} is a percentage and must be between 0 and 100");
            }
            if (colorDepth <= 0 || colorDepth > 48)
            {
                throw new ArgumentOutOfRangeException(nameof(colorDepth), colorDepth.Value, $"{nameof(colorDepth)} out of range");
            }

            // Replace existing values in the paramter list if they already exist
            // Otherwise just add them to the collection
            var encoderParams = value.Param.Where(e => e != null).ToList();
            if (compression != null) encoderParams.AddOrUpdate(new EncoderParameter(Encoder.Compression, (long)compression));
            if (quality != null) encoderParams.AddOrUpdate(new EncoderParameter(Encoder.Quality, (long)quality));
            if (colorDepth != null) encoderParams.AddOrUpdate(new EncoderParameter(Encoder.ColorDepth, (long)colorDepth));
            if (transform != null) encoderParams.AddOrUpdate(new EncoderParameter(Encoder.Transformation, (long)transform));
            if (frame != null) encoderParams.AddOrUpdate(new EncoderParameter(Encoder.SaveFlag, (long)frame));

            // Unfortunately the parameters can't be appended to the original collection directly
            // Create a new collection now that we know the total array size
            var paramCollection = new EncoderParameters(encoderParams.Count);
            for (int i = 0; i < encoderParams.Count; i++)
            {
                paramCollection.Param[i] = encoderParams[i];
            }

            // Clear up the original collection
            value?.Dispose();

            return paramCollection;
        }
    }
}
