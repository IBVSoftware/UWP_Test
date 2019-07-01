using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VirtualCanvasInk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private CanvasBitmap backgroundImage;
        private CanvasImageBrush backgroundBrush;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnCreateResources(CanvasVirtualControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(Task.Run(async () =>
            {
                // Load the background image and create an image brush from it
                this.backgroundImage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Globe.png"));
                this.backgroundBrush = new CanvasImageBrush(sender, this.backgroundImage);
            }).AsAsyncAction());
        }

        private int count;
        private void OnDraw(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        {
            //NOTE: Change this value to 10000 to see overall performances decrease!
            const int size = 6000;

            var rect = new Rect(0, 0, size, size);

            using (var session = sender.CreateDrawingSession(rect))
            {
                session.FillRectangle(rect, this.backgroundBrush);
                session.DrawRectangle(rect, Colors.Red, 10);
            }

            this.count++;

            this.Frames.Text = this.count.ToString();
        }


        private void OnZoomIm(object sender, DoubleTappedRoutedEventArgs e)
        {
            this.sv.ChangeView(0, 0, 0.1F);
        }
    }
}
