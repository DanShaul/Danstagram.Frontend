using Danstagram.Models;
using System.Threading.Tasks;

namespace Danstagram.Models
{

    public class BindableErrorBase : BindableBase
    {

        #region Properties

        private bool isError = false;
        public bool IsError { get { return isError; } set { SetProperty(ref isError, value); } }

        private string errorMessage = "";
        public string ErrorMessage { get { return errorMessage; } set { SetProperty(ref errorMessage, value); } }

        #endregion

        #region Methods

        public void ResetErrorMessage()
        {
            IsError = false;
            ErrorMessage = "";
        }

        public void SetErrorMessage(string message)
        {
            IsError = true;
            ErrorMessage = message;
        }

        #endregion

        #region Events

        public virtual void Reset() { ResetErrorMessage(); }
        public virtual bool Validate() { return true; }

        #endregion

    }

}