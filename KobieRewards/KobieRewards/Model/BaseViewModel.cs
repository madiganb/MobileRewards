using System;
using System.Collections.Generic;
using System.Text;

namespace KobieRewards.Model
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
