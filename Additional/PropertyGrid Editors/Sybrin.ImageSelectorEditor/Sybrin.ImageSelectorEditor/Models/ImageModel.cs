using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Serialization;

namespace Sybrin.ImageSelectorEditor {
    public class ImageModel : ModelBase {
        public ImageModel() {
            if (this.ImageData?.Length == 0) {
                this.ImageData = Convert.FromBase64String(ImageString);
            }
        }

        private string imageString = "";
        /// <summary>
        /// Gets or sets the Base64 string that will represent the ByteArray of the Image, to save to the Application.saf file.
        /// </summary>

        public string ImageString {
            get {
                return this.imageString;
            }
            set {
                if (this.imageString != value) {
                    this.imageString = value;
                }
            }
        }


        // Stores the Bytes of the Image to be saved to the Application.saf file.
        private byte[] imageData = null;
        /// <summary>
        /// Gets or sets the Image data in Bytes for the Image, used to bind to the Image control on the form.
        /// </summary>

        // Add these attributes to ignore saving the bytes to the Applicaiton.saf, as the byte array does not save properly.
        // Convert the byteArray to Base64 and rather save that string, and convert the Base64 string to a byteArray when loaded.
        [JsonIgnore, XmlIgnore]
        public byte[] ImageData {
            get {
                if (this.imageData == null && this.imageString != "") {
                    this.imageData = Convert.FromBase64String(ImageString);
                }return this.imageData; }
            private set {
                if (this.imageData != value) {
                    this.imageData = value;
                    SetPropertyChanged("ImageData");
                }
            }
        }

        private string name = "";
        /// <summary>
        /// Gets or sets the Name of the File
        /// </summary>

        public string Name {
            get { return this.name; }
            set {
                if (this.name != value) {
                    this.name = value;
                    SetPropertyChanged("Name");
                }
            }
        }

        public void SetImage(string path) {
            var imageFormat = determineImage(path);
            var image = Image.FromFile(path);

            ImageData = imageToByteArray(image, imageFormat);
            ImageString = Convert.ToBase64String(imageData);
            Name = Path.GetFileNameWithoutExtension(path);
        }



        private ImageFormat determineImage(string path) {
            var extension = Path.GetExtension(path).Replace(".", "");
            var imageFormat = ImageFormat.Jpeg;
            switch (extension.ToUpper()) {
                case "BMP": imageFormat = ImageFormat.Bmp; break;
                case "EMF": imageFormat = ImageFormat.Emf; break;
                case "EXIF": imageFormat = ImageFormat.Exif; break;
                case "GIF": imageFormat = ImageFormat.Gif; break;
                case "ICON": imageFormat = ImageFormat.Icon; break;
                case "JPEG":
                case "JPG": imageFormat = ImageFormat.Jpeg; break;
                case "MEMORYBMP": imageFormat = ImageFormat.MemoryBmp; break;
                case "PNG": imageFormat = ImageFormat.Png; break;
                case "WMF": imageFormat = ImageFormat.Wmf; break;
                default: throw new NotSupportedException($"Extension '{extension} not supported.");
            }

            return imageFormat;
        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn, ImageFormat format) {

            var ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();

        }

        private Image byteArrayToImage(byte[] byteArrayIn) {
            var ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }

    public class ImageModels : ObservableCollection<ImageModel> {
        public override string ToString() {
            return $"{Count} Image{(Count == 1 ? "" : "s")}";
        }
    }
}
