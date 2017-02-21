# PropertyGrid Type Converters
A Typeconverter is similar to an Editor in a sense that it tells the PropertyGrid that it must change the behaviour of a certain object. However, Typeconverters are nit as flexible as Editors in terms of opening new windows to change the object. It is limited to how *that* property is *displayed* in the PropertyGrid.

> Common use for ```TypeConverters``` is to change the appearance (DisplayText) of a property, allowing the Value ofa property to be selected from a dropdown of values, etc.

Usage:

```cs
[TypeConverter(typeof(MyConverter))]
public string MyProperty { get; set; }
```

Here are a list of Commly used TypeConverters:

# TypeConverter
When you have a complex object and you have overridden its ```ToString``` method, you can use the TypeConverter to update the PropertyGrid value with the value returned from the ToString.
```cs
[TypeConverter(typeof(TypeConverter))]
Students student { get; set; }
``` 

Lets say the ```Students``` object is a collection of the following class:

```cs
namespace Sample{
    public class Student{
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }

        public string ToString(){
            return string.Format("{0}, {1} - {2}", Lastname, Name, Age);
        }
    }

    public class Students : List<Student>{
        public string ToString(){
            return string.Format("{0} Student{1}", this.Count, this.Count == 1 ? "" : "s");
        }
    }
}
``` 

Without a ```[TypeConverter(typeof(TypeConverter))]``` attribute on this property, it will be displayed in the propertyGrid like the following:

```cs
Students student { get; set; }
// Will be displayed as 'Sample.Students'
```

```cs
[TypeConverter(typeof(TypeConverter))]
Students student { get; set; }
// Will be displayed as '0 Students', where '0' will be substitute with how many objects there are in the Students array.
```


# ExpandableObjectConverter
Allows a complex object to be expanded, so that its other properties can be configured on the PropertyGrid.

```cs
private object expandableObjectConverter = "";
/// <summary>
/// Gets or sets the ExpandableConverter TypeConverter [Sybrin10.Kernel.Attributes.ExpandableConverter]
/// </summary>
[Category("1 - General"), Description("Sets the ExpandableConverter TypeConverter [Sybrin10.Kernel.Attributes.ExpandableConverter]")]
[DefaultValue("")]
[DisplayName("3.2 - ExpandableConverter")]
[TypeConverter(typeof(ExpandableConverter))]
public object ExpandableConverter {
    get { return this.expandableConverter; }
    set {
        if (this.expandableConverter != value) {
            this.expandableConverter = value;
        }
    }
}
```

# DocConverter

The DocConverter TypeConverter allows you to save a selected Document to a string value, by selecting the appropriate document from a dropDown.
> Note:
> * This converter Requires a Sybrin Client to be logged on to a server.
> * You need to manually set the DocNo to the DocKeyConverter Attribute when a document has changed, so that the DocKeyConverter knows which document to use when the fields are retrieved. 

```cs
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
```

# DocKeyConverter
Allows you to select a Document Field from a dropdown, and save the Field Name to a string value.

> Note: 
> * Requires Sybrin Client to be logged on to a server.
> * Requires a document to be selected through a DocConverter (With the Selected Document assigned to the static DocKeyConverter.DocNo attribute)


```cs
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
```

# FileStoreConverter
Allows you to select one of the File Stores configured on the server.
> Note: 
> * Requires a Sybrin Client to be logged on to a Server.

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

# ImportToolConverter
When you want to select a configured import tool on the server.
> Note:
> * Requires a Sybrin Client to be connected to a server with Import tools configured.
```cs
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
```

# IVSTemplateConverter
When you want to select an IVS Template from a list of configured IVS Templates.
> Note:
> * Requires a connection to a Sybrin Server with IVS Templates configured.

```cs
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
``` 

# LibraryConverter
Allows you to select a library from a filestore.
> Note:
> * Requires a connection to a Sybrin Server that has Libraries configured (Under at least one Filestore)
> * Requires the a Fielstore number assigned to the static LibraryConverter.DocNo property. This should be done when a Filestore is selected.
```cs
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
```

# LinkDocConverter
Like the DocConverter, this TypeConverter allows you to seelct a linked document that is linked to the selected Document.
> Note:
> * Requires a connection to a Sybrin Server
> * Requires a document Number to be assigned to the static value '```Sybrin.Client.Attributes.LinkDocConverter.BaseDocNo```' to pull its linked documents from.
> * Requires Linked Documents to be configured on the above mentioned document
```cs
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
```

# PipeConverter
Allows a pipe to be selected from a list.
> Note: Requires a connection to a Sybrin Server with Pipes configured.
```cs
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
            // Set the Selected Pipe ID for the StageConverter to be populated with the correct PipeStages, belonging to that PipeID.
            Sybrin.Client.Attributes.PipeStageConverter.PipeID = PipeConverterNo;
        }
    }
}
[Browsable(false), XmlIgnore()]
public int PipeConverterNo { get { return (pipeConverter == null) ? 0 : pipeConverter.Split('-')[0].Trim().ToInt(); } }
```

# PipeStageConverter
Allows you to select a stage from a selected pipe.

> Note:
> * Requires a Connection to a Sybrin Server that has Application Stages configured in at least one pipe. 
> * Requires a valid Pipe to be selected in the  static value ```Sybrin.Client.Attributes.PipeStageConverter.PipeID```

```cs
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
```

# ReportListConverter
Selects a report from the configured reports on the Server.
> Note:
> * Requires a connection to a Sybrin Server with one or more reports configured.
```cs
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
```

# SecondaryDocKeyConverter
This is exactly the same as the DocKeyConverter, but allows a second document to be used. This is handy if you want to configure more than one Document and their index fields. This secondaryDocKeyConverter allows an additional list of Sybrin Fields to be used.
> Note: Requires a conneciton to a Sybrin Server with at least one Document configured.
> * Requires the static ```Sybrin.Client.Attributes.SecondaryDocKeyConverter.DocNo``` value to be set to a secondary document number.
```cs
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
```

# SybrinListConvconverter
Allows you to select a List configured on a Server.
> Note: Requires a connection to a Sybrin Server that contains one or more configured list 
```cs
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
```

# TableListConverter
Allows you to select from a list of tables that is found in the SQL server that the Server is pointing to.
> Note: Requires a connection to the Sybrin Server.
```cs
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
            // Select the selected Table as the table to use to pull Columns from using the TableColumnLuistConverter.
            Sybrin.Client.Attributes.TableColumnListConverter.tableName = tableListConverter;
        }
    }
}
```

# TableColumnListConverter
Allows you to select a column from the selected table in the TableListConverter.
> Note: 
> * Requires a connection to a Sybrin Server.
> * Requires a table to be selected and assigned to the static ```Sybrin.Client.Attributes.TableColumnListConverter.tableName``` value.
```cs
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
```

# UserGroupConverter
Allows you to select a UserGroup value from a dropdown.
> Note: Requires a connection to a Sybrin Server wit hUserGroups configured.
```cs
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
```

# UserListConverter

Allows you to select a user from a List of users on the Server.
> Note: 
> * Requires a connection to a Sybrin Server.
> * Requires one or more users to be configured on this server.

```cs
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
```
# UserValuesConverter
Allows you to select a Uservalue from a dropdown.

> Note: Requires a connection to a Sybrin Server.
> * Requires at least one or more User Values configured on the server.
```cs
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
```

# SortedEnumConverter
Allows you to select an Enum value from a sorted list (Populated from the Enum)
> Note: Requires the type to be mapped to an Enum.
```cs
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
```
