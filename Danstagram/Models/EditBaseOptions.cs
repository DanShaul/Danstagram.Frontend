
namespace Danstagram.Models
{

    public class EditBaseOptions<T> : BindableErrorBase
    {

        #region Properties

        private bool isFocused = false;
        public bool IsFocused { get { return isFocused; } set { SetProperty(ref isFocused, value); } }

        private bool isRequired = false;
        public bool IsRequired { get { return isRequired; } set { SetProperty(ref isRequired, value); } }

        private string caption = "";
        public string Caption { get { return caption; } set { SetProperty(ref caption, value); } }


        private bool isEnabled = true;
        public bool IsEnabled { get { return isEnabled; } set { SetProperty(ref isEnabled, value); } }

        private bool isVisible = true;
        public bool IsVisible { get { return isVisible; } set { SetProperty(ref isVisible, value); } }

        #endregion

        #region Methods

        public bool Validate(object value)
        {
            return Validate<T>(value);
        }

        public bool Validate<TDataType>(object value)
        {
            string errorMessage = null;

            var requiredErrorMessage = "{0} cannot be empty";
            if (typeof(TDataType).Equals(typeof(string)))
            {
                if (IsRequired && string.IsNullOrEmpty((string)value)) errorMessage = requiredErrorMessage;
            }
            else
            {
                if (IsRequired && (value == null)) errorMessage = requiredErrorMessage;
            }

            if (errorMessage != null)
                SetErrorMessage(string.Format(errorMessage, Caption));
            else
                ResetErrorMessage();

            return (errorMessage == null);
        }

        #endregion

    }

}