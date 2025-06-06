﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenSpaceVisual3D.cs" company="Helix 3D Toolkit">
//   http://helixtoolkit.codeplex.com, license: MIT
// </copyright>
// <summary>
//   An abstract base class for visuals that use screen space dimensions when rendering.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace HelixEngine
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;

    /// <summary>
    /// An abstract base class for visuals that use screen space dimensions when rendering.
    /// </summary>
    public abstract class ScreenSpaceVisual3D : ModelVisual3D
    {
        /// <summary>
        /// Identifies the <see cref="Color"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color), typeof(ScreenSpaceVisual3D), new UIPropertyMetadata(Colors.Black, ColorChanged));

        /// <summary>
        /// Identifies the <see cref="DepthOffset"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DepthOffsetProperty = DependencyProperty.Register(
            "DepthOffset", typeof(double), typeof(ScreenSpaceVisual3D), new UIPropertyMetadata(0.0, GeometryChanged));

        /// <summary>
        /// Identifies the <see cref="Points"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register(
            "Points", typeof(IList<Point3D>), typeof(ScreenSpaceVisual3D), new UIPropertyMetadata(null, GeometryChanged));

        /// <summary>
        /// Initializes a new instance of the <see cref = "ScreenSpaceVisual3D" /> class.
        /// </summary>
        protected ScreenSpaceVisual3D()
        {
            this.Mesh = new MeshGeometry3D();
            this.Model = new GeometryModel3D { Geometry = this.Mesh };
            this.Content = this.Model;
            this.Points = new List<Point3D>();
            this.ColorChanged();
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get
            {
                return (Color)this.GetValue(ColorProperty);
            }

            set
            {
                this.SetValue(ColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the depth offset.
        /// A small positive number (0.0001) will move the visual slightly in front of other objects.
        /// </summary>
        /// <value>
        /// The depth offset.
        /// </value>
        public double DepthOffset
        {
            get
            {
                return (double)this.GetValue(DepthOffsetProperty);
            }

            set
            {
                this.SetValue(DepthOffsetProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the points collection.
        /// </summary>
        /// <value>
        /// The points collection.
        /// </value>
        public IList<Point3D> Points
        {
            get
            {
                return (IList<Point3D>)this.GetValue(PointsProperty);
            }

            set
            {
                this.SetValue(PointsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the clipping object.
        /// </summary>
        protected CohenSutherlandClipping Clipping { get; set; }

        /// <summary>
        /// Gets or sets the mesh.
        /// </summary>
        protected MeshGeometry3D Mesh { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        protected GeometryModel3D Model { get; set; }

        /// <summary>
        /// Called when geometry properties have changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        protected static void GeometryChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScreenSpaceVisual3D)sender).UpdateGeometry();
        }

        /// <summary>
        /// The composition target_ rendering.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        protected void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            if (!Visual3DHelper.IsAttachedToViewport3D(this))
            {
                return;
            }

            if (this.UpdateTransforms())
            {
                this.UpdateClipping();
                this.UpdateGeometry();
            }
        }

        /// <summary>
        /// Updates the geometry.
        /// </summary>
        protected abstract void UpdateGeometry();

        /// <summary>
        /// Updates the transforms.
        /// </summary>
        /// <returns>
        /// True if the transform is updated.
        /// </returns>
        protected abstract bool UpdateTransforms();

        /// <summary>
        /// Changes the material when the color changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private static void ColorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((ScreenSpaceVisual3D)sender).ColorChanged();
        }

        /// <summary>
        /// Changes the material when the color changed.
        /// </summary>
        private void ColorChanged()
        {
            var mg = new MaterialGroup();
            mg.Children.Add(new DiffuseMaterial(Brushes.Black));
            mg.Children.Add(new EmissiveMaterial(new SolidColorBrush(this.Color)));
            mg.Freeze();
            this.Model.Material = mg;
        }

        /// <summary>
        /// Updates the clipping object.
        /// </summary>
        private void UpdateClipping()
        {
            var vp = Visual3DHelper.GetViewport3D(this);
            if (vp == null)
            {
                return;
            }

            this.Clipping = new CohenSutherlandClipping(10, vp.ActualWidth - 20, 10, vp.ActualHeight - 20);
        }

        public void StartRendering()
        {
            CompositionTarget.Rendering += OnCompositionTargetRendering;
        }
        public void StopRendering()
        {
            CompositionTarget.Rendering -= OnCompositionTargetRendering;
        }
    }
}