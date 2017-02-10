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
using Sybrin.Dynamic;
using System.IO;
using Sybrin.Extensions;

namespace Sybrin.XProcessing {
    [Export("Sybrin.XProcessing", typeof(IXProcess))]
    [ExportMetadata("Description", "My File Loader")]
    public class XProcess : XProcessBase {
        #region Constructors

        public XProcess() {
        }

        #endregion Constructors

        #region Properties

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
            var logger = processInfo.GetValueInstance<ILogger>();

            try {
                result.StartTimer();

                "This should be logged to the SimpleLogs Folder".SimpleLog("My XProcess");

                logger.WriteDebug(this, string.Format("Initiating XProcess: {0}", this));
                execute(item, processInfo, client, logger);
                result.ResultType = ResultType.Completed;
            } catch (Exception ex) {
                result.ResultType = ResultType.Failed;
                logger.WriteError(this, ex);
                throw ex;
            } finally {
                logger.WriteDebug(this, string.Format(""));
                result.StopTimer();
            }
        }
        #endregion void execute(object item, ProcessInfo processInfo)

        private void execute(object item, ProcessInfo processInfo, Client.Client client, ILogger logger) {

            logger.WriteDebug("My XProcess", "Starting Process...");
            //// Executes each item in the XObjectList recursively
            //if (item is XObjectList) {
            //    (item as XObjectList).ForEach(i => execute(i, processInfo, client, logger));
            //    return;
            //}

            //// Processing of the individual XItem item starts here
            //var xItem = item as IXObject;
            var originalList = item as XObjectList;


            logger.WriteDebug("My XProcess", string.Format("Reading File Contents {0}...", properties.FileName));
            var lines = File.ReadAllLines(properties.FileName);
            logger.WriteDebug("My XProcess", string.Format("Contents read {0} Lines...", lines.Length));

            int counter = 0;

            lines.ForEach(line => {
                if (counter != 0) {

                    var values = line.Split(',');
                    var wrapper = new AllocItemWrapper();

                    var newItem = 1.CreateAllocItem(client, Common.UpdateFlags.Insert);
                    newItem.GetField("Idx1").Value = values[0];
                    newItem.GetField("Idx2").Value = values[1];

                    wrapper.AllocItem = newItem;

                    logger.WriteDebug("My XProcess", string.Format("Adding new Wrapper to ItemList {0}", wrapper));
                    originalList.Add(wrapper);

                    //var sql = string.Format("INSERT INTO iwfTest (Idx1, Idx2) VALUES ('{0}', '{1}')", values[0], values[1]);

                    //client.ExecuteSqlString(sql);
                }
                ++counter;
            });
            
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
