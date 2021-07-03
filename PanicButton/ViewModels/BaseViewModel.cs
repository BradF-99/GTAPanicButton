using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PanicButton.Interfaces;
using PanicButton.Models;

namespace PanicButton.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IListen<SignalMessage>
    {
        public virtual void Handle(SignalMessage msg)
        {
            throw new NotImplementedException("This should have been over-ridden.");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
