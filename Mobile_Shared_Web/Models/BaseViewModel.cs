namespace Mobile_Shared_Web.Models
{
    public abstract class BaseViewModel
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage = (_errorMessage ?? string.Empty); }
            set { _errorMessage = value; }
        }

        public abstract ValidationResponse ValidateViewModel();
    }
}