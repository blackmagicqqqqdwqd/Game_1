using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerIntetactor playerIntetactor;
    void Start()
    {
        playerIntetactor = Scene_1.s.interactorsBase.GetInteractor<PlayerIntetactor>();
    }

 
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) { playerIntetactor.Jump(); }
    }
}
public class PlayerRepocitory : Repository
{
    public int damag;
    int hp;
    public override void Initialize()
    {
        damag = 10;
        hp = 10;
    }
}
public class PlayerIntetactor : Interactor
{
    Repository repository;


    public override void Initialize()
    {
        repository = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>();
    }
    public void Jump()
    {
        Debug.Log((repository as PlayerRepocitory).damag);
    }
}
