using Sybrin.ImageSelectorEditor.Editors;
using Sybrin10.Kernel.BaseClasses;
using System.ComponentModel;
using System.Drawing.Design;

namespace Sybrin.ImageSelectorEditor {
    public class XProcessProperties : CustomDescriptorBase {
        #region Constructors


        #endregion Constructors

        #region Properties

        #region Enabled
        private bool enabled = true;
        /// <summary>
        /// Gets or sets whether the Properties are enabled or not
        /// </summary>
        [Category("1 - General"), Description("Sets whether the Properties are enabled or not")]
        [DefaultValue("True")]
        [DisplayName("1 - Enabled")]
        public bool Enabled {
            get { return enabled; }
            set {
                if (enabled != value) {
                    enabled = value;
                }
            }
        }
        #endregion Enabled

        private ImageModels images = new ImageModels();
        /// <summary>
        /// Gets or sets the Images to be saved to the Configuration
        /// </summary>
        [Category("1 - General"), Description("Sets the Images to be saved to the Configuration")]
        [DefaultValue(null)]
        [DisplayName("2 - Images")]
        [Editor(typeof(Editors.ImageEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(TypeConverter))]
        public ImageModels Images {
            get { return this.images; }
            set {
                if (this.images != value) {
                    this.images = value;
                }
            }
        }

        #endregion Properties

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetPropertyChanged(string propName) {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion Events

        #region Methods

        public override string ToString() {
            return string.Format("(Sybrin.ImageSelectorEditor, Configured = {0})", Enabled);
        }

        #endregion Methods

        #region Helpers
        /// <summary>
        /// The following References should be included in 'using'
        /// - Sybrin10.Kernel.Attributes    (For Sybrin Attributes)
        /// - System.ComponentModel     (For Normal System Attributes)
        /// - System.Drawing.Design     (For Editors)
        /// - Sybrin.Client.Attributes  (DocConverter, DocKeyConverter etc)
        /// </summary>


        #region FolderBrowser
        //private string filePath = "";
        ///// <summary>
        ///// Gets or sets the location of a file using a Folder Browser Editor
        ///// </summary>
        //[Category("1 - Helpers"), Description("Sets the location of a file using a Folder Browser Editor")]
        //[DefaultValue("")]
        //[DisplayName("1.1 - Folder Path")]
        //[Editor(typeof(FolderBrowser), typeof(UITypeEditor))]
        //public string TemporyFilePath {
        //    get { return filePath; }
        //    set {
        //        if (filePath != value)
        //            filePath = value;
        //    }
        //} 
        #endregion FolderBrowser

        #region DocumentConverter
        //private string document;
        ///// <summary>
        ///// Gets or sets the document that your XProcess is using
        ///// </summary>
        //[Category("1 - General"), Description("Sets the document that LetterGeneration is using")]
        //[DefaultValue("Select a Document")]
        //[DisplayName("1.2 - Sybrin Document")]
        //[TypeConverter(typeof(DocConverter))]
        //public string Document {
        //    get { return document; }
        //    set {
        //        if (document != value) {
        //            document = value;
        //        }
        //    }
        //} 
        #endregion DocumentConverter

        #region DocKeyConverter
        //private string docFields;
        ///// <summary>
        ///// Gets or sets the field from the selected document
        ///// </summary>
        //[Category("1 - General"), Description("Sets the field from the selected document")]
        //[DefaultValue("")]
        //[DisplayName("1.3 Sybrin Field")]
        //[TypeConverter(typeof(DocKeyConverter))]
        //public string MyProperty {
        //    get { return docFields; }
        //    set {
        //        if (docFields != value) {
        //            docFields = value;
        //        }
        //    }
        //} 
        #endregion DocKeyConverter

        #region Multiline String Converter

        //private string sqlSelect;
        ///// <summary>
        ///// Gets or sets the SQL Statement to execute
        ///// </summary>
        //[Category("1 - General"), Description("Sets the SQL Statement to execute")]
        //[DefaultValue("")]
        //[DisplayName("x.x SQL Select")]
        //[Editor(typeof(MultilineStringConverter), typeof(UITypeEditor))]
        //public string SqlSelect {
        //    get { return sqlSelect; }
        //    set {
        //        if (sqlSelect != value) {
        //            sqlSelect = value;
        //        }
        //    }
        //} 
        #endregion Multiline String Converter

        #endregion Helpers
    }
}
