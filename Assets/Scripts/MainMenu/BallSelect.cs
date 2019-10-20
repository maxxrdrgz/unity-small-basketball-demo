using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSelect : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    // Start is called before the first frame update
    void Awake()
    {
        GetButtonsAndAddListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetButtonsAndAddListeners(){
        GameObject[] btns = GameObject.FindGameObjectsWithTag("MenuBall");

        for(int i =0; i < btns.Length; i++){
            buttons.Add(btns[i].GetComponent<Button>());
            buttons[i].onClick.AddListener(()=> SelectABall());
        }
    }

    public void SelectABall(){
        int index = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        // inform game manager to use selected ball
        if(GameManager.instance != null){
            GameManager.instance.SetBallIndex(index);
        }
    }
}
