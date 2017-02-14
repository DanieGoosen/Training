using Sybrin10.Dynamic;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel;
using Sybrin10.Kernel.BaseClasses;
using Sybrin10.Kernel;
using Sybrin10.Extensions;
using System.Windows;
using System.Threading;
using Sybrin.XDisplayForm.Views;
using Sybrin.XDisplayForm.ViewModels;

namespace Sybrin.XDisplayForm {
    [Export("Sybrin.XDisplayForm", typeof(IXProcess))]
    [ExportMetadata("Description", "DisplayForm XProcess")]
    public class XProcess : XProcessBase {
        #region Constructors

        public XProcess() {
        }

        #endregion Constructors

        #region Properties

        public ILogger Logger { get; set; }

        #region Name
        private string name = string.Empty;
        /// <summary>
        /// Gets or sets the name of the Service
        /// </summary>
        [Category("1 - General"), Description("Sets the name of the Service")]
        [DefaultValue("[XProcess]")]
        [DisplayName("1 - Name")]
        public string Name {
            get { return name; }
            set {
                if (name != value) {
                    name = value;
                }
            }
        }
        #endregion Name

        #region Properties
        private XProcessProperties properties = new XProcessProperties();
        /// <summary>
        /// Gets or sets the properties of the current XProcess
        /// </summary>
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public override PropertyBase Properties {
            get { return properties; }
            set {
                if (properties != value)
                    properties = (XProcessProperties)value;
            }
        }
        #endregion Properties

        #endregion Properties

        #region Methods

        #region override void Execute(IXObject item, ProcessInfo processInfo)
        /// <summary>
        /// Entry point if the XProcess is not a Collective Process
        /// </summary>
        /// <param name="item"></param>
        /// <param name="processInfo"></param>
        public override void Execute(IXObject item, ProcessInfo processInfo) {
            execute(item, processInfo);
        }
        #endregion override void Execute(IXObject item, ProcessInfo processInfo)

        #region override void Execute(XObjectList itemList, ProcessInfo processInfo)
        /// <summary>
        /// Entry point if the XProcess is a Collective Process
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="processInfo"></param>
        public override void Execute(XObjectList itemList, ProcessInfo processInfo) {
            execute(itemList, processInfo);
        }
        #endregion override void Execute(XObjectList itemList, ProcessInfo processInfo)

        #region void execute(object item, ProcessInfo processInfo)
        /// <summary>
        /// The item (Either of type IXObject or XObjectList) is passed as an object with the processInfo, the XProcess 
        /// starts here, adding the client and a logger to the flow.
        /// </summary>
        /// <param name="item">IXObject or XObjectList</param>
        /// <param name="processInfo">The ProcessInfo</param>
        public void execute(object item, ProcessInfo processInfo) {
            var result = new ProcessResult(this);
            processInfo.Results.Add(result);

            var client = processInfo.GetValueInstance<Client.Client>(c => c.LoggedOn);
            Logger = processInfo.GetValueInstance<ILogger>() ?? Core.GetObjectInstance<ILogger>();

            try {
                result.StartTimer();

                if (properties.Enabled) {
                    Logger.WriteDebug(this, string.Format("Initiating XProcess: {0}", this));
                    execute(item, processInfo, client, Logger);
                    result.ResultType = ResultType.Completed;
                } else
                    Logger.WriteDebug(this, string.Format("{0} is Disabled", this));
            } catch (Exception ex) {
                result.ResultType = ResultType.Failed;
                Logger.WriteError(this, ex);
                throw ex;
            } finally {
                Logger.WriteDebug(this, string.Format("XProcess Stopped."));
                result.StopTimer();
            }
        }
        #endregion void execute(object item, ProcessInfo processInfo)

        private void execute(object item, ProcessInfo processInfo, Client.Client client, ILogger logger) {
            // I don't care about any of the information passed to this XProcess, I just want to show a form.

            // Start an instance of System.Windows.Application if it doesnt already exist.
            startApplication();
            var windowThread = createThread(ApartmentState.STA);

            Logger.WriteDebug(this, "Starting Thread");
            windowThread.Start();

            keepAlive(windowThread);
        }

        private void keepAlive(Thread windowThread) {
            Logger.WriteDebug(this, "Keeping Thread alive while the Window is open...");
            while (windowThread.IsAlive) {
                // wait for the thread to be finished
                Thread.Sleep(100);
            }
            Logger.WriteDebug(this, "Thread Stopped");
        }

        private Thread createThread(ApartmentState apartmentState) {

            Logger.WriteDebug(this, "Creating Window (STA) Thread...");
            Thread windowThread = new Thread(() => {
                // Create an instance of a new window.
                var window = new MainWindow();
                // This window needs some content, WPF Knowledge is applied here to 
                // add an existing component as content, together with a binding to 
                // its datacontext.
                var mainVM = new MainVM() { Title = "Title set through the DataContext of the View (Binding Works!)" };
                window.DataContext = mainVM;
                
                // Subscribe to the Closed event of the Window and handle any disposing of objects, logging, etc.
                window.Closed += (s, e) => {
                    Logger.WriteDebug(this, "Window Closed");
                };
                window.ShowDialog();
            });

            // Change the thread to be Single Threaded Apartment for the window to show.
            windowThread.SetApartmentState(ApartmentState.STA);
            return windowThread;
        }

        private void startApplication() {
            // Requires the following References to be added:
            //  * PresentationCore
            //  * PresentationFramework
            //  * System.Xaml
            //  * WindowsBase
            try {

                if (Application.Current == null) {
                    Logger.WriteDebug(this, "Application is null, attempting to create an instance...");
                    new Application() {
                        ShutdownMode = ShutdownMode.OnExplicitShutdown
                    };
                    Logger.WriteDebug(this, "Application Created.");
                }
            } catch (Exception ex) {
                var newEx = new Exception("Failed to create an instance of the Application", ex);
                Logger.WriteError(this, newEx);
                throw newEx;
            }

        }

        #region ToString()
        public override string ToString() {
            return string.Format(Name);
        }
        #endregion ToString()

        #endregion Methods

        #region Events


        #endregion Events
    }
}
