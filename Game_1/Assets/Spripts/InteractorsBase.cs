using System;
using System.Collections.Generic;

public abstract class InteractorsBase 
{
    Dictionary<Type ,Interactor> Interactors;
    public abstract void Initialize();
    public void CreatInteractor<T>() where T: Interactor,new() {
        T interactor = new T();
        Interactors[typeof(T)] = interactor;
    }
}
