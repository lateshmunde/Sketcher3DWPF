using Sketcher3D.GeometryEngine;
using System.Windows;
using System.Windows.Controls;

namespace Sketcher3D
{
    public partial class ShapeInputDlg : Window
    {
        private Shape CreatedShape;
        //public Shape CreatedShape;

        private string shapeType;

        private TextBox nameBox;
        private TextBox aBox; // length, radius, side
        private TextBox bBox; // width
        private TextBox cBox; // heig

        public ShapeInputDlg(string type)
        {
            InitializeComponent();
            shapeType = type;
            BuildUI();
        }

        public Shape GetShape()
        {
            return CreatedShape;
        }

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
        }

        private TextBox Add(string label, string def)
        {
            int row = CentralGrid.RowDefinitions.Count;
            CentralGrid.RowDefinitions.Add(new RowDefinition());

            Label lbl = new Label();
            lbl.Content = label;
            lbl.Margin = new Thickness(5);

            Grid.SetRow(lbl, row);
            CentralGrid.Children.Add(lbl);

            TextBox tb = new TextBox();
            tb.Text = def;
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
                    CreatedShape = new Cuboid(
                        name,
                        double.Parse(aBox.Text),
                        double.Parse(bBox.Text),
                        double.Parse(cBox.Text));
                }
                else if (shapeType == "Cube")
                {
                    CreatedShape = new Cube(
                        name,
                        double.Parse(aBox.Text));
                }
                else if (shapeType == "Sphere")
                {
                    CreatedShape = new Sphere(
                        name,
                        double.Parse(aBox.Text));
                }
                else if (shapeType == "Cylinder")
                {
                    CreatedShape = new Cylinder(
                        name,
                        double.Parse(aBox.Text),
                        double.Parse(cBox.Text));
                }
                else if (shapeType == "Cone")
                {
                    CreatedShape = new Cone(
                        name,
                        double.Parse(aBox.Text),
                        double.Parse(cBox.Text));
                }
                else if (shapeType == "Pyramid")
                {
                    CreatedShape = new Pyramid(
                        name,
                        double.Parse(aBox.Text),
                        double.Parse(bBox.Text),
                        double.Parse(cBox.Text));
                }

                DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Invalid numeric input");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
