using System;
using System.Collections.Generic;


public class InteractorsBase
{
    Dictionary<Type, Interactor> interactors;
    SceneConfig sceneConfig;
    public InteractorsBase(SceneConfig sceneConfig)
    {
        sceneConfig = this.sceneConfig;
    }
    public void CreatAllInteractor()
    {
        interactors = sceneConfig.CreatAllIntetactor();

    }
    public void InitializeAllInteractor()
    {

    }
    public void Remove<T>() where T : Interactor
    {
        interactors.Remove(typeof(T));
    }
    public Interactor GetInteractor<T>() where T : Interactor
    {
        return interactors[typeof(T)];
    }
    /*
    public void StartAllInteractor()
    {
        foreach (var interactor in interactors.Values)
        {
            interactor.OnStart();
        }
    }
    */
}