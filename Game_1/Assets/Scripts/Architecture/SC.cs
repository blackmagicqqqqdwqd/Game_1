using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Turret;

public class SC : SceneConfig
{
    public override Dictionary<Type, Interactor> CreatAllIntetactor()
    {
        Dictionary<Type, Interactor> InteractorMap = new Dictionary<Type, Interactor>();
        CreatInteractor<TurretsInteractor>(InteractorMap);
        CreatInteractor<LazerWallsInteractor>(InteractorMap);
        CreatInteractor<PlayerInteractor>(InteractorMap);
        CreatInteractor<HP_UIInteractor>(InteractorMap);
        // интеракторы 
        // интеракторы 
        // ...
        return InteractorMap;
    }

    public override Dictionary<Type, Repository> CreatAllRepository()
    {
        Dictionary<Type, Repository> RepocitoryMap = new Dictionary<Type, Repository>();
        CreatRepocitory<TurretsRepocitort>(RepocitoryMap);
        CreatRepocitory<LazerWallsRepository>(RepocitoryMap);
        CreatRepocitory<PlayerRepocitory>(RepocitoryMap);
        CreatRepocitory<HP_UIRepocitory>(RepocitoryMap);
        //репозитории
        //репозитории
        //...
        return RepocitoryMap;
    }
}
