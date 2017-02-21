using Sybrin.ImageSelectorEditor.Views;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Sybrin.ImageSelectorEditor.Editors {
    public class ImageEditor : UITypeEditor {
        /// <summary>
        /// Determines how this editor will be handled in the PropertyGrid. I want thi to be launched through a modal button to open the screen.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.Modal;
        }

        

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            var window = new MainWindow() {  };
            if (!(value is ImageModels)) throw new NotSupportedException("The Value passed through has to be of type ImageModels");
            var images = value as ImageModels;
            var mainVM = new MainVM() { Images = images ?? new ImageModels() };
            window.DataContext = mainVM;

            window.ShowDialog();

            value = mainVM.Images;

            return base.EditValue(context, provider, value);
        }
    }
}
