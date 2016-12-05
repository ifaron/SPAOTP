using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIInvoker
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://10.68.211.222:9030/api/examapi/GetExamsForTrainee?traineeid=123123";

            var result = GetInvoke<List<Exam>>(url);
        }

        static ProcessResult<TResult> GetInvoke<TResult>(string url)
        {
            ProcessResult<TResult> result = new ProcessResult<TResult>();            

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
            ProcessResult<TResult> result = new ProcessResult<TResult>();

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
    }

    
    public class ProcessResult<T>
    {
        public ProcessResult()
        {
            Success = true;
        }

        public bool Success
        {
            get;
            set;
        }

        public string Error
        {
            get;
            set;
        }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Data
        {
            get;
            set;
        }
    }

    public class TraineeLoginParameter
    {
        public string Account
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }

    public class LoginInfo
    {
        /// <summary>
        /// 用户ID
        /// 学员：TraineeId
        /// 教师：OrganizationId
        /// </summary>
        public string UserID
        {
            get;
            set;
        }

        public string LoginName
        {
            get;
            set;
        }

        public string LoginAccount
        {
            get;
            set;
        }

        public string ClientIP
        {
            get;
            set;
        }

        public string LoginToken
        {
            get;
            set;
        }

        public DateTime LastAccessTime
        {
            get;
            set;
        }

        /// <summary>
        /// 所属单位ID
        /// </summary>        
        public string OrganizationId
        {
            get;
            set;
        }
    }

    public partial class Exam
    {
        #region Fields

        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 考试名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime ForbidEnterTime
        {
            get;
            set;
        }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public string PaperId
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get;
            set;
        }

        /// <summary>
        /// 单位ID
        /// </summary>
        public string OrganizationId
        {
            get;
            set;
        }

        /// <summary>
        /// 考试是否应用快照功能
        /// </summary>
        public bool AutoPistolgraph
        {
            get;
            set;
        }

        /// <summary>
        /// 是否开放（无法指定学员，即可对所有学员开放，）
        /// </summary>
        public bool IsOpen
        {
            get;
            set;
        }
        public string DegreeOfOpenness
        {
            get;
            set;
        }

        #endregion

    }
}
