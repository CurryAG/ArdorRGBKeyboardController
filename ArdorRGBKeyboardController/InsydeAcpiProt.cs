using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;

namespace ArdorRGBKeyboardController
{
    internal class InsydeAcpiProt
    {
        [DllImport("InsydeDCHU.dll")]
        public static extern int GetDCHU_Data_Integer(int command, ref int data);

        [DllImport("InsydeDCHU.dll")]
        public static extern int SetDCHU_Data(int command, byte[] buffer, int length);

        [DllImport("InsydeDCHU.dll")]
        public static extern int SetDCHU_DataEx(int command, byte[] buffer, int length, ref byte OutputBuffer);

        [DllImport("InsydeDCHU.dll")]
        public static extern int GetDCHU_Data_Buffer(int command, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        public static extern int ReadAppSettings(int page, int offset, int length, ref byte buffer);

        [DllImport("InsydeDCHU.dll")]
        public static extern int WriteAppSettings(int page, int offset, int length, ref byte buffer);

        public static int SetWMIPackageEx(int command, byte[] buffer, ref byte[] o_buffer)
        {
            int num = 0;
            try
            {
                _ = new byte[256];
                return SetDCHU_DataEx(command, buffer, 256, ref o_buffer[0]);
            }
            catch
            {
                return 0;
            }
        }

        public static int ReadAcpiBrightness()
        {
            int offset = 35;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiBrightness(int value)
        {
            int offset = 35;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static void WriteAcpiAppVersion()
        {
            int offset = 16;
            int length = 4;
            byte[] array = new byte[4]
            {
            (byte)Assembly.GetExecutingAssembly().GetName().Version.Major,
            (byte)Assembly.GetExecutingAssembly().GetName().Version.Minor,
            (byte)Assembly.GetExecutingAssembly().GetName().Version.Build,
            (byte)Assembly.GetExecutingAssembly().GetName().Version.Revision
            };
            WriteAppSettings(0, offset, length, ref array[0]);
            offset = 0;
            WriteAppSettings(2, offset, length, ref array[0]);
        }

        public static string ReadAcpiAppVersion()
        {
            int offset = 16;
            int num = 4;
            byte[] array = new byte[num];
            ReadAppSettings(0, offset, num, ref array[0]);
            return new StringBuilder(array[0] + "." + array[1] + "." + array[2] + "." + array[3]).ToString();
        }

        public static int ReadAcpiOverrideBootEffect()
        {
            int offset = 7;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiOverrideBootEffect(int value)
        {
            int offset = 7;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static int ReadAcpiSleepTimerTrigger()
        {
            int offset = 36;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiSleepTimerTrigger(int value)
        {
            int offset = 36;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static int ReadAcpiSleepTimer()
        {
            int offset = 37;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return (array[0] * 60 + array[1]) * 60 + array[2];
        }

        public static void WriteAcpiSleepTimer(int value)
        {
            int offset = 37;
            int length = 3;
            WriteAppSettings(2, offset, length, ref (new byte[3]
            {
            (byte)(value / 3600),
            (byte)(value / 60 % 60),
            (byte)(value % 60)
            })[0]);
        }

        public static int ReadAcpiStatus()
        {
            int offset = 80;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiStatus(int value)
        {
            int offset = 80;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiAllColor()
        {
            int offset = 81;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiAllColor(Color color)
        {
            int offset = 81;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static Color ReadAcpiLeftColor()
        {
            int offset = 85;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiLeftColor(Color color)
        {
            int offset = 85;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiLeftStatus()
        {
            int offset = 84;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiLeftStatus(int value)
        {
            int offset = 84;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiMidColor()
        {
            int offset = 89;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiMidColor(Color color)
        {
            int offset = 89;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiMidStatus()
        {
            int offset = 88;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiMidStatus(int value)
        {
            int offset = 88;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiRightColor()
        {
            int offset = 93;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiRightColor(Color color)
        {
            int offset = 93;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiRightStatus()
        {
            int offset = 92;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiRightStatus(int value)
        {
            int offset = 92;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiTPColor()
        {
            int offset = 97;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiTPColor(Color color)
        {
            int offset = 97;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiTPStatus()
        {
            int offset = 96;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiTPStatus(int value)
        {
            int offset = 96;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiLogoColor()
        {
            int offset = 101;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiLogoColor(Color color)
        {
            int offset = 101;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiLogoStatus()
        {
            int offset = 100;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiLogoStatus(int value)
        {
            int offset = 100;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiLightBarColor()
        {
            int offset = 105;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiLightBarColor(Color color)
        {
            int offset = 105;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiLightBarStatus()
        {
            int offset = 104;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiLightBarStatus(int value)
        {
            int offset = 104;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static int ReadAcpiMode()
        {
            int offset = 32;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiMode(int value)
        {
            int offset = 32;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static int ReadAcpiSpeed()
        {
            int offset = 34;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiSpeed(int value)
        {
            int offset = 34;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiWaveColor()
        {
            int offset = 66;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiWaveColor(Color color)
        {
            int offset = 66;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiWaveStatus()
        {
            int offset = 64;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiWaveStatus(int value)
        {
            int offset = 64;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static int ReadAcpiWaveFreeze()
        {
            int offset = 65;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiWaveFreeze(int value)
        {
            int offset = 65;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiBreathColor()
        {
            int offset = 53;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiBreathColor(Color color)
        {
            int offset = 53;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiBreathStatus()
        {
            int offset = 52;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiBreathStatus(int value)
        {
            int offset = 52;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiScanColor1()
        {
            int offset = 70;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiScanColor1(Color color)
        {
            int offset = 70;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static Color ReadAcpiScanColor2()
        {
            int offset = 73;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiScanColor2(Color color)
        {
            int offset = 73;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiScanStatus()
        {
            int offset = 69;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiScanStatus(int value)
        {
            int offset = 69;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiBlinkColor()
        {
            int offset = 57;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiBlinkColor(Color color)
        {
            int offset = 57;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiBlinkStatus()
        {
            int offset = 56;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiBlinkStatus(int value)
        {
            int offset = 56;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiRandomColor()
        {
            int offset = 77;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiRandomColor(Color color)
        {
            int offset = 77;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiRandomStatus()
        {
            int offset = 76;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiRandomStatus(int value)
        {
            int offset = 76;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiRippleColor()
        {
            int offset = 49;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiRippleColor(Color color)
        {
            int offset = 49;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiRippleStatus()
        {
            int offset = 48;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiRippleStatus(int value)
        {
            int offset = 48;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiSnakeColor()
        {
            int offset = 61;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiSnakeColor(Color color)
        {
            int offset = 61;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiSnakeStatus()
        {
            int offset = 60;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiSnakeStatus(int value)
        {
            int offset = 60;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static Color ReadAcpiFnColor()
        {
            int offset = 45;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void WriteAcpiFnColor(Color color)
        {
            int offset = 45;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static int ReadAcpiStaticStatus()
        {
            int offset = 33;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public static void WriteAcpiStaticStatus(int value)
        {
            int offset = 33;
            int length = 1;
            byte[] bytes = BitConverter.GetBytes(value);
            WriteAppSettings(2, offset, length, ref bytes[0]);
        }

        public static void PerkeyClearReg()
        {
            int offset = 128;
            int num = 384;
            byte[] array = new byte[num];
            WriteAppSettings(2, offset, num, ref array[0]);
        }

        public static Color GetStaticKeyColor(int row, int col)
        {
            int page = (640 + row * 64 + col * 3 + col / 5) / 256;
            int offset = (640 + row * 64 + col * 3 + col / 5) % 256;
            int num = 3;
            byte[] array = new byte[num];
            ReadAppSettings(page, offset, num, ref array[0]);
            return Color.FromRgb(array[0], array[1], array[2]);
        }

        public static void SetStaticKeyColor(int row, int col, Color color)
        {
            int page = (640 + row * 64 + col * 3 + col / 5) / 256;
            int offset = (640 + row * 64 + col * 3 + col / 5) % 256;
            int num = 3;
            byte[] array = new byte[num];
            array[0] = color.R;
            array[1] = color.G;
            array[2] = color.B;
            WriteAppSettings(page, offset, num, ref array[0]);
        }

        public static void ReadPerkeyAllStaticColor(ref byte buffer)
        {
            int offset = 128;
            int length = 384;
            ReadAppSettings(2, offset, length, ref buffer);
        }

        public static void WritePerkeyAllStaticColor(ref byte buffer)
        {
            int offset = 128;
            int length = 384;
            WriteAppSettings(2, offset, length, ref buffer);
        }

        public static bool SupportPerkeyDefaultStaticColor()
        {
            int data = 0;
            GetDCHU_Data_Integer(96, ref data);
            if (((data >> 8) & 1).Equals(1))
            {
                return true;
            }
            return false;
        }

        public static int ReadAcpiUILanguage()
        {
            int offset = 2;
            int num = 1;
            byte[] array = new byte[num];
            ReadAppSettings(1, offset, num, ref array[0]);
            return array[0];
        }

        public static void SetAcpiAppLaunch(int value)
        {
            int offset = 8;
            int length = 1;
            WriteAppSettings(2, offset, length, ref (new byte[1] { (byte)value })[0]);
        }

        public static double Read_PanelSize()
        {
            byte[] array = new byte[256];
            byte[] o_buffer = new byte[256];
            int num = 0;
            int num2 = 0;
            double result = 15.5;
            array[0] = 12;
            SetWMIPackageEx(4, array, ref o_buffer);
            num = ((o_buffer[68] & 0xF0) << 4) + o_buffer[66];
            num2 = ((o_buffer[68] & 0xF) << 8) + o_buffer[67];
            if (num != 0 && num != 0)
            {
                result = Math.Sqrt(Math.Pow(num, 2.0) + Math.Pow(num2, 2.0)) / 25.4;
            }
            return result;
        }
    }
}
