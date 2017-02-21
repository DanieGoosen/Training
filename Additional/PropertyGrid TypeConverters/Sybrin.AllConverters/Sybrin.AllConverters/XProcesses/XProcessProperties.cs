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
using Sybrin.AllConverters.Tools;
using System.Xml.Serialization;
using Sybrin.Extensions;

namespace Sybrin.AllConverters {
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

        private string docConverter = "{ Select a Document }";
        /// <summary>
        /// Gets or sets the DocConverter TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the DocConverter TypeConverter")]
        [DefaultValue("")]
        [DisplayName("1.1 - DocConverter")]
        [TypeConverter(typeof(DocConverter))]
        public string DocConverter {
            get { return this.docConverter; }
            set {
                if (this.docConverter != value) {
                    this.docConverter = value;
                    // Set the Document as the selected Document in the global static value.
                    Sybrin.Client.Attributes.DocKeyConverter.IndexList = null;
                    Sybrin.Client.Attributes.DocKeyConverter.DocNo = docConverterNo;
                    // Selects the document as the base document for the LinkedDocumentConverter to use to get Document Links.
                    Sybrin.Client.Attributes.LinkDocConverter.BaseDocNo = docConverterNo;
                }
            }
        }
        [Browsable(false), XmlIgnore()]
        public int docConverterNo { get { return (docConverter == null) ? 0 : docConverter.Split('-')[0].Trim().ToInt(); } }

        private string docKeyConverter = "0 - {Field not set}";
        /// <summary>
        /// Gets or sets Sybrin Index field mapped to the description property
        /// </summary>
        [DisplayName("1.2 - DocKeyConverter"), Category("1 - General"), DefaultValue("0 - {Field not set}")]
        [Description("The Sybrin Index field mapped to the description property")]
        [TypeConverter(typeof(DocKeyConverter))]
        public string DocKeyConverter {
            get { return docKeyConverter; }
            set { if (docKeyConverter != value) docKeyConverter = value; }
        }
        [Browsable(false), XmlIgnore()]
        public int DocKeyConverterNo { get { return (docKeyConverter == null) ? 0 : docKeyConverter.Split('-')[0].Trim().ToInt(); } }

        private string filesStoreConverter = "{ Select a FileStore }";
        /// <summary>
        /// Gets or sets the FileStoreConverter TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the FileStoreConverter TypeConverter")]
        [DefaultValue("")]
        [DisplayName("1.3 - FilesStoreConverter")]
        [TypeConverter(typeof(FilesStoreConverter))]
        public string FilesStoreConverter {
            get { return this.filesStoreConverter; }
            set {
                if (this.filesStoreConverter != value) {
                    this.filesStoreConverter = value;
                    Sybrin.Client.Attributes.LibraryConverter.DocNo = FileStoreConverterNo;
                }
            }
        }
        [Browsable(false), XmlIgnore()]
        public int FileStoreConverterNo { get { return (filesStoreConverter == null) ? 0 : filesStoreConverter.Split('-')[0].Trim().ToInt(); } }

        private string importToolConverter = "{ Select an Import Tool }";
        /// <summary>
        /// Gets or sets the ImportToolConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the ImportToolConverter")]
        [DefaultValue("")]
        [DisplayName("1.4 - ImportToolConverter")]
        [TypeConverter(typeof(ImportToolConverter))]
        public string ImportToolConverter {
            get { return this.importToolConverter; }
            set {
                if (this.importToolConverter != value) {
                    this.importToolConverter = value;
                }
            }
        }

        private string ivsTemplatesConverter = "{ Select an IVS Template }";
        /// <summary>
        /// Gets or sets the IVSTemplateConverter TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the IVSTemplateConverter TypeConverter")]
        [DefaultValue("")]
        [DisplayName("1.5 - IVSTemplatesConverter")]
        [TypeConverter(typeof(IVSTemplatesConverter))]
        public string IVSTemplatesConverter {
            get { return this.ivsTemplatesConverter; }
            set {
                if (this.ivsTemplatesConverter != value) {
                    this.ivsTemplatesConverter = value;
                }
            }
        }

        private string libraryConverter = "{ Select a Library }";
        /// <summary>
        /// Gets or sets the Library TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the Library TypeConverter")]
        [DefaultValue("")]
        [DisplayName("1.6 - LibraryConverter")]
        [TypeConverter(typeof(LibraryConverter))]
        public string LibraryConverter {
            get { return this.libraryConverter; }
            set {
                if (this.libraryConverter != value) {
                    this.libraryConverter = value;
                }
            }
        }

        private string linkDocConverter = "{ Select a Linked Document }";
        /// <summary>
        /// Gets or sets the LinkDocument TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the LinkDocument TypeConverter")]
        [DefaultValue("")]
        [DisplayName("1.7 - LinkDocConverter")]
        [TypeConverter(typeof(LinkDocConverter))]
        public string LinkDocConverter {
            get { return this.linkDocConverter; }
            set {
                if (this.linkDocConverter != value) {
                    this.linkDocConverter = value;
                }
            }
        }

        private string pipeConverter = "{ Select a Pipe }";
        /// <summary>
        /// Gets or sets the PipeConverter TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the PipeConverter TypeConverter")]
        [DefaultValue("")]
        [DisplayName("1.8 - PipeConverter")]
        [TypeConverter(typeof(PipeConverter))]
        public string PipeConverter {
            get { return this.pipeConverter; }
            set {
                if (this.pipeConverter != value) {
                    this.pipeConverter = value;
                    Sybrin.Client.Attributes.PipeStageConverter.PipeID = PipeConverterNo;
                }
            }
        }
        [Browsable(false), XmlIgnore()]
        public int PipeConverterNo { get { return (pipeConverter == null) ? 0 : pipeConverter.Split('-')[0].Trim().ToInt(); } }

        private string pipeStageConverter = "{ Select a Pipe Stage }";
        /// <summary>
        /// Gets or sets the PipeStage Converter
        /// </summary>
        [Category("1 - General"), Description("Sets the PipeStage Converter")]
        [DefaultValue("")]
        [DisplayName("1.9 - PipeStageConverter")]
        [TypeConverter(typeof(PipeStageConverter))]
        public string PipeStageConverter {
            get { return this.pipeStageConverter; }
            set {
                if (this.pipeStageConverter != value) {
                    this.pipeStageConverter = value;
                }
            }
        }

        private string reportListConverter = "{ Select a Report }";
        /// <summary>
        /// Gets or sets the ReportListConverter TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the ReportListConverter TypeConverter [Sybrin.Client.Attributes.ReportListConverter]")]
        [DefaultValue("")]
        [DisplayName("2.1 - ReportListConverter")]
        [TypeConverter(typeof(ReportListConverter))]
        public string ReportListConverter {
            get { return this.reportListConverter; }
            set {
                if (this.reportListConverter != value) {
                    this.reportListConverter = value;
                }
            }
        }

        private string secondaryDocKeyConverter = "{ Select a Secondary Document Key }";
        /// <summary>
        /// Gets or sets the SecondaryDocKey TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the SecondaryDocKey TypeConverter [Sybrin.Client.Attributes.SecondaryDocKeyConverter]")]
        [DefaultValue("")]
        [DisplayName("2.2 - SecondaryDocKeyConverter")]
        [TypeConverter(typeof(SecondaryDocKeyConverter))]
        public string SecondaryDocKeyConverter {
            get { return this.secondaryDocKeyConverter; }
            set {
                
                if (this.secondaryDocKeyConverter != value) {
                    this.secondaryDocKeyConverter = value;
                }
            }
        }

        private string sybrinListConverter = "{ Select a Sybrin List }";
        /// <summary>
        /// Gets or sets the Sybrin List Converter
        /// </summary>
        [Category("1 - General"), Description("Sets the Sybrin List Converter [Sybrin.Client.Attributes.SybrinListConverter]")]
        [DefaultValue("")]
        [DisplayName("2.3 - SybrinListConverter")]
        [TypeConverter(typeof(SybrinListConverter))]
        public string SybrinListonverter {
            get { return this.sybrinListConverter; }
            set {
                if (this.sybrinListConverter != value) {
                    this.sybrinListConverter = value;
                }
            }
        }


        private string tableListConverter = "{ Select a Table }";
        /// <summary>
        /// Gets or sets the TableListConverter TypeConverter
        /// </summary>
        [Category("1 - General"), Description("Sets the TableListConverter TypeConverter [Sybrin.Client.Attributes.TableListConverter]")]
        [DefaultValue("")]
        [DisplayName("2.5 - TableListConverter")]
        [TypeConverter(typeof(TableListConverter))]
        public string TableListConverter {
            get { return this.tableListConverter; }
            set {
                if (this.tableListConverter != value) {
                    this.tableListConverter = value;
                    Sybrin.Client.Attributes.TableColumnListConverter.tableName = tableListConverter;
                }
            }
        }

