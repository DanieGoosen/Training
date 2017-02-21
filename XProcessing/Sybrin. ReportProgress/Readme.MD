# Reporting Progress Using Events

Events can be used to report progress of a current process, and if desired, these can be used public and can be accessed by externaly objects (projects) if needed. So for instance: Lets take a look at the [DML creator](https://github.com/XcyTheR101/Training/tree/master/XProcessing/DML%20Folder%20Creator).

It creates a folder structure based on the DML level. It would be nice if this project reported the progress somehow. This can be achieved using an ```event```.

lets concider the following class:

```cs
public static class EventManager{
    public static Action<int> ReportProgress;

    public static void OnReportProgress(int percentage){
        if (ReportProgress != null){
            ReportProgress.Invoke(percentage);
        }
    }
}
```

if you add this class to your DML Project, you can use the ReportProgress Action as an event to notify *whoever is subscribed to the action* that the progress has changed.

You simply have to do the following:

## Step 1

Create the abovementioned class in your DML project.

## Step 2

Get the amount of folders that will be created eventually fro mthe DML Level. This can be achieved with the following calculation: 10^x, where 'x' is the DML Level.

## Step 3 

In the folder creation, keep track of how many folders you have already created, and get a percentage (integer value, use Math.Round to round to get a whole value)

## Step 4 

Use the Event to notify whoever is subscribed to the event of the new  percentage:

```cs
EventManager.OnReportProgress(percentage);
```

This line will go to the EventManager, and broadcast the percentage to every object that is subscribed to the event.

### Thats it. Now, implement the listening of the events.

## Step 5
 In a seperate project, or in a seperate class, for instance the form in the ```Bonus``` part of the DML creation project, you can now subscribe to the EventManager (in your ```ViewModel``` that is used for the ```DataContext``` for your Form), since it is a public static instance:

 ```cs
 // You can use the Shortcut 'Ctrl' + '.' to autocomplete the method signature`OnReportProgress'
 <YourNameSpaceOfttheDMLProject>.EventManager.ReportProgress += OnReportProgress;

 private void OnReportProgress(int percentage){
     // do any logic here to update your form, or whatever.
     this.DMLProgress = percentage;
 }

 private int dmlProgress = 0;

 public int DMLProgress {
    get {
        return dmlProgress; 
    }
    set {
        if (dmlProgress != value) {
            dmlProgress = value;
            // Notify the change to the Form.
            SetPropertyChanged("DMLProgress");
        }
    }
 }

 
 ```

 What the above line will do, is to *Subscribe* to the action in the EventManager class. This means whenever something triggers the action, the method that you have specified to *listen*, will be triggered.

 Now, you can simply have a ReportProgress to display the current Progress:

 ```cs
 <ProgressBar Height="35" Minimum="0" Maximum="100" Value="{Binding DMLProgress}"/>
 ```

 and it will automaticaly be updated when the DMLProgress changes, which it will if subscribed to the Action oin the EventManager.