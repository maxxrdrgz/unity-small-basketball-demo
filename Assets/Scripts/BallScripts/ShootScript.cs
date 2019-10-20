using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootScript : MonoBehaviour
{
    public float power = 2.0f;
    public float life = 1.0f;
    public float dead_sense = 25f;

    public int dots = 30;
    private Vector2 startPos;
    private bool shoot, aiming, hit_ground;

    [SerializeField]
    private GameObject Dots;
    private List<GameObject> projectilesPath;
    private Rigidbody2D rbody;
    private Collider2D myCollider;

    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Dots = GameObject.Find("dots");
        rbody.isKinematic = true;
        myCollider.enabled = false;
        startPos = transform.position;
        projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);

        for(int i = 0; i < projectilesPath.Count; i++){
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if(hit_ground){
            life -= Time.deltaTime;
            Color c = GetComponent<Renderer>().material.GetColor("_Color");
            GetComponent<Renderer>().material.SetColor("_Color", new Color(c.r, c.g, c.b, life));
            if(life < 0){
                if(GameManager.instance != null){
                    GameManager.instance.CreateBall();
                }
                Destroy(gameObject);
            }
        }
    }

    void Aim(){
        if(shoot){
            return;
        }
        if(Input.GetAxis("Fire1") == 1){
            if(!aiming){
                aiming = true;
                startPos = Input.mousePosition;
                CalculatePath();
                ShowPath();
            }else{
                CalculatePath();
            }
        } else if(aiming && !shoot){
            if(InDeadZone(Input.mousePosition) || InReleaseZone(Input.mousePosition)){
                aiming = false;
                HidePath();
                return;
            }
            rbody.isKinematic = false;
            myCollider.enabled = true;
            shoot = true;
            aiming = false;
            rbody.AddForce(GetForce(Input.mousePosition));
            HidePath();
        }
    }

    Vector2 GetForce(Vector3 mouse){
        return (new Vector2(startPos.x, startPos.y) - new Vector2(mouse.x, mouse.y)) * power;
    }

    bool InDeadZone(Vector2 mouse){
        if(Mathf.Abs(startPos.x - mouse.x) <= dead_sense && Mathf.Abs(startPos.y - mouse.y) <= dead_sense){
            return true;
        }
        return false;
    }

    bool InReleaseZone(Vector2 mouse){
        if(mouse.x <= 70){
            return true;
        }
        return false;
    }

    void CalculatePath(){
        Vector2 vel = GetForce(Input.mousePosition) * Time.fixedDeltaTime/rbody.mass;
        for(int i = 0; i < projectilesPath.Count; i++){
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
            float time = i / 30f;
            Vector3 point = PathPoint(transform.position, vel, time);
            point.z = 1.0f;
            projectilesPath[i].transform.position = point;
        }

    }

    Vector2 PathPoint(Vector2 startP, Vector2 startVel, float t){
        return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
    }

    void HidePath(){
        for(int i = 0; i < projectilesPath.Count; i++){
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    void ShowPath(){
        for(int i = 0; i < projectilesPath.Count; i++){
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            hit_ground = true;
        }
    }
}