        private string tableColumnListConverter = "{ Select a Column }";
        /// <summary>
        /// Gets or sets the TableColumnListConverter TypeConverter. []
        /// </summary>
        [Category("1 - General"), Description("Sets the TableColumnListConverter TypeConverter. [Sybrin.Client.Attributes.TableColumnListConverter]")]
        [DefaultValue("")]
        [DisplayName("2.6 - TableColumnListConverter")]
        [TypeConverter(typeof(TableColumnListConverter))]
        public string TableColumnListConverter {
            get { return this.tableColumnListConverter; }
            set {
                if (this.tableColumnListConverter != value) {
                    this.tableColumnListConverter = value;
                }
            }
        }


        private string userGroupConverter = "{ Select a User Group }";
        /// <summary>
        /// Gets or sets the UserGroupConverter TypeConverter [Sybrin.Client.Attributes.UserGroupConverter]
        /// </summary>
        [Category("1 - General"), Description("Sets the UserGroupConverter TypeConverter [Sybrin.Client.Attributes.UserGroupConverter]")]
        [DefaultValue("")]
        [DisplayName("2.7 - UserGroupConverter")]
        [TypeConverter(typeof(UserGroupConverter))]
        public string UserGroupConverter {
            get { return this.userGroupConverter; }
            set {
                if (this.userGroupConverter != value) {
                    this.userGroupConverter = value;
                }
            }
        }

        private string userListConverter = "{ Select a User List }";
        /// <summary>
        /// Gets or sets the UserListConverter TyepConverter [Sybrin.Client.Attributes.UserListConverter]
        /// </summary>
        [Category("1 - General"), Description("Sets the UserListConverter TyepConverter [Sybrin.Client.Attributes.UserListConverter]")]
        [DefaultValue("")]
        [DisplayName("2.8 - UserListConverter")]
        [TypeConverter(typeof(UserListConverter))]
        public string UserListConverter {
            get { return this.userListConverter; }
            set {
                if (this.userListConverter != value) {
                    this.userListConverter = value;
                }
            }
        }

        private string userValuesConverter = "{ Select a UserValue }";
        /// <summary>
        /// Gets or sets the UserValuesConverter TypeConverter [Sybrin.Client.Attributes.UserValuesConverter]
        /// </summary>
        [Category("1 - General"), Description("Sets the UserValuesConverter TypeConverter [Sybrin.Client.Attributes.UserValuesConverter]")]
        [DefaultValue("")]
        [DisplayName("2.9 - UserValuesConverter")]
        [TypeConverter(typeof(UserValuesConverter))]
        public string UserValuesConverter {
            get { return this.userValuesConverter; }
            set {
                if (this.userValuesConverter != value) {
                    this.userValuesConverter = value;
                }
            }
        }

        private TestEnum sortedEnumConverter = TestEnum.AnotherValue;
        /// <summary>
        /// Gets or sets the SortedEnumConverter TypeConverter [Sybrin10.Attributes.SortedEnumConverter]
        /// </summary>
        [Category("1 - General"), Description("Sets the SortedEnumConverter TypeConverter [Sybrin10.Attributes.SortedEnumConverter]")]
        [DefaultValue(TestEnum.AnotherValue)]
        [DisplayName("3.1 - SortedEnumConverter")]
        [TypeConverter(typeof(SortedEnumConverter))]
        public TestEnum SortedEnumConverter {
            get { return this.sortedEnumConverter; }
            set {
                if (this.sortedEnumConverter != value) {
                    this.sortedEnumConverter = value;
                }
            }
        }

        private string expandableConverter = "";
        /// <summary>
        /// Gets or sets the ExpandableConverter TypeConverter [Sybrin10.Kernel.Attributes.ExpandableConverter]
        /// </summary>
        [Category("1 - General"), Description("Sets the ExpandableConverter TypeConverter [Sybrin10.Kernel.Attributes.ExpandableConverter]")]
        [DefaultValue("")]
        [DisplayName("3.2 - ExpandableConverter")]
        [TypeConverter(typeof(ExpandableConverter))]
        public string ExpandableConverter {
            get { return this.expandableConverter; }
            set {
                if (this.expandableConverter != value) {
                    this.expandableConverter = value;
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
            return string.Format("(Sybrin.AllConverters, Configured = {0})", Enabled);
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
