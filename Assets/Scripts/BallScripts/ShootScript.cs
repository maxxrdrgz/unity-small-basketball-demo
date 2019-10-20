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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
