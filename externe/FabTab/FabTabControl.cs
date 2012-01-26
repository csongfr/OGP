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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls.Primitives;
using FabTab.DragDrop;

namespace FabTab
{
    [TemplatePart(Name = "PART_SelectedContentHost", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "PART_HiddenContentHost", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "HeaderPanel", Type = typeof(TabPanel))]
    public class FabTabControl : TabControl
    {

        public static readonly DependencyProperty CloseButtonStyleProperty = DependencyProperty.Register(
                        "CloseButtonStyle", typeof(Style), typeof(FabTabControl));

        public static readonly DependencyProperty ContentTabImageButtonStyleProperty = DependencyProperty.Register(
                        "ContentTabImageButtonStyle", typeof(Style), typeof(FabTabControl));

        public static readonly DependencyProperty ContentTabHeaderContentProperty = DependencyProperty.Register(
                        "ContentTabHeaderContent", typeof(FrameworkElement), typeof(FabTabControl));

        public static readonly DependencyProperty ContentTabHeaderComboBoxStyleProperty = DependencyProperty.Register(
                        "ContentTabHeaderComboBoxStyle", typeof(Style), typeof(FabTabControl));

        public static readonly DependencyProperty HiddenContentProperty = DependencyProperty.Register("HiddenContent", typeof(StackPanel),
                                                                                                            typeof(FabTabControl));

        public static readonly DependencyProperty ShowToolTipImagesProperty = DependencyProperty.Register("ShowToolTipImages", typeof(bool),
                        typeof(FabTabControl), new PropertyMetadata(true));

        public static readonly DependencyProperty ShowDefaultTransitionAnimationProperty = DependencyProperty.Register("ShowDefaultTransitionAnimation", typeof(bool),
            typeof(FabTabControl), new PropertyMetadata(true));

        public static readonly DependencyProperty ContentOpacityMaskProperty = DependencyProperty.Register("ContentOpacityMask", typeof(Brush),
                        typeof(FabTabControl));

        private ContentTabView _contentTabView = null;
        private bool _showContentsTab = true;
        private bool _contentsTabAdded;
        private ComboBox _contentsTabComboBox;
        private bool _showTabCloseButtons = true;
        private bool _allowMultiLineTabHeaders = false;
        private bool _itemsChanging = false;
        private bool _allowDragAndDropTabReordering = true;

        public bool ShowContentsTab
        {
            get { return _showContentsTab; }
            set { _showContentsTab = value; }
        }

        public bool ShowTabCloseButtons
        {
            get { return _showTabCloseButtons; }
            set { _showTabCloseButtons = value; }
        }

        public bool AllowMultiLineTabHeaders
        {
            get { return _allowMultiLineTabHeaders; }
            set { _allowMultiLineTabHeaders = value; }
        }

        public bool AllowDragAndDropTabReordering
        {
            get { return _allowDragAndDropTabReordering; }
            set { _allowDragAndDropTabReordering = value; }
        }

        //TODO: rename this to something more meaningful
        protected StackPanel HiddenContent
        {
            get { return (StackPanel)this.GetValue(HiddenContentProperty); }
            set
            {
                this.SetValue(HiddenContentProperty, value);
            }
        }

        public Style CloseButtonStyle
        {
            get { return (Style)this.GetValue(CloseButtonStyleProperty); }
            set
            {
                this.SetValue(CloseButtonStyleProperty, value);

            }
        }

        public Style ContentTabImageButtonStyle
        {
            get { return (Style)this.GetValue(ContentTabImageButtonStyleProperty); }
            set
            {
                this.SetValue(ContentTabImageButtonStyleProperty, value);

            }
        }

        public FrameworkElement ContentTabHeaderContent
        {
            get { return (FrameworkElement)this.GetValue(ContentTabHeaderContentProperty); }
            set
            {
                this.SetValue(ContentTabHeaderContentProperty, value);

            }
        }

        public Style ContentTabHeaderComboBoxStyle
        {
            get { return (Style)this.GetValue(ContentTabHeaderComboBoxStyleProperty); }
            set
            {
                this.SetValue(ContentTabHeaderComboBoxStyleProperty, value);

            }
        }

