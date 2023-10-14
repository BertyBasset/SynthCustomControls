using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Testbed {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            knob1.ValueChanged += (o, e) => {
                switch ((int)e) {
                    case 0: txtText1.Text = "-2"; break;
                    case 1: txtText1.Text = "-1"; break;
                    case 2: txtText1.Text = "0"; break;
                    case 3: txtText1.Text = "+1"; break;
                    case 4: txtText1.Text = "+2"; break;
                    default:
                        break;
                }

                if (e >=  2)
                    Led1.LedOn = true;
                else
                    Led1.LedOn = false;

            };

            knob2.ValueChanged += (o, e) => {
                txtText2.Text = $"{e:F3}";
                if (e > 5)
                    Led2.LedOn = true;
                else
                    Led2.LedOn = false;
            };

            knob3.ValueChanged += (o, e) => {
                switch ((int)e) {
                    case 0: txtText3.Text = "None"; break;
                    case 1: txtText3.Text = "Reverb"; break;
                    case 2: txtText3.Text = "Echo"; break;
                    case 3: txtText3.Text = "Delay"; break;
                    case 4: txtText3.Text = "Comb"; break;
                    default:
                        break;
                }
                if (e >= 2)
                    Led3.LedOn = true;
                else
                    Led3.LedOn = false;
            };

            knob4.ValueChanged += (o, e) => {
                switch ((int)e) {
                    case 0: txtText4.Text = "Saw"; break;
                    case 1: txtText4.Text = "Sine"; break;
                    case 2: txtText4.Text = "Triangle"; break;
                    case 3: txtText4.Text = "Square"; break;
                    case 4: txtText4.Text = "SuperSaw"; break;
                    default:
                        break;
                }
                if (e >= 2)
                    Led4.LedOn = true;
                else
                    Led4.LedOn = false;
            };
        }
    }
}
