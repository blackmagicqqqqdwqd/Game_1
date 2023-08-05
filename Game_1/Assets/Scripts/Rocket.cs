using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class Rocket: MonoBehaviour
{
    Vector3 targetPos;
    Vector3 playerPos = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;
    public void rocketClone(float x, float y, Color color)
    {
        GameObject this_rocket = new GameObject("SomeRocket");
        var SR = this_rocket.AddComponent<SpriteRenderer>();
        SR.sprite = Resources.Load<Sprite>("Sprites/ProtectedCircle");
        SR.color = color;
        SR.sortingOrder = 1;
        this_rocket.transform.position = new Vector3(x, y, 0);
        StartCoroutine(LaunchRocket(Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player,this_rocket,Random.Range(0,4))); 
    }
    IEnumerator LaunchRocket(GameObject target, GameObject rocket, int dir)
    {
        yield return new WaitForSeconds(0.1f);
        targetPos = target.transform.position - rocket.transform.position*2;        
        rocket.transform.position=Vector3.Lerp(rocket.transform.position, targetPos, 0.2f);
        if (Vector3.Distance(rocket.transform.position, targetPos) > 2f) StartCoroutine(LaunchRocket(target, rocket, dir));
        else Destroy(rocket);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) == true) { 
            Debug.Log(1);
            switch (Random.Range(0, 4))
            {
                case 0:
                    rocketClone(playerPos.x - 10, playerPos.y -10, Color.red);
                    break;
                case 1:
                    rocketClone(playerPos.x - 10, playerPos.y + 10, Color.red);
                    break;
                case 2:
                    rocketClone(playerPos.x + 10, playerPos.y + 10, Color.red);
                    break;
                case 3:
                    rocketClone(playerPos.x + 10, playerPos.y - 10, Color.red);
                    break;
            }
        }
    }
}
