using Sybrin10.Kernel.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sybrin10.Attributes;
using System.ComponentModel;
using Sybrin10.Kernel.Attributes;
using System.Drawing.Design;
using Sybrin.Client.Attributes;
using System.Diagnostics;

namespace Sybrin.CustomFileLoader {
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

        private string filePath = "";
        /// <summary>
        /// Gets or sets the File path of the File to be loaded
        /// </summary>
        [Category("1 - General"), Description("Sets the File path of the File to be loaded")]
        [DefaultValue("")]
        [DisplayName("2 - File Path")]
        [Editor(typeof(FileSelector), typeof(UITypeEditor))]
        public string FilePath {
            [DebuggerNonUserCode]
            get { return this.filePath; }
            [DebuggerNonUserCode]
            set {
                if (this.filePath != value) {
                    this.filePath = value;
                }
            }
        }

        private bool firstLineContainsHeaders = true;
        /// <summary>
        /// Gets or sets the Whether the first line of the file contains the Headers
        /// </summary>
        [Category("1 - General"), Description("Sets the Whether the first line of the file contains the Headers")]
        [DefaultValue(true)]
        [DisplayName("3 - First Line Contains Headers")]
        public bool FirstLineContainsHeaders {
            [DebuggerNonUserCode]
            get { return this.firstLineContainsHeaders; }
            [DebuggerNonUserCode]
            set {
                if (this.firstLineContainsHeaders != value) {
                    this.firstLineContainsHeaders = value;
                }
            }
        }

        private bool skipLines = false;
        /// <summary>
        /// Gets or sets the whether some lines should be skipped durinf the File Loading process
        /// </summary>
        [Category("1 - General"), Description("Sets the whether some lines should be skipped durinf the File Loading process")]
        [DefaultValue(false)]
        [DisplayName("4.1 - Skip Lines")]
        public bool SkipLines {
            [DebuggerNonUserCode]
            get { return this.skipLines; }
            [DebuggerNonUserCode]
            set {
                if (this.skipLines != value) {
                    this.skipLines = value;
                }
            }
        }

        private int amountToSkip = 1;
        /// <summary>
        /// Gets or sets the amount of lines to skip
        /// </summary>
        [Category("1 - General"), Description("Sets the amount of lines to skip")]
        [DefaultValue(1)]
        [DisplayName("4.2 - Amount to Skip")]
        [DynamicPropertyFilter("SkipLines", "True")] // Only show this property when the Property 'SkipLines' is set to True.
        public int AmountToSkip {
            [DebuggerNonUserCode]
            get { return this.amountToSkip; }
            [DebuggerNonUserCode]
            set {
                if (this.amountToSkip != value) {
                    this.amountToSkip = value;
                }
            }
        }

        private char seperationChar = ',';
        /// <summary>
        /// Gets or sets the Seperation character that is used to seperate values.
        /// </summary>
        [Category("1 - General"), Description("Sets the Seperation character that is used to seperate values.")]
        [DefaultValue(',')]
        [DisplayName("5 - Seperation Character")]
        public char SeperationChar {
            [DebuggerNonUserCode]
            get { return this.seperationChar; }
            [DebuggerNonUserCode]
            set {
                if (this.seperationChar != value) {
                    this.seperationChar = value;
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
            return string.Format("(Sybrin.CustomFileLoader, Configured = {0})", Enabled);
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
