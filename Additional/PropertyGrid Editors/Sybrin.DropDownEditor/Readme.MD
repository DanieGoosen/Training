# Creating a Custom Dropdown Editor

In the properyGrid, you might want to create your own custom content in a Dropdown. Lets take the [Image Selector Editor](https://github.com/XcyTheR101/Training/tree/master/Additional/PropertyGrid%20Editors/Sybrin.ImageSelectorEditor) as an example. Lets say from a selection of Images, you want to use one as a selected image lateron. I am referencing the already created editor in [this](https://github.com/XcyTheR101/Training/tree/master/Additional/PropertyGrid%20Editors/Sybrin.ImageSelectorEditor) example. For your convenience I have added the dll to the Solution Directory of this project (You will have to manually copy taht dll into the Packages folder for the refernce to work)

## What we want to achieve
We want to allow the implementor to configure a bunch of images using the Image Selector Editor, and then use those images to select one from a dropdown.

[Include Image(s) of final result.]

# Getting Started
First of all, a Dropdown editor is a bit more tricky than a normal editor opening up via a modal button.

> The Dropdown control in the PropertyGrid uses a WinForms Control to display its content. The area supplied can really be anything, as long as its inside a WinForms UserControl.
> <br><br> Luckily the WinForms control supports an ```ElementHost``` that allows you to add a ```WPF``` view inside it.

## Classes Needed

Before we continue, lets create the classes that you will need to create a custom Dropdown Editor

> Note: When you will create multiple editors, it is recomennded to create each editor in its own folder.


 1. ```Editors/ImageDropDownControl.cs```:<br>
    A WinForms UserControl, with a ElementHost on it. Rename the ElementHost to 'wpfHost'
 2. ```Editors/ImageDropDownEditor.cs```:<br>
    Your own editor class, that will use the WinForms control to host a custm view inside the DropDown Area.
 3. ```Editors/ImageDropDownView.xaml```: <br>
    Your WPF Control that will ultimately handle all the visuals of the control. 
 4. ```Editors/ImageDropDownVM.cs```: <br>
    The ViewModel that will be used as a DataContext to the WPF View.
 5. ```Editors/ImageDropDownModel.cs```: <br>
    The model(s) that the ViewModel will contain.

## 1. ImageDropDownControl (WinForms Control)

Create a constructor that will accept accept the EditorService as wel as the value Passed from the ProeprtyGrid:

> Note: this is in the Code behind of the WinForms Control.

```cs
public partial class ImageDropdownControl : UserControl {
    public ImageDropdownControl() {
        InitializeComponent();
    }

    public ImageDropdownControl(IWindowsFormsEditorService editorService, object value) : this() {
        this.EditorService = editorService;
        this.Value = value;

        // Initiate the WPF Control over here.
        var mainView = new ImageDropDownView();
        var mainVM = new ImageDropDownVM();
        mainView.DataContext = mainVM;
        
        // Add the WPF View to the ElementHost
        wpfHost.Child = mainView;
    }

    public IWindowsFormsEditorService EditorService { get; set; }

    object Value { get; set; }

}
```

## 2. Image DropDown Editor 

Create an editor like always, that inherits from UITypeEditor:
```cs
public class DropDownEditorEditor : UITypeEditor {
    public override UITypeEditorEditStyle
        GetEditStyle(ITypeDescriptorContext context) {
        // Instead of accessing this editor through a modal, it will be accesed through a Dropdown.
        return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(ITypeDescriptorContext context,
                            IServiceProvider provider, object value) {
        // Use the EditorService to access the content of the Dropdown through the provider
        IWindowsFormsEditorService wfes =
            provider.GetService(typeof(IWindowsFormsEditorService)) as
            IWindowsFormsEditorService;

        if (wfes != null) {
            // Create the control that should override the content of the Dropdown
            // Send the value passed by the PropertyGrid through to the new control, as well as the Editor Service
            ImageDropdownControl newContent = new ImageDropdownControl(wfes, value);
            
            // Launch the Dropdown control with the content as the new Control
            wfes.DropDownControl(newContent);
            try {
                value = iconControl.FontVM.SelectedModel;
            } catch (Exception ex) { }
        }

        return value;
    }

    public override bool Equals(object obj) {
        return base.Equals(obj);
    }

    public override bool IsDropDownResizable {
        get {
            return true;
        }
    }
}
```

With the sample code, everything is ready from the PropertyGrid side. Now, you have to add logic to the WinForms control you've specified to replace the default content of the dropdown area. You do this in the code behind of the WinForms UserControl you've created, but before that, lets create the neccesary WPF objects that will be used to represent the content.

## 3. WPF View

Create the following WPF UserControl: ```Editors/ImageDropDownView.xaml```
```cs
<UserControl x:Class="Sybrin.DropDownEditor.Editors.ImageDropDownView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:Sybrin.DropDownEditor.Editors"
             mc:Ignorable="d" 
             d:DesignHeight="300" d:DesignWidth="300">
    <Grid>
        <Label Content="WPF View!" HorizontalAlignment="Center" VerticalAlignment="Center"/>
    </Grid>
</UserControl>
```

## 4. ViewModel

Create the ViewModel that will be used as the DataContext for the View. See the [MVVM](https://github.com/XcyTheR101/MVVM) tutorial on the Base Classes/RelayCommand that will be needed later on.

```cs
public class ImageDropDownVM : ModelBase {
}
```

## 5. Model

Create the Model(s) that will be used in the ViewModels of the WPF Control. See the [MVVM](https://github.com/XcyTheR101/MVVM) tutorial on the Base Classes/RelayCommand that will be needed later on.

```cs
public class ImageDropDownModel : ModelBase{
}
```



At this point, the dropdoown will contain whatever you have made in the WPF view.

Now, lets start with the logic to display the Images in the DropDown control, in order to select one.
