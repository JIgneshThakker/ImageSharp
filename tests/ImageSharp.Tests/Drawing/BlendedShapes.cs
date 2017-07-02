﻿// <copyright file="DrawImageEffectTest.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests.Drawing
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using ImageSharp.PixelFormats;
    using Xunit;
    using SixLabors.Primitives;

    public class BlendedShapes
    {
        public static IEnumerable<object[]> modes = ((PixelBlenderMode[])Enum.GetValues(typeof(PixelBlenderMode)))
                                                                    .Select(x=> new object[] { x });

        [Theory]
        [WithBlankImages(nameof(modes), 250, 250, PixelTypes.Rgba32)]
        public void DrawBlendedValues<TPixel>(TestImageProvider<TPixel> provider, PixelBlenderMode mode)
            where TPixel : struct, IPixel<TPixel>
        {
            using (var img = provider.GetImage())
            {
                var scaleX = (img.Width / 100);
                var scaleY = (img.Height / 100);
                img.Mutate(x=>x
                            .Fill(Color.DarkBlue, new Rectangle(0 * scaleX, 40 * scaleY, 100 * scaleX, 20 * scaleY))
                            .Fill(Color.HotPink, new Rectangle(20 * scaleX, 0 * scaleY, 30 * scaleX, 100 * scaleY), new ImageSharp.GraphicsOptions(true)
                            {
                                BlenderMode = mode
                            }));
                img.DebugSave(provider, new { mode });
            }
        }

        [Theory]
        [WithBlankImages(nameof(modes), 250, 250, PixelTypes.Rgba32)]
        public void DrawBlendedValues_transparent<TPixel>(TestImageProvider<TPixel> provider, PixelBlenderMode mode)
            where TPixel : struct, IPixel<TPixel>
        {
            using (var img = provider.GetImage())
            {
                var scaleX = (img.Width / 100);
                var scaleY = (img.Height / 100);
                img.Mutate(x=>x.Fill(Color.DarkBlue, new Rectangle(0* scaleX, 40 * scaleY, 100 * scaleX, 20 * scaleY)));
                img.Mutate(x => x.Fill(Color.HotPink, new Rectangle(20 * scaleX, 0 * scaleY, 30 * scaleX, 100 * scaleY), new ImageSharp.GraphicsOptions(true)
                {
                    BlenderMode = mode
                }));
                img.Mutate(x => x.Fill(Color.Transparent, new SixLabors.Shapes.EllipsePolygon(40 * scaleX, 50 * scaleY, 50 * scaleX, 50 * scaleY), new ImageSharp.GraphicsOptions(true)
                {
                    BlenderMode = mode
                }));
                img.DebugSave(provider, new { mode });
            }
        }

        [Theory]
        [WithBlankImages(nameof(modes), 250, 250, PixelTypes.Rgba32)]
        public void DrawBlendedValues_transparent50Percent<TPixel>(TestImageProvider<TPixel> provider, PixelBlenderMode mode)
            where TPixel : struct, IPixel<TPixel>
        {
            using (var img = provider.GetImage())
            {
                var scaleX = (img.Width / 100);
                var scaleY = (img.Height / 100);
                img.Mutate(x=>x.Fill(Color.DarkBlue, new Rectangle(0 * scaleX, 40, 100 * scaleX, 20* scaleY)));
                img.Mutate(x => x.Fill(Color.HotPink, new Rectangle(20 * scaleX, 0, 30 * scaleX, 100 * scaleY), new ImageSharp.GraphicsOptions(true)
                {
                    BlenderMode = mode
                }));

                var c = Color.Red.WithOpacity(128);

                img.Mutate(x => x.Fill(c, new SixLabors.Shapes.EllipsePolygon(40 * scaleX, 50 * scaleY, 50 * scaleX, 50 * scaleY), new ImageSharp.GraphicsOptions(true)
                {
                    BlenderMode = mode
                }));
                img.DebugSave(provider, new { mode });
            }
        }



        [Theory]
        [WithBlankImages(nameof(modes), 250, 250, PixelTypes.Rgba32)]
        public void DrawBlendedValues_doldidEllips<TPixel>(TestImageProvider<TPixel> provider, PixelBlenderMode mode)
            where TPixel : struct, IPixel<TPixel>
        {
            using (var img = provider.GetImage())
            {
                var scaleX = (img.Width / 100);
                var scaleY = (img.Height / 100);
                img.Mutate(x => x.Fill(Color.DarkBlue, new Rectangle(0 * scaleX, 40* scaleY, 100 * scaleX, 20 * scaleY)));
                img.Mutate(x => x.Fill(Color.Black, new SixLabors.Shapes.EllipsePolygon(40 * scaleX, 50 * scaleY, 50 * scaleX, 50 * scaleY), new ImageSharp.GraphicsOptions(true)
                {
                    BlenderMode = mode
                }));
                img.DebugSave(provider, new { mode });
            }
        }
    }
}
