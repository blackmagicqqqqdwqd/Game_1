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
   public void Initialize()
   {

   }
}
