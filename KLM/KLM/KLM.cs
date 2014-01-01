using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KLM
{
    public class KLM
    {
        HidDevice device;

        public Dictionary<Regions, Tuple<Colors, Levels>> CurrentConfig;
        public Modes CurrentMode;

        public KLM(HidDevice hid)
        {
            device = hid;
            CurrentConfig = new Dictionary<Regions, Tuple<Colors, Levels>>();
        }

        public bool SetColor(Regions region, Colors color, Levels intensity)
        {
            if (device.WriteFeatureData(new byte[] { 1, 2, 66, (byte)region, (byte)color, (byte)intensity, 0, 236 }))
            {
                CurrentConfig[region] = new Tuple<Colors, Levels>(color, intensity);
                return true;
            }
            return false;
        }

        public bool SetMode(Modes mode)
        {
            if (device.WriteFeatureData(new byte[] { 1, 2, 65, (byte)mode, 0, 0, 0, 236 }))
            {
                CurrentMode = mode;
                return true;
            }
            return false;
        }

        public static KLM GetKeyboard()
        {
            return new KLM(HidDevices.Enumerate().Where((info) => info.Attributes.ProductId == 0xFF00 && info.Attributes.VendorId == 0x1770).First());
        }

        public enum Regions
        {
            left = 1,
            middle = 2,
            right = 3
        }

        public enum Colors
        {
            Off = 0,
            Red = 1,
            Orange = 2,
            Yellow = 3,
            Green = 4,
            Sky = 5,
            Blue = 6,
            Purple = 7,
            White = 8
        }

        public enum Levels
        {
            Light = 3,
            Low = 2,
            Medium = 1,
            High = 0
        }

        public enum Modes
        {
            Normal = 1,
            Gaming = 2,
            Breathe = 3,
            Demo = 4,
            Wave = 5
        }
    }
}
