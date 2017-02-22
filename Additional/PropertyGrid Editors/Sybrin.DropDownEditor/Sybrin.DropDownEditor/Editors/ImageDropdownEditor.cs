using Sybrin.ImageSelectorEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;

namespace Sybrin.DropDownEditor.Editors {
    public class ImageDropdownEditor : UITypeEditor {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
            // used to access the content area of the Dropdown
            IWindowsFormsEditorService editorService = null;

            var props = (context.Instance as XProcessProperties);
            if (provider != null) 
                editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            // Launch the custom control in the provided area for the dropdown.
            if (editorService != null) {

                var image = (value as ImageModel);
                var selectionControl = new ImageDropdownControl(props, editorService, image);
                editorService.DropDownControl(selectionControl);

                // Return the selected value from the WPF control.
                // If the Parent ID is null, the dropdown has been closed by not selecting an image (clicked outside), then return the original value
                return selectionControl.MainVM.SelectedImage == null ? value : selectionControl.MainVM.SelectedImage;
            }


            return base.EditValue(context, provider, value);
        }
    }
}
