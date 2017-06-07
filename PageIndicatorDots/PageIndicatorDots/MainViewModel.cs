using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PageIndicatorDots.Annotations;

namespace PageIndicatorDots
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Person selectedPerson;

        public MainViewModel()
        {
            People = new List<Person>()
            {
                new Person("Johnny Bravo"),
                new Person("Peter Griffin"),
                new Person("José Manuel Nieto"),
                new Person("Alex Rainman :)"),
            };
        }

        public IEnumerable<Person> People { get; }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (Equals(value, selectedPerson)) return;
                selectedPerson = value;
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

    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}