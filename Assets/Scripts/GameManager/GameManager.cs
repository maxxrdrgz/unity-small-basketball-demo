using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private AudioSource audio;
    private float volume = 1.0f;
    [SerializeField]
    private AudioClip rim_hit1, rim_hit2, bounce1, bounce2, net_sound; 

    private int index = 0;
    private BallCreator ballCreator;
    private void Awake() {
        MakeSingleton();
        ballCreator = GetComponent<BallCreator>();
        audio = GetComponent<AudioSource>();
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

    public void PlaySound(int id){
        switch(id){
            case 1:
                audio.PlayOneShot(net_sound, volume);
                break;
            case 2:
                if(Random.Range(0, 2) > 1){
                    audio.PlayOneShot(rim_hit1, volume);
                }else{
                    audio.PlayOneShot(rim_hit2, volume);
                }
                break;
            case 3:
                if(Random.Range(0, 2) > 1){
                    audio.PlayOneShot(bounce1, volume);
                }else{
                    audio.PlayOneShot(bounce2, volume);
                }
                break;
            case 4:
                if(Random.Range(0, 2) > 1){
                    audio.PlayOneShot(bounce1, volume/2);
                }else{
                    audio.PlayOneShot(bounce2, volume/2);
                }
                break;
            case 5:
                if(Random.Range(0, 2) > 1){
                    audio.PlayOneShot(rim_hit1, volume/2);
                }else{
                    audio.PlayOneShot(rim_hit2, volume/2);
                }
                break;
        }
    }
}
