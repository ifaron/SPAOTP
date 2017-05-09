using Microsoft.Win32;
using OTP.Business.Contract;
using OTP.Framework.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WebAPIInvoker
{
    class Program
    {
        static void Main(string[] args)
        {
            //var url = "http://10.68.215.222:9033/api/accountapi/TraineeLogin";

            //var result = PostInvoke<TraineeLoginInput, TraineeLoginOutput>(url, new TraineeLoginInput
            //{
            //    Account = "zs",
            //    Password = "123456"
            //});         

           // HttpApi.Test();


        }

        static ProcessResult<TResult> GetInvoke<TResult>(string url)
        {
            var result = new ProcessResult<TResult>();            

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var task = httpClient.GetAsync(url);
                    task.Wait();
                    result = task.Result.Content.ReadAsAsync<ProcessResult<TResult>>().Result;
                }

                return result;
            }
            catch (Exception ex)
            {                
                result.Success = false;
                result.Error = "访问Web服务时生发错误";
                return result;
            }
        }

        static ProcessResult<TResult> PostInvoke<T, TResult>(string url, T value)
        {
            var result = new ProcessResult<TResult>();

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var task = httpClient.PostAsJsonAsync(url, value);
                    task.Wait();
                    result = task.Result.Content.ReadAsAsync<ProcessResult<TResult>>().Result;
                }

                return result;
            }
            catch (Exception ex)
            {                
                result.Success = false;
                result.Error = "访问Web服务时生发错误";
                return result;
            }
        }

        static void Test()
        {
            //if (!UnsafeNclNativeMethods.HttpApi.Supported)
            //{
            //    throw new PlatformNotSupportedException();
            //}

           
        }
    }



    [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
    internal static unsafe class HttpApi
    {
        private const string HTTPAPI = "httpapi.dll";
        private const string TOKENBINDING = "tokenbinding.dll";

        [DllImport(HTTPAPI, ExactSpelling = true, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern uint HttpInitialize(HTTPAPI_VERSION version, uint flags, void* pReserved);

        

        [StructLayout(LayoutKind.Sequential)]
        internal struct HTTPAPI_VERSION
        {
            internal ushort HttpApiMajorVersion;
            internal ushort HttpApiMinorVersion;
        }


        private static HTTPAPI_VERSION version;

        [Flags]
        internal enum HTTP_FLAGS : uint
        {
            NONE = 0x00000000,
            HTTP_RECEIVE_REQUEST_FLAG_COPY_BODY = 0x00000001,
            HTTP_RECEIVE_SECURE_CHANNEL_TOKEN = 0x00000001,
            HTTP_SEND_RESPONSE_FLAG_DISCONNECT = 0x00000001,
            HTTP_SEND_RESPONSE_FLAG_MORE_DATA = 0x00000002,
            HTTP_SEND_RESPONSE_FLAG_BUFFER_DATA = 0x00000004,
            HTTP_SEND_RESPONSE_FLAG_RAW_HEADER = 0x00000004,
            HTTP_SEND_REQUEST_FLAG_MORE_DATA = 0x00000001,
            HTTP_PROPERTY_FLAG_PRESENT = 0x00000001,
            HTTP_INITIALIZE_SERVER = 0x00000001,
            HTTP_INITIALIZE_CBT = 0x00000004,
            HTTP_SEND_RESPONSE_FLAG_OPAQUE = 0x00000040,
        }

        public static void Test()
        {

        }


        private static void InitHttpApi(ushort majorVersion, ushort minorVersion)
        {
            version.HttpApiMajorVersion = majorVersion;
            version.HttpApiMinorVersion = minorVersion;



            // For pre-Win7 OS versions, we need to check whether http.sys contains the CBT patch.
            // We do so by passing HTTP_INITIALIZE_CBT flag to HttpInitialize. If the flag is not 
            // supported, http.sys is not patched. Note that http.sys will return invalid parameter
            // also on Win7, even though it shipped with CBT support. Therefore we must not pass
            // the flag on Win7 and later.



            //if (ComNetOS.IsWin7orLater)
            //{
            // on Win7 and later, we don't pass the CBT flag. CBT is always supported.
            var statusCode = HttpApi.HttpInitialize(version, (uint)HTTP_FLAGS.HTTP_INITIALIZE_SERVER, null);
            //}
            //else
            //{
            //    statusCode = HttpApi.HttpInitialize(version,
            //        (uint)(HTTP_FLAGS.HTTP_INITIALIZE_SERVER | HTTP_FLAGS.HTTP_INITIALIZE_CBT), null);

            //    // if the status code is INVALID_PARAMETER, http.sys does not support CBT.
            //    if (statusCode == ErrorCodes.ERROR_INVALID_PARAMETER)
            //    {
            //        if (Logging.On) Logging.PrintWarning(Logging.HttpListener, SR.GetString(SR.net_listener_cbt_not_supported));

            //        // try again without CBT flag: HttpListener can still be used, but doesn't support EP
            //        extendedProtectionSupported = false;
            //        statusCode = HttpApi.HttpInitialize(version, (uint)HTTP_FLAGS.HTTP_INITIALIZE_SERVER, null);
            //    }
            //}


        }      
        static HttpApi()
        {
            InitHttpApi(2, 0);
        }

        

        static volatile bool supported;
        internal static bool Supported
        {
            get
            {
                return supported;
            }
        }

       
    }


}
