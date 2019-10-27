﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private int touchedGround = 0;
    private bool touchedRim;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Rim"){
            touchedRim = true;
            if(GameManager.instance != null){
                if(Random.Range(0, 2) > 1){
                    GameManager.instance.PlaySound(2);
                }else{
                    GameManager.instance.PlaySound(5);
                }
            }
        }
        if(other.gameObject.tag == "Holder"){
            if(GameManager.instance != null){
                if(Random.Range(0, 2) > 1){
                    GameManager.instance.PlaySound(3);
                }else{
                    GameManager.instance.PlaySound(4);
                }
            }
        }
        if(other.gameObject.tag == "Ground"){
            touchedGround++;
            if(touchedGround <= 3){
                if(GameManager.instance != null){
                    if(Random.Range(0, 2) > 1){
                        GameManager.instance.PlaySound(3);
                    }else{
                        GameManager.instance.PlaySound(4);
                    }
                }
            }
        }
        if(other.gameObject.tag == "Backboard"){
            touchedRim = true;
            if(GameManager.instance != null){
                if(Random.Range(0, 2) > 1){
                    GameManager.instance.PlaySound(2);
                }else{
                    GameManager.instance.PlaySound(5);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Net"){
            if(touchedRim){
                GameManager.instance.IncrementBalls(1);
            }else{
                GameManager.instance.IncrementBalls(2);
            }

            if(GameManager.instance != null){
                GameManager.instance.PlaySound(1);
            }
        }
    }
}