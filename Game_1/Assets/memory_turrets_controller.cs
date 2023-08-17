using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class memory_turrets_controller : MonoBehaviour
{
    private enum State
    {
        show_color, atack, wait, finish, move_on_atack, massage_on_atack
    }
    [SerializeField]private State state = State.wait;
    int x = 0;
    public List<Memory_turrets> memory_Turrets = new List<Memory_turrets>();
    List<int> way = new List<int>();
    public bool process_underway = false;
    void Start()
    {
        var v = CircleSpawn(10,4,45,transform.position);
        var v1 = CircleSpawn(4.2f, 4, 45, transform.position);
        state = State.move_on_atack;
        Scene_1.s.interactorsBase.GetInteractor<Memory_turrets_Interactor>().Creat(v[0], v1[0]);
        Scene_1.s.interactorsBase.GetInteractor<Memory_turrets_Interactor>().Creat(v[1], v1[1]);
        Scene_1.s.interactorsBase.GetInteractor<Memory_turrets_Interactor>().Creat(v[2], v1[2]);
        Scene_1.s.interactorsBase.GetInteractor<Memory_turrets_Interactor>().Creat(v[3], v1[3]);
    }
    void Update()
    {
        if (All_if_state(Memory_turrets.State.destroy))
        {
            foreach (var v in memory_Turrets)
            {
                Destroy(v.gameObject);
            }
            Scene_1.Now_atack = false;
            Scene_1.s.repositorysBase.GetRepository<Memory_turrets_Repocitory>().memory_Turrets_Controller = null;
            Destroy(this);

        }
        //Debug.Log( All_if_state(Memory_turrets.State.show_color));
        
            switch (state)
            {
                case State.show_color:
                    Show_color();
                    break;
                case State.atack:
                    Atack();
                    break;
                case State.finish:
                    Finish();
                    break;
                case State.move_on_atack:
                    Move_on_atack();
                    break;
                case State.massage_on_atack:
                    if (!process_underway) StartCoroutine(Massage_on_atack());

                    break;
            
        }

      
    }
    List<Vector3> CircleSpawn(float rad, int amount, float rot, Vector3 coord)
    {
        if (rad > 0 && amount > 0)
        {
            List<Vector3> SpawnedObjects = new List<Vector3>();
            for (int i = 1; i <= amount; ++i)
            {
                var v = new Vector3(coord.x + rad * Mathf.Cos((360 / amount) * i * Mathf.Deg2Rad + rot * Mathf.Deg2Rad), coord.y + rad * Mathf.Sin((360 / amount) * i * Mathf.Deg2Rad + rot * Mathf.Deg2Rad), 0);

                SpawnedObjects.Add(v);
            }
            return SpawnedObjects;
        }
        else return null;
    }
    public IEnumerator Massage_on_atack()
    {
        process_underway = true;
            foreach (var i in memory_Turrets)
            {
                i.GetComponent<SpriteRenderer>().color = Color.black;
            }
        
        yield return new WaitForSeconds(3);
       
            foreach (var i in memory_Turrets)
            {
                i.GetComponent<SpriteRenderer>().color = Color.white;
            }
        yield return new WaitForSeconds(1);
        state = State.atack;
        process_underway = false;
    }
    public void Show_color()
    {

        if (!process_underway)
        {
            
            process_underway = true;
            List<int> ways = f();
            if (ways.Count > 0)
            {
                var v = ways[Random.Range(0, ways.Count)];
                way.Add(v);
                StartCoroutine(memory_Turrets[v].Show_color(this));
            }
            else if (ways.Count == 0)
            {
                foreach (var v in memory_Turrets) v.show_number = 0;
                state = State.massage_on_atack;
                process_underway = false;
            }
        }
    }
    public void Atack()
    {
        if (!process_underway)
        {

            process_underway = true;
            memory_Turrets[way[x]].my_state = Memory_turrets.State.atack;
            if ((x + 1) >= way.Count) state = State.wait;
            x++;
        }
      
    }
    public void Finish()
    {
        if (!process_underway) { }
    }
    public void Move_on_atack()
    {
        if (!process_underway)
        {
           
            foreach (var v in memory_Turrets) v.my_state = Memory_turrets.State.move_on_spawn_zone;
            process_underway = true;
        }
        if (All_if_state(Memory_turrets.State.show_color) && process_underway)
        {
            state = State.show_color;
            process_underway = false;
        }
    }
    public List<int> f()
    {

        {
            List<int> list = new List<int>();
            for (int i = 0; i < memory_Turrets.Count; i++)
            {
                if (memory_Turrets[i].colors.Count > memory_Turrets[i].show_number)
                {
                    list.Add(i);
                }
            }
            //
            return list;

        }
    }
    private bool All_if_state(Memory_turrets.State local_state)
    {
        bool b = true;
        foreach (var v in memory_Turrets)
        {
            if (local_state != v.my_state) {  b = false; }
        }
        
        return b;
    }
}

public class Memory_turrets_Interactor :Interactor
{
    private Memory_turrets_Repocitory memory_turrets_Repocitory;
    public override void Initialize()
    {
        memory_turrets_Repocitory = Scene_1.s.repositorysBase.GetRepository<Memory_turrets_Repocitory>();
    }
    public memory_turrets_controller Creat_controller()
    {
        GameObject gameObject = new GameObject("Contoller_memory_turrets");
        memory_turrets_controller contoller = gameObject.AddComponent<memory_turrets_controller>();
        memory_turrets_Repocitory.memory_Turrets_Controller = contoller;

        if (Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player == null)
            gameObject.transform.position = Scene_1.s.repositorysBase.GetRepository<PlayerRepocitory>().player.transform.position;
        return contoller;

    }
    public void Creat(Vector2 position,Vector3 atack)
    {
        if(memory_turrets_Repocitory.memory_Turrets_Controller != null)
        {
            GameObject g = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Memory_turrets"));
            g.transform.position = new Vector3(position.x, position.y, 0);
            memory_turrets_Repocitory.memory_Turrets_Controller.memory_Turrets.Add(g.GetComponent<Memory_turrets>());

            g.GetComponent<Memory_turrets>().m = memory_turrets_Repocitory.memory_Turrets_Controller.gameObject;
            g.GetComponent<Memory_turrets>().atack_position = atack;
        }
    }
    public void Atack()
    {
        Creat_controller();
        Scene_1.Now_atack = true;
    }
}
public class Memory_turrets_Repocitory : Repository
{
    
    public memory_turrets_controller memory_Turrets_Controller;
 
}