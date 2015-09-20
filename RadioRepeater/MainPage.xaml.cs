using System;
using Windows.Devices.Gpio;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RadioRepeater
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private SolidColorBrush redDot = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush blackDot = new SolidColorBrush(Windows.UI.Colors.Black);

        public MainPage()
        {
            this.InitializeComponent();
            Repeater link = new Repeater();
            link.RXCORTimeout = TimeSpan.FromMinutes(.5);
            link.TXCWIDPulse = TimeSpan.FromMilliseconds(100);
            bool runok = link.runrepeater();

        }
    }
}
