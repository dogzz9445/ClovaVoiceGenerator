using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace Common
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool? SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }
            storage = value;
            RaisePropertyChangedEvent(propertyName);
            return true;
        }

        protected bool? SetObservableProperty<T>(ref T? storage, T value, [CallerMemberName] string? propertyName = null) where T : INotifyPropertyChanged
        {
            if (storage != null)
            {
                storage.PropertyChanged -= new PropertyChangedEventHandler(RaisePropertyChangedEvent);
            }
            bool? result = SetProperty(ref storage, value);
            if (storage != null)
            {
                storage.PropertyChanged += new PropertyChangedEventHandler(RaisePropertyChangedEvent);
            }
            return result;
        }

        protected bool? SetCollectionProperty<T>(ref T? storage, T value, [CallerMemberName] string? propertyName = null) where T : INotifyCollectionChanged
        {
            if (storage != null)
            {
                storage.CollectionChanged -= new NotifyCollectionChangedEventHandler(RaisePropertyChangedEvent);
            }
            bool? result = SetProperty(ref storage, value);
            if (storage != null)
            {
                storage.CollectionChanged += new NotifyCollectionChangedEventHandler(RaisePropertyChangedEvent);
            }
            return result;
        }

        //protected bool SetObservableCollection<T>(ref T storage, T value, [CallerMemberName] string propertyName = null) where T : IColl

        protected void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChangedEvent(object? sender, PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(sender, eventArgs);
        }

        private void RaisePropertyChangedEvent(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("Add"));
            }
            if (e.OldItems != null)
            {
                PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs("delete"));
            }
        }
    }
}
