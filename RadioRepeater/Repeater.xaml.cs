using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RadioRepeater
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Repeater : Page
    {
        public Repeater()
        {
            this.InitializeComponent();
        }


        private SolidColorBrush redDot = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush greenDot = new SolidColorBrush(Windows.UI.Colors.Green);


        // used to transfer I/O info
        private GpioPin RXCORChannel;
        private GpioPin RXCTCSSChannel;
        private GpioPin TXPTTChannel;
        private GpioPin TXCTCSSChannel;
        private GpioPin TXCWIDChannel;
        private GpioPinValue channelValue;

        // setup all of the timers
        private DispatcherTimer CORTimeout;
        private DispatcherTimer CWIDTimeout;
        private DispatcherTimer CWIDPulse;

        // synthesized RX signal (rxcor and rxctcss)
        bool rx = false;
        bool rxcor = false;
        bool rxctcss = false;

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

        // make TXCWIDPulse a public item
        private TimeSpan TXCWIDPulseField = TimeSpan.FromMilliseconds(100);

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


        // make TXCWIDTimeout a public item
        private TimeSpan TXCWIDTimeoutField = TimeSpan.FromMinutes(9.75);

        public TimeSpan TXCWIDTimeout
        {
            get
            {
                return TXCWIDTimeoutField;
            }
            set
            {
                TXCWIDTimeoutField = value;
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
                RXCORChannel = null;
                return;
            }

            // Set up the input RXCOR line
            RXCORChannel = gpio.OpenPin(RXCORPin);
            RXCORChannel.SetDriveMode(GpioPinDriveMode.Input);
            RXCORChannel.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            RXCORChannel.ValueChanged += RXCORPin_ValueChanged;


            // Set up the input RXCTCSS line
            RXCTCSSChannel = gpio.OpenPin(RXCTCSSPin);
            RXCTCSSChannel.SetDriveMode(GpioPinDriveMode.Input);
            RXCTCSSChannel.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            RXCTCSSChannel.ValueChanged += RXCTCSSPin_ValueChanged;


            // Set up, and turn off TXPTT to start
            TXPTTChannel = gpio.OpenPin(TXPTTPin);
            if (TXPTTActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            TXPTTChannel.Write(channelValue);
            TXPTTChannel.SetDriveMode(GpioPinDriveMode.Output);


            // Set up, and turn off CTCSS to start
            TXCTCSSChannel = gpio.OpenPin(TXCTCSSPin);
            if (TXCTCSSActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            TXCTCSSChannel.Write(channelValue);
            TXCTCSSChannel.SetDriveMode(GpioPinDriveMode.Output);


            // Set up, and turn off CWID to start
            TXCWIDChannel = gpio.OpenPin(TXCWIDPin);
            if (TXCWIDActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            TXCWIDChannel.Write(channelValue);
            TXCWIDChannel.SetDriveMode(GpioPinDriveMode.Output);
        }

        /// <summary>
        /// This is the Event from the change in COR line. If the CTCSS line is active, 
        /// then the TX should be turned on, and the CTCSS should be turned on, and 
        /// the timeout timer should be started, if it's not running. 
        /// On unkey, the TX and CTCSS should be turned off. The timeout timer should 
        /// be stopped.
        /// Also, update the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>


        private void RXCORPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.RisingEdge)
            {
                if (RXCORActive)
                { // on
                    rxcor = true;
                }
                else
                { // off
                    rxcor = false;
                }
            }
            else
            {
                if (!RXCORActive)
                { // on
                    rxcor = true;
                }
                else
                { // off
                    rxcor = false;
                }
            }

            // Process each case
            if (rxctcss)
            {
                rx = true;
                TXPTTOn();
                TXCTCSSOn();
            }
            else
            {
                rx = false;
                TXPTTOff();
                TXCTCSSOff();

            }

            // Update display 
            if (rxcor)
            {
                // Output the time, and change to green.
                RXCORText.Text = "Last RXCOR on: " + DateTime.Now;
                RXCOR.Fill = greenDot;
                // Output the time, and change to green
                if (rx)
                {
                    // Output the time, and change to green.
                    RXText.Text = "Last RX on: " + DateTime.Now;
                    RX.Fill = greenDot;
                }
            }
            else
            {
                // Output the time, and change to red.
                RXCORText.Text = "Last RXCOR off: " + DateTime.Now;
                RXCOR.Fill = redDot;
                // Output the time, and change to red.
                RXText.Text = "Last RX off: " + DateTime.Now;
                RX.Fill = redDot;
            }
        }

        /// <summary>
        /// This is the event from the inbound CTCSS line. If the COR line is active, 
        /// then the TX should be turned on, and the CTCSS should be turned on, and 
        /// the timeout timer should be started, if it's not running.
        /// On unkey, the TX and CTCSS should be turned off. 
        /// Also, update the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>

        private void RXCTCSSPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.RisingEdge)
            {
                if (RXCTCSSActive)
                { // on
                    rxctcss = true;
                }
                else
                { // off
                    rxctcss = false;
                }
            }
            else
            {
                if (!RXCTCSSActive)
                { // on
                    rxctcss = true;
                }
                else
                { // off
                    rxctcss = false;
                }
            }

            // Process each case
            if (rxcor)
            {
                rx = true;
                TXPTTOn();
                TXCTCSSOn();
            }
            else
            {
                rx = false;
                TXPTTOff();
                TXCTCSSOff();

            }

            // Update display 
            if (rxctcss)
            {
                // Output the time, and change to green.
                RXCTCSSText.Text = "Last RXCTCSS on: " + DateTime.Now;
                RXCTCSS.Fill = greenDot;
                // Output the time, and change to green
if (rx) {
                    // Output the time, and change to green.
                    RXText.Text = "Last RX on: " + DateTime.Now;
                    RX.Fill = greenDot;
                }
            } else
            {
                // Output the time, and change to red.
                RXCTCSSText.Text = "Last RXCTCSS off: " + DateTime.Now;
                RXCTCSS.Fill = redDot;
                // Output the time, and change to red.
                RXText.Text = "Last RX off: " + DateTime.Now;
                RX.Fill = redDot;
            }
        }

        /// <summary>
        /// This will turn off the TX radio and update the display.
        /// </summary>

        private void TXPTTOff()
        {
            if (TXPTTActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            TXPTTChannel.Write(channelValue);
            TXPTTChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Output the time, and change to red.
            TXPTTText.Text = "Last TXPTT off: " + DateTime.Now;
            TXPTT.Fill = redDot;
        }

        /// <summary>
        /// This will turn on the TX 
        /// The CW ID timer must be started, if it isn't running.
        /// Also, update the display.
        /// </summary>

        private void TXPTTOn()
        {
            if (TXPTTActive)
            {
                channelValue = GpioPinValue.High;
            }
            else
            {
                channelValue = GpioPinValue.Low;
            }
            TXPTTChannel.Write(channelValue);
            TXPTTChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Setup the CWID timer, if not already running
            CWIDTimerStart();

            // Output the time, and change to green.
            TXPTTText.Text = "Last TXPTT on: " + DateTime.Now;
            TXPTT.Fill = greenDot;

        }


        /// <summary>
        /// This will turn off the CTCSS tones when the RX signal goes off and 
        /// update the display.
        /// </summary>

        private void TXCTCSSOff()
        {
            if (TXCTCSSActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }
            TXCTCSSChannel.Write(channelValue);
            TXCTCSSChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Output the time, and change to red.
            TXCTCSSText.Text = "Last TXCTCSS off: " + DateTime.Now;
            TXCTCSS.Fill = redDot;

        }


        /// <summary>
        /// This will turn on the CTCSS encoder when the RX signal comes on
        /// and update the display.
        /// </summary>

        private void TXCTCSSOn()
        {
            if (TXCTCSSActive)
            {
                channelValue = GpioPinValue.High;
            }
            else
            {
                channelValue = GpioPinValue.Low;
            }
            TXCTCSSChannel.Write(channelValue);
            TXCTCSSChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Output the time, and change to green.
            TXCTCSSText.Text = "Last TXCTCSS on: " + DateTime.Now;
            TXCTCSS.Fill = greenDot;

        }

        /// <summary>
        /// This starts a CWID cycle.
        /// First, turn on the id line.
        /// Second, clear out the ID timer.
        /// Third, if the RX is on, restart the ID timer.
        /// Fourth, start the CWID Pulse timer (this is what turns off the CWID line)
        /// and, update the display.
        /// </summary>

        private void CWIDTimeout_Tick(object sender, object e)
        {
            // fire off the CW ID device
            if (TXCWIDActive)
            {
                channelValue = GpioPinValue.High;
            }
            else
            {
                channelValue = GpioPinValue.Low;
            }
            TXCWIDChannel.Write(channelValue);
            TXCWIDChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Setup the CWID pulse, to turn off the CWID'er
            CWIDPulse = new DispatcherTimer();
            TimeSpan off = TXCWIDPulse;
            CWIDPulse.Interval = off;
            CWIDPulse.Tick += CWIDPulse_Tick;
            CWIDPulse.Start();

            // if the RX is still active, restart the timer
            if (rx)
            {
                // Setup the CWID timer
                CWIDTimerStart();
            }
            else
            { // shut off the timer
                CWIDTimeout.Stop();
            }

            // Output the time, and change to red.
            TXCWIDText.Text = "Last CWID on: " + DateTime.Now;
            TXCWID.Fill = greenDot;
        }

        /// <summary>
        /// Timer event for COR timeout, turn off tx and ctcss and update display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CORTimeout_Tick(object sender, object e)
        {
            // turn off TX
            TXPTTOff();
            TXCTCSSOff();

            // turn off timer
            CORTimeout.Stop();

            // Output the time, and change to red.
            TXPTTText.Text = "Last PTT timeout: " + DateTime.Now;
            TXPTT.Fill = redDot;
            TXCTCSSText.Text = "Last TXCTCSS timeout: " + DateTime.Now;
            TXCTCSS.Fill = redDot;
        }



        /// <summary>
        /// Timer to turn off the CWID timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CWIDPulse_Tick(object sender, object e)
        {
            // Turn off the pulse timer
            CWIDPulse.Stop();

            // turn off the CW ID device
            if (TXCWIDActive)
            {
                channelValue = GpioPinValue.Low;
            }
            else
            {
                channelValue = GpioPinValue.High;
            }

            // Output the time, and change to red.
            TXCWIDText.Text = "Last CWID off: " + DateTime.Now;
            TXCWID.Fill = redDot;
        }

        /// <summary>
        /// This will restart the CWID timer, if it's not already started
        /// </summary>
        private void CWIDTimerStart()
        {
            // Setup the CWID timer
            if (!CWIDTimeout.IsEnabled)
            {
                CWIDTimeout = new DispatcherTimer();
                TimeSpan off = TXCWIDTimeout;
                CWIDTimeout.Interval = off;
                CWIDTimeout.Tick += CWIDTimeout_Tick;
                CWIDTimeout.Start();
            }
        }


        /// <summary>
        /// This will restart the COR timer, if it's not already started
        /// </summary>
        private void CORTimerStart()
        {
            // Setup the RXCOR timer
            if (!CORTimeout.IsEnabled)
            {
                CORTimeout = new DispatcherTimer();
                TimeSpan off = RXCORTimeout;
                CORTimeout.Interval = off;
                CORTimeout.Tick += CORTimeout_Tick;
                CORTimeout.Start();
            }
        }



    }
}

