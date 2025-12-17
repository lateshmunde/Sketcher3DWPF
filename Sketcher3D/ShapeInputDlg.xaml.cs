using Sketcher3D.GeometryEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sketcher3D
{
    /// <summary>
    /// Interaction logic for ShapeInputDlg.xaml
    /// </summary>
    public partial class ShapeInputDlg : Window
    {
        public ShapeInputDlg()
        {
            InitializeComponent();
        }

        private void OkButton_Cuboid_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(len.Text, out double l) ||
            !double.TryParse(wid.Text, out double w) ||
            !double.TryParse(ht.Text, out double h))
            {
                MessageBox.Show("Enter valid numbers");
                return;
            }
            string namCb = name.Text;
            double length = l;
            double width = w;
            double height = h;

            Cuboid nam = new Cuboid(namCb, length, width, height);
            ShapeManager smgr = new ShapeManager();
            smgr.AddShape(nam);

            FileHandle.SaveToFile("latesh.skt", smgr.GetShapesVec());


            Close();
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cancel making Shape!");
            Close();
        }
    }
}


