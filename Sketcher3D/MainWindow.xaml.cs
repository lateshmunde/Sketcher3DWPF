using Sketcher3D.GeometryEngine;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Sketcher3D
{
    public partial class MainWindow : Window
    {
        private ShapeManager shapeManager = new ShapeManager();
        private Model3DGroup scene = new Model3DGroup();

        public MainWindow()
        {
            InitializeComponent();
            Setup3D();
        }

        // 3D SETUP 
        private void Setup3D()
        {
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = new Point3D(0, 0, 500);
            camera.LookDirection = new Vector3D(0, 0, -1);
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.FieldOfView = 60;

            MainViewport.Camera = camera;

            DirectionalLight light =
                new DirectionalLight(Colors.White, new Vector3D(-1, -1, -2));

            scene.Children.Add(light);

            ModelVisual3D visual = new ModelVisual3D();
            visual.Content = scene;

            MainViewport.Children.Add(visual);
        }

        // ADD SHAPE
        private void AddShapes(Shape shape)
        {
            shapeManager.AddShape(shape);

            MeshGeometry3D mesh =
                MeshBuilder.FromTriangulation(shape.GetTriangulation());

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
                AddShapes(dlg.CreatedShape);
            }
        }
    }
}
