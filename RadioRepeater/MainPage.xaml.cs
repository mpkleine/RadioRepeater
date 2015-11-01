﻿using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Core;

/// <summary>
/// This program will run a Raspberry Pi 2 as a Radio Repeater.
/// This device initially set up for a VHF to UHF for the SCARS group.
/// All of this is documented on the GitHub site at: https://github.com/search?utf8=%E2%9C%93&q=radiorepeater
/// Program written 10/2015 by Mark P Kleine, n5hzr
/// </summary>

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RadioRepeater
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Timer to keep track of the 'ragchew', or timeout timer. 
        public static DispatcherTimer COSTimeout;
        // Timer to keep track of the 'CWID timer'
        public static DispatcherTimer CWIDTimeout;
        // Timer to hold the CWID pulse to trigger the CWID'er
        public static DispatcherTimer CWIDPulse;
        // Timer to hold the TXPTT for a bit on the unkey of the radio.
        public static DispatcherTimer PTTPulse;
        // Timer to update the current time display on the monitor
        public static DispatcherTimer TimeTimer;

        // Constant colors for the display panel
        private SolidColorBrush yellowDot = new SolidColorBrush(Windows.UI.Colors.Yellow);
        private SolidColorBrush redDot = new SolidColorBrush(Windows.UI.Colors.Red);
        private SolidColorBrush greenDot = new SolidColorBrush(Windows.UI.Colors.Green);

        // hardware channels used to transfer I/O info
        private GpioPin RXCOSChannel;
        private GpioPin RXCTCSSChannel;
        private GpioPin TXPTTChannel;
        private GpioPin TXCTCSSChannel;
        private GpioPin TXCWIDChannel;
        private GpioPinValue channelValue;

        // synthesized RX signals (rxcos and rxctcss)
        // used to test in the program so we don't have to worry about active high/low status
        bool rx = false;
        bool rxcos = false;
        bool rxctcss = false;

        // Let's start this deal
        public MainPage()
        {
            // Initialize the timers
            COSTimeout = new DispatcherTimer();
            CWIDTimeout = new DispatcherTimer();
            CWIDPulse = new DispatcherTimer();
            PTTPulse = new DispatcherTimer();
            TimeTimer = new DispatcherTimer();

            this.InitializeComponent();

            // Override the ragchew timer, for demo
            RXCOSTimeout = TimeSpan.FromMinutes(.5);
            // Ovverride the CWID Timeout Timer field for demo
            TXCWIDTimeout = TimeSpan.FromMinutes(1);

            // Override the CWID Hang pulse for demo
            TXCWIDPulse = TimeSpan.FromMilliseconds(2000);
            // Override the PTT Hang pulse for demo
            TXPTTPulse = TimeSpan.FromMilliseconds(2000);

            // Initialize the GPIO and set up the event timers
            InitGPIO();
        }


        // make the RXCOSpin a public item
        private int RXCOSPinField = 5;

        public int RXCOSPin
        {
            get
            {
                return RXCOSPinField;
            }
            set
            {
                RXCOSPinField = value;
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

        // make the RXCOSActive a public item
        private bool RXCOSActiveField = false;

        public bool RXCOSActive
        {
            get
            {
                return RXCOSActiveField;
            }
            set
            {
                RXCOSActiveField = value;
            }
        }

        // make the RXCOSTimeout a public item
        private TimeSpan RXCOSTimeoutField = TimeSpan.FromMinutes(3);

        public TimeSpan RXCOSTimeout
        {
            get
            {
                return RXCOSTimeoutField;
            }
            set
            {
                RXCOSTimeoutField = value;
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


        // make TXPTTPulse a public item
        private TimeSpan TXPTTPulseField = TimeSpan.FromMilliseconds(100);

        public TimeSpan TXPTTPulse
        {
            get
            {
                return TXPTTPulseField;
            }
            set
            {
                TXPTTPulseField = value;
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

            // Setup the RX display info
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXText.Text = "RX Startup at: " + DateTime.Now;
                RX.Fill = redDot;
            });

            // Setup the current time, and create timer to keep updating it.
            await SetupDisplayTime();

            // Initialize GPIO
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                RXCOSChannel = null;
                return;
            }

            // Set up the input RXCOS line
            RXCOSChannel = gpio.OpenPin(RXCOSPin);
            RXCOSChannel.SetDriveMode(GpioPinDriveMode.Input);
            RXCOSChannel.DebounceTimeout = TimeSpan.FromMilliseconds(15);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXCOSText.Text = "RXCOS Startup: " + DateTime.Now;
                RXCOS.Fill = redDot;
            });


            // Set up the input RXCTCSS line
            RXCTCSSChannel = gpio.OpenPin(RXCTCSSPin);
            RXCTCSSChannel.SetDriveMode(GpioPinDriveMode.Input);
            RXCTCSSChannel.DebounceTimeout = TimeSpan.FromMilliseconds(15);
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXCTCSSText.Text = "RXCTCSS Startup: " + DateTime.Now;
                RXCTCSS.Fill = redDot;
            });

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
            TXCWIDChannel.SetDriveMode(GpioPinDriveMode.Output);

            // Update the screen info
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCWIDText.Text = "TXCWID Startup: " + DateTime.Now;
                TXCWID.Fill = redDot;
            });

            // Now that all is up and running, and then start the RX triggers
            RXCOSChannel.ValueChanged += RXCOSPin_ValueChanged;
            RXCTCSSChannel.ValueChanged += RXCTCSSPin_ValueChanged;

        }

        /// <summary>
        /// This will display the current time, and create a timer 
        /// to update the current time every minute
        /// </summary>
        /// <returns></returns>
        private async System.Threading.Tasks.Task SetupDisplayTime()
        {
            // Fill in the current time, and set up the ticker to update the current time.
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TimeText.Text = DateTime.Now.ToString();
            });

            // Setup the Time Ticker, to update the display screen
            TimeSpan timeSec = TimeSpan.FromSeconds(1);
            TimeTimer.Interval = timeSec;
            TimeTimer.Tick += TimeTimer_Tick;
            TimeTimer.Start();
        }

        /// <summary>
        /// This is the Event from the change in COS line. If the CTCSS line is active, 
        /// then the TX should be turned on, and the CTCSS should be turned on, and 
        /// the timeout timer should be started, if it's not running. 
        /// On unkey, the TX and CTCSS should be turned off. The timeout timer should 
        /// be stopped.
        /// Also, update the display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>


        private async void RXCOSPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.RisingEdge)

            {
                if (RXCOSActive)
                { // on
                    rxcos = true;
                }
                else
                { // off
                    rxcos = false;
                }
            }
            else
            {
                if (!RXCOSActive)
                { // on
                    rxcos = true;
                }
                else
                { // off
                    rxcos = false;
                }
            }


            // Bring up transmitter,
            if (rxcos && rxctcss)
            {
                rx = true;
                // Setup the CWID timer, if not already running
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TXPTTOn();
                });
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        TXCTCSSOn();
                    });
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            COSTimeoutStart();
                        });
            }
            else
            {
                if (rx)
                {
                    rx = false;
                    // Stop the CWID timer, if not already running
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        TXPTTOff();
                    });
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        TXCTCSSOff();
                    });
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        COSTimeoutStop();
                    });

                }
            }

            // Update display 
            if (rxcos)
            {
                // Output the time, and change to green.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXCOSText.Text = "Last RXCOS on: " + DateTime.Now;
                    RXCOS.Fill = greenDot;
                });
            }
            else
            {
                // Output the time, and change to red.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    RXCOSText.Text = "Last RXCOS off: " + DateTime.Now;
                    RXCOS.Fill = redDot;
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
        /// This is the event from the inbound CTCSS line. If the COS line is active, 
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
            if (rxcos && rxctcss)
            {
                rx = true;
                // Setup the CWID timer, if not already running
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TXPTTOn();
                });
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        TXCTCSSOn();
                    });
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        COSTimeoutStart();
                    });
            }
            else
            {
                if (rx)
                {
                    rx = false;
                    // Stop the CWID timer, if not already running
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        TXPTTOff();
                    });
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            TXCTCSSOff();
                        });
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            COSTimeoutStop();
                        });
                }
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

        private void TXPTTOff()
        {
            // Setup the PTT pulse, to delay PTT off time
            TimeSpan PTToff = TXPTTPulse;
            PTTPulse.Interval = PTToff;
            PTTPulse.Tick += PTTPulse_Tick;
            PTTPulse.Start();

            // Output the time, and change to yellow
            TXPTTText.Text = "Last TXPTT unkey: " + DateTime.Now;
            TXPTT.Fill = yellowDot;
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

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CWIDTimeoutStart();
            });
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
            // Stop the CWID timer
            CWIDTimeout.Stop();

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
            TimeSpan CWIDPulseoff = TXCWIDPulse;
            CWIDPulse.Interval = CWIDPulseoff;
            CWIDPulse.Tick += CWIDPulse_Tick;
            CWIDPulse.Start();

            // Output the time, and change to green.
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXCWIDText.Text = "Last CWID on: " + DateTime.Now;
                TXCWID.Fill = greenDot;
            });
        }


        /// <summary>
        /// Timer event for Time timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeTimer_Tick(object sender, object e)
        {
            // Update the time
            TimeText.Text = DateTime.Now.ToString();
        }


        /// <summary>
        /// Timer event for COS timeout, turn off ctcss, 
        /// start the timer for the TX PTT turnoff and update display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void COSTimeout_Tick(object sender, object e)
        {

            // Shut off the virtual RX indicator
            rx = false;

            // turn off CTCSS
            TXCTCSSOff();

            // Start timer to turn off TX PTT
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                TXPTTOff();
            });

            // turn off timer
            COSTimeout.Stop();

            // Output the time, and change to yellow/red.
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                RXText.Text = "Last RX timeout: " + DateTime.Now;
                RX.Fill = yellowDot;
                TXPTTText.Text = "Last TXPTT timeout: " + DateTime.Now;
                TXPTT.Fill = yellowDot;
                TXCTCSSText.Text = "Last TXCTCSS timeout: " + DateTime.Now;
                TXCTCSS.Fill = yellowDot;
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
            TXCWIDChannel.Write(channelValue);
            TXCWIDChannel.SetDriveMode(GpioPinDriveMode.Output);

            if (rx)
            {
                // Output the time, and change to yellow.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TXCWIDText.Text = "Last CWID on: " + DateTime.Now;
                    TXCWID.Fill = yellowDot;
                });
            }
            else
            {
                // Output the time, and change to red.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TXCWIDText.Text = "Last CWID off: " + DateTime.Now;
                    TXCWID.Fill = redDot;
                });
            }
        }


        /// <summary>
        /// This will restart the CWID timer, if it's not already started
        /// </summary>
        private async void CWIDTimeoutStart()
        {
            if (!CWIDTimeout.IsEnabled)
            {
                TimeSpan cwidOff = TXCWIDTimeout;
                CWIDTimeout.Interval = cwidOff;
                CWIDTimeout.Tick += CWIDTimeout_Tick;
                CWIDTimeout.Start();

                // Output the time, and change to yellow.
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    TXCWIDText.Text = "Last CWID start: " + DateTime.Now;
                    TXCWID.Fill = yellowDot;
                });
            }
        }


        /// <summary>
        /// This will start the COS timer
        /// </summary>
        private void COSTimeoutStart()
        {
            // Setup the RXCOS timer
            TimeSpan COSoff = RXCOSTimeout;
            COSTimeout.Interval = COSoff;
            COSTimeout.Tick += COSTimeout_Tick;
            COSTimeout.Start();
        }


        /// <summary>
        /// This will stop the COS timer
        /// </summary>
        private void COSTimeoutStop()
        {
            // Stop the RXCOS timer
            COSTimeout.Stop();
        }


        /// <summary>
        /// Timer to extend the PTT on unkey to allow CTCSS to shut off first
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PTTPulse_Tick(object sender, object e)
        {
            // Turn off the pulse timer
            PTTPulse.Stop();

            // turn off the PTT, as long as receiver hasn't been keyed in the mean time
            if (!rx)
            {
                // Turn off the PTT line
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
        }


    }
}
