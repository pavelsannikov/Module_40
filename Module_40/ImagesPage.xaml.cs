using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Module_40
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagesPage : ContentPage
    {
        class ImageWidget
        {
            public ImageCell Cell { get; set; }
            public string path { get; set; }
        }
        ImageWidget SelectedWidget = null;
        List<ImageWidget> ImagesList= new List<ImageWidget>();
        private TableView tableView;
        public ImagesPage()
        {
            InitializeComponent();
            string path = "/storage/emulated/0/DCIM/Camera";

            var files = Directory.GetFiles(path);
            tableView = new TableView
            {
                Intent = TableIntent.Form,
                HasUnevenRows = true,
                Root = new TableRoot
                {
                    new TableSection
                    {

                    }
                }
            };
            foreach (var f in files)
            {
                var names = f.Split('/');
                ImageCell cell = new ImageCell
                {
                    ImageSource = ImageSource.FromFile(f),
                    Text = names.Last(),
                    TextColor = Color.Black,
                };
                cell.Tapped += ImageSelected;
                tableView.Root[0].Add(cell);
                ImagesList.Add(new ImageWidget
                {
                    Cell = cell,
                    path = f
                });
            }
            ContentStackLayout.Children.Add(tableView);
        }
        private void ImageSelected(object sender, EventArgs e)
        {
            var Cell = (ImageCell)sender;
            if(SelectedWidget != null)
            {
                SelectedWidget.Cell.TextColor = Color.Black;
            }
            else
            {
                OpenButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            Cell.TextColor = Color.Blue;
            foreach (var cell in ImagesList)
            {
                if (cell.Cell == Cell)
                {
                    SelectedWidget = cell;
                }
            }
            
            //Navigation.PushAsync(new ImageViewPage(image.ImageSource));
        }
        private void OpenButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ImageViewPage(SelectedWidget.Cell.ImageSource));
        }
        private void DeleteButtonClicked(object sender, EventArgs e)
        {
            tableView.Root[0].Remove(SelectedWidget.Cell);
            File.Delete(SelectedWidget.path);
            ImagesList.Remove(SelectedWidget);
            OpenButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            SelectedWidget = null;
        }
    }
}