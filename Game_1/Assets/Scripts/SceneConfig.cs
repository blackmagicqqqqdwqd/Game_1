using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class SceneConfig
{
     
    abstract public Dictionary<Type,Interactor> CreatAllIntetactor();
    abstract public Dictionary<Type,Repository> CreatAllRepository();
    public void CreatInteractor<T>(Dictionary<Type,Interactor> InteractorMap) where T:Interactor, new()
    {
        InteractorMap [typeof(T)] = new T();
    }
     public void CreatRepocitory<T>(Dictionary<Type,Repository> RepocitoryMap) where T:Repository, new()
    {
        RepocitoryMap [typeof(T)] = new T();
    }
}
