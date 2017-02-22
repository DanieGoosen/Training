using Sybrin.ImageSelectorEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sybrin.DropDownEditor.Editors {
    public class ImageDropDownVM : ModelBase {

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
            get { return this.selectedImage; }
            set {
                if (this.selectedImage != value) {
                    this.selectedImage = value;
                    SetPropertyChanged("SelectedImage");
                    SelectedIndexCallback?.Invoke(this.Images.IndexOf(value));
                }
            }
        }

        private string searchText = "";
        /// <summary>
        /// Gets or sets the Search Text to search on.
        /// </summary>

        public string SearchText {
            get { return this.searchText; }
            set {
                if (this.searchText != value) {
                    this.searchText = value;
                    search(value.Trim().ToUpper());
                    SetPropertyChanged("SearchText");
                }
            }
        }

        private void search(string text) {
            if (text == "")
                Images.ToList().ForEach(f => f.IsVisible = true);
            else {
                Images.ToList().ForEach(f => f.IsVisible = true);
                Images.ToList().Where(f => !f.Name.Trim().ToUpper().Contains(text)).ToList().ForEach(f => f.IsVisible = false);
            }
        }


        // Method that will be executed when the Selected Item (Index) has changed, to close the Dropdown.
        public Action<int> SelectedIndexCallback { get; set; }

    }
}
