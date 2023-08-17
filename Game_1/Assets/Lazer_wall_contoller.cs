using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public class Lazer_wall_contoller : MonoBehaviour
{
    public bool decreasing_force;
    public bool atack;
    private Color[] color_random = new[] { Color.blue, Color.red, Color.magenta };
    public List<Lazerwall> lazers = new List<Lazerwall>();
    public Vector3 Spawn_point;
    public float Spawn_distans;
    private int now_count = 0;
    public int Max_count;
    private float force;
    private void Update()
    {
        
        Is_Finish();
        if (atack)
        {
           
            if (now_count != 0 && (now_count < Max_count)&& lazers.Count > 0 && ((Spawn_point - lazers[lazers.Count - 1].transform.position).magnitude > Spawn_distans))
            {
                if (decreasing_force) force -= 0.5f;
                now_count++;
                Color color = Color.white;
                if (color_random.Length != 0 && color_random != null) color = color_random[Random.Range(0, color_random.Length)];
                Scene_1.s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(Spawn_point, new Vector2(0, force), color);

            }
            else if (now_count == 0 && Max_count != 0)
            {
                //Debug.Log(now_count);
                if (decreasing_force) force -= 0.5f;
                Color color = Color.white;
                if (color_random.Length != 0 && color_random != null) color = color_random[Random.Range(0, color_random.Length)];
                now_count++;
                Scene_1.s.interactorsBase.GetInteractor<LazerWallsInteractor>().Creat(Spawn_point, new Vector2(0, force), color);
            }
        }
      
    }
    public void Is_Finish()
    {
      
        if (now_count == Max_count && lazers .Count == 0 && Max_count != 0)
        {
            atack = false;
            Scene_1.Now_atack = false;
       
           
        }
    }
    public void Set_params(int max_count, float spawn_distans, Vector3 spawn, int force =4)
    {
        now_count = 0;
        atack = true;
        Max_count = max_count;
        Spawn_distans = spawn_distans;
        Spawn_point = spawn;
        this.force = force;
        decreasing_force = false;
    }
}
public class LazerWallsRepository : Repository
{
    public bool Atack_is_now { get; set; }
    public Lazer_wall_contoller lazer_Wall_Contoller { get; set; }
    public LazerWallsRepository()
    {
  
    }

}

public class LazerWallsInteractor : Interactor
{
  
    LazerWallsRepository repository;
    public override void Initialize() => repository = Scene_1.s.repositorysBase.GetRepository<LazerWallsRepository>();
    public void Creat(Vector2 center, Vector2 speed, Color color, int length = 10)
    {
        if (repository.lazer_Wall_Contoller == null) { Creat_controller(new Vector3(0,0,1)); }
        else
        {
            Debug.Log(3);
            GameObject lazer_wall = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Lazer"));
            lazer_wall.transform.position = center;
            lazer_wall.GetComponent<Rigidbody2D>().AddForce(speed,ForceMode2D.Impulse);
            Lazerwall script = lazer_wall.GetComponent<Lazerwall>();
            script.Change_color(color);
            repository.lazer_Wall_Contoller.lazers.Add(script);
        }
    }
    public Lazer_wall_contoller Creat_controller(Vector3 spawn)
    {
        
        if ( repository.lazer_Wall_Contoller == null )
        {
            GameObject controller = new GameObject("Controller");
            controller.transform .position = spawn;
          
            repository.lazer_Wall_Contoller = controller.AddComponent<Lazer_wall_contoller>();
        }
        return repository.lazer_Wall_Contoller;
    }


    public void atack_sleepers(int count, Vector3 Spawn_position)
    {
        Scene_1.Now_atack = true;
        Lazer_wall_contoller contoller  = Creat_controller(Spawn_position);
        contoller.Set_params(count, Random.Range(3.2f,6f), Spawn_position, Random.Range(3,7));
    }
    public void atack_decreasing(Vector3 Spawn_position)
    {
        Scene_1.Now_atack = true;
        Lazer_wall_contoller contoller = Creat_controller(Spawn_position);
        contoller.decreasing_force = true;
        contoller.Set_params(Random.Range(6, 8), Random.Range(3.2f, 6f), Spawn_position, Random.Range(5, 8));
    }   
  
}