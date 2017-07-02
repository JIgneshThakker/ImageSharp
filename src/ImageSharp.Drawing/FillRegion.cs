﻿// <copyright file="FillRegion.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using Drawing;
    using Drawing.Brushes;
    using Drawing.Processors;
    using ImageSharp.PixelFormats;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Flood fills the image with the specified brush.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The details how to fill the region of interest.</param>
        /// <param name="options">The graphics options.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Brush brush, GraphicsOptions options)
        {
            return source.ApplyProcessor(new FillProcessor(brush, options));
        }

        /// <summary>
        /// Flood fills the image with the specified brush.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The details how to fill the region of interest.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Brush brush)
        {
            return source.Fill(brush, GraphicsOptions.Default);
        }

        /// <summary>
        /// Flood fills the image with the specified color.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Color color)
        {
            return source.Fill(new SolidBrush(color));
        }

        /// <summary>
        /// Flood fills the image with in the region with the specified brush.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <param name="options">The graphics options.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Brush brush, Region region, GraphicsOptions options)
        {
            return source.ApplyProcessor(new FillRegionProcessor(brush, region, options));
        }

        /// <summary>
        /// Flood fills the image with in the region with the specified brush.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="region">The region.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Brush brush, Region region)
        {
            return source.Fill(brush, region, GraphicsOptions.Default);
        }

        /// <summary>
        /// Flood fills the image with in the region with the specified color.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <param name="options">The options.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Color color, Region region, GraphicsOptions options)
        {
            return source.Fill(new SolidBrush(color), region, options);
        }

        /// <summary>
        /// Flood fills the image with in the region with the specified color.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color.</param>
        /// <param name="region">The region.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Color color, Region region)
        {
            return source.Fill(new SolidBrush(color), region);
        }
    }
}
