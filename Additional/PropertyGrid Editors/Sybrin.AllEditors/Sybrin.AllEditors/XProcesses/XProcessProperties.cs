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
using Sybrin10.Dynamic;
using Sybrin10.Attributes.Editors;

namespace Sybrin.AllEditors {
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

        private Sybrin10.Scheduler.Schedule scheduleEditor = new Sybrin10.Scheduler.Schedule();
        /// <summary>
        /// Gets or sets the ScheduleEditor Editor [Sybrin10.Kernel.Attributes.ScheduleEditor]
        /// </summary>
        [Category("1 - General"), Description("Sets the ScheduleEditor Editor [Sybrin10.Kernel.Attributes.ScheduleEditor]")]
        [DefaultValue(null)]
        [DisplayName("4.1 - ScheduleEditor")]
        [Editor(typeof(ScheduleEditor), typeof(UITypeEditor))]
        public Sybrin10.Scheduler.Schedule ScheduleEditor {
            get { return this.scheduleEditor; }
            set {
                if (this.scheduleEditor != value) {
                    this.scheduleEditor = value;
                }
            }
        }

        private string folderBrowser = "";
        /// <summary>
        /// Gets or sets the FolderBrowser Editor [Sybrin10.Kernel.Attributes.FolderBrowser]
        /// </summary>
        [Category("1 - General"), Description("Sets the FolderBrowser Editor [Sybrin10.Kernel.Attributes.FolderBrowser]")]
        [DefaultValue("")]
        [DisplayName("4.2 - FolderBrowser")]
        [Editor(typeof(FolderBrowser), typeof(UITypeEditor))]
        public string FolderBrowser {
            get { return this.folderBrowser; }
            set {
                if (this.folderBrowser != value) {
                    this.folderBrowser = value;
                }
            }
        }

        private string fileSelector = "";
        /// <summary>
        /// Gets or sets the FileSelector Editor [Sybrin10.Kernel.Attributes.FileSelector]
        /// </summary>
        [Category("1 - General"), Description("Sets the FileSelector Editor [Sybrin10.Kernel.Attributes.FileSelector]")]
        [DefaultValue("")]
        [DisplayName("4.3 - FileSelector")]
        [Editor(typeof(FileSelector), typeof(UITypeEditor))]
        public string FileSelector {
            get { return this.fileSelector; }
            set {
                if (this.fileSelector != value) {
                    this.fileSelector = value;
                }
            }
        }

        private string connectionStringBuilderEditor = "";
        /// <summary>
        /// Gets or sets the ConnectionStringBuilderEditor Editor [Sybrin10.Kernel.Attributes.ConnectionStringBuilderEditor]
        /// </summary>
        [Category("1 - General"), Description("Sets the ConnectionStringBuilderEditor Editor [Sybrin10.Kernel.Attributes.ConnectionStringBuilderEditor]")]
        [DefaultValue("")]
        [DisplayName("4.5.1 - ConnectionStringBuilderEditor")]
        [Editor(typeof(ConnectionStringBuilderEditor), typeof(UITypeEditor))]
        public string ConnectionStringBuilderEditor {
            get { return this.connectionStringBuilderEditor; }
            set {
                if (this.connectionStringBuilderEditor != value) {
                    this.connectionStringBuilderEditor = value;
                }
            }
        }

        private string connectionStringEditor = "";
        /// <summary>
        /// Gets or sets the ConnectionStringEditor Editor [Sybrin10.Attributes.Editors.ConnectionStringEditor]
        /// </summary>
        [Category("1 - General"), Description("Sets the ConnectionStringEditor Editor [Sybrin10.Attributes.Editors.ConnectionStringEditor]")]
        [DefaultValue("")]
        [DisplayName("4.5.2 - ConnectionStringEditor")]
        [Editor(typeof(ConnectionStringEditor), typeof(UITypeEditor))]
        public string ConnectionStringEditor {
            get { return this.connectionStringEditor; }
            set {
                if (this.connectionStringEditor != value) {
                    this.connectionStringEditor = value;
                }
            }
        }

        private System.Drawing.Color colorEditor = default(System.Drawing.Color);
        /// <summary>
        /// Gets or sets the ColorEditor Editor [Sybrin10.Kernel.Attributes.ColorEditor]
        /// </summary>
        [Category("1 - General"), Description("Sets the ColorEditor Editor [Sybrin10.Kernel.Attributes.ColorEditor]")]
        [DefaultValue("")]
        [DisplayName("4.6.1 - ColorEditor")]
        [Editor(typeof(Sybrin10.Kernel.Attributes.ColorEditor), typeof(UITypeEditor))]
        public System.Drawing.Color ColorEditor {
            get { return this.colorEditor; }
            set {
                if (this.colorEditor != value) {
                    this.colorEditor = value;
                }
            }
        }

        private System.Drawing.Color colorEditor2 = default(System.Drawing.Color);
        /// <summary>
        /// Gets or sets the ColorEditor Editor [Sybrin10.Kernel.Attributes.ColorEditor]
        /// </summary>
        [Category("1 - General"), Description("Sets the ColorEditor Editor [System.Drawing.Design.ColorEditor]")]
        [DefaultValue("")]
        [DisplayName("4.6.2 - ColorEditor")]
        [Editor(typeof(System.Drawing.Design.ColorEditor), typeof(UITypeEditor))]
        public System.Drawing.Color ColorEditor2 {
            get { return this.colorEditor2; }
            set {
                if (this.colorEditor2 != value) {
                    this.colorEditor2 = value;
                }
            }
        }

        private IXProcess catalogItemSelector = default(IXProcess);
        /// <summary>
        /// Gets or sets the CatalogItemSelector Editor [Sybrin10.Kernel.Attributes.CatalogItemSelector]
        /// </summary>
        [Category("1 - General"), Description("Sets the CatalogItemSelector Editor [Sybrin10.Kernel.Attributes.CatalogItemSelector]")]
        [DefaultValue(default(IXProcess))]
        [DisplayName("4.7 - CatalogItemSelector")]
        [Editor(typeof(CatalogItemSelector), typeof(UITypeEditor))]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IXProcess CatalogItemSelector {
            get { return this.catalogItemSelector; }
            set {
                if (this.catalogItemSelector != value) {
                    this.catalogItemSelector = value;
                }
            }
        }

        private object[] descriptiveCollectionEditor = new List<object>().ToArray();
        /// <summary>
        /// Gets or sets the DescriptiveCollectionEditor Editor [Sybrin10.Attributes.DescriptiveCollectionEditor]
        /// </summary>
        [Category("1 - General"), Description("Sets the DescriptiveCollectionEditor Editor [Sybrin10.Attributes.DescriptiveCollectionEditor]")]
        [DefaultValue(null)]
        [DisplayName("5.1 - DescriptiveCollectionEditor")]
        [Editor(typeof(DescriptiveCollectionEditor), typeof(UITypeEditor))]
        public object[] DescriptiveCollectionEditor {
            get { return this.descriptiveCollectionEditor; }
            set {
                if (this.descriptiveCollectionEditor != value) {
                    this.descriptiveCollectionEditor = value;
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
            return string.Format("(Sybrin.AllEditors, Configured = {0})", Enabled);
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
