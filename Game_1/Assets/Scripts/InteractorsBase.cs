using System.Collections.Generic;
using System;


public class InteractorsBase
{
    Dictionary<Type, Interactor> interactors;
    public void CreatInteractor<T>() where T : Interactor, new()
    {
        T interactor = new T();
        interactors[typeof(T)] = interactor;
    }
    public Interactor GetInteractor<T>() where T : Interactor
    {
        return interactors[typeof(T)];
    }
}
