using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollitionDetecred : MonoBehaviour
{
    public Color_state mycolor;
    public bool statk;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().shield)
        {

            if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().color != mycolor && statk == false)
            {

                Scene_1.s.interactorsBase.GetInteractor<PlayerInteractor>().Get_Damag();
                statk = true;
            }
            Destroy(gameObject);
            //gameObject(false);
        }
    }
}