using Sketcher3D.GeometryEngine;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Sketcher3D
{
    public partial class MainWindow : Window
    {
        private ShapeManager shapeManager = new ShapeManager();
        private Model3DGroup scene = new Model3DGroup();

        // ================= CAMERA =================
        private PerspectiveCamera camera;

        private System.Windows.Point lastMousePos;
        private bool isRotating = false;

        private double yaw = 0;      // left-right
        private double pitch = 0;    // up-down
        private double distance = 500;

        private Point3D target = new Point3D(0, 0, 0);
        // ==========================================

        public MainWindow()
        {
            InitializeComponent();
            Setup3D();
        }

        // ================= CLAMP =================
        private static double Clamp(double v, double min, double max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }

        // ================= SETUP =================
        private void Setup3D()
        {
            camera = new PerspectiveCamera
            {
                FieldOfView = 60,
                UpDirection = new Vector3D(0, 1, 0)
            };

            UpdateCamera();
            MainViewport.Camera = camera;

            DirectionalLight light =
                new DirectionalLight(Colors.White, new Vector3D(-1, -1, -2));
            scene.Children.Add(light);

            ModelVisual3D visual = new ModelVisual3D
            {
                Content = scene
            };
            MainViewport.Children.Add(visual);

            // Mouse events
            MainViewport.MouseDown += Viewport_MouseDown;
            MainViewport.MouseUp += Viewport_MouseUp;
            MainViewport.MouseMove += Viewport_MouseMove;
            MainViewport.MouseWheel += Viewport_MouseWheel;
        }

        // ================= ADD SHAPE =================
        private void AddShapes(Shape shape)
        {
            shapeManager.AddShape(shape);

            MeshGeometry3D mesh =
                MeshBuilder.FromTriangulation(shape.GetTriangulation());

            GeometryModel3D model = new GeometryModel3D
            {
                Geometry = mesh,
                Material = new DiffuseMaterial(
                    new SolidColorBrush(Colors.LightBlue))
            };

            scene.Children.Add(model);
        }

        // ================= INPUT =================
        private void Viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isRotating = true;
                lastMousePos = e.GetPosition(this);
            }
        }

        private void Viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isRotating = false;
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isRotating) return;

            System.Windows.Point currentPos = e.GetPosition(this);
            Vector delta = currentPos - lastMousePos;
            lastMousePos = currentPos;

            yaw += delta.X * 0.3;
            pitch -= delta.Y * 0.3;

            pitch = Clamp(pitch, -89, 89);

            UpdateCamera();
        }

        private void Viewport_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            distance -= e.Delta * 0.3;
            distance = Clamp(distance, 50, 3000);
            UpdateCamera();
        }

        // ================= CAMERA LOGIC =================
        private void UpdateCamera()
        {
            double yawRad = yaw * Math.PI / 180.0;
            double pitchRad = pitch * Math.PI / 180.0;

            double x = distance * Math.Cos(pitchRad) * Math.Sin(yawRad);
            double y = distance * Math.Sin(pitchRad);
            double z = distance * Math.Cos(pitchRad) * Math.Cos(yawRad);

            camera.Position = new Point3D(
                target.X + x,
                target.Y + y,
                target.Z + z);

            camera.LookDirection = target - camera.Position;
        }

        // ================= BUTTONS =================
        private void Cuboid_Click(object sender, RoutedEventArgs e) => OpenDialog("Cuboid");
        private void Cube_Click(object sender, RoutedEventArgs e) => OpenDialog("Cube");
        private void Cone_Click(object sender, RoutedEventArgs e) => OpenDialog("Cone");
        private void Sphere_Click(object sender, RoutedEventArgs e) => OpenDialog("Sphere");
        private void Cylinder_Click(object sender, RoutedEventArgs e) => OpenDialog("Cylinder");
        private void Pyramid_Click(object sender, RoutedEventArgs e) => OpenDialog("Pyramid");

        private void OpenDialog(string type)
        {
            ShapeInputDlg dlg = new ShapeInputDlg(type);
            if (dlg.ShowDialog() == true)
                AddShapes(dlg.GetShape());
        }
    }
}