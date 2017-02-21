# PropertyGrid Editors

An ```Editor``` that is applied to a property in a ```PropertyGrid``` will change the way the ```PropertyGrid``` will handel that ```object```. For instance, if you have an ```object``` that contains a colleciton of pictures, and you want to view, add, or remove these images from the object. You will have to create a ```Custom PropertyGrid Editor``` to allow the PropertyGrid to handle this behaviour you desire. 

Before we get to creating your own Editor, we will look at the existing editors that can be used for common purposes:

An ```Editor``` can be applied to a property by using the ```Editor``` Attribute to decorate the Property with metaData, so that the PropertyGrid knows what to do with this object.

```cs
[Editor(typeof(MyEditor), typeof(UITypeEditor))]
public MyProperty Prop { get; set; }
```

Looking at the example above, the PropertyGrid will know that the property 'Prop' (of type ```MyProperty```) will use the Editor ```MyEditor```, that is a ```UITypeEditor``` editor type.

> Whenever a PropertyGrid needs to change the behaviour of some object in terms of opening a new window, changing the (visual) contents of a dropdown, an Editor will need to be used. 

Here are a few Commonly used Editors:
# ScheduleEditor

Used to configure a Sybrin10 ```Schedule``` object, that can in turn be used to execute some task at a specific schedule.

```cs
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
```


# Folder Browser
A folder browser works with a ```string``` type, and will allow the PropertyGrid to open up a Folder browser for you to easily supplu the ```string``` a value of a location of a folder. 

```cs
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
```

# File Selector
Like the folder Browser, the File Selector works with a type ```string``` and will allow you to open up a File Selector window to easily supply a value of a selected file to the ```string```
```cs
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
```

# Connection String Builder
When you need to configure a connection string to a database, its difficult to manually build up the connection string. The ```Connection String Editor``` will take you through a small wizard like process to build up a connection string.
```cs
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
```

# Color Picker
When you have to select a color instead of manually typing in the RGB value or the HEX code, a color picker Editor can come in handy.
```cs
private System.Drawing.Color colorEditor2 = default(System.Drawing.Color);
/// <summary>
/// Gets or sets the ColorEditor Editor [Sybrin10.Kernel.Attributes.ColorEditor]
/// </summary>
[Category("1 - General"), Description("Sets the ColorEditor Editor [System.Drawing.Design.ColorEditor]")]
[DefaultValue("")]
[DisplayName("4.6 - ColorEditor")]
[Editor(typeof(System.Drawing.Design.ColorEditor), typeof(UITypeEditor))]
public System.Drawing.Color ColorEditor2 {
    get { return this.colorEditor2; }
    set {
        if (this.colorEditor2 != value) {
            this.colorEditor2 = value;
        }
    }
}
```

# Catalog Item Selector
The Catalog Item Selector will attempt to load a collection of Interfaces from the Current Directory (loaded DLLS), and allow you to select objects that implement that interface.
```cs
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
```

# Descriptive Collection Editor
The PropertyGrid allows you to configure a collection of Objects. It does so however in a very un-userfriendly way. The window is small and not really usable. The Descriptive Collection Editor replaces that window with another, more user-friendly window that contains Sybrin10.Kernel functions to copy multiple objects, which you can pase in another Descriptive Collection Editor window.
```cs
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
```