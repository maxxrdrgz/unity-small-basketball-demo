using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private int index = 0;
    private BallCreator ballCreator;
    private void Awake() {
        MakeSingleton();
        ballCreator = GetComponent<BallCreator>();
    }

    void MakeSingleton(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetBallIndex(int index){
        this.index = index;
    }

    public void CreateBall(){
        ballCreator.CreatBall(index);
    }

    void OnLevelWasLoaded(){
        if(SceneManager.GetActiveScene().name == "Gameplay"){
            CreateBall();
        }
    }
}
