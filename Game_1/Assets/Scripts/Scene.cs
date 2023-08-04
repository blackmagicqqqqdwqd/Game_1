using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Scene 
{
   public InteractorsBase interactorsBase { private set;  get; }
   public RepositorysBase repositorysBase { private set;  get; }
   SceneConfig sceneconfig;
   public Scene(SceneConfig config)
   {
       sceneconfig = config;
       repositorysBase = new RepositorysBase(config);
        interactorsBase = new InteractorsBase(config);

    }
   public IEnumerator InitializeRoutine()
   {
        repositorysBase.CreatAllRepository();
        interactorsBase.CreatAllInteractor();
        repositorysBase.InitializeAllRepository();
        interactorsBase.InitializeAllInteractor();
        yield return null;
        
       // interactorsBase.InitializeAllInteractor(); // подписываем на репозитори
        
    }
   public IEnumerator StartGame()
   {
    return null;
   }
}
