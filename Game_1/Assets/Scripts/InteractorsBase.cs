using System;
using System.Collections.Generic;

<<<<<<< HEAD
public abstract class InteractorsBase 
{
    Dictionary<Type ,Interactor> Interactors;
    public abstract void Initialize();
    public void CreatInteractor<T>() where T: Interactor,new() {
        T interactor = new T();
        Interactors[typeof(T)] = interactor;
=======
public class InteractorsBase 
{
    Dictionary<Type ,Interactor> interactors;
    public void CreatInteractor<T>() where T: Interactor,new() {
        T interactor = new T();
        interactors[typeof(T)] = interactor;
    }
    public void Remove<T>() where T:Interactor
    {
        interactors.Remove(typeof(T));
    }
      public Interactor GetInteractor<T>() where T: Interactor
    {
        return interactors[typeof(T)];        
    }
    public void InitializeAllInteractor()
    {
        foreach (var interactor in interactors)
        {
           // interactor.
        }

>>>>>>> main
    }
}
