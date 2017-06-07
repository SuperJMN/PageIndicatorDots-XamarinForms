using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PageIndicatorDots.Controls
{
    public partial class PageIndicator : ContentView
    {
        public static BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object),
            typeof(PageIndicator), propertyChanged: OnSelectedItemChanged);

        public static BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int),
            typeof(PageIndicator), propertyChanged: OnSelectedIndexChanged, defaultValue: -1);

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable),
            typeof(PageIndicator), propertyChanged: OnItemsSourceChanged);

        public static BindableProperty SelectableItemsProperty =
            BindableProperty.Create("SelectableItems", typeof(IList<SelectableItem>), typeof(PageIndicator));

        public PageIndicator()
        {
            InitializeComponent();
            SelectItemCommand = new Command(o => SetSelectedItem((SelectableItem)o));
        }

        public Command SelectItemCommand { get; set; }

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public IList<SelectableItem> SelectableItems
        {
            get { return (IList<SelectableItem>)GetValue(SelectableItemsProperty); }
            set { SetValue(SelectableItemsProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        private void SetSelectedItem(SelectableItem o)
        {
            foreach (var i in SelectableItems)
            {
                var isSelected = i == o;
                if (isSelected)
                    SelectedItem = i.Item;
                i.IsSelected = isSelected;
            }
        }

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var target = (PageIndicator)bindable;

            target.UpdateSelectedItem();

        }

        private void UpdateSelectedItem()
        {
            if (this.SelectableItems == null)
            {
                return;
            }

            if (SelectedIndex == -1)
            {
                SetSelectedItem(null);
            }
            else
            {
                var toSelect = SelectableItems[SelectedIndex];
                SetSelectedItem(toSelect);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var target = (PageIndicator)bindable;
            var targetSelectableItems = target.SelectableItems;
            var selected = targetSelectableItems.FirstOrDefault(item => item.Item == newValue);
            target.SetSelectedItem(selected);
            target.UpdateSelectedIndex();
        }

        private void UpdateSelectedIndex()
        {
            var selectable = SelectableItems.FirstOrDefault(item => item.Item == SelectedItem);

            if (selectable == null)
            {
                SelectedIndex = -1;
            }

            SelectedIndex = SelectableItems.IndexOf(selectable);
        }


        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (newvalue == null)
                return;

            var target = (PageIndicator)bindable;
            var value = (IEnumerable)newvalue;
            var selectableItems = value.Cast<object>().Select(o => new SelectableItem(o)).ToList();
            target.SelectableItems = selectableItems;
            target.UpdateSelectedItem();
        }
    }
}