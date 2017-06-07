using System.Collections;
using Xamarin.Forms;

namespace PageIndicatorDots.Controls
{
    public class BindableStackLayout : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<BindableStackLayout, IEnumerable>(p => p.ItemsSource, null, propertyChanged: OnItemsSourceChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create<BindableStackLayout, DataTemplate>(p => p.ItemTemplate, null, propertyChanged: OnItemTemplateChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create<BindableStackLayout, object>(p => p.SelectedItem, null, BindingMode.TwoWay);

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void OnItemTemplateChanged(BindableObject bindable, DataTemplate oldvalue, DataTemplate newvalue)
        {
            var bsl = (BindableStackLayout)bindable;
            bsl.UpdateItems();
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var bsl = (BindableStackLayout)bindable;
            bsl.Children.Clear();
            bsl.UpdateItems();
        }

        private void UpdateItems()
        {
            if (ItemsSource == null)
            {
                return;
            }

            foreach (var v in ItemsSource)
            {
                var view = GetView(v);
                Children.Add(view);
            }
        }

        private View GetView(object item)
        {
            View view;
            if (ItemTemplate == null)
            {
                view = new Label { Text = item.ToString() };
            }
            else
            {
                view = (View)ItemTemplate.CreateContent();
                view.BindingContext = item;
            }
            return view;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
    }
}