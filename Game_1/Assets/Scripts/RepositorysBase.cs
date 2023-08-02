using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepositorysBase 
{
    Dictionary<Type, Repository> repositorys;
    public abstract void Initialize();
    public void CreatRepository<T>() where T: Repository,new() {
        T repository = new T();
        repositorys[typeof(T)] = repository;
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
    public Repository GetRepository<T>() where T: Repository
    {
        return repositorys[typeof(T)];        
    }
}
