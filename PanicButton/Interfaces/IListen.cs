using System;
using System.Collections.Generic;
using System.Text;

namespace PanicButton.Interfaces
{
    public interface IListen { }
    public interface IListen<in T> : IListen
    {
        void Handle(T obj);
    }
}