        public bool ShowDefaultTransitionAnimation
        {
            get { return (bool)this.GetValue(ShowDefaultTransitionAnimationProperty); }
            set { this.SetValue(ShowDefaultTransitionAnimationProperty, value); }
        }
        
        /// <summary>
        /// This exposes the OpacityMask of the ContentPresenter in which the FabTabItem.Content is displayed.
        /// Exposing this allows some cool animations to be done on the OpacityMask.
        /// </summary>
        public Brush ContentOpacityMask
        {
            get { return (Brush)this.GetValue(ContentOpacityMaskProperty); }
            set { this.SetValue(ContentOpacityMaskProperty, value); }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            //need the second check only for .NET 4.0 because this method gets called way more often for some reason.
            if (item is ContentTabView && !(this.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated))
            {
                _contentTabView = item as ContentTabView;
            }
            return (item is FabTabItem);
        }

        
        

        public bool ShowToolTipImages
        {
            get
            {
                return (bool)this.GetValue(ShowToolTipImagesProperty);
            }
            set
            {
                this.SetValue(ShowToolTipImagesProperty, value);
            }

        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            //Added some extra _contentTabview stuff in this method in the if conditional
            //as well as nulling it out outside of the if conditional.  Also had to 
            //null out _contentTabView in OnSelectionChanged.  All these changes were necessary
            //because of some differing behavior in .NET 4.0 beta for when IsItemsItsOwnContainer
            //gets called which caused me state issues with_contentTabView.
            if (_contentTabView != null)
            {
                ContentTabItem tabItem = new ContentTabItem();
                if (this.ContentTabHeaderContent != null)
                {
                    tabItem.Header = this.ContentTabHeaderContent;
                }
                else
                {
                    FabTabResources rd = new FabTabResources();
                    tabItem.Header = rd.DefaultContentTabHeader;
                }
                _contentTabView = null;
                return tabItem;
            }

            FabTabItem newClosableTabItem = new FabTabItem();
            newClosableTabItem.TabClosing += new RoutedEventHandler(newClosableTabItem_TabClosing);
            return newClosableTabItem;
        }

        void newClosableTabItem_TabClosing(object sender, RoutedEventArgs e)
        {
            //TODO: see if this check is redundant
            if (!e.Handled)
            {
                FabTabItem closingTab = e.OriginalSource as FabTabItem;

                //first unwire the handler we are currently in from the event,
                //then raise the event from the tab again so that any subscribers beyond FabTabControl
                //in the chain can have the event handlers invoked first so they can choose to mark it handled
                //thereby cancelling the tab close.
                //Then rewire this handler to the TabClosing event.
                //This seems a bit hackish, but in the case of views that were added to the FabTabControl's
                //Items collection in XAML, by the time in codebehind that the consumer can subscribe to the
                //TabClosing event, the FabTabControl has already wired up this eventhandler.
                closingTab.RemoveHandler(e.RoutedEvent, new RoutedEventHandler(newClosableTabItem_TabClosing));
                closingTab.RaiseEvent(e);
                closingTab.AddHandler(e.RoutedEvent, new RoutedEventHandler(newClosableTabItem_TabClosing));

                if (closingTab != null && !e.Handled)
                {
                    
                    this.CloseTab(closingTab);
                    if (this.SelectedItem != null)
                    {
                        FabTabItem lastTabItem = this.ItemContainerGenerator.ContainerFromIndex(this.Items.Count - 1) as FabTabItem;
                        if (lastTabItem != null)
                        {
                            //when a tab closes that happens be the source or target on either of the advisors
                            //it will disallow further drag and drop operations.  therefore whenever a tab closes
                            //reset this to something valid, in this case, the last item in the collection.
                            DragDropManager.GetDragSourceAdvisor(lastTabItem).SourceUI = lastTabItem;
                            DragDropManager.GetDropTargetAdvisor(lastTabItem).TargetUI = lastTabItem;
                                                                                                               
                        }
                        
                    }
                }
            }
        }


