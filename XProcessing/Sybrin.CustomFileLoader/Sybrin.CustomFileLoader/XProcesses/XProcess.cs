using Sybrin10.Dynamic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sybrin10.Attributes;
using System.ComponentModel;
using Sybrin10.Kernel.BaseClasses;
using Sybrin10.Kernel;
using Sybrin10.Extensions;
using System.IO;
using Sybrin.CustomFileLoader.Tools;
using Sybrin.Dynamic;

namespace Sybrin.CustomFileLoader {
    [Export("Sybrin.CustomFileLoader", typeof(IXProcess))]
    [ExportMetadata("Description", "My XProcess")]
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
            Tools.Extensions.Logger = processInfo.GetValueInstance<ILogger>() ?? Core.GetObjectInstance<ILogger>(); // Create a default logger if there is none configured in the Configuration

            try {
                result.StartTimer();

                if (properties.Enabled) {
                    $"Initiating XProcess: {this}".CustomLog();
                    execute(item, processInfo, client, Logger);
                    result.ResultType = ResultType.Completed;
                } else
                    $"{this} is Disabled".CustomLog();
            } catch (Exception ex) {
                result.ResultType = ResultType.Failed;
                ex.CustomLog($"{this} did not execute successfully.");
                Logger.WriteError(this, ex);
                throw ex;
            } finally {
                "XProcess Finished.".CustomLog();
                result.StopTimer();
            }
        }
        #endregion void execute(object item, ProcessInfo processInfo)

        private void execute(object item, ProcessInfo processInfo, Client.Client client, ILogger logger) {
            // No need for recursively iterating through each object in the itemlist, because you don't really care about the previous items.
            try {
                XObjectList newLines = new XObjectList();

                var fileName = properties.FilePath;
                if (!File.Exists(fileName)) {
                    $"File does not exist: '{fileName}'".CustomLog();
                    throw new Exception($"File {fileName} does not exist");
                }

                $"Reading File Contents...".CustomLog();
                List<string> lines = File.ReadAllLines(fileName).ToList();

                $"{lines.Count} Line{(lines.Count == 1 ? "" : "s")} read.".CustomLog();
                List<string> headers = new List<string>();
                if (lines.Count > 0) {
                    if (properties.FirstLineContainsHeaders) {
                        "Reading Header Columns from the first line...".CustomLog();
                        headers = lines[0].Split(properties.SeperationChar).ToList();

                    } else {
                        throw new Exception("First line should contain the Headers.");
                    }

                    if (properties.SkipLines) {
                        $"Configured to skip lines, skipping {properties.AmountToSkip} line(s)".CustomLog();
                        lines.RemoveRange(0, properties.AmountToSkip); // Remove the lines to skip.


                        "Reading line contents into the xItem(s)...".CustomLog();

                        // The below uses an IEnumerable to add the index to the ForEach loop using Linq.
                        // It can also be done using a normal For loop, and a counter integer. 
                        lines.ForEach(line => {
                            var newXItem = new BaseXObject(); // Create a new default XObject item.
                            var lineContents = line.Split(properties.SeperationChar).ToList();
                            headers.Select((header, index) => new { header = header, index = index}).ForEach(enumerable => {
                                newXItem[enumerable.header] = lineContents[enumerable.index];
                            });

                            newLines.Add(newXItem); // Add to the ItemList
                        });
                    }
                } else {
                    throw new Exception("File does not contain any lines");
                }
                // View the contents 
                //newLines[0].GetPropertyNames();
                //newLines[0]["Name"];
                if (item is XObjectList) {
                    (item as XObjectList).AddRange(newLines); // Add the new items to the XObjectList.
                }
            } catch (Exception ex) {
                throw new Exception($"Failed to execute '{Tools.Extensions.Name}'", ex);
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
