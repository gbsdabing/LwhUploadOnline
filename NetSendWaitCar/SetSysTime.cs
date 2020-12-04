using System;
using System.Runtime.InteropServices;

namespace NetSendWaitCar
{
    public class SetSysTime
    {
        [DllImport("Kernel32.dll")]
        private static extern bool SetLocalTime(ref SystemTime sysTime);

        public static bool SetLocalTimeByStr(DateTime time_now)
        {
            bool flag = false;
            SystemTime sysTime = new SystemTime();
            sysTime.wYear = Convert.ToUInt16(time_now.Year);
            sysTime.wMonth = Convert.ToUInt16(time_now.Month);
            sysTime.wDay = Convert.ToUInt16(time_now.Day);
            sysTime.wHour = Convert.ToUInt16(time_now.Hour);
            sysTime.wMinute = Convert.ToUInt16(time_now.Minute);
            sysTime.wSecond = Convert.ToUInt16(time_now.Second);
            sysTime.wMiliseconds = Convert.ToUInt16(time_now.Millisecond);
            try
            {
                flag = SetLocalTime(ref sysTime);
            }
            catch (Exception er)
            {
                IOControl.WriteLogs("SetSystemDateTime函数执行异常" + er.Message);
            }

            return flag;
        }
    }
    public struct SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMiliseconds;
    }
}
