using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Invoker
{
    public class SunytechAPIDynamicInvoker
    {
        #region Delegate

        delegate uint DelegateConnect();

        delegate uint DelegateDisconnect();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateSaveSnapshot(int[] stationIds, ref int snapshotId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateApplySnapshot(int snapshotId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateDeleteSnapshot(int snapshotId);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateRun(int[] stationIds, float speed, float step);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateFreeze(int[] stationIds, float simulationTime);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateUnfreeze(int[] stationIds, float speed);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateStop(int[] stationIds, float simulationTime);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateChangeSpeed(int[] stationIds, float speed, float simulationTime);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate uint DelegateGetTagsCount(int[] stationIds, ref uint count);

        

        #endregion

        #region Public

        public void LoadLibrary()
        {
            _Handle = NativeMethod.LoadLibrary(LIB_PATH);
            if (_Handle == 0)
            {
                throw new ApplicationException(string.Format("动态链接库{0}加载失败", LIB_PATH));
            }
        }

        public void FreeLibrary()
        {
            bool isFree = NativeMethod.FreeLibrary(_Handle);
            if (!isFree)
            {
                throw new ApplicationException(string.Format("动态链接库{0}释放失败", LIB_PATH));
            }
        }

        public uint Connect()
        {
            var func = GetDelegate<DelegateConnect>();
            return func();
        }

        public uint Disconnect()
        {
            var func = GetDelegate<DelegateDisconnect>();
            return func();
        }

      

        #endregion

        #region Private

        private T GetDelegate<T>()
        {
            var lpProcName = typeof(T).Name.Replace("Delegate", "");
            IntPtr intPtr = NativeMethod.GetProcAddress(_Handle, lpProcName);
            T func = (T)(object)Marshal.GetDelegateForFunctionPointer(intPtr, typeof(T));
            return func;
        }

        private static class NativeMethod
        {
            [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
            public static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

            [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
            public static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

            [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
            public static extern bool FreeLibrary(int hModule);
        }

        private const string LIB_PATH = @"OTSVDrv.dll";
        private int _Handle;

        #endregion
    }
}
