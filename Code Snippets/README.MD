# VS 2015 Code Snippets

Exract the Snippets in the following location, Visual studio will automatically pick up the changes and refresh.

%localappdata%..\..\..\Documents\Visual Studio 2015\Code Snippets\Visual C#\

---

## mozClassInitialize
Initializes a class in the following structure (does not include the class, but only its contents):

```cs
#region Constructors



#endregion Constructors

#region Properties



#endregion Properties

#region Events



#endregion Events

#region Methods

public override string ToString() {
    return string.Format(base.ToString());
}

#endregion Methods
```

Its good practise to group similar objects in regions. This snippet supplies these regions where you can group your Constructors, Properties, events and Methods. You can group your methods even further where you have your ```private``` and ```public``` methods seperated.

---

## mozDependencyProp   
Creates a Dependency Property, to add a custom property to any WPF (user) Control:

```cs
    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public static readonly DependencyProperty MyCustomProperty =
        DependencyProperty.Register("MyCustom", typeof(string), typeof(Object));
    public string MyCustom {
        get { return this.GetValue(MyCustomProperty) as string; }
        set { this.SetValue(MyCustomProperty, value); }
    }
```

---

## mozPropBase

This snippet creates a property that will decorate a Property Grid with the basic metadata.

```cs
private int myVar = new;
/// <summary>
/// Gets or sets the description
/// </summary>
[Category("1 - General"), Description("Sets the description")]
[DefaultValue("")]
[DisplayName("Display Name")]
public int MyProperty {
    [DebuggerNonUserCode]
    get { return this.myVar; }
    [DebuggerNonUserCode]
    set {
        if (this.myVar != value) {
            this.myVar = value;
        }
    }
}
```

```[Category("1 - General")]```: This places the property to a category, and the PropertyGrid will attempt to group all of the properties under the same group together under an expander.<br><br>
```[Description("Sets the description")]```: Changes the description of the property underneath the propertyGrid to be more descriptive as to what the property actually does.<br><br>
```[DefaultValue("")]```: Provides a default value to the Property. A default proeprty will have normal text, and any other property will be bold.<br><br>
```[DisplayName("Display Name")]```: Changes the name of the property in the property Grid.<br><br>

**Note:** ```[DebuggerNonUserCode]``` is used to force the debugger to skip the properties when debugging. This will cause the debugger to not enter the getters and setters of properties. 

---

## mozPropCommand
Creates a command that will be bound to a control such as a button that has an action event (click). When the button is clicked, this action bould to the button will be executed.

**Note:** This makes use of the ```RelayCommand``` class, supplied at the end of this file.
```cs
public bool command { get; set; }
private ICommand _command;
public ICommand Command {
    get {
        if (_command == null) {
            _command = new RelayCommand(param => Method(), param => command);
        }
        return _command;
    }
}

public void Method() {
}
```

---

## mozPropCommandViewModelBase

Makes use of the Command implemented in the ViewModel Base class

**Note:** The ViewModelBase class implementatino is supplied at the end of this file.

```cs
private ICommand _command;
public ICommand Command {
    get {
        if (_command == null) {
            _command = CreateCommand(Method, command);
        }
        return _command;
    }
}
public bool command(object obj) {
    return true;
}

public void Method(object obj) {
}
```

---

## mozPropNormal

Creates a normal Property with a getter and a setter.

```cs
private int myVar = new;
/// <summary>
/// Gets or sets the description
/// </summary>

public int MyProperty {
    [DebuggerNonUserCode]
    get { return this.myVar; }
    [DebuggerNonUserCode]
    set {
        if (this.myVar != value) {
            this.myVar = value;
        }
    }
}
```

## mozPropNormalINotify
Creates a property that also calls the SetPropertyChanged event to notify the ```View``` that the property changed and should be updated. 

```cs
private int myVar = new;
/// <summary>
/// Gets or sets the description
/// </summary>

public int MyProperty {
    [DebuggerNonUserCode]
    get { return this.myVar; }
    [DebuggerNonUserCode]
    set {
        if (this.myVar != value) {
            this.myVar = value;
            SetPropertyChanged("MyProperty");
        }
    }
}
```

---

## mozPropNotifyMethod
Creates the method that will fire the INotifypropertyChanged event.

```cs
public void SetPropertyChanged(string propName) {
    var handler = PropertyChanged;
    if (handler != null) {
        handler.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
```

**Note:** this does not include the event that is needed by INotifyPropertyChanged,
```cs
public event PropertyChangedEventHandler PropertyChanged;
```

---

## mozSybDocumentField
Creates the layout that will be used by Sybrin t pre-populate the Property Grid with a dropdown which will allow you to select a document that is configured in the connection to the Sybrin Server

