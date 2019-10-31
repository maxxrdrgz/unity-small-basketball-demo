using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private Sprite[] ballSprites;

    private float minX = -4.7f, maxX=8f, minY = -2.5f, maxY = 1.5f;

    private void Awake() {
        
    }

    /** 
        Creates the ball game object with the specificed image given by index.

        @params {int} index of color ball
    */
    public void CreatBall(int index){
        GameObject gameBall = Instantiate(
            ball, 
            new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0), 
            Quaternion.identity) as GameObject;
        gameBall.GetComponent<SpriteRenderer>().sprite = ballSprites[index];
    }
}
