using System.ComponentModel;
using System.Runtime.CompilerServices;
using PageIndicatorDots.Annotations;

namespace PageIndicatorDots.Controls
{
    public class SelectableItem : INotifyPropertyChanged
    {
        private bool isSelected;
        public object Item { get; }

        public SelectableItem(object item)
        {
            Item = item;
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value == isSelected) return;
                isSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}