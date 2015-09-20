using System;
using Windows.Devices.Gpio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace RadioRepeater
{
    public class Repeater
    {

        // used to transfer I/O info
        private GpioPin channel;
        private GpioPinValue channelValue;

        // setup all of the timers
        private DispatcherTimer main;
        private DispatcherTimer CORTimeout;
        private DispatcherTimer CWIDTimeout;
        private DispatcherTimer CWIDPulse;

        // make the RXCORpin a public item
        private int RXCORPinField = 1;

        public int RXCORPin
        {
            get
            {
                return RXCORPinField;
            }
            set
            {
                RXCORPinField = value;
            }
        }

        // make the RXCTCSSPin a public item
        private int RXCTCSSPinField = 2;

        public int RXCTCSSPin
        {
            get
            {
                return RXCTCSSPinField;
            }
            set
            {
                RXCTCSSPinField = value;
            }
        }

        // make the TXPTTPin a public item
        private int TXPTTPinField = 3;

        public int TXPTTPin
        {
            get
            {
                return TXPTTPinField;
            }
            set
            {
                TXPTTPinField = value;
            }
        }

        // make the TXCTCSSPin a public item
        private int TXCTCSSPinField = 4;

        public int TXCTCSSPin
        {
            get
            {
                return TXCTCSSPinField;
            }
            set
            {
                TXCTCSSPinField = value;
            }
        }

        // make the TXCWIDPin a public item
        private int TXCWIDPinField = 5;

        public int TXCWIDPin
        {
            get
            {
                return TXCWIDPinField;
            }
            set
            {
                TXCWIDPinField = value;
            }
        }




        // make the RXCORActive a public item
        private bool RXCORActiveField = false;

        public bool RXCORActive
        {
            get
            {
                return RXCORActiveField;
            }
            set
            {
                RXCORActiveField = value;
            }
        }

        // make the RXCORTimeout a public item
        private TimeSpan RXCORTimeoutField = TimeSpan.FromMinutes(3);

        public TimeSpan RXCORTimeout
        {
            get
            {
                return RXCORTimeoutField;
            }
            set
            {
                RXCORTimeoutField = value;
            }
        }

        // make the RXCTCSSActive a public item
        private bool RXCTCSSActiveField = false;

        public bool RXCTCSSActive
        {
            get
            {
                return RXCTCSSActiveField;
            }
            set
            {
                RXCTCSSActiveField = value;
            }
        }

        // make the TXPTTActive a public item
        private bool TXPTTActiveField = false;

        public bool TXPTTActive
        {
            get
            {
                return TXPTTActiveField;
            }
            set
            {
                TXPTTActiveField = value;
            }
        }

        // make the TXCTCSSActive a public item
        private bool TXCTCSSActiveField = false;

        public bool TXCTCSSActive
        {
            get
            {
                return TXCTCSSActiveField;
            }
            set
            {
                TXCTCSSActiveField = value;
            }
        }

        // make the TXCWIDActive a public item
        private bool TXCWIDActiveField = false;

        public bool TXCWIDActive
        {
            get
            {
                return TXCWIDActiveField;
            }
            set
            {
                TXCWIDActiveField = value;
            }
        }

        // make TXCTCSSHang a public item
        private TimeSpan TXCTCSSHangField = TimeSpan.FromMilliseconds(100);

        public TimeSpan TXCTCSSHang
        {
            get
            {
                return TXCTCSSHangField;
            }
            set
            {
                TXCTCSSHangField = value;
            }
        }

       // make TXCWIDPulse a public item
       private TimeSpan TXCWIDPulseField = TimeSpan.FromMinutes(9.75);

       public TimeSpan TXCWIDPulse
       {
           get
           {
               return TXCWIDPulseField;
           }
           set
           {
               TXCWIDPulseField = value;
           }
       }


       public bool runrepeater()
       {
            // Initialize the GPIO
            InitGPIO();

            // Go back to calling program, system is run off of timers from here
            return true;
       }


        // Initialize the GPIO 
        public void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                channel = null;
                return;
            }

            // Set up the input RXCOR line
            channel = gpio.OpenPin(RXCORPin);
// sdj  channel.SetDriveMode( GpioPinEdge.RisingEdge);

            // Set up the input RXCTCSS line
            channel = gpio.OpenPin(RXCTCSSPin);
// fsdf channel.SetDriveMode(GpioPinEdge.RisingEdge);

            // Turn off TXPTT to start
            channel = gpio.OpenPin(TXPTTPin);
            if (TXPTTActive)
            { 
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            channel.Write(channelValue);
            channel.SetDriveMode(GpioPinDriveMode.Output);

            // Turn off CTCSS to start
            channel = gpio.OpenPin(TXCTCSSPin);
            if (TXCTCSSActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            channel.Write(channelValue);
            channel.SetDriveMode(GpioPinDriveMode.Output);

            // Turn off CWID to start
            channel = gpio.OpenPin(TXCWIDPin);
            if (TXCWIDActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            channel.Write(channelValue);
            channel.SetDriveMode(GpioPinDriveMode.Output);

        }

    }

}
