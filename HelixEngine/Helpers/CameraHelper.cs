﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace HelixEngine
{
    /// <summary>
    /// Provides helper methods related to Media3D.Camera.
    /// </summary>
    public static class CameraHelper
    {
        /// <summary>
        /// Animates the camera position and directions.
        /// </summary>
        /// <param name="camera">
        /// The camera to animate.
        /// </param>
        /// <param name="newPosition">
        /// The position to animate to.
        /// </param>
        /// <param name="newDirection">
        /// The direction to animate to.
        /// </param>
        /// <param name="newUpDirection">
        /// The up direction to animate to.
        /// </param>
        /// <param name="animationTime">
        /// Animation time in milliseconds.
        /// </param>
        public static void AnimateTo(
            PerspectiveCamera camera,
            Point3D newPosition,
            Vector3D newDirection,
            Vector3D newUpDirection,
            double animationTime)
        {
            var fromPosition = camera.Position;
            var fromDirection = camera.LookDirection;
            var fromUpDirection = camera.UpDirection;

            camera.Position = newPosition;
            camera.LookDirection = newDirection;
            camera.UpDirection = newUpDirection;

            if (animationTime > 0)
            {
                var a1 = new Point3DAnimation(
                    fromPosition, newPosition, new Duration(TimeSpan.FromMilliseconds(animationTime)))
                    {
                        AccelerationRatio = 0.3,
                        DecelerationRatio = 0.5,
                        FillBehavior = FillBehavior.Stop
                    };
                camera.BeginAnimation(ProjectionCamera.PositionProperty, a1);

                var a2 = new Vector3DAnimation(
                    fromDirection, newDirection, new Duration(TimeSpan.FromMilliseconds(animationTime)))
                    {
                        AccelerationRatio = 0.3,
                        DecelerationRatio = 0.5,
                        FillBehavior = FillBehavior.Stop
                    };
                camera.BeginAnimation(ProjectionCamera.LookDirectionProperty, a2);

                var a3 = new Vector3DAnimation(
                    fromUpDirection, newUpDirection, new Duration(TimeSpan.FromMilliseconds(animationTime)))
                    {
                        AccelerationRatio = 0.3,
                        DecelerationRatio = 0.5,
                        FillBehavior = FillBehavior.Stop
                    };
                camera.BeginAnimation(ProjectionCamera.UpDirectionProperty, a3);
            }
        }

        /// <summary>
        /// Copies the specified camera, converts field of view/width if neccessary.
        /// </summary>
        /// <param name="source">
        /// The source camera.
        /// </param>
        /// <param name="dest">
        /// The destination camera.
        /// </param>
        public static void Copy(PerspectiveCamera source, PerspectiveCamera dest)
        {
            if (source == null || dest == null)
            {
                return;
            }

            dest.LookDirection = source.LookDirection;
            dest.Position = source.Position;
            dest.UpDirection = source.UpDirection;
            dest.FieldOfView = source.FieldOfView;
            dest.NearPlaneDistance = source.NearPlaneDistance;
            dest.FarPlaneDistance = source.FarPlaneDistance;
        }

        /// <summary>
        /// Copy the direction of the source <see cref="Camera"/>. Used for the CoordinateSystem view.
        /// </summary>
        /// <param name="source">
        /// The source camera.
        /// </param>
        /// <param name="dest">
        /// The destination camera.
        /// </param>
        /// <param name="distance">
        /// New length of the LookDirection vector.
        /// </param>
        public static void CopyDirectionOnly(ProjectionCamera source, ProjectionCamera dest, double distance)
        {
            if (source == null || dest == null)
            {
                return;
            }

            Vector3D dir = source.LookDirection;
            dir.Normalize();
            dir *= distance;

            dest.LookDirection = dir;
            dest.Position = new Point3D(-dest.LookDirection.X, -dest.LookDirection.Y, -dest.LookDirection.Z);
            dest.UpDirection = source.UpDirection;
        }

        /// <summary>
        /// Creates a default perspective camera.
        /// </summary>
        /// <returns>A perspective camera.</returns>
        public static PerspectiveCamera CreateDefaultCamera()
        {
            var camera = new PerspectiveCamera();
            Reset(camera);
            return camera;
        }

        /// <summary>
        /// Set the camera target point without changing the look direction.
        /// </summary>
        /// <param name="camera">
        /// The camera.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="animationTime">
        /// The animation time.
        /// </param>
        public static void LookAt(PerspectiveCamera camera, Point3D target, double animationTime)
        {
            LookAt(camera, target, camera.LookDirection, animationTime);
        }

        /// <summary>
        /// Set the camera target point and look direction
        /// </summary>
        /// <param name="camera">
        /// The camera.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="newLookDirection">
        /// The new look direction.
        /// </param>
        /// <param name="animationTime">
        /// The animation time.
        /// </param>
        public static void LookAt(
            PerspectiveCamera camera, Point3D target, Vector3D newLookDirection, double animationTime)
        {
            Point3D newPosition = target - newLookDirection;

            AnimateTo(camera, newPosition, newLookDirection, camera.UpDirection, animationTime);
        }

        /// <summary>
        /// Set the camera target point and camera distance.
        /// </summary>
        /// <param name="camera">
        /// The camera.
        /// </param>
        /// <param name="target">
        /// The target point.
        /// </param>
        /// <param name="distance">
        /// The distance to the camera.
        /// </param>
        /// <param name="animationTime">
        /// The animation time.
        /// </param>
        public static void LookAt(PerspectiveCamera camera, Point3D target, double distance, double animationTime)
        {
            Vector3D d = camera.LookDirection;
            d.Normalize();
            LookAt(camera, target, d * distance, animationTime);
        }

        /// <summary>
        /// Resets the specified perspective camera.
        /// </summary>
        /// <param name="camera">
        /// The camera.
        /// </param>
        public static void Reset(PerspectiveCamera camera)
        {
            if (camera == null)
            {
                return;
            }

            camera.Position = new Point3D(0, 0, 10);
            camera.LookDirection = new Vector3D(0, 0, -10);
            camera.UpDirection = new Vector3D(0, 1, 0);
        }

        /// <summary>
        /// Zooms to fit the extents of the specified viewport.
        /// </summary>
        /// <param name="actualCamera">
        /// The actual camera.
        /// </param>
        /// <param name="viewport">
        /// The viewport.
        /// </param>
        /// <param name="animationTime">
        /// The animation time.
        /// </param>
        public static void ZoomExtents(PerspectiveCamera actualCamera, Viewport3D viewport, double animationTime)
        {
            var bounds = Visual3DHelper.FindBounds(viewport.Children);
            var diagonal = new Vector3D(bounds.SizeX, bounds.SizeY, bounds.SizeZ);

            if (bounds.IsEmpty || diagonal.LengthSquared == 0)
            {
                return;
            }

            ZoomExtents(actualCamera, viewport, bounds, animationTime);
        }

        /// <summary>
        /// Zooms to fit the specified bounding rectangle.
        /// </summary>
        /// <param name="actualCamera">
        /// The actual camera.
        /// </param>
        /// <param name="viewport">
        /// The viewport.
        /// </param>
        /// <param name="bounds">
        /// The bounding rectangle.
        /// </param>
        /// <param name="animationTime">
        /// The animation time.
        /// </param>
        public static void ZoomExtents(
            PerspectiveCamera actualCamera, Viewport3D viewport, Rect3D bounds, double animationTime)
        {
            var diagonal = new Vector3D(bounds.SizeX, bounds.SizeY, bounds.SizeZ);
            var center = bounds.Location + diagonal * 0.5;
            double radius = diagonal.Length * 0.5;
            ZoomExtents(actualCamera, viewport, center, radius, animationTime);
        }

        /// <summary>
        /// Zooms to fit the specified sphere.
        /// </summary>
        /// <param name="camera">
        /// The camera.
        /// </param>
        /// <param name="viewport">
        /// The viewport.
        /// </param>
        /// <param name="center">
        /// The center of the sphere.
        /// </param>
        /// <param name="radius">
        /// The radius of the sphere.
        /// </param>
        /// <param name="animationTime">
        /// The animation time.
        /// </param>
        public static void ZoomExtents(
            PerspectiveCamera camera, Viewport3D viewport, Point3D center, double radius, double animationTime)
        {
            double disth = radius / Math.Tan(0.5 * camera.FieldOfView * Math.PI / 180);
            double vfov = camera.FieldOfView / viewport.ActualWidth * viewport.ActualHeight;
            double distv = radius / Math.Tan(0.5 * vfov * Math.PI / 180);

            double dist = Math.Max(disth, distv);
            var dir = camera.LookDirection;
            dir.Normalize();
            LookAt(camera, center, dir * dist, animationTime);
        }
    }
}
