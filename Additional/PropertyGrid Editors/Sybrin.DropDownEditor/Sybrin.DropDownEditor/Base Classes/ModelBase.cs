using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Sybrin.DropDownEditor {
    public abstract class ModelBase : INotifyPropertyChanged {
        protected ICommand CreateCommand(Action<object> executeAction) {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");

            return new RelayCommand(executeAction);
        }

        public void InvokeOnUIThread(Action action) {
            if (Application.Current == null) throw new NullReferenceException("No instance of the Current Application can be found");
            if (Application.Current.Dispatcher == null) throw new NullReferenceException("The Dispatcher of the Current Application is null");
            Application.Current.Dispatcher.Invoke(action);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void SetPropertyChanged(string propName) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
