﻿using System;
using Windows.Devices.Gpio;
using System.Threading;
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
using Windows.UI.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RadioRepeater
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // setup all of the timers
        public static DispatcherTimer CORTimeout;
        public static DispatcherTimer CWIDTimeout;
        public static DispatcherTimer CWIDPulse;




        private SolidColorBrush redDot = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush greenDot = new SolidColorBrush(Windows.UI.Colors.Green);


        // used to transfer I/O info
        private GpioPin RXCORChannel;
        private GpioPin RXCTCSSChannel;
        private GpioPin TXPTTChannel;
        private GpioPin TXCTCSSChannel;
        private GpioPin TXCWIDChannel;
        private GpioPinValue channelValue;

        // synthesized RX signal (rxcor and rxctcss)
        bool rx = false;
        bool rxcor = false;
        bool rxctcss = false;

        public MainPage()
        {

            // Set up the timers
            CORTimeout = new DispatcherTimer();
            CWIDTimeout = new DispatcherTimer();
            CWIDPulse = new DispatcherTimer();

            var dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;

            this.InitializeComponent();

            // Initialize the GPIO
            InitGPIO();



        }

        // make the RXCORpin a public item
        private int RXCORPinField = 5;

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
        private int RXCTCSSPinField = 6;

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
        private int TXPTTPinField = 12;

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
        private int TXCTCSSPinField = 13;

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
        private int TXCWIDPinField = 16;

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

        // Initialize the GPIO 
        public async void InitGPIO()
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXText.Text = "RX Startup at: " + DateTime.Now;
                RX.Fill = redDot;
            });

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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXCORText.Text = "RXCOR Startup: " + DateTime.Now;
                RXCOR.Fill = redDot;
            });

            //            RXCORChannel.ValueChanged += RXCORPin_ValueChanged;


            // Set up the input RXCTCSS line
            RXCTCSSChannel = gpio.OpenPin(RXCTCSSPin);
            RXCTCSSChannel.SetDriveMode(GpioPinDriveMode.Input);
            RXCTCSSChannel.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXCTCSSText.Text = "RXCTCSS Startup: " + DateTime.Now;
                RXCTCSS.Fill = redDot;
            });

            //   RXCTCSSChannel.ValueChanged += RXCTCSSPin_ValueChanged;


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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXPTTText.Text = "TXPTT Startup: " + DateTime.Now;
                TXPTT.Fill = redDot;
            });

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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCTCSSText.Text = "TXCTCSS Startup: " + DateTime.Now;
                TXCTCSS.Fill = redDot;
            });

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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCWIDText.Text = "TXCWID Startup: " + DateTime.Now;
                TXCWID.Fill = redDot;
            });

            TXCWIDChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Wait until everything stabilizes, and then start the RX triggers
            RXCORChannel.ValueChanged += RXCORPin_ValueChanged;
            RXCTCSSChannel.ValueChanged += RXCTCSSPin_ValueChanged;


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


        private async void RXCORPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
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


            // Bring up transmitter,
            if (rxcor & rxctcss)
            {
                rx = true;
                //xx CORTimerStart();
                TXPTTOn();
                TXCTCSSOn();
            }
            else
            {
                rx = false;
                //x    CORTimerStop();
                TXPTTOff();
                TXCTCSSOff();

            }

            // Update display 
            if (rxcor)
            {
                // Output the time, and change to green.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXCORText.Text = "Last RXCOR on: " + DateTime.Now;
                    RXCOR.Fill = greenDot;
                });
            }
            else
            {
                // Output the time, and change to red.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXCORText.Text = "Last RXCOR off: " + DateTime.Now;
                    RXCOR.Fill = redDot;
                });
            }

            // Output the time, and change to green
            if (rx)
            {
                // Output the time, and change to green.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXText.Text = "Last RX on: " + DateTime.Now;
                    RX.Fill = greenDot;
                });
            }
            else
            {
                // Output the time, and change to red.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXText.Text = "Last RX off: " + DateTime.Now;
                    RX.Fill = redDot;
                });
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

        private async void RXCTCSSPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
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

            // Bring up the transmitter, if both are true
            if (rxcor & rxctcss)
            {
                rx = true;
                //x      CORTimerStart();
                TXPTTOn();
                TXCTCSSOn();
            }
            else
            {
                rx = false;
                //x  CORTimerStop();
                TXPTTOff();
                TXCTCSSOff();

            }

            // Update display 
            if (rxctcss)
            {
                // Output the time, and change to green.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXCTCSSText.Text = "Last RXCTCSS on: " + DateTime.Now;
                    RXCTCSS.Fill = greenDot;
                });
            }
            else
            {
                // Output the time, and change to red.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXCTCSSText.Text = "Last RXCTSS off: " + DateTime.Now;
                    RXCTCSS.Fill = redDot;
                });
            }

            // Output the time, and change to green
            if (rx)
            {
                // Output the time, and change to green.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXText.Text = "Last RX on: " + DateTime.Now;
                    RX.Fill = greenDot;
                });
            }
            else
            {
                // Output the time, and change to red.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXText.Text = "Last RX off: " + DateTime.Now;
                    RX.Fill = redDot;
                });
            }
        }

        /// <summary>
        /// This will turn off the TX radio and update the display.
        /// </summary>

        private async void TXPTTOff()
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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXPTTText.Text = "Last TXPTT off: " + DateTime.Now;
                TXPTT.Fill = redDot;
            });
        }

        /// <summary>
        /// This will turn on the TX 
        /// The CW ID timer must be started, if it isn't running.
        /// Also, update the display.
        /// </summary>

        private async void TXPTTOn()
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

            // Output the time, and change to green.
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXPTTText.Text = "Last TXPTT on: " + DateTime.Now;
                TXPTT.Fill = greenDot;
            });


            // Setup the CWID timer, if not already running
            //x CWIDTimerStart();

        }


        /// <summary>
        /// This will turn off the CTCSS tones when the RX signal goes off and 
        /// update the display.
        /// </summary>

        private async void TXCTCSSOff()
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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCTCSSText.Text = "Last TXCTCSS off: " + DateTime.Now;
                TXCTCSS.Fill = redDot;
            });


        }


        /// <summary>
        /// This will turn on the CTCSS encoder when the RX signal comes on
        /// and update the display.
        /// </summary>

        private async void TXCTCSSOn()
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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCTCSSText.Text = "Last TXCTCSS on: " + DateTime.Now;
                TXCTCSS.Fill = greenDot;
            });


        }

        /// <summary>
        /// This starts a CWID cycle.
        /// First, turn on the id line.
        /// Second, clear out the ID timer.
        /// Third, if the RX is on, restart the ID timer.
        /// Fourth, start the CWID Pulse timer (this is what turns off the CWID line)
        /// and, update the display.
        /// </summary>

        private async void CWIDTimeout_Tick(object sender, object e)
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
            // CWIDPulse = new DispatcherTimer();
            //oo   TimeSpan off = TXCWIDPulse;
            //oo   MainPage.CWIDPulse.Interval = off;
            //oo   MainPage.CWIDPulse.Tick += CWIDPulse_Tick;
            //oo  MainPage.CWIDPulse.Start();

            // if the RX is still active, restart the timer
            if (rx)
            {
                // Setup the CWID timer
                //x         CWIDTimerStart();
            }
            else
            { // shut off the timer
              //oo     MainPage.CWIDTimeout.Stop();
            }

            // Output the time, and change to red.
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCWIDText.Text = "Last CWID on: " + DateTime.Now;
                TXCWID.Fill = greenDot;
            });

        }

        /// <summary>
        /// Timer event for COR timeout, turn off tx and ctcss and update display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CORTimeout_Tick(object sender, object e)
        {
            // turn off TX
            TXPTTOff();
            TXCTCSSOff();

            // turn off timer
            //oo    MainPage.CORTimeout.Stop();

            // Output the time, and change to red.
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXPTTText.Text = "Last PTT timeout: " + DateTime.Now;
                TXPTT.Fill = redDot;
                TXCTCSSText.Text = "Last TXCTCSS timeout: " + DateTime.Now;
                TXCTCSS.Fill = redDot;
            });
        }



        /// <summary>
        /// Timer to turn off the CWID timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CWIDPulse_Tick(object sender, object e)
        {
            // Turn off the pulse timer
            //oo    MainPage.CWIDPulse.Stop();

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
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCWIDText.Text = "Last CWID off: " + DateTime.Now;
                TXCWID.Fill = redDot;
            });

        }

        /// <summary>
        /// This will restart the CWID timer, if it's not already started
        /// </summary>
        private void CWIDTimerStart()
        {

            // Setup the CWID timer
            //oo     if (!MainPage.CWIDTimeout.IsEnabled)
            //oo   {
            // CWIDTimeout = new DispatcherTimer();
            //oo     TimeSpan off = TXCWIDTimeout;
            //oo       MainPage.CWIDTimeout.Interval = off;
            //oo     MainPage.CWIDTimeout.Tick += CWIDTimeout_Tick;
            //oo     MainPage.CWIDTimeout.Start();
            //oo    }
        }


        /// <summary>
        /// This will restart the COR timer, if it's not already started
        /// </summary>
        private void CORTimerStart()
        {
            // Setup the RXCOR timer
            //oo           if (!MainPage.CORTimeout.IsEnabled)
            //oo        {
            //CORTimeout = new DispatcherTimer();
            //oo        TimeSpan off = RXCORTimeout;
            //oo MainPage.CORTimeout.Interval = off;
            //oo MainPage.CORTimeout.Tick += CORTimeout_Tick;
            //oo  MainPage.CORTimeout.Start();
            //oo     }

        }

        /// <summary>
        /// This will stop the COR timer
        /// </summary>
        private void CORTimerStop()
        {
            // Setup the RXCOR timer
            if (!MainPage.CORTimeout.IsEnabled)
            {
                //oo          MainPage.CORTimeout.Stop();
            }

        }




    }
}
