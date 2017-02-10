using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Sybrin.XDisplayForm.Base_Classes {
    public class BaseModel : INotifyPropertyChanged {
        public BaseModel() {

        }

        public void InvokeOnUIThread(Action action) {
            if (Application.Current == null) throw new NullReferenceException("No instance of Current (Windows.Application) application");
            if (Application.Current.Dispatcher == null) throw new NullReferenceException("No instance of Dispatcher on Windows.Current");
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