```cs
private string myVar = "0 - {Document not set}";
/// <summary>
/// Gets or sets the Sybrin Document description 
/// </summary>
[DisplayName("Display Name"), Category("1 - General"), DefaultValue("0 - {Document not set}")]
[Description("The Sybrin Document description")]
[TypeConverter(typeof(DocConverter))]
public string MyProperty {
    [DebuggerNonUserCode]
    get { return myVar; }
    [DebuggerNonUserCode]
    set { if (myVar != value) myVar = value; }
}
[Browsable(false), XmlIgnore()]
public int MyPropertyNo { get { return (myVar == null) ? 0 : myVar.Split('-')[0].Trim().ToInt(); } }

```

**Note:** note the TypeConverter. This typeconverter converts the string type during runtime to a DocConverter, found in Sybrin Core. It has already done all the heavy lifting of creating a typeconverter, so you can just plug it in and use it over here.

---

## mozSybIndexField
The same as the Sybrin Document, but this snippet will allow a user to select a fiedl from a selected document, and populate the dropdown with the fields configured in that document.

```cs
private string myVar = "0 - {Field not set}";
/// <summary>
/// Gets or sets Sybrin Index field mapped to the description property
/// </summary>
[DisplayName("Display Name"), Category("1 - General"), DefaultValue("0 - {Field not set}")]
[Description("The Sybrin Index field mapped to the description property")]
[TypeConverter(typeof(DocKeyConverter))]
public string MyProperty {
    [DebuggerNonUserCode]
    get { return myVar; }
    [DebuggerNonUserCode]
    set { if (myVar != value) myVar = value; }
}
[Browsable(false), XmlIgnore()]
public int MyPropertyNo { get { return (myVar == null) ? 0 : myVar.Split('-')[0].Trim().ToInt(); } }
```

---

## mozSybReportField
Not used as often, but will populate the dropdown with all the reports configured in the connection to the Sybrin Server.

```cs
private string myVar = "0 - {Report not set}";
/// <summary>
/// Gets or sets the Sybrin Report description
/// </summary>
[DisplayName("Display Name"), Category("1 - General"), DefaultValue("0 - {Report not set}")]
[Description("The Sybrin Report description")]
[TypeConverter(typeof(ReportListConverter))]
public string MyProperty {
    [DebuggerNonUserCode]
    get { return myVar; }
    [DebuggerNonUserCode]
    set { if (myVar != value) myVar = value; }
}
[Browsable(false), XmlIgnore()]
public int MyPropertyNo { get { return (myVar == null) ? 0 : myVar.Split('-')[0].Trim().ToInt(); } }
```

---
# RelayCommand
A relayCommand is using the built-in ICommand property to bind a command to a control that supports commands. A Relay command is implemented to initialize an instance of the interface ICommand:

```cs
public class RelayCommand : ICommand {

    private readonly Func<object, bool> canExecute;
    private readonly Action<object> execute;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
    /// </summary>
    /// <param name="executeAction">The action to execute.</param>
    public RelayCommand(Action<object> executeAction)
        : this(executeAction, null) {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="RelayCommand" /> class.
    /// </summary>
    /// <param name="executeAction">The action to execute.</param>
    /// <param name="canExecuteFunction">The function that evaluates if the command can execute or not.</param>
    public RelayCommand(Action<object> executeAction, Func<object, bool> canExecuteFunction) {
        execute = executeAction;
        canExecute = canExecuteFunction;
    }

    /// <summary>
    ///     Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;

    /// <summary>
    ///     Raises the can execute changed.
    /// </summary>
    public void RaiseCanExecuteChanged() {
        if (CanExecuteChanged != null) {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    /// <summary>
    ///     Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">
    ///     Data used by the command.  If the command does not require data to be passed, this object can
    ///     be set to null.
    /// </param>
    /// <returns>
    ///     true if this command can be executed; otherwise, false.
    /// </returns>
    [DebuggerStepThrough]
    public virtual bool CanExecute(object parameter) {
        if (canExecute == null) {
            return true;
        }

        return canExecute(parameter);
    }

    /// <summary>
    ///     Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">
    ///     Data used by the command.  If the command does not require data to be passed, this object can
    ///     be set to null.
    /// </param>
    [DebuggerStepThrough]
    public virtual void Execute(object parameter) {
        execute(parameter);
    }
}
```

## Implementation

The implementation is a two-step process. It must be implemented in the Front end (```View```) as well as the Back-end (```Model```)

### Model Implementation

```cs
private ICommand _buttonClickedCommand;
public ICommand ButtonClickedCommand {
    get {
        if (_buttonClickedCommand == null) {
            _buttonClickedCommand = CreateCommand(ButtonClick, canButtonClick);
        }
        return _buttonClickedCommand;
    }
}
public bool canButtonClick(object obj) {
    // do some checks to see if the cutton can be clicked or not.
    return true;
}

public void ButtonClick(object obj) {
    MessageBox.Show("Button was clicked!");
}
```

### View implementation

The command is bound by the button through the ```Command``` property. it will look at the DataContext of the button, and try to resolve the 'ButtonClickedCommand' command. When it finds it, it will execute the command that is linked to the Command.

```cs
    <Grid>
        <Button Content="Click Me!" Height="25" Width="75" Command="{Binding ButtonClickedCommand}"/>
    </Grid>
```