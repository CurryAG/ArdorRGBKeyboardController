using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArdorRGBKeyboardController
{
    public abstract class BaseKB
    {
        private const int PAGE_SIZE = 256;

        private byte[] _wmi13;

        private byte[] _wmi17;

        private int _wmi122;

        private int _overridebooteffect;

        private int _sleepTimer;

        private int _sleepTimerTirgger;

        public int OverrideBootEffect
        {
            get
            {
                return _overridebooteffect;
            }
            set
            {
                if (!_overridebooteffect.Equals(value))
                {
                    InsydeAcpiProt.WriteAcpiOverrideBootEffect(value);
                    WriteLog(_overridebooteffect, value, "OverrideBootEffect");
                    _overridebooteffect = value;
                }
            }
        }

        public int SleepTimer
        {
            get
            {
                return _sleepTimer;
            }
            set
            {
                if (!_sleepTimer.Equals(value))
                {
                    InsydeAcpiProt.WriteAcpiSleepTimer(value);
                    WriteLog(_sleepTimer, value, "SleepTimer");
                    _sleepTimer = value;
                }
            }
        }

        public int SleepTimerTrigger
        {
            get
            {
                return _sleepTimerTirgger;
            }
            set
            {
                if (!_sleepTimerTirgger.Equals(value))
                {
                    InsydeAcpiProt.WriteAcpiSleepTimerTrigger(value);
                    WriteLog(_sleepTimerTirgger, value, "SleepTimerTrigger");
                    _sleepTimerTirgger = value;
                }
            }
        }

        private void GetWmi17()
        {
            int data = 90;
            InsydeAcpiProt.GetDCHU_Data_Integer(70, ref data);
            _wmi17 = new byte[256];
            InsydeAcpiProt.GetDCHU_Data_Buffer(17, ref _wmi17[0]);
        }

        public int CustomID()
        {
            return _wmi17[2];
        }

        public Color CustomMainSkin()
        {
            if (_wmi17 == null)
            {
                GetWmi17();
            }
            return Color.FromRgb(_wmi17[3], _wmi17[4], _wmi17[5]);
        }

        public bool IsCustom3ColKB()
        {
            if (_wmi17 == null)
            {
                GetWmi17();
            }
            if (!_wmi17[14].Equals(1))
            {
                return false;
            }
            return true;
        }

        public void GetPalette(ref Color[] color)
        {
            color[0] = Color.FromRgb(_wmi17[16], _wmi17[17], _wmi17[18]);
            color[1] = Color.FromRgb(_wmi17[19], _wmi17[20], _wmi17[21]);
            color[2] = Color.FromRgb(_wmi17[22], _wmi17[23], _wmi17[24]);
            color[3] = Color.FromRgb(_wmi17[25], _wmi17[26], _wmi17[27]);
            color[4] = Color.FromRgb(_wmi17[28], _wmi17[29], _wmi17[30]);
            color[5] = Color.FromRgb(_wmi17[32], _wmi17[33], _wmi17[34]);
            color[6] = Color.FromRgb(_wmi17[35], _wmi17[36], _wmi17[37]);
            color[7] = Color.FromRgb(_wmi17[38], _wmi17[39], _wmi17[40]);
            color[8] = Color.FromRgb(_wmi17[41], _wmi17[42], _wmi17[43]);
            color[9] = Color.FromRgb(_wmi17[44], _wmi17[45], _wmi17[46]);
        }

        private void GetWmi13()
        {
            int data = 90;
            InsydeAcpiProt.GetDCHU_Data_Integer(70, ref data);
            _wmi13 = new byte[256];
            InsydeAcpiProt.GetDCHU_Data_Buffer(13, ref _wmi13[0]);
        }

        public void CopyWmi13(ref byte[] _copy)
        {
            if (_wmi13 == null)
            {
                GetWmi13();
            }
            Array.Copy(_wmi13, _copy, 256);
        }

        public int GetKBType()
        {
            if (_wmi13 == null)
            {
                GetWmi13();
            }
            if (_wmi13[15] == 3 || _wmi13[15] == 19 || _wmi13[15] == 35 || _wmi13[15] == 51 || _wmi13[15] == 243)
            {
                return 3;
            }
            return _wmi13[15];
        }

        public void WriteKBTypeToReg()
        {
            if (_wmi13 != null)
            {
                int offset = 4;
                int length = 1;
                InsydeAcpiProt.WriteAppSettings(2, offset, length, ref (new byte[1] { (byte)GetKBType() })[0]);
            }
        }

        public int ReadKBTypeFromReg()
        {
            int offset = 4;
            int num = 1;
            byte[] array = new byte[num];
            InsydeAcpiProt.ReadAppSettings(2, offset, num, ref array[0]);
            return array[0];
        }

        public bool SupportLogo()
        {
            if (_wmi122.Equals(0))
            {
                _wmi122 = InsydeAcpiProt.GetDCHU_Data_Integer(122, ref _wmi122);
            }
            return ((_wmi122 >> 18) & 1) != 0;
        }

        public bool SupportLightbar()
        {
            if (_wmi122.Equals(0))
            {
                _wmi122 = InsydeAcpiProt.GetDCHU_Data_Integer(122, ref _wmi122);
            }
            return ((_wmi122 >> 12) & 1) != 0;
        }

        public bool SupportLightbar_x170()
        {
            int data = 0;
            InsydeAcpiProt.GetDCHU_Data_Integer(96, ref data);
            if (((data >> 11) & 1).Equals(1))
            {
                return true;
            }
            return false;
        }

        public bool SupportWhiteKB6Level()
        {
            if (_wmi122.Equals(0))
            {
                _wmi122 = InsydeAcpiProt.GetDCHU_Data_Integer(122, ref _wmi122);
            }
            return ((_wmi122 >> 14) & 1) != 0;
        }

        public bool SupportOverrideBootEffect()
        {
            if (_wmi13 == null)
            {
                GetWmi13();
            }
            return (_wmi13[43] & 1) == 1;
        }

        public void UpdateCurrentOverrideBootEffect()
        {
            OverrideBootEffect = InsydeAcpiProt.ReadAcpiOverrideBootEffect();
        }

        public void SetSleepTimerTrigger(int value)
        {
            byte[] bytes = BitConverter.GetBytes((402653184 + (value << 8)) | 0xFF);
            InsydeAcpiProt.SetDCHU_Data(121, bytes, 4);
        }

        public void SetSleepTimerTriggerOff()
        {
            byte[] bytes = BitConverter.GetBytes(402653184);
            InsydeAcpiProt.SetDCHU_Data(121, bytes, 4);
        }

        public void ResetWmi13ToDefault()
        {
            byte[] bytes = BitConverter.GetBytes(570425345);
            InsydeAcpiProt.SetDCHU_Data(121, bytes, 4);
            _wmi13 = null;
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
    }
}
