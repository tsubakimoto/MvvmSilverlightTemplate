using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace TestApplication.ViewModels.Common.Base
{
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected NotificationObject() { }

        protected void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if (h != null && !string.IsNullOrEmpty(propertyName))
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var h = this.PropertyChanged;
            if (h != null)
            {
                var propertyName = GetPropertyName(propertyExpression);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    h(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            var name = string.Empty;
            if (expression != null)
            {
                var body = expression.Body;
                var member = body as MemberExpression;
                if (member != null)
                {
                    name = member.Member.Name;
                }
            }
            return string.Concat(name);
        }

        public override string ToString()
        {
            return string.Concat(base.ToString(), "@", base.GetHashCode());
        }
    }
}
