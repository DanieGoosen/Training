using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Sybrin.ImageSelectorEditor;
using System.Threading;

namespace Sybrin.DropDownEditor.Editors {
    public partial class ImageDropdownControl : UserControl {
        public ImageDropdownControl() {
            InitializeComponent();
        }

        public ImageDropdownControl(XProcessProperties properties, IWindowsFormsEditorService editorService, object value) : this() {
            this.EditorService = editorService;
            if (!(value is ImageModel)) return;

            this.Value = (value as ImageModel);

            // Initiate the WPF Control over here.
            var mainView = new ImageDropDownView();
            MainVM = new ImageDropDownVM();
            MainVM.Images = properties.Images;
            MainVM.Images.ToList().ForEach(i => i.IsVisible = true); //Reset the visibility
            MainVM.SelectedImage = MainVM.Images.FirstOrDefault(i => i.Name == Value.Name);
            MainVM.SelectedIndexCallback = IndexChanged;
            mainView.DataContext = MainVM;

            // Add the WPF View to the ElementHost
            wpfHost.Child = mainView;
        }

        private void IndexChanged(int index) {
            Thread.Sleep(300);
            EditorService.CloseDropDown();
            Value = MainVM.Images[index];
        }

        public IWindowsFormsEditorService EditorService { get; set; }

        ImageModel Value { get; set; }

        public ImageDropDownVM MainVM { get; set; }

    }
}
