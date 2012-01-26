using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.IO;
using System.Windows.Forms.Integration;

namespace FabTab
{
    /// <summary>
    /// Interaction logic for ContentTabView.xaml
    /// </summary>
    partial class ContentTabView : UserControl
    {
        private Dictionary<object, object> _views;
        private FabTabControl _tabControl;

        public ContentTabView(FabTabControl tabControl)
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ContentTabView_Loaded);
            _tabControl = tabControl;
        }

        void ContentTabView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateViews();
        }

        private void UpdateViews()
        {
            if (_views != null)
            {
                this.wrapPanel.Children.Clear();
                foreach (object view in _views.Keys)
                {
                    FrameworkElement element = view as FrameworkElement;
                    if (element != null && !(element is ContentTabView))
                    {
                        Image newImage = null;
                        //if we've got a tabitem, then grab it's content for the bitmap
                        if (element is FabTabItem)
                        {
                            FabTabItem fabTab = element as FabTabItem;
                            //thanks for the patch for WinForms interop butters877!
                            if (fabTab.Content is WindowsFormsHost)
                            {
                                System.Windows.Forms.Control control = ((WindowsFormsHost)fabTab.Content).Child;
                                System.Drawing.Bitmap bit = new System.Drawing.Bitmap(control.Width, control.Height);
                                control.DrawToBitmap(bit, new System.Drawing.Rectangle(0, 0, control.Width, control.Height));
                                BitmapSource source = loadBitmap(bit);

                                newImage = new Image();
                                newImage.Source = source;
                                newImage.Height = 150;
                                newImage.Stretch = Stretch.Uniform;

                                SetImageScreenshotOnFabTabItemAttachedProperty(element, source);

                                
                            }
                            else
                            {
                                newImage = CreateBitmap((FrameworkElement)(element as FabTabItem).Content);
                            }
                        }
                        else
                        {
                            newImage = CreateBitmap(element);
                        }

                        if (newImage != null)
                        {
                            //TODO: Make this user configurable
                            ImageButton imageButton = new ImageButton();
                            imageButton.Content = newImage;

                            imageButton.Style = _tabControl.ContentTabImageButtonStyle;
                            object title = null;
                            if (_views.TryGetValue(view, out title))
                            {
                                imageButton.Title = title;
                            }

                            imageButton.Click += new RoutedEventHandler(imageButton_Click);
                            imageButton.ApplyTemplate();

                            Button closeButton = imageButton.Template.FindName("PART_CloseButton", imageButton) as Button;

                            if (_tabControl.ShowTabCloseButtons)
                            {
                                closeButton.Click += new RoutedEventHandler(closeButton_Click);
                                closeButton.Command = new RoutedCommand();
                                CommandBinding b = new CommandBinding(closeButton.Command, new ExecutedRoutedEventHandler(imageButtonCommandExecuted));
                                imageButton.CommandBindings.Add(b);
                            }
                            else
                            {
                                closeButton.Visibility = Visibility.Hidden;
                            }

                            this.wrapPanel.Children.Add(imageButton);
                        }
                    }
                }
            }
        }

        void imageButtonCommandExecuted(object sender, ExecutedRoutedEventArgs args)
        {
            ImageButton imageButton = sender as ImageButton;
            if (imageButton != null)
            {
                int indexOfClickedButton = this.wrapPanel.Children.IndexOf(imageButton);
                FabTabItem tabItem = _tabControl.ItemContainerGenerator.ContainerFromIndex(indexOfClickedButton + 1) as FabTabItem;
                if (tabItem != null)
                {
                    tabItem.OnRaiseTabClosingEvent(new RoutedEventArgs(FabTabItem.TabClosingEvent, tabItem));
                }
            }
        }

        void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //set handled equal to tree so it doesn't keep going up the visual tree
            //and hit the imageButton_Click handler
            e.Handled = true;
        }

        void imageButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = e.OriginalSource as Button;
            if (clickedButton != null)
            {
                int indexOfClickedButton = this.wrapPanel.Children.IndexOf(clickedButton);
                //add one to the index to account for the ContentTab being an item
                //in the tab control itself.
                _tabControl.SelectedIndex = indexOfClickedButton + 1;

            }

        }



        internal void SetViews(Dictionary<object, object> views)
        {
            _views = views;
            UpdateViews();

        }

        internal Image CreateBitmap(FrameworkElement element)
        {
            if (element == null)
                return new Image();

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            VisualBrush elementBrush = new VisualBrush();
            elementBrush.AutoLayoutContent = false;
            elementBrush.Visual = element;

            int w = (int)element.ActualWidth;
            int h = (int)element.ActualHeight;

            context.DrawRectangle(elementBrush, null, new Rect(0, 0, w, h));
            context.Close();

            if (w == 0 || h == 0)
                return new Image();

            RenderTargetBitmap bitmap = new RenderTargetBitmap(w, h, 96, 96, PixelFormats.Default);
            bitmap.Render(visual);

            //PngBitmapEncoder pngEncoder = new PngBitmapEncoder();
            //pngEncoder.Frames.Add(BitmapFrame.Create(bitmap));
            //using (MemoryStream outputStream = new MemoryStream())
            //{
            //    pngEncoder.Save(outputStream);
            //    outputStream.Flush();
            //    outputStream.Seek(0, System.IO.SeekOrigin.Begin);
            //    BitmapImage bi = new BitmapImage();
            //    bi.BeginInit();
            //    bi.CacheOption = BitmapCacheOption.OnLoad;
            //    //setting the DecodePixelHeight to a smaller value drastically
            //    //reduces memory use and thus increases performance.
            //    bi.DecodePixelHeight = 250;
            //    bi.StreamSource = outputStream;
            //    bi.EndInit();

            //    Image viewImage = new Image();
            //    viewImage.Source = bi;
            //    viewImage.Height = 150;
            //    viewImage.Stretch = Stretch.Uniform;
            //    bitmap = null;
            //    return viewImage;
            //}

            SetImageScreenshotOnFabTabItemAttachedProperty(element, bitmap);


            Image image = new Image();
            image.Source = bitmap;
            image.Height = 150;
            //changed to UniformToFill because Uniform caused weird behavior if entire windows
            //were resized
            image.Stretch = Stretch.UniformToFill;

            return image;

        }

        private void SetImageScreenshotOnFabTabItemAttachedProperty(FrameworkElement element, BitmapSource bitmap)
        {
            //TODO: Make the size of the tooltip image user-configurable?
            Image tooltipImage = new Image();
            tooltipImage.Source = bitmap;
            tooltipImage.Height = 300;
            tooltipImage.Width = 400;
            //changed to UniformToFill because Uniform caused weird behavior if entire windows
            //were resized
            tooltipImage.Stretch = Stretch.UniformToFill;
            FabTabItem item = element.Parent as FabTabItem;
            if (item == null)
            {
                item = _tabControl.ItemContainerGenerator.ContainerFromItem(element) as FabTabItem;
            }
            FabTabItemProperties.SetFabTabItemImage(item, tooltipImage);
        }

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(source.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
