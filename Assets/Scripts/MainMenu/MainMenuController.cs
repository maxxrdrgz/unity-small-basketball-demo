using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private Animator mainAnim, ballAnim;
    [SerializeField]
    private GameObject mainHolder, ballHolder; 

    private void Awake() {
        mainAnim = mainHolder.GetComponent<Animator>();
        ballAnim = ballHolder.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /** 
        Once scene is loaded, it will reset the number of balls remaining.
    */
    public void PlayGame(){
        SceneManager.LoadScene("Gameplay");
        if(GameManager.instance != null){
            GameManager.instance.ResetBallsRemaining();
        }
    }

    /** 
        Plays the animations that display the ball select panel and hides the
        main menu panel
    */
    public void SelectBall(){
        mainAnim.Play("FadeOut");
        ballAnim.Play("FadeIn");
    }

    /** 
        Plays the animations that display the main menu panel and hides the
        ball select panel.
    */
    public void BackToMenu(){
        mainAnim.Play("FadeIn");
        ballAnim.Play("FadeOut");
    }
}
