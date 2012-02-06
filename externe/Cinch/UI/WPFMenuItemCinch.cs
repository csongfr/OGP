﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Cinch
{
    /// <summary>
    /// Provides a mechanism for constructing MenuItems
    /// within a ViewModel
    /// </summary>
    /// <example>
    /// <![CDATA[
    /// 
    ///  AND IN VIEWMODEL C# DO THIS TO CREATE MENUS
    ///  private List<WPFMenuItem> CreateMenus()
    ///  {
    ///    var menu = new List<WPFMenuItem>();
    ///    //create the File Menu
    ///    var miFile = new WPFMenuItem("File");
    ///    var miExit = new WPFMenuItem("Exit");
    ///    miExit.Command = ExitApplicationCommand;
    ///    miFile.Children.Add(miExit);
    ///    menu.Add(miFile);
    ///    //create the Actions Menu
    ///    menu.Add(new WPFMenuItem("Actions"));
    ///    return menu;
    ///  }
    /// 
    /// 
    ///  public List<WPFMenuItem> MenuOptions
    ///  {
    ///     get
    ///     {
    ///         return CreateMenus();
    ///     }
    ///  }
    /// 
    ///   AND IN XAML DO THE FOLLOWING FOR THE STYLE
    ///   <Style x:Key="ContextMenuItemStyle">
    ///     <Setter Property="MenuItem.Header" Value="{Binding Text}"/>
    ///     <Setter Property="MenuItem.ItemsSource" Value="{Binding Children}"/>
    ///     <Setter Property="MenuItem.Command" Value="{Binding Command}" />
    ///     <Setter Property="MenuItem.Icon" Value="{Binding Icon}" />
    ///   </Style>
    /// 
    ///   AND YOU CAN CREATE A MENU LIKE THIS
    ///   <StackPanel Orientation="Horizontal">
    ///     <Image Source="{Binding Image}" Width="16" Height="16" />
    ///     <TextBlock Margin="5" HorizontalAlignment="Left" VerticalAlignment="Center" 
    ///         Text="{Binding Header}" />
    ///     <StackPanel.ContextMenu>
    ///         <ContextMenu ItemContainerStyle="{StaticResource ContextMenuItemStyle}" 
    ///             ItemsSource="{Binding MenuOptions}" />
    ///     </StackPanel.ContextMenu>
    ///   </StackPanel>
    /// ]]>
    /// </example>
    internal class WPFMenuItemCinch
    {
        #region Public Properties
        public String Text { get; set; }
        public String IconUrl { get; set; }
        public List<WPFMenuItemCinch> Children { get; private set; }
        public SimpleCommandCinch Command { get; set; }
        #endregion

        #region Ctor
        public WPFMenuItemCinch(string item)
        {
            Text = item;
            Children = new List<WPFMenuItemCinch>();
        }
        #endregion
    }
}
