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
    /** 
        Gets the dots gameobject, converts them to a list and stores them in 
        projectilesPath. Then their renderers are disabled.
    */
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
    /** 
        Checks if hit ground is true, if so then make th eball slowly fade away
        by changing the alpha of the renderer. Decrement total number of balls,
        check if any are left and then create the new ball.
    */
    void Update()
    {
        Aim();

        if(hit_ground){
            life -= Time.deltaTime;
            Color c = GetComponent<Renderer>().material.GetColor("_Color");
            GetComponent<Renderer>().material.SetColor("_Color", new Color(c.r, c.g, c.b, life));
            if(life < 0){
                if(GameManager.instance != null){
                    GameManager.instance.DecrementBalls();
                    if(GameManager.instance.GetBallsRemaining() > 0){
                        GameManager.instance.CreateBall();
                    }
                }
                Destroy(gameObject);
            }
        }
    }

    /**
        This function will detect when the mouse is being held down and
        dragged. This will show the dots. Once the mouse is released, and it is
        not in the dead zone or release zone, this funciton will add fource to 
        the gameobject this script is attached to.
    */
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

    /** 
        Returns a force vector for the ball to travel towards.

        @params {Vector3} position of the mouse input
        @return {Vector2} Returns a force vector for the ball to travel towards
    */
    Vector2 GetForce(Vector3 mouse){
        return (new Vector2(startPos.x, startPos.y) - new Vector2(mouse.x, mouse.y)) * power;
    }

    /** 
        Calculates if the mouse has dragged far enough past the dead_sense. Returns
        true or false depending on that.

        @params {Vector2} position of mouse input
        @return {bool} returns true if x and y of mouse input is less than 25
    */
    bool InDeadZone(Vector2 mouse){
        if(Mathf.Abs(startPos.x - mouse.x) <= dead_sense && Mathf.Abs(startPos.y - mouse.y) <= dead_sense){
            return true;
        }
        return false;
    }

    /**
        Checks if the mouse.x position is less than 70 pixels.

        @params {Vector2} position of mouse input
        @return {bool} returns true if x and y of mouse input is less than 25
    */
    bool InReleaseZone(Vector2 mouse){
        if(mouse.x <= 70){
            return true;
        }
        return false;
    }

    /** 
        This function will setup the dots. The PathPoint() function is used to
        calculate the position of the next dot. Then the dots are displayed.
    */
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

    /**
        Calculates a point in space affected by time, gravity, velocity and the
        initial position.

        @params {Vector2} initial position for gameobject
        @params {Vector2} initial velocity for gameobject
        @params {float} time

        @return {Vector2} position of a point in the plane affected by gravity,
        time and distance.
    */
    Vector2 PathPoint(Vector2 startP, Vector2 startVel, float t){
        return startP + startVel * t + 0.5f * Physics2D.gravity * t * t;
    }

    /** 
        Disables the renderer component on the dots.
    */
    void HidePath(){
        for(int i = 0; i < projectilesPath.Count; i++){
            projectilesPath[i].GetComponent<Renderer>().enabled = false;
        }
    }

    /** 
        Enables the renderer component on the dots.
    */
    void ShowPath(){
        for(int i = 0; i < projectilesPath.Count; i++){
            projectilesPath[i].GetComponent<Renderer>().enabled = true;
        }
    }

    /** 
        Checks for collision with the gameObject and the ground. Sets hit_ground.

        @params {Collision2D} The Collision2D data associated with this collision.
    */
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            hit_ground = true;
        }
    }
}
