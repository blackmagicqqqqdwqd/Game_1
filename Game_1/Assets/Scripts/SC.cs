using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC : SceneConfig
{
    public override Dictionary<Type, Interactor> CreatAllIntetactor()
    {
        Dictionary<Type, Interactor> InteractorMap = new Dictionary<Type, Interactor>();
       // CreatInteractor<PlayerIntetactor>(InteractorMap);
        // ����������� 
        // ����������� 
        // ...
        return InteractorMap;
    }

    public override Dictionary<Type, Repository> CreatAllRepository()
    {
        Dictionary<Type, Repository> RepocitoryMap = new Dictionary<Type, Repository>();
       // CreatRepocitory<PlayerRepocitory>(RepocitoryMap);
        //�����������
        //�����������
        //...
        return RepocitoryMap;
    }
}