        static FabTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FabTabControl), new FrameworkPropertyMetadata(typeof(FabTabControl)));
        }

        public FabTabControl()
        {
            this.Loaded += new RoutedEventHandler(FabTabControl_Loaded);
            this.HiddenContent = new StackPanel();


        }


        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //this is a little hackish, but this boolean is needed for performance reasons.
            //See comments in OnSelectionChanged method
            _itemsChanging = true;

            //If the ItemsSource is set to ObservableCollection<T> by the developer, this method
            //will fire anytime an item is added to the collection.  If it's set to something
            //that's not Obvservable, it will only fire when ItemsSource is nulled out.
            //This matters quite alot in terms of adding ContentsTab when appropriate.
            //Closing tabs does fire this method because we null the itemssource out.
            //This method DOES get fired for just declaring tabitems or usercontrols
            //in the tab control in XAML though, because CollectionView must support notification.
            base.OnItemsChanged(e);

            //need to make sure that newly added or removed views are taken out of their hidden place
            //in the visual tree
            ForceItemRender();

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //wire up the tab items were added on the fly from code
                FabTabItem newClosableTabItem = e.NewItems[0] as FabTabItem;
                if (newClosableTabItem == null)
                {
                    newClosableTabItem = this.ItemContainerGenerator.ContainerFromItem(e.NewItems[0]) as FabTabItem;
                }

                if (newClosableTabItem != null)
                {
                    newClosableTabItem.TabClosing += new RoutedEventHandler(newClosableTabItem_TabClosing);
                }

                AddContentsTabIfNecessary();
                UpdateContentsTabDropdownIfNecessary();
                UpdateContentsTabViewsIfNecessary();
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (ShowContentsTab && _contentsTabAdded && this.Items.Count <= 2)
                {
                    _contentsTabAdded = false;
                    RemoveContentsTab();
                }
                else
                {
                    UpdateContentsTabDropdownIfNecessary();
                    UpdateContentsTabViewsIfNecessary();
                }

            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                UpdateContentsTabDropdownIfNecessary();
                if (this.ItemsSource != null)
                {
                    UpdateContentsTabViewsIfNecessary();
                }

            }

            //TODO: handle replace to update content tab view?

            _itemsChanging = false;
            
        }

        private void UpdateContentsTabViewsIfNecessary()
        {
            if (ShowContentsTab && _contentsTabAdded && this.Items.Count > 1)
            {
                ContentTabView view = this.Items[0] as ContentTabView;
                if (view != null)
                {
                    Dictionary<object, object> namedContentList = new Dictionary<object, object>();
                    foreach (object item in this.Items)
                    {
                        FabTabItem tabItem = item as FabTabItem;
                        if (tabItem == null)
                        {
                            tabItem = this.ItemContainerGenerator.ContainerFromItem(item) as FabTabItem;
                        }
                        if (tabItem != null)
                        {
                            namedContentList.Add(item, tabItem.Header);
                        }
                    }

                    view.SetViews(namedContentList);

                }
            }
        }

        private void UpdateContentsTabDropdownIfNecessary()
        {
            //How do we do this if the header isn't merely string content????
            if (ShowContentsTab && _contentsTabAdded)
            {
                if (this.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                {
                    this.ItemContainerGenerator.StatusChanged += new EventHandler(ItemContainerGenerator_StatusChanged);
                }

                ContentTabItem contentTab = this.ItemContainerGenerator.ContainerFromIndex(0) as ContentTabItem;
                if (contentTab != null)
                {
                    contentTab.ApplyTemplate();
                    _contentsTabComboBox = contentTab.Template.FindName("PART_tabHeaderComboBox", contentTab) as ComboBox;
                    _contentsTabComboBox.SelectionChanged -= new SelectionChangedEventHandler(combo_SelectionChanged);
                    _contentsTabComboBox.Items.Clear();
                    if (_contentsTabComboBox != null)
                    {
                        foreach (object item in this.Items)
                        {
                            if (!(item is ContentTabView || item is ContentTabItem))
                            {
                                TabItem tabItem = item as TabItem;
                                if (tabItem != null)
                                {
                                    InsertUniqueHeader(tabItem);
                                }
                                else
                                {
                                    tabItem = this.ItemContainerGenerator.ContainerFromItem(item) as TabItem;
                                    if (tabItem != null)
                                    {
                                        InsertUniqueHeader(tabItem);
                                    }
                                }
                            }

                        }

                        //now wire up to the combo to select tabs
                        _contentsTabComboBox.SelectionChanged += new SelectionChangedEventHandler(combo_SelectionChanged);
                    }
                }
            }


        }

        void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (this.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                this.ItemContainerGenerator.StatusChanged -= new EventHandler(ItemContainerGenerator_StatusChanged);
                this.UpdateContentsTabDropdownIfNecessary();
            }
        }

        private void InsertUniqueHeader(TabItem tabItem)
        {
            //add a a dictionary entry so combobox.selectedindex returns
            //the correct value, therefore make sure everything is unique to
            //handle the cases of identical headers
            DictionaryEntry e = new DictionaryEntry(Guid.NewGuid(), tabItem.Header);
            _contentsTabComboBox.Items.Add(e);
        }

        void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (_contentsTabComboBox != null)
            {
                //select the corresponding tab, which will be one off from the combobox
                //because of the ContentTab.
                this.SelectedIndex = _contentsTabComboBox.SelectedIndex + 1;
                _contentsTabComboBox.SelectionChanged -= new SelectionChangedEventHandler(combo_SelectionChanged);
                _contentsTabComboBox.SelectedIndex = -1;
                _contentsTabComboBox.SelectionChanged += new SelectionChangedEventHandler(combo_SelectionChanged);
            }
        }

        private void AddContentsTabIfNecessary()
        {
            if (ShowContentsTab && !_contentsTabAdded && this.Items.Count > 1)
            {
                _contentsTabAdded = true;
                AddContentsTab();
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            //have to check _itemsChanging for performance reasons, otherwise this method
            //gets called when we force renders for adding/removing tabs, and then we're constantly'
            //updating the snapshots WAY more than we need to.
            if (this.ShowContentsTab && !_itemsChanging)
            {
                if (this.ItemsSource != null)
                {
                    //juggle the parents of our hidden stackpanel that gets stuffed into
                    //the hidden contentpresenter
                    if (e.AddedItems.Count > 0)
                    {
                        this.HiddenContent.Children.Remove(e.AddedItems[0] as FrameworkElement);
                    }

                    base.OnSelectionChanged(e);

                    if (e.RemovedItems.Count > 0)
                    {
                        this.HiddenContent.Children.Add(e.RemovedItems[0] as FrameworkElement);
                    }
                }

                //this only needs to happen if we are in this method because the user actually
                //change the tab they selected, and not when new items are added or removed
                //to the collection and ForceRenderItems  (which forces selection of all items)
                //is called from OnItemsChanged---thus the _itemsChanging check above
                UpdateContentsTabViewsIfNecessary();
            }
            
            base.OnSelectionChanged(e);

        }

        void FabTabControl_Loaded(object sender, RoutedEventArgs e)
        {
            AddContentsTabIfNecessary();

            if (this.ShowContentsTab)
            {
                ForceItemRender();
                UpdateContentsTabViewsIfNecessary();
            }

        }

        private void ForceItemRender()
        {

            if (this.ItemsSource != null)
            {
                ForceItemRenderForItemsSource();
            }
            else
            {
                ForItemRenderForItems();

            }
        }

        private void ForItemRenderForItems()
        {
            //loop through everything and force it to render to the visual tree so the bitmaps
            //for the content tab look correct.
            object selectedItem = this.SelectedItem;
            foreach (object item in this.Items)
            {
                FabTabItem tabItem = item as FabTabItem;
                if (tabItem == null)
                {
                    tabItem = this.ItemContainerGenerator.ContainerFromItem(item) as FabTabItem;
                }
                if (tabItem != null)
                {
                    tabItem.IsSelected = true;
                    this.UpdateLayout();
                }
            }
            
            //now reset the selected item, or set a reasonable selected tab.
            if (selectedItem != null)
            {
                this.SelectedItem = selectedItem;
            }
            else
            {
                //set to first non content tab if there was no item selected.
                if (this.Items.Count > 0)
                {
                    if (this.Items[0] is ContentTabView)
                    {
                        this.SelectedIndex = 1;
                    }
                    else
                    {
                        this.SelectedIndex = 0;
                    }
                }
            }
        }

        private void ForceItemRenderForItemsSource()
        {
            //add all the unselected items into the hidden stackpanel that gets stuffed into
            //the hidden content presenter...without this, snapshots of the other views for the contentstab
            //don't work.
            this.HiddenContent.Children.Clear();
            for (int counter = 0; counter < this.Items.Count; counter++)
            {
                FrameworkElement element = this.Items[counter] as FrameworkElement;
                if (element != null && this.SelectedItem != element)
                {
                    this.HiddenContent.Children.Add(element);
                }
            }
            this.UpdateLayout();
        }




        private void RemoveContentsTab()
        {
            if (this.ItemsSource != null)
            {
                IList items = this.ItemsSource as IList;
                if (items != null)
                {
                    this.ItemsSource = null;
                    items.RemoveAt(0);
                    this.ItemsSource = items;
                }
            }
            else
            {
                this.Items.RemoveAt(0);
            }
        }

        private void AddContentsTab()
        {
            if (this.ItemsSource != null)
            {
                //using ItemsSource, modify collection to contain TOC Tab
                IList items = this.ItemsSource as IList;
                if (items != null)
                {
                    this.ItemsSource = null;
                    items.Insert(0, GetContentsTab());
                    this.ItemsSource = items;
                    (this.ItemContainerGenerator.ContainerFromItem(items[1]) as TabItem).Focus();
                }

            }
            else
            {
                //presumably using items collection, modify that collection to contain TOC Tab
                this.Items.Insert(0, GetContentsTab());

            }
        }

        private object GetContentsTab()
        {
            ContentTabView contentTabView = new ContentTabView(this);
            return contentTabView;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.ContentTabHeaderComboBoxStyle == null && BindingOperations.GetBinding(this, FabTabControl.ContentTabHeaderComboBoxStyleProperty) == null)
            {
                this.ContentTabHeaderComboBoxStyle = this.TryFindResource("ComboBoxDefaultStyle") as Style;
            }


            //wire up the tabitems wired up directly in xaml
            if (this.Items != null)
            {
                foreach (object currentItem in this.Items)
                {
                    FabTabItem tabItem = currentItem as FabTabItem;
                    if (tabItem != null)
                    {
                        //tabItem.TabContainer = this;
                        tabItem.TabClosing += new RoutedEventHandler(newClosableTabItem_TabClosing);
                    }
                }
            }

            //configure the FabTabPanel appropriately, which would mean following the default
            //out of the box WPF TabPanel behavior (which doesn't always look too good with ContentTab
            if (this.AllowMultiLineTabHeaders)
            {
                FabTabPanel tabPanel = this.Template.FindName("HeaderPanel", this) as FabTabPanel;
                if (tabPanel != null)
                {
                    tabPanel.AllowMultiLineTabHeaders = true;
                }

            }

        }



        #region IClosableTabHost Members

        public void CloseTab(TabItem tab)
        {
            if (tab != null)
            {
                if (this.ItemsSource != null)
                {
                    IList listItemsSource = this.ItemsSource as IList;
                    if (listItemsSource != null)
                    {
                        object selectedItem = this.SelectedItem;

                        //if we're closing the tab that was selected, find the preceding item
                        //and set it back to that.
                        if (selectedItem == tab.Content)
                        {
                            int indexOfCurrentTab = this.Items.IndexOf(tab.Content) - 1;
                            if (indexOfCurrentTab >= this.Items.Count)
                            {
                                selectedItem = this.Items[this.Items.IndexOf(tab.Content) - 1];
                            }
                            else if (!this.ShowContentsTab)
                            {
                                selectedItem = this.Items[0];
                            }
                            else if (indexOfCurrentTab != -1)
                            {
                                selectedItem = this.Items[1];
                            }
                        }

                        listItemsSource.Remove(tab.Content);
                        this.ItemsSource = null;
                        this.ItemsSource = listItemsSource;

                        this.SelectedItem = selectedItem;


                    }
                    //TODO: Throw an exception here because a tab is trying to be closed
                    //in an uneditable list
                }
                else
                {

                    //remove the tab item if that's actually in the collection
                    if (this.Items.Contains(tab))
                    {
                        this.Items.Remove(tab);
                    }
                    //otherwise remove the view
                    else
                    {
                        this.Items.Remove(tab.Content);
                    }

                }
            }
        }

        #endregion
    }
}
