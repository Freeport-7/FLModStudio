﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinesVisual3D.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixEngine
{
    using System.Windows;

    /// <summary>
    /// A visual element that contains a set of line segments. The thickness of the lines is defined in screen space.
    /// </summary>
    public class LinesVisual3D : ScreenSpaceVisual3D
    {
        /// <summary>
        /// Identifies the <see cref="Thickness"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
            "Thickness", typeof(double), typeof(LinesVisual3D), new UIPropertyMetadata(1.0, GeometryChanged));

        /// <summary>
        /// The builder.
        /// </summary>
        protected readonly LineGeometryBuilder Builder;

        /// <summary>
        /// Initializes a new instance of the <see cref = "LinesVisual3D" /> class.
        /// </summary>
        public LinesVisual3D()
        {
            this.Builder = new LineGeometryBuilder(this);
        }

        /// <summary>
        /// Gets or sets the thickness of the lines.
        /// </summary>
        /// <value>
        /// The thickness.
        /// </value>
        public double Thickness
        {
            get
            {
                return (double)this.GetValue(ThicknessProperty);
            }

            set
            {
                this.SetValue(ThicknessProperty, value);
            }
        }

        /// <summary>
        /// Updates the geometry.
        /// </summary>
        protected override void UpdateGeometry()
        {
            if (this.Points == null)
            {
                this.Mesh.Positions = null;
                return;
            }

            int n = this.Points.Count;
            if (n > 0)
            {
                if (this.Mesh.TriangleIndices.Count != n * 3)
                {
                    this.Mesh.TriangleIndices = this.Builder.CreateIndices(n);
                }

                this.Mesh.Positions = this.Builder.CreatePositions(this.Points, this.Thickness, this.DepthOffset, 0, null);
            }
            else
            {
                this.Mesh.Positions = null;
            }
        }

        /// <summary>
        /// Updates the transforms.
        /// </summary>
        /// <returns>
        /// True if the transform is updated.
        /// </returns>
        protected override bool UpdateTransforms()
        {
            return this.Builder.UpdateTransforms();
        }
    }
}