using Microsoft.Win32;
using Sketcher3D.GeometryEngine;
using System.Collections.Generic;
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
        private Triangulation tri = new Triangulation();
        PerspectiveCamera camera;

        // Mouse interaction state
        private System.Windows.Point lastMousePos;
        private bool isRotating = false;

        // Transforms
        private AxisAngleRotation3D rotX;
        private AxisAngleRotation3D rotY;
        private TranslateTransform3D zoomTransform;

        // Camera distance
        private double zoom = 200;

        public MainWindow()
        {
            InitializeComponent();
            Setup3D();
        }

        // 3D SETUP
        private void Setup3D()
        {
            camera = new PerspectiveCamera(); //WPF default coordinate system
            camera.Position = new Point3D(0, 0, 200); //placed on +Z
            camera.LookDirection = new Vector3D(0, 0, -1);
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.FieldOfView = 60;

            MainViewport.Camera = camera; //Assigns the camera to the viewport

            DirectionalLight light =
                new DirectionalLight(Colors.White, new Vector3D(0, 0, -1));

            scene.Children.Add(light);

            //transforms
            rotX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            rotY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            zoomTransform = new TranslateTransform3D(0, 0, 0);

            Transform3DGroup tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(rotX));
            tg.Children.Add(new RotateTransform3D(rotY));
            tg.Children.Add(zoomTransform);

            scene.Transform = tg;


            ModelVisual3D visual = new ModelVisual3D(); //It connects the 3D scene to the viewport
            visual.Content = scene;

            MainViewport.Children.Add(visual);
        }

        // ADD 3D SHAPE
        private void Add3DShape(Shape shape) // For shapes //Called when user clicks OK in dialog 
        {
            shapeManager.AddShape(shape);

            MeshGeometry3D mesh =
                MeshBuilder.FromTriangulation(shape.GetTriangulation());

            GeometryModel3D model = new GeometryModel3D();
            model.Geometry = mesh;
            model.Material =
                new DiffuseMaterial(new SolidColorBrush(Colors.Red));

            model.Transform = new Transform3DGroup(); 

            scene.Children.Add(model);
        }

        private void Add3DShape() //for LoadSTL
        {
            MeshGeometry3D mesh =
                MeshBuilder.FromTriangulation(tri);

            GeometryModel3D model = new GeometryModel3D();
            model.Geometry = mesh;
            model.Material =
                new DiffuseMaterial(new SolidColorBrush(Colors.LightBlue));

            model.Transform = new Transform3DGroup();

            scene.Children.Add(model);
        }

        private void Add3DShape(MeshGeometry3D mesh) //for transformations
        {
            GeometryModel3D model = new GeometryModel3D();
            model.Geometry = mesh;
            model.Material =
                new DiffuseMaterial(new SolidColorBrush(Colors.LightBlue));

            model.Transform = new Transform3DGroup();

            scene.Children.Add(model);
        }

        // BUTTON HANDLERS 
        private void Cuboid_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Cuboid");
        }

        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Cube");
        }

        private void Cone_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Cone");
        }

        private void Sphere_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Sphere");
        }

        private void Cylinder_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Cylinder");
        }

        private void Pyramid_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Pyramid");
        }

        private void OpenDialog(string type)
        {
            ShapeInputDlg dlg = new ShapeInputDlg(type); 

            if (dlg.ShowDialog() == true)
            {
                Add3DShape(dlg.GetShape()); // If user clicks OK: shape is returned
            }
        }
        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            OpenDialogTransform("Translate");
        }

        private void OpenDialogTransform(string type)
        {
            ShapeInputDlg dlg = new ShapeInputDlg(type);

            if (dlg.ShowDialog() == true)
            {
                List<double> pts = dlg.GetmTransformed();
                //List<double> norms = dlg.GetDoubleNormals();

                MeshGeometry3D mesh = MeshBuilder.GetMeshPoints(pts);
                Add3DShape(mesh); // If user clicks OK: shape is returned
            }
        }

        private void SaveSkt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Shapes";
            dlg.Filter = "Sketcher Files (*.skt)|*.skt";

            if (dlg.ShowDialog() != true)
                return;

            List<Shape> shapesVec = shapeManager.GetShapesVec();

            if (FileHandle.SaveToFile(dlg.FileName, shapesVec))
            {
                MessageBox.Show(
                    "Shapes saved in .skt format.",
                    "Save in .skt",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(
                    "Shapes not saved!",
                    "Not Saved",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void SaveGNU_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Save Shapes";
            dlg.Filter = "GNU Plot Data (*.dat)|*.dat";

            if (dlg.ShowDialog() != true)
                return;

            List<Shape> shapesVec = shapeManager.GetShapesVec();

            if (FileHandle.SaveToFileGNUPlot(dlg.FileName, shapesVec))
            {
                MessageBox.Show("Shapes saved for GNU.", "Save for GNU", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Shapes not saved!", "Not Saved", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadSTL_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open STL";
            dlg.Filter = "STL Files (*.stl)|*.stl";

            if (dlg.ShowDialog() != true)
                return;

            FileHandle.ReadSTL(dlg.FileName, tri);

            if (tri.GetPointsDoubleData().Count != 0)
            {
                Add3DShape();
                MessageBox.Show("Shapes loaded and rendered in 3D viewer!", "Load", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Shapes not loaded!", "Not Loaded", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            //scene.Children.Clear();
            shapeManager.ClearShape();
            
        }

        private void MainViewport_PreviewMouseDown(object sender, MouseButtonEventArgs e) //start rotation
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                lastMousePos = e.GetPosition(MainViewport);
                isRotating = true;
                Mouse.Capture(MainViewport);
            }
        }

        private void MainViewport_PreviewMouseUp(object sender, MouseButtonEventArgs e) //stop rotaion
        {
            isRotating = false;
            Mouse.Capture(null);
        }


        private void MainViewport_PreviewMouseMove(object sender, MouseEventArgs e) //rotation
        {
            if (!isRotating)
                return;

            System.Windows.Point currentPos = e.GetPosition(MainViewport);
            Vector delta = currentPos - lastMousePos;

            rotY.Angle += delta.X * 0.5;   // horizontal drag
            rotX.Angle += delta.Y * 0.5;   // vertical drag

            lastMousePos = currentPos;
        }

        private void MainViewport_MouseWheel(object sender, MouseWheelEventArgs e) //mouse wheel
        {
            if (e.Delta > 0)
                zoom -= 5;
            else
                zoom += 5;

            if (zoom < 20) zoom = 20;
            if (zoom > 300) zoom = 300;

            zoomTransform.OffsetZ = zoom - 100;
        }

       
    }
}

