using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace NetduinoPlusApplication3
{
    public class Program
    {
        Random rnd = new Random();
        static OutputPort yellowLed = new OutputPort(Pins.GPIO_PIN_D9, true);
        static OutputPort redLed = new OutputPort(Pins.GPIO_PIN_D8, true);
        static OutputPort greenLed = new OutputPort(Pins.GPIO_PIN_D7, true);
        static InputPort trueSwitch = new InputPort(Pins.GPIO_PIN_D10, true, Port.ResistorMode.PullUp);
        static InputPort falseSwitch = new InputPort(Pins.GPIO_PIN_D11, true, Port.ResistorMode.PullUp);

        #region signs
        static int[][] signs = new int[][]
        {
            new int[] // A
            {
                1, 1, 1, 1, 1, 1, 0
            },
            new int[] // B
            {
                0, 1, 0, 1, 1, 1, 1
            },
            new int[] // C
            {
                1, 1, 0, 0, 1, 0, 1
            },
            new int[] // D
            {
                0, 0, 1, 1, 1, 1, 1
            },
            new int[] // E
            {
                1, 1, 0, 1, 1, 0, 1
            },
            new int[] // F
            {
                1, 1, 0, 1, 1, 0, 0
            },
            new int[] // G
            {
                1, 1, 0, 1, 1, 1, 1
            },
            new int[] // H
            {
                0, 1, 1, 1, 1, 1, 0
            },
            new int[] // I
            {
                0, 1, 0, 0, 1, 0, 0
            },
            new int[] // J
            {
                0, 0, 1, 0, 1, 1, 1
            },
            new int[] // K
            {
                0, 1, 1, 0, 1, 1, 0
            },
            new int[] // L
            {
                0, 1, 0, 0, 1, 1
            },
            new int[] // M
            {
                1, 0, 0, 0, 1, 1, 0
            },
            new int[] // N
            {
                0, 0, 0, 1, 1, 1, 0
            },
            new int[] // O
            {
                1, 1, 1, 0, 1, 1, 1
            },
            new int[] // P
            {
                1, 1, 1, 1, 1, 0, 0
            },
            new int[] // Q
            {
                1, 1, 1, 1, 0, 1, 0
            },
            new int[] // R
            {
                0, 0, 0, 1, 1, 0, 0
            },
            new int[] // S
            {
                1, 1, 0, 1, 1, 0, 1
            },
            new int[] // T
            {
                0, 1, 0, 1, 1, 0, 1  
            },
            new int[] // U
            {
                0, 1, 1, 0, 1, 1, 1
            },
            new int[] // V
            {
                0, 0, 0, 0, 1, 1, 1
            },
            new int[] // W
            {
                0, 1, 1, 0, 0, 0, 1
            },
            new int[] // X
            {
                0, 1, 1, 1, 1, 1, 0
            },
            new int[] // Y
            {
                0, 1, 1, 1, 0, 1, 1
            },
            new int[] // Z
            {
                1, 0, 1, 1, 1, 0, 1
            }
        };
#endregion

        #region codes 
        static int[][] codes = new int[][]
        {
            new int[] // A
            {
                0, 1
            },
            new int[] // B
            {
                1, 0, 0, 0
            },
            new int[] // C
            {
                1, 0, 1, 0
            },
            new int[] // D
            {
                1, 0, 0
            },
            new int[] // E
            {
                0
            },
            new int[] // F
            {
                0, 0, 1, 0
            },
            new int[] // G
            {
                1, 1, 0
            },
            new int[] // H
            {
                0, 0, 0, 0
            },
            new int[] // I
            {
                0, 0
            },
            new int[] // J
            {
                0, 1, 1, 1
            },
            new int[] // K
            {
                1, 0, 1
            },
            new int[] // L
            {
                1, 1, 0
            },
            new int[] // M
            {
                1, 1
            },
            new int[] // N
            {
                1, 0
            },
            new int[] // O
            {
                1, 1, 1
            },
            new int[] // P
            {
                0, 1, 1, 0
            },
            new int[] // Q
            {
                1, 1, 0, 1
            },
            new int[] // R
            {
                0, 1, 0
            },
            new int[] // S
            {
                0, 0, 0
            },
            new int[] // T
            {
                1
            },
            new int[] // U
            {
                0, 0, 1
            },
            new int[] // V
            {
                0, 0, 0, 1
            },
            new int[] // W
            {
                0, 1, 1
            },
            new int[] // X
            {
                1, 0, 0, 1
            },
            new int[] // Y
            {
                1, 0, 1, 1
            },
            new int[] // Z
            {
                1, 1, 0, 0
            }
        };
        #endregion

        #region leds
        static OutputPort[] leds = new OutputPort[]
            {
                new OutputPort(Pins.GPIO_PIN_D0, true),
                new OutputPort(Pins.GPIO_PIN_D1, true),
                new OutputPort(Pins.GPIO_PIN_D2, true),
                new OutputPort(Pins.GPIO_PIN_D3, true),
                new OutputPort(Pins.GPIO_PIN_D4, true),
                new OutputPort(Pins.GPIO_PIN_D5, true),
                new OutputPort(Pins.GPIO_PIN_D6, true)
            };
        #endregion
        public static void Main()
        {
            Random rnd = new Random();

            while (true)
            {
                #region for-leds
                //for (int i = 0; i <= 6; i++)
                //{
                //    leds[i].Write(true);
                //    Thread.Sleep(250);
                //    leds[i].Write(false);
                //    Thread.Sleep(250);
                //}
                #endregion

                int random = rnd.Next(25);
                int decision = rnd.Next(1);

                showSignal(random);

                if (decision == 1)
                {
                    showLetter(random);
                }
                else
                {
                    showLetter(rnd.Next(25));
                }

                while (true)
                {
                    if ((!trueSwitch.Read() && decision == 1) || (!falseSwitch.Read() && decision == 0))
                    {
                        greenLed.Write(false);
                        break;
                    }
                    else if (!trueSwitch.Read() || !falseSwitch.Read())
                    {
                        redLed.Write(false);
                        break;
                    }
                }
                Thread.Sleep(3000);
                resetLeds();
                Thread.Sleep(1000);
            }
        }

        public static void showSignal(int random)
        {
            for (int i = 0; i < codes[random].Length; i++)
            {
                signal(codes[random][i]);
            };
        }

        public static void showLetter(int random){
            for (int i = 0; i < signs[random].Length; i++)
            {
                if (signs[random][i] == 1)
                {
                    leds[i].Write(false);
                }
            };
        }

        public static void resetLeds()
        {
            for (int i = 0; i <= 6; i++)
            {
                leds[i].Write(true); //True is actually false   
            };
            yellowLed.Write(true);
            greenLed.Write(true);
            redLed.Write(true);
        }
        public static void signal(int isLong) {
            if (isLong == 1)
            {
                yellowLed.Write(false);
                Thread.Sleep(1000);
                yellowLed.Write(true);
                Thread.Sleep(1000);
            }
            else if (isLong == 0)
            {
                yellowLed.Write(false);
                Thread.Sleep(350);
                yellowLed.Write(true);
                Thread.Sleep(1000);
            }
        }
    }
}
