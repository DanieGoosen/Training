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
using Sybrin.XProcessing.Data;

namespace Sybrin.XProcessing {
    public class XProcessProperties : CustomDescriptorBase {
        #region Constructors


        #endregion Constructors

        #region Properties

        private string fileName = "";
        /// <summary>
        /// Gets or sets the Location to the File name to be read
        /// </summary>
        [Category("1 - General"), Description("Sets the Location to the File name to be read")]
        [DefaultValue("")]
        [DisplayName("1 - File Name")]
        public string FileName {
            [DebuggerNonUserCode]
            get { return this.fileName; }
            [DebuggerNonUserCode]
            set {
                if (this.fileName != value) {
                    this.fileName = value;
                }
            }
        }

        private string nameProperty = "Idx1";
        /// <summary>
        /// Gets or sets the Property uin the IXObject where the Name will be saved
        /// </summary>
        [Category("1 - General"), Description("Sets the Property uin the IXObject where the Name will be saved")]
        [DefaultValue("Idx1")]
        [DisplayName("2.1 - Name Property")]
        public string NameProperty {
            [DebuggerNonUserCode]
            get { return this.nameProperty; }
            [DebuggerNonUserCode]
            set {
                if (this.nameProperty != value) {
                    this.nameProperty = value;
                }
            }
        }

        private string surnameproperty = "Idx2";
        /// <summary>
        /// Gets or sets the pdsarsdf
        /// </summary>
        [Category("1 - General"), Description("Sets the pdsarsdf")]
        [DefaultValue("Idx2")]
        [DisplayName("2.2 Surname Property")]
        public string SurnameProperty {
            [DebuggerNonUserCode]
            get { return this.surnameproperty; }
            [DebuggerNonUserCode]
            set {
                if (this.surnameproperty != value) {
                    this.surnameproperty = value;
                }
            }
        }

        private Student student = new Student();
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [Category("1 - General"), Description("Sets the description")]
        [DefaultValue("")]
        [DisplayName("4 - My Stuydent")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Student Student {
            [DebuggerNonUserCode]
            get { return this.student; }
            [DebuggerNonUserCode]
            set {
                if (this.student != value) {
                    this.student = value;
                }
            }
        }

        private Students students = new Students();
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [Category("1 - General"), Description("Sets the description")]
        [DefaultValue("")]
        [DisplayName("4.2 - Students")]
        [Editor(typeof(DescriptiveCollectionEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(TypeConverter))]
        public Students Students {
            [DebuggerNonUserCode]
            get { return this.students; }
            [DebuggerNonUserCode]
            set {
                if (this.students != value) {
                    this.students = value;
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
            return string.Format("(Sybrin.XProcessing)");
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
