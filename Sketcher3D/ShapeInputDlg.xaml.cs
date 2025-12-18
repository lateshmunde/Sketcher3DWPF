using Sketcher3D.GeometryEngine;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sketcher3D
{
    public partial class ShapeInputDlg : Window
    {
        private Shape CreatedShape;
        private string shapeType;
        private string transfrom;

        private TextBox nameBox;
        private TextBox aBox; // length, radius, side
        private TextBox bBox; // width
        private TextBox cBox; // height

        private List<double> mTransformed = new List<double>();

        public ShapeInputDlg(string type)
        {
            InitializeComponent();
            shapeType = type;
            BuildUI();
        }

        public Shape GetShape() => CreatedShape;
        public List<double> GetmTransformed() => mTransformed;

        private void BuildUI()
        {
            CentralGrid.Children.Clear();
            CentralGrid.RowDefinitions.Clear();

            nameBox = Add("Name", "Default");

            if (shapeType == "Cuboid")
            {
                aBox = Add("Length", "10");
                bBox = Add("Width", "10");
                cBox = Add("Height", "10");
            }
            else if (shapeType == "Cube")
            {
                aBox = Add("Side", "10");
            }
            else if (shapeType == "Sphere")
            {
                aBox = Add("Radius", "10");
            }
            else if (shapeType == "Cylinder" || shapeType == "Cone")
            {
                aBox = Add("Radius", "10");
                cBox = Add("Height", "10");
            }
            else if (shapeType == "Pyramid")
            {
                aBox = Add("Base Length", "10");
                bBox = Add("Base Width", "10");
                cBox = Add("Height", "10");
            }

            if (shapeType == "Translate")
            {
                aBox = Add("TranslateX", "1");
                bBox = Add("TranslateY", "1");
                cBox = Add("TranslateZ", "1");
            }

        }

        private TextBox Add(string label, string value)
        {
            int row = CentralGrid.RowDefinitions.Count;
            CentralGrid.RowDefinitions.Add(new RowDefinition());

            Label lbl = new Label();
            lbl.Content = label;
            lbl.Margin = new Thickness(5);

            Grid.SetRow(lbl, row);
            CentralGrid.Children.Add(lbl); //Places label in grid row

            TextBox tb = new TextBox();
            tb.Text = value;
            tb.Margin = new Thickness(5);

            Grid.SetRow(tb, row);
            Grid.SetColumn(tb, 1);

            CentralGrid.Children.Add(tb);
            return tb;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;

            try
            {
                if (shapeType == "Cuboid")
                {
                    CreatedShape = new Cuboid(name, double.Parse(aBox.Text), double.Parse(bBox.Text), double.Parse(cBox.Text));
                }
                else if (shapeType == "Cube")
                {
                    CreatedShape = new Cube(name, double.Parse(aBox.Text));
                }
                else if (shapeType == "Sphere")
                {
                    CreatedShape = new Sphere(name, double.Parse(aBox.Text));
                }
                else if (shapeType == "Cylinder")
                {
                    CreatedShape = new Cylinder(name, double.Parse(aBox.Text), double.Parse(cBox.Text));
                }
                else if (shapeType == "Cone")
                {
                    CreatedShape = new Cone(name, double.Parse(aBox.Text), double.Parse(cBox.Text));
                }
                else if (shapeType == "Pyramid")
                {
                    CreatedShape = new Pyramid(name, double.Parse(aBox.Text), double.Parse(bBox.Text), double.Parse(cBox.Text));
                }
                else if (shapeType == "Translate")
                {
                    ShapeManager shapeManager = new ShapeManager();
                    Shape shape = shapeManager.GetLastShape();
                    //double tx = double.Parse(aBox.Text);
                    //double ty = double.Parse(bBox.Text);
                    //double tz = double.Parse(cBox.Text);

                    List<double> vec = shape.GetTriangulation().GetPointsDoubleData();
                    mTransformed = Transformations.Translate(vec, double.Parse(aBox.Text), double.Parse(bBox.Text), double.Parse(cBox.Text));
                }

                DialogResult = true; //Closes dialog , Signals success
            }
            catch
            {
                MessageBox.Show("Invalid numeric input");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; //Closes dialog, Shape is discarded
        }
    }
}
