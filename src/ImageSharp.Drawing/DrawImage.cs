// <copyright file="DrawImage.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using Drawing.Processors;
    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Draws the given image together with the current one by blending their pixels.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="image">The image to blend with the currently processing image.</param>
        /// <param name="size">The size to draw the blended image.</param>
        /// <param name="location">The location to draw the blended image.</param>
        /// <param name="options">The options.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DrawImage(this IImageOperations source, IImage image, Size size, Point location, GraphicsOptions options)
        {
            if (size == default(Size))
            {
                size = new Size(image.Width, image.Height);
            }

            if (location == default(Point))
            {
                location = Point.Empty;
            }

            source.ApplyProcessor(new DrawImageProcessor(image, size, location, options));
            return source;
        }

        /// <summary>
        /// Draws the given image together with the current one by blending their pixels.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="image">The image to blend with the currently processing image.</param>
        /// <param name="percent">The opacity of the image image to blend. Must be between 0 and 1.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Blend(this IImageOperations source, IImage image, float percent)
        {
            GraphicsOptions options = GraphicsOptions.Default;
            options.BlendPercentage = percent;
            return DrawImage(source, image, default(Size), default(Point), options);
        }

        /// <summary>
        /// Draws the given image together with the current one by blending their pixels.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="image">The image to blend with the currently processing image.</param>
        /// <param name="blender">The blending mode.</param>
        /// <param name="percent">The opacity of the image image to blend. Must be between 0 and 1.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Blend(this IImageOperations source, IImage image, PixelBlenderMode blender, float percent)
        {
            GraphicsOptions options = GraphicsOptions.Default;
            options.BlendPercentage = percent;
            options.BlenderMode = blender;
            return DrawImage(source, image, default(Size), default(Point), options);
        }

        /// <summary>
        /// Draws the given image together with the current one by blending their pixels.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="image">The image to blend with the currently processing image.</param>
        /// <param name="options">The options, including the blending type and belnding amount.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Blend(this IImageOperations source, IImage image, GraphicsOptions options)
        {
            return DrawImage(source, image, default(Size), default(Point), options);
        }

        /// <summary>
        /// Draws the given image together with the current one by blending their pixels.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="image">The image to blend with the currently processing image.</param>
        /// <param name="percent">The opacity of the image image to blend. Must be between 0 and 1.</param>
        /// <param name="size">The size to draw the blended image.</param>
        /// <param name="location">The location to draw the blended image.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DrawImage(this IImageOperations source, IImage image, float percent, Size size, Point location)
        {
            GraphicsOptions options = GraphicsOptions.Default;
            options.BlendPercentage = percent;
            return source.DrawImage(image, size, location, options);
        }

        /// <summary>
        /// Draws the given image together with the current one by blending their pixels.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="image">The image to blend with the currently processing image.</param>
        /// <param name="blender">The type of bending to apply.</param>
        /// <param name="percent">The opacity of the image image to blend. Must be between 0 and 1.</param>
        /// <param name="size">The size to draw the blended image.</param>
        /// <param name="location">The location to draw the blended image.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DrawImage(this IImageOperations source, IImage image, PixelBlenderMode blender, float percent, Size size, Point location)
        {
            GraphicsOptions options = GraphicsOptions.Default;
            options.BlenderMode = blender;
            options.BlendPercentage = percent;
            return source.DrawImage(image, size, location, options);
        }
    }
}