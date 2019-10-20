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

    public void PlayGame(){
        SceneManager.LoadScene("Gameplay");
    }

    public void SelectBall(){
        mainAnim.Play("FadeOut");
        ballAnim.Play("FadeIn");
    }

    public void BackToMenu(){
        mainAnim.Play("FadeIn");
        ballAnim.Play("FadeOut");
    }
}
