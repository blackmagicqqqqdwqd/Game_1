using System;
using System.Collections.Generic;


public class InteractorsBase
{
    Dictionary<Type, Interactor> interactors;
    SceneConfig sceneConfig;
    public InteractorsBase(SceneConfig sceneConfig)
    {
        this.sceneConfig = sceneConfig;
    }
    public void CreatAllInteractor()
    {
        interactors = sceneConfig.CreatAllIntetactor();

    }
    public void InitializeAllInteractor()
    {
        foreach (var interactor in interactors.Values) { interactor.Initialize(); }
    }
    public void Remove<T>() where T : Interactor
    {
        interactors.Remove(typeof(T));
    }
    public T GetInteractor<T>() where T : Interactor
    {
        return interactors[typeof(T)] as T;
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