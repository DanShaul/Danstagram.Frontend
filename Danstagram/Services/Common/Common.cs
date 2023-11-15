using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Danstagram.Services.Common
{
    public static class Common
    {
        #region Methods
        public static Task<HttpResponseMessage> WrapTimeout(this Task<HttpResponseMessage> task)
        {
            if (!task.Wait(5000))//wait for 5 seconds
                throw new TaskCanceledException(task);
            else
                return task;
        }
        #endregion
    }
}
