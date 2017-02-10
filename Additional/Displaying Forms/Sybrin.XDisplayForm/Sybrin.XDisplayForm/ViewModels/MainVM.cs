using Sybrin.XDisplayForm.Base_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sybrin.XDisplayForm.ViewModels {
    public class MainVM : BaseViewModel{
        public MainVM() {

        }

        private string title = "Window Title";
        /// <summary>
        /// Gets or sets the Window Title
        /// </summary>

        public string Title {
            [DebuggerNonUserCode]
            get { return this.title; }
            [DebuggerNonUserCode]
            set {
                if (this.title != value) {
                    this.title = value;
                    SetPropertyChanged("Title");
                }
            }
        }

    }
}
