using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace Sybrin.ImageSelectorEditor {
    public class MainVM : ViewModelBase {
        public MainVM() {
            this.Images = new ImageModels();
        }

        private ImageModels images = new ImageModels();
        /// <summary>
        /// Gets or sets the Image Models
        /// </summary>

        public ImageModels Images {
            get { return this.images; }
            set {
                if (this.images != value) {
                    this.images = value;
                    SelectedImage = images?.FirstOrDefault();
                }
            }
        }
        

        private ImageModel selectedImage = null;
        /// <summary>
        /// Gets or sets the Selected Image
        /// </summary>

        public ImageModel SelectedImage {
            [DebuggerNonUserCode]
            get { return this.selectedImage; }
            set {
                if (this.selectedImage != value) {
                    this.selectedImage = value;
                    CanRemoveImage = selectedImage != null;
                    SetPropertyChanged("CanRemoveImage");
                    SetPropertyChanged("SelectedImage");
                }
            }
        }

        private bool canRemoveImage = false;
        /// <summary>
        /// Gets or sets the Can remove the Image
        /// </summary>

        public bool CanRemoveImage {
            get { return this.canRemoveImage; }
            set {
                if (this.canRemoveImage != value) {
                    this.canRemoveImage = value;
                    SetPropertyChanged("CanRemoveImage");
                }
            }
        }



        private ICommand _AddImageCommand;
        public ICommand AddImageCommand {
            get {
                if (_AddImageCommand == null) {
                    _AddImageCommand = CreateCommand(AddImage);
                }
                return _AddImageCommand;
            }
        }


        private ICommand _RemoveImageCommand;
        public ICommand RemoveImageCommand {
            get {
                if (_RemoveImageCommand == null) {
                    _RemoveImageCommand = CreateCommand(RemoveImage);
                }
                return _RemoveImageCommand;
            }
        }

        public void RemoveImage(object obj) {
            if (MessageBox.Show($"Are you sure you want to remove Image '{SelectedImage.Name}'", "Delete Image?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                Images.Remove(SelectedImage);
                SelectedImage = null;
            }
        }



        public void AddImage(object obj) {
            var fileBrowser = new System.Windows.Forms.OpenFileDialog() { Multiselect = false };
            if (fileBrowser.ShowDialog() == DialogResult.OK) {
                var image = new ImageModel();
                image.SetImage(fileBrowser.FileName);
                Images.Add(image);
                SelectedImage = image;
            }
        }


        protected ICommand CreateCommand(Action<object> executeAction) {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");

            return new RelayCommand(executeAction);
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public void SetPropertyChanged(string propName) {
        //    var handler = PropertyChanged;
        //    if (handler != null) {
        //        handler.Invoke(this, new PropertyChangedEventArgs(propName));
        //    }
        //}
    }
}
