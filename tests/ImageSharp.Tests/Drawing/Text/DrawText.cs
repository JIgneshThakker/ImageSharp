﻿// <copyright file="DrawText.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests.Drawing.Text
{
    using System;
    using System.Numerics;

    using ImageSharp.Drawing;
    using ImageSharp.Drawing.Brushes;
    using ImageSharp.Drawing.Pens;
    using ImageSharp.Drawing.Processors;
    using ImageSharp.PixelFormats;
    using ImageSharp.Tests.Drawing.Paths;

    using SixLabors.Fonts;
    using SixLabors.Shapes;

    using Xunit;

    public class DrawText : IDisposable
    {
        Rgba32 color = Rgba32.HotPink;

        SolidBrush brush = Brushes.Solid(Color.HotPink);

        IPath path = new SixLabors.Shapes.Path(
            new LinearLineSegment(
                new SixLabors.Primitives.PointF[] { new Vector2(10, 10), new Vector2(20, 10), new Vector2(20, 10), new Vector2(30, 10), }));

        private ProcessorWatchingImage img;

        private readonly FontCollection FontCollection;

        private readonly Font Font;

        public DrawText()
        {
            this.FontCollection = new FontCollection();
            this.Font = this.FontCollection.Install(TestFontUtilities.GetPath("SixLaborsSampleAB.woff")).CreateFont(12);
            this.img = new ProcessorWatchingImage(10, 10);
        }

        public void Dispose()
        {
            this.img.Dispose();
        }

        [Fact]
        public void FillsForEachACharachterWhenBrushSetAndNotPen()
        {
            this.img.Mutate(x => x.DrawText(
                "123",
                this.Font,
                Brushes.Solid(Color.Red),
                null,
                Vector2.Zero,
                new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void FillsForEachACharachterWhenBrushSetAndNotPenDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Brushes.Solid(Color.Red), null, Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void FillsForEachACharachterWhenBrushSet()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Brushes.Solid(Color.Red), Vector2.Zero, new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void FillsForEachACharachterWhenBrushSetDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Brushes.Solid(Color.Red), Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void FillsForEachACharachterWhenColorSet()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Color.Red, Vector2.Zero, new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count);
            FillRegionProcessor processor =
                Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);

            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(Color.Red, brush.Color);
        }

        [Fact]
        public void FillsForEachACharachterWhenColorSetDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Color.Red, Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count);
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
            FillRegionProcessor processor =
                Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);

            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(Color.Red, brush.Color);
        }

        [Fact]
        public void DrawForEachACharachterWhenPenSetAndNotBrush()
        {
            this.img.Mutate(x => x.DrawText(
                "123",
                this.Font,
                null,
                Pens.Dash(Color.Red, 1),
                Vector2.Zero,
                new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void DrawForEachACharachterWhenPenSetAndNotBrushDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, null, Pens.Dash(Color.Red, 1), Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void DrawForEachACharachterWhenPenSet()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Pens.Dash(Color.Red, 1), Vector2.Zero, new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void DrawForEachACharachterWhenPenSetDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Pens.Dash(Color.Red, 1), Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(3, this.img.ProcessorApplications.Count); // 3 fills where applied
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
        }

        [Fact]
        public void DrawForEachACharachterWhenPenSetAndFillFroEachWhenBrushSet()
        {
            this.img.Mutate(x => x.DrawText(
                "123",
                this.Font,
                Brushes.Solid(Color.Red),
                Pens.Dash(Color.Red, 1),
                Vector2.Zero,
                new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(6, this.img.ProcessorApplications.Count);
        }

        [Fact]
        public void DrawForEachACharachterWhenPenSetAndFillFroEachWhenBrushSetDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("123", this.Font, Brushes.Solid(Color.Red), Pens.Dash(Color.Red, 1), Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(6, this.img.ProcessorApplications.Count);
        }

        [Fact]
        public void BrushAppliesBeforPen()
        {
            this.img.Mutate(x => x.DrawText(
                "1",
                this.Font,
                Brushes.Solid(Color.Red),
                Pens.Dash(Color.Red, 1),
                Vector2.Zero,
                new TextGraphicsOptions(true)));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(2, this.img.ProcessorApplications.Count);
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[1].processor);
        }

        [Fact]
        public void BrushAppliesBeforPenDefaultOptions()
        {
            this.img.Mutate(x => x.DrawText("1", this.Font, Brushes.Solid(Color.Red), Pens.Dash(Color.Red, 1), Vector2.Zero));

            Assert.NotEmpty(this.img.ProcessorApplications);
            Assert.Equal(2, this.img.ProcessorApplications.Count);
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[0].processor);
            Assert.IsType<FillRegionProcessor>(this.img.ProcessorApplications[1].processor);
        }
    }
}
