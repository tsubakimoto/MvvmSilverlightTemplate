using TestApplication.ViewModels.Common.Base;

namespace TestApplication.ViewModels.Common
{
    public class ComboBox_Dto : NotificationObject
    {
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(() => this.Name);
                }
            }
        }
        private string _name = string.Empty;

        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(() => this.Value);
                }
            }
        }
        private string _value = string.Empty;
    }
}
