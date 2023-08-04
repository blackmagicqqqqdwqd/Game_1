using System;
using System.Collections.Generic;
using UnityEngine;

public class RepositorysBase 
{
    Dictionary<Type, Repository> repositorys;
    SceneConfig sceneConfig;
    public RepositorysBase(SceneConfig sceneConfig)
    {
        this.sceneConfig = sceneConfig;
    }
    public void CreatAllRepository() 
    {
       repositorys = sceneConfig.CreatAllRepository();
    }
    public void RemoveRepository<T>() where T: Repository
    {
        repositorys.Remove(typeof(T));
    }
    public void InitializeAllRepository()
    {
        foreach (var repository in repositorys.Values)
        {
           repository.Initialize();
        }

    }
    public T GetRepository<T>() where T: Repository
    {
        return repositorys[typeof(T)] as T;        
    }
}
