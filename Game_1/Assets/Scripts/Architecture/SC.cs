using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Turret;
using static Turret_Rot;

public class SC : SceneConfig
{
    public override Dictionary<Type, Interactor> CreatAllIntetactor()
    {
        Dictionary<Type, Interactor> InteractorMap = new Dictionary<Type, Interactor>();
        CreatInteractor<TurretsInteractor>(InteractorMap);
        CreatInteractor<LazerWallsInteractor>(InteractorMap);
        CreatInteractor<PlayerInteractor>(InteractorMap);
        CreatInteractor<HP_UIInteractor>(InteractorMap);
        CreatInteractor<RocketInteractor>(InteractorMap);
       // CreatInteractor<Turret_RotIterator>(InteractorMap);
        CreatInteractor<Memory_turrets_Interactor>(InteractorMap);
        CreatInteractor<GAMEOVER_UIInteractor>(InteractorMap);
        CreatInteractor<LazerwebInteractor>(InteractorMap);
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
        CreatRepocitory<RocketRepocitory>(RepocitoryMap);
        CreatRepocitory<LazerwebRepocitory>(RepocitoryMap);
        // CreatRepocitory<Turret_RotRepocitory>(RepocitoryMap);
        CreatRepocitory<GAMEOVER_UIRepocitory>(RepocitoryMap);
        CreatRepocitory<LazerwebRepocitory>(RepocitoryMap);
        CreatRepocitory<Memory_turrets_Repocitory>(RepocitoryMap);
        //репозитории
        //репозитории
        //...
        return RepocitoryMap;
    }
}
