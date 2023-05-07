using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArdorRGBKeyboardController
{
    internal class RGBKB : BaseKB
    {
        private int _iStatus;

        private Color _all = Color.FromRgb(0, 0, byte.MaxValue);

        private Color _left = Color.FromRgb(byte.MaxValue, 0, 0);

        private int _iLeft;

        private Color _mid = Color.FromRgb(0, byte.MaxValue, 0);

        private int _iMid;

        private Color _right = Color.FromRgb(0, 0, byte.MaxValue);

        private int _iRight;

        private Color _tp = Color.FromRgb(0, 0, byte.MaxValue);

        private int _itp;

        private Color _logo = Color.FromRgb(0, byte.MaxValue, 0);

        private int _ilogo;

        private Color _lightBar = Color.FromRgb(0, 0, byte.MaxValue);

        private int _ilightbar;

        private int _brightness;

        private int _iMode;

        private const int PAGE_SIZE = 256;

        private bool _initRead;

        private int _kbType = 2;

        private byte[] _wmi13;

        public int Status
        {
            get
            {
                return _iStatus;
            }
            set
            {
                if (_initRead)
                {
                    _iStatus = value;
                    return;
                }
                InsydeAcpiProt.WriteAcpiStatus(value);
                WriteLog(_iStatus, value, "Status");
                _iStatus = value;
            }
        }

        public Color All
        {
            get
            {
                return _all;
            }
            set
            {
                if (!_all.Equals(value))
                {
                    if (_initRead)
                    {
                        _all = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiAllColor(value);
                    WriteLog(_all, value, "AllColor");
                    _all = value;
                }
            }
        }

        public Color Left
        {
            get
            {
                return _left;
            }
            set
            {
                if (!_left.Equals(value))
                {
                    if (_initRead)
                    {
                        _left = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiLeftColor(value);
                    WriteLog(_left, value, "LeftColor");
                    _left = value;
                }
            }
        }

        public int iLeft
        {
            get
            {
                return _iLeft;
            }
            set
            {
                if (!_iLeft.Equals(value))
                {
                    if (_initRead)
                    {
                        _iLeft = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiLeftStatus(value);
                    WriteLog(_iLeft, value, "LeftStatus");
                    _iLeft = value;
                }
            }
        }

        public Color Mid
        {
            get
            {
                return _mid;
            }
            set
            {
                if (!_mid.Equals(value))
                {
                    if (_initRead)
                    {
                        _mid = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiMidColor(value);
                    WriteLog(_mid, value, "MidColor");
                    _mid = value;
                }
            }
        }

        public int iMid
        {
            get
            {
                return _iMid;
            }
            set
            {
                if (!_iMid.Equals(value))
                {
                    if (_initRead)
                    {
                        _iMid = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiMidStatus(value);
                    WriteLog(_iMid, value, "MidStatus");
                    _iMid = value;
                }
            }
        }

        public Color Right
        {
            get
            {
                return _right;
            }
            set
            {
                if (!_right.Equals(value))
                {
                    if (_initRead)
                    {
                        _right = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiRightColor(value);
                    WriteLog(_right, value, "RightColor");
                    _right = value;
                }
            }
        }

        public int iRight
        {
            get
            {
                return _iRight;
            }
            set
            {
                if (!_iRight.Equals(value))
                {
                    if (_initRead)
                    {
                        _iRight = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiRightStatus(value);
                    WriteLog(_iRight, value, "RightStatus");
                    _iRight = value;
                }
            }
        }

        public Color TP
        {
            get
            {
                return _tp;
            }
            set
            {
                if (!_tp.Equals(value))
                {
                    if (_initRead)
                    {
                        _tp = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiTPColor(value);
                    WriteLog(_tp, value, "TPColor");
                    _tp = value;
                }
            }
        }

        public int iTp
        {
            get
            {
                return _itp;
            }
            set
            {
                if (!_itp.Equals(value))
                {
                    if (_initRead)
                    {
                        _itp = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiTPStatus(value);
                    WriteLog(_itp, value, "TPStatus");
                    _itp = value;
                }
            }
        }

        public Color Logo
        {
            get
            {
                return _logo;
            }
            set
            {
                if (!_logo.Equals(value))
                {
                    if (_initRead)
                    {
                        _logo = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiLogoColor(value);
                    WriteLog(_logo, value, "LogoColor");
                    _logo = value;
                }
            }
        }

        public int iLogo
        {
            get
            {
                return _ilogo;
            }
            set
            {
                if (!_ilogo.Equals(value))
                {
                    if (_initRead)
                    {
                        _ilogo = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiLogoStatus(value);
                    WriteLog(_ilogo, value, "LogoStatus");
                    _ilogo = value;
                }
            }
        }

        public Color LightBar
        {
            get
            {
                return _lightBar;
            }
            set
            {
                if (!_lightBar.Equals(value))
                {
                    if (_initRead)
                    {
                        _lightBar = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiLightBarColor(value);
                    WriteLog(_lightBar, value, "LightBarColor");
                    _logo = value;
                }
            }
        }

        public int iLightBar
        {
            get
            {
                return _ilightbar;
            }
            set
            {
                if (!_ilightbar.Equals(value))
                {
                    if (_initRead)
                    {
                        _ilightbar = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiLightBarStatus(value);
                    WriteLog(_ilightbar, value, "LightBarStatus");
                    _ilightbar = value;
                }
            }
        }

        public int Brightness
        {
            get
            {
                return _brightness;
            }
            set
            {
                if (!_brightness.Equals(value))
                {
                    if (_initRead)
                    {
                        _brightness = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiBrightness(value);
                    WriteLog(_brightness, value, "Brightness");
                    _brightness = value;
                }
            }
        }

        public int Mode
        {
            get
            {
                return _iMode;
            }
            set
            {
                if (!_iMode.Equals(value))
                {
                    if (_initRead)
                    {
                        _iMode = value;
                        return;
                    }
                    InsydeAcpiProt.WriteAcpiMode(value);
                    WriteLog(_iMode, value, "Mode");
                    _iMode = value;
                }
            }
        }

        public void FirstRun()
        {
            ResetWmi13ToDefault();
            Left = Color.FromRgb(byte.MaxValue, 0, 0);
            Mid = Color.FromRgb(0, byte.MaxValue, 0);
            Right = Color.FromRgb(0, 0, byte.MaxValue);
            All = Color.FromRgb(0, 0, byte.MaxValue);
            TP = Color.FromRgb(0, 0, byte.MaxValue);
            Logo = Color.FromRgb(0, byte.MaxValue, 0);
            LightBar = Color.FromRgb(0, 0, byte.MaxValue);
            iLeft = 1;
            iMid = 1;
            iRight = 1;
            iTp = 1;
            iLogo = 1;
            iLightBar = 1;
            Status = 1;
            Brightness = 4;
            Mode = 1;
            Left = GetDefaultLeftColor();
            Mid = GetDefaultMidColor();
            Right = GetDefaultRightColor();
            if (Left.Equals(Mid) && Left.Equals(Right))
            {
                All = Left;
            }
            WriteAllReg();
        }

        private Color GetDefaultLeftColor()
        {
            if (_wmi13 == null)
            {
                _wmi13 = new byte[256];
                CopyWmi13(ref _wmi13);
            }
            return Color.FromRgb(_wmi13[2], _wmi13[3], _wmi13[4]);
        }

        private Color GetDefaultMidColor()
        {
            if (_wmi13 == null)
            {
                _wmi13 = new byte[256];
                CopyWmi13(ref _wmi13);
            }
            return Color.FromRgb(_wmi13[5], _wmi13[6], _wmi13[7]);
        }

        private Color GetDefaultRightColor()
        {
            if (_wmi13 == null)
            {
                _wmi13 = new byte[256];
                CopyWmi13(ref _wmi13);
            }
            return Color.FromRgb(_wmi13[8], _wmi13[9], _wmi13[10]);
        }

        public void Init()
        {
            _initRead = true;
            _kbType = GetKBType();
            ReadAllReg();
            _initRead = false;
            SetMode();
            SetLedOnOff();
        }

        public int SetStatus()
        {
            SetLedOnOff();
            return Status;
        }

        public void UpdateStatus()
        {
            Status = InsydeAcpiProt.ReadAcpiStatus();
            iLeft = InsydeAcpiProt.ReadAcpiLeftStatus();
            iMid = InsydeAcpiProt.ReadAcpiMidStatus();
            iRight = InsydeAcpiProt.ReadAcpiRightStatus();
        }

        public void SetBrightnessLevel()
        {
            int num = 0;
            SetBrightnessValue(Brightness switch
            {
                1 => 47,
                2 => 95,
                3 => 143,
                4 => 191,
                _ => 0,
            });
        }

        public void SetBrightnessLevel(int value)
        {
            if (value != 0)
            {
                Brightness = value;
            }
            int num = 0;
            SetBrightnessValue(value switch
            {
                1 => 47,
                2 => 95,
                3 => 143,
                4 => 191,
                _ => 47,
            });
        }

        private void SetBrightnessValue(int value)
        {
            byte[] bytes = BitConverter.GetBytes(-201326592 + value);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void UpdateBrightness()
        {
            Brightness = InsydeAcpiProt.ReadAcpiBrightness();
        }

        public void SetBrightnessDown()
        {
            Brightness = InsydeAcpiProt.ReadAcpiBrightness();
            Status = 1;
            if (Brightness != 1)
            {
                Brightness--;
            }
            SetLedOnOff();
            SetBrightnessLevel();
        }

        public void SetBrightnessUp()
        {
            Brightness = InsydeAcpiProt.ReadAcpiBrightness();
            Status = 1;
            if (Brightness != 4)
            {
                Brightness++;
            }
            SetLedOnOff();
            SetBrightnessLevel();
        }

        public void SetOverrideBootEffect(int value)
        {
            byte[] bytes = BitConverter.GetBytes(503316480 + value);
            InsydeAcpiProt.SetDCHU_Data(121, bytes, 4);
        }

        public void SetMode()
        {
            if (Status.Equals(1))
            {
                SetBrightnessLevel(Brightness);
            }
            switch (Mode)
            {
                case 1:
                    SetCustomColor();
                    break;
                case 2:
                    SetBreathMode();
                    break;
                case 0:
                    SetRandomMode();
                    break;
                case 7:
                    SetFlashMode();
                    break;
                case 6:
                    SetTempoMode();
                    break;
                case 5:
                    SetDanceMode();
                    break;
                case 4:
                    SetWaveMode();
                    break;
                case 3:
                    SetCycleMode();
                    break;
                case 8:
                    SetAllColor();
                    break;
            }
        }

        public void SetCustomColor()
        {
            SetColor(1, Left);
            SetColor(2, Mid);
            SetColor(3, Right);
            if (SupportLogo())
            {
                SetColor(7, Logo);
            }
            if (SupportLightbar())
            {
                SetColor(8, LightBar);
            }
        }

        public void SetBreathMode()
        {
            SetColor(1, Left);
            SetColor(2, Mid);
            SetColor(3, Right);
            if (SupportLogo())
            {
                SetColor(7, Logo);
            }
            if (SupportLightbar())
            {
                SetColor(8, LightBar);
            }
            uint num = 2u;
            num <<= 16;
            num |= 0x10000000u;
            byte[] bytes = BitConverter.GetBytes(0xA000u | num);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetRandomMode()
        {
            byte[] bytes = BitConverter.GetBytes(1879048192u);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetFlashMode()
        {
            byte[] bytes = BitConverter.GetBytes(2684354560u);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetTempoMode()
        {
            byte[] bytes = BitConverter.GetBytes(2415919104u);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetDanceMode()
        {
            byte[] bytes = BitConverter.GetBytes(2147483648u);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetWaveMode()
        {
            byte[] bytes = BitConverter.GetBytes(2952790016u);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetCycleMode()
        {
            byte[] bytes = BitConverter.GetBytes(855703552u);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetAllColor()
        {
            SetColor(1, All);
            SetColor(2, All);
            SetColor(3, All);
            if (SupportLogo())
            {
                SetColor(7, Logo);
            }
            if (SupportLightbar())
            {
                SetColor(8, LightBar);
            }
        }

        public void SetColor(int KbPart, Color _color)
        {
            uint num = 0u;
            int num2 = 0;
            switch (KbPart)
            {
                case 1:
                    num2 = 240;
                    break;
                case 2:
                    num2 = 241;
                    break;
                case 3:
                    num2 = 242;
                    break;
                case 8:
                    num2 = 243;
                    break;
                case 7:
                    num2 = 246;
                    break;
            }
            if (_kbType == 6 || _kbType == 22)
            {
                num = (uint)((_color.B << 16) | (_color.R << 8) | _color.G);
                if (_color.R == 0 && _color.G == byte.MaxValue && _color.B == 127)
                {
                    num = 0x460000u | (uint)(_color.R << 8) | _color.G;
                }
            }
            else
            {
                num = (uint)(((byte)Math.Round(40.0 / 51.0 * (double)(int)_color.B, 0) << 16) | (_color.R << 8) | _color.G);
            }
            byte[] bytes = BitConverter.GetBytes((num2 << 24) + num);
            InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
        }

        public void SetLedOnOff()
        {
            uint num = 0u;
            uint num2 = 7u;
            uint num3 = 0u;
            uint num4 = 0u;
            uint num5 = 0u;
            uint num6 = 0u;
            uint num7 = 0u;
            if (Status.Equals(1))
            {
                num6 = 1u;
                num2 = ((!(TP == Color.FromRgb(0, 0, 0)) && !iTp.Equals(0)) ? 1u : 7u);
                num = (uint)((!SupportLightbar()) ? ((int)((num | num4 | num5 | num2 | num3) + (num6 << 12)) + (iLogo << 13) + (iTp << 15) + (iLeft << 16) + (iMid << 17) + (iRight << 18)) : ((int)((num | num4 | num5 | num2 | num3) + (num6 << 12)) + (iLogo << 13) + (iLightBar << 15) + (iLeft << 16) + (iMid << 17) + (iRight << 18)));
            }
            else
            {
                num = (num4 | num5 | num2 | num3) + (num6 << 12) + (num7 << 13);
                byte[] bytes = BitConverter.GetBytes(-201326592);
                InsydeAcpiProt.SetDCHU_Data(103, bytes, 4);
            }
            byte[] bytes2 = BitConverter.GetBytes((224 << 24) + num);
            InsydeAcpiProt.SetDCHU_Data(103, bytes2, 4);
        }

        private Color StringToColor(string color)
        {
            string[] array = color.Split('_');
            return Color.FromArgb(byte.MaxValue, Convert.ToByte(array[0]), Convert.ToByte(array[1]), Convert.ToByte(array[2]));
        }

        private string ColorToString(Color color)
        {
            return color.R + "_" + color.G + "_" + color.B;
        }

        public void WriteAllReg()
        {
            InsydeAcpiProt.WriteAcpiLeftColor(Left);
            InsydeAcpiProt.WriteAcpiMidColor(Mid);
            InsydeAcpiProt.WriteAcpiRightColor(Right);
            InsydeAcpiProt.WriteAcpiAllColor(All);
            InsydeAcpiProt.WriteAcpiTPColor(TP);
            InsydeAcpiProt.WriteAcpiLogoColor(Logo);
            InsydeAcpiProt.WriteAcpiLightBarColor(LightBar);
            InsydeAcpiProt.WriteAcpiLeftStatus(iLeft);
            InsydeAcpiProt.WriteAcpiMidStatus(iMid);
            InsydeAcpiProt.WriteAcpiRightStatus(iRight);
            InsydeAcpiProt.WriteAcpiTPStatus(iTp);
            InsydeAcpiProt.WriteAcpiLogoStatus(iLogo);
            InsydeAcpiProt.WriteAcpiLightBarStatus(iLightBar);
            InsydeAcpiProt.WriteAcpiStatus(Status);
            InsydeAcpiProt.WriteAcpiMode(Mode);
        }

        public void ReadAllReg()
        {
            Status = InsydeAcpiProt.ReadAcpiStatus();
            Mode = InsydeAcpiProt.ReadAcpiMode();
            iLeft = InsydeAcpiProt.ReadAcpiLeftStatus();
            iMid = InsydeAcpiProt.ReadAcpiMidStatus();
            iRight = InsydeAcpiProt.ReadAcpiRightStatus();
            iTp = InsydeAcpiProt.ReadAcpiTPStatus();
            iLogo = InsydeAcpiProt.ReadAcpiLogoStatus();
            iLightBar = InsydeAcpiProt.ReadAcpiLightBarStatus();
            Left = InsydeAcpiProt.ReadAcpiLeftColor();
            Mid = InsydeAcpiProt.ReadAcpiMidColor();
            Right = InsydeAcpiProt.ReadAcpiRightColor();
            All = InsydeAcpiProt.ReadAcpiAllColor();
            TP = InsydeAcpiProt.ReadAcpiTPColor();
            Logo = InsydeAcpiProt.ReadAcpiLogoColor();
            LightBar = InsydeAcpiProt.ReadAcpiLightBarColor();
            Brightness = InsydeAcpiProt.ReadAcpiBrightness();
        }

        private void WriteLog(int before, int after, string attribute)
        {
            StringBuilder stringBuilder = new StringBuilder("Keyboard_");
            stringBuilder.Append(attribute + "_" + after);
            stringBuilder.Append("\n\n");
            stringBuilder.Append("KB Type: " + GetKBType() + "\n");
            stringBuilder.Append("Set " + attribute + ": " + before + " -> " + after);
            Thread.Sleep(10);
        }

        private void WriteLog(Color before, Color after, string attribute)
        {
            StringBuilder stringBuilder = new StringBuilder("Keyboard_");
            stringBuilder.Append(attribute + "_" + ColorToString(after));
            stringBuilder.Append("\n\n");
            stringBuilder.Append("KB Type: " + GetKBType() + "\n");
            stringBuilder.Append("Set " + attribute + ": " + ColorToString(before) + " -> " + ColorToString(after));
            Thread.Sleep(10);
        }
    }

}
