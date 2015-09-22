# RadioRepeater

This program will allow users to connect two radios in a repeater fashion. 
The main use for this type of device would be to link a radio receiver with 
a radio transmitter to create an amateur radio repeater. This program will 
manage the keying of the transmitter, the keying of an external CW tone 
board (initially built for a Compsec TS-64 board).

The COR input signal is anded with the incoming CTCSS signal to provide a 
signal to actually turn on the system. Once that signal pair is received, 
the TXPTT signal and TXCTCSS signal is turned on, to bring up the 
transmitter. This will be subject to a 3 minute RX time-out timer.

This program to provide a 10 minute TX CW Identification signal. The timing 
of this can be configured from config file. The output pulse length is set 
to 100 ms, and is configurable via the config file.

All inputs and outputs are configured to use/accept either active High, or 
active Low inputs, based on a configuration setting.

Hardware used for this project:
Raspberry Pi 2
SDRAM 8gB to 16gB
Comspec TS-64WDS tone encoder 
http://www.com-spec.com/tone_signaling/images/pdf%20TS-64WDS%20Manual.pdf
Hardware CW Identification generator Comspec ID-8 (active high, or low to 
   trigger the device). The ID-8 has a PTT output that grounds a tranmsitter 
   input. Triggering the ID-8 will bring up the PTT of transmitter, (not the 
   CTCSS of the encoder), and wait for a small time until the transmitter 
   comes up, send the CW id, and then drop the transmitter PTT. If the 
   RadioRepeater happens to come up while the CW ID is occuring, the CTCSS  
   encoder will turn on, and the incoming audio will mix with the CW ID tones, 
   and they will be heard mixed on the output of the transmitter.

The output display of the RPi2 (HDMI) shows a dashboard that lists the
state of all 5 input and output lines, with the last time that the signal
was toggled off/on.

Raspberry Pi 2 - GPIO pin-out connections:

1 - RX COR input (from Kenwood xxx Radio)

2 - RX CTCSS input (from Comspec, or Kenwood radio, not sure which)

3 - TX PTT Output (to Kenwood Radio)

4 - TX CTCSS Output

5 - TX CW Identification device

GPIO pin configuration is soft coded, and can be modified by the configuration file.
