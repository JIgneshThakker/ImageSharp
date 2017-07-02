﻿// <copyright file="Resize.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using ImageSharp.PixelFormats;

    using ImageSharp.Processing;
    using Processing.Processors;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Resizes an image in accordance with the given <see cref="ResizeOptions"/>.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="options">The resize options.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width within the resize options will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, ResizeOptions options)
        {
            return source.Run(img =>
            {
                // cheat and bound through a run, inside here we should just be mutating, this reallt needs moving over to a processor

                // Ensure size is populated across both dimensions.
                if (options.Size.Width == 0 && options.Size.Height > 0)
                {
                    options.Size = new Size((int)MathF.Round(img.Width * options.Size.Height / (float)img.Height), options.Size.Height);
                }

                if (options.Size.Height == 0 && options.Size.Width > 0)
                {
                    options.Size = new Size(options.Size.Width, (int)MathF.Round(img.Height * options.Size.Width / (float)img.Width));
                }

                Rectangle targetRectangle = ResizeHelper.CalculateTargetLocationAndBounds(img, options);

                img.Mutate(x => Resize(x, options.Size.Width, options.Size.Height, options.Sampler, targetRectangle, options.Compand));
            });
        }

        /// <summary>
        /// Resizes an image to the given <see cref="Size"/>.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="size">The target image size.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, Size size)
        {
            return Resize(source, size.Width, size.Height, new BicubicResampler(), false);
        }

        /// <summary>
        /// Resizes an image to the given <see cref="Size"/>.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="size">The target image size.</param>
        /// <param name="compand">Whether to compress and expand the image color-space to gamma correct the image during processing.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, Size size, bool compand)
        {
            return Resize(source, size.Width, size.Height, new BicubicResampler(), compand);
        }

        /// <summary>
        /// Resizes an image to the given width and height.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target image width.</param>
        /// <param name="height">The target image height.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, int width, int height)
        {
            return Resize(source, width, height, new BicubicResampler(), false);
        }

        /// <summary>
        /// Resizes an image to the given width and height.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target image width.</param>
        /// <param name="height">The target image height.</param>
        /// <param name="compand">Whether to compress and expand the image color-space to gamma correct the image during processing.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, int width, int height, bool compand)
        {
            return Resize(source, width, height, new BicubicResampler(), compand);
        }

        /// <summary>
        /// Resizes an image to the given width and height with the given sampler.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target image width.</param>
        /// <param name="height">The target image height.</param>
        /// <param name="sampler">The <see cref="IResampler"/> to perform the resampling.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, int width, int height, IResampler sampler)
        {
            return Resize(source, width, height, sampler, false);
        }

        /// <summary>
        /// Resizes an image to the given width and height with the given sampler.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target image width.</param>
        /// <param name="height">The target image height.</param>
        /// <param name="sampler">The <see cref="IResampler"/> to perform the resampling.</param>
        /// <param name="compand">Whether to compress and expand the image color-space to gamma correct the image during processing.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, int width, int height, IResampler sampler, bool compand)
        {
            return Resize(source, width, height, sampler, new Rectangle(0, 0, width, height), compand);
        }

        /// <summary>
        /// Resizes an image to the given width and height with the given sampler and
        /// source rectangle.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target image width.</param>
        /// <param name="height">The target image height.</param>
        /// <param name="sampler">The <see cref="IResampler"/> to perform the resampling.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        /// <param name="targetRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the target image object to draw to.
        /// </param>
        /// <param name="compand">Whether to compress and expand the image color-space to gamma correct the image during processing.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, int width, int height, IResampler sampler, Rectangle sourceRectangle, Rectangle targetRectangle, bool compand)
        {
            return source.Run(img =>
             {
                 // todo : stop cheeting here and move this stuff into the processors itself
                 if (width == 0 && height > 0)
                 {
                     width = (int)MathF.Round(img.Width * height / (float)img.Height);
                     targetRectangle.Width = width;
                 }

                 if (height == 0 && width > 0)
                 {
                     height = (int)MathF.Round(img.Height * width / (float)img.Width);
                     targetRectangle.Height = height;
                 }

                 Guard.MustBeGreaterThan(width, 0, nameof(width));
                 Guard.MustBeGreaterThan(height, 0, nameof(height));

                 img.Mutate(x => x.ApplyProcessor(new ResizeProcessor(sampler, width, height, targetRectangle) { Compand = compand }, sourceRectangle));
             });
        }

        /// <summary>
        /// Resizes an image to the given width and height with the given sampler and
        /// source rectangle.
        /// </summary>
        /// <param name="source">The image to resize.</param>
        /// <param name="width">The target image width.</param>
        /// <param name="height">The target image height.</param>
        /// <param name="sampler">The <see cref="IResampler"/> to perform the resampling.</param>
        /// <param name="targetRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the target image object to draw to.
        /// </param>
        /// <param name="compand">Whether to compress and expand the image color-space to gamma correct the image during processing.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        /// <remarks>Passing zero for one of height or width will automatically preserve the aspect ratio of the original image</remarks>
        public static IImageOperations Resize(this IImageOperations source, int width, int height, IResampler sampler, Rectangle targetRectangle, bool compand)
        {
            return source.Run(img =>
            {
                // todo : stop cheeting here and move this stuff into the processors itself
                if (width == 0 && height > 0)
                {
                    width = (int)MathF.Round(img.Width * height / (float)img.Height);
                    targetRectangle.Width = width;
                }

                if (height == 0 && width > 0)
                {
                    height = (int)MathF.Round(img.Height * width / (float)img.Width);
                    targetRectangle.Height = height;
                }

                Guard.MustBeGreaterThan(width, 0, nameof(width));
                Guard.MustBeGreaterThan(height, 0, nameof(height));

                img.Mutate(x => x.ApplyProcessor(new ResizeProcessor(sampler, width, height, targetRectangle) { Compand = compand }));
            });
        }
    }
}
