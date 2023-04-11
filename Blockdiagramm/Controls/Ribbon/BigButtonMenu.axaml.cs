using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Blockdiagramm.Controls.Ribbon
{
    public partial class BigButtonMenu : RibbonButtonControl
    {
        public static readonly DirectProperty<BigButtonMenu, AvaloniaList<RibbonMenuItem>> MenuItemsProperty =
            AvaloniaProperty.RegisterDirect<BigButtonMenu, AvaloniaList<RibbonMenuItem>>(nameof(MenuItems),
                o => o.MenuItems, (o, v) => o.MenuItems = v);

        private AvaloniaList<RibbonMenuItem> menuItems = new();

        public AvaloniaList<RibbonMenuItem> MenuItems
        {
            get => menuItems;
            set => SetAndRaise(MenuItemsProperty, ref menuItems, value);
        }

        private RibbonMenuItem? firstItem;
        private RibbonMenuItem? lastItem;

        public BigButtonMenu()
        {
            InitializeComponent();

            menuItems.CollectionChanged += OnMenuItemsChanged;
        }

        private void OnMenuItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            StackPanel menuParent = spMenuPanel;

            if ((e.Action == NotifyCollectionChangedAction.Add) && (e.NewItems != null))
            {
                int length = e.NewItems.Count;
                int lastIndex = e.NewStartingIndex + length - 1;
                foreach (RibbonMenuItem item in e.NewItems)
                {
                    item.Click += (sender, e) => popMenu.IsOpen = false;

                    // Check if the item is the first item or last item
                    if (e.NewStartingIndex == 0)
                    {
                        if (firstItem != null)
                        {
                            firstItem.IsFirst = false;
                        }

                        firstItem = item;
                        item.IsFirst = true;
                    }

                    if (lastIndex == menuItems.Count - 1)
                    {
                        if (lastItem != null)
                        {
                            lastItem.IsLast = false;
                        }

                        lastItem = item;
                        item.IsLast = true;
                    }

                    //item.Click += (sender, e) => popMenu.IsOpen = false;
                    menuParent.Children.Add(item);
                }
            }
            else if ((e.Action == NotifyCollectionChangedAction.Remove) && (e.OldItems != null))
            {
                // Query the items removed in the menu
                IEnumerable<RibbonMenuItem> query = from item in menuParent.Children
                                                    where e.OldItems.Contains(item as RibbonMenuItem)
                                                    select item as RibbonMenuItem;

                List<RibbonMenuItem> deletedList = query.ToList();
                menuParent.Children.RemoveAll(deletedList);

                int length = menuItems.Count;
                for (int i = 0; i < length; i++)
                {
                    RibbonMenuItem item = menuItems[i];
                    item.IsFirst = false;
                    item.IsLast = false;

                    if (i == 0)
                    {
                        item.IsFirst = true;
                    }
                    if (i == length - 1)
                    {
                        item.IsLast = true;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                menuParent.Children.Clear();
            }
        }

        public void OnMenuButtonClick(object sender, RoutedEventArgs args) => popMenu.IsOpen = true;

        public void OnMenuButtonLostFocus(object sender, RoutedEventArgs args) => popMenu.IsOpen = false;
    }
}
