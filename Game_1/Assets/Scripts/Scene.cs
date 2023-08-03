using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Scene 
{
   InteractorsBase interactorsBase;
   RepositorysBase repositorysBase;
   SceneConfig sceneconfig;
   public Scene(SceneConfig config)
   {
       sceneconfig = config;
       interactorsBase = new InteractorsBase(config);
       repositorysBase = new RepositorysBase(config);

   }
   public IEnumerator InitializeRoutine()
   {
        repositorysBase.CreatAllRepository();
        interactorsBase.CreatAllInteractor();
        yield return null;
        repositorysBase.InitializeAllRepository(); 
        interactorsBase.InitializeAllInteractor(); // подписываем на репозитории
    }
   public IEnumerator StartGame()
   {
    return null;
   }
}
