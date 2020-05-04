using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    GUIStyle score_style = new GUIStyle();
    GUIStyle text_style = new GUIStyle();
    GUIStyle bold_style = new GUIStyle();
    GUIStyle over_style = new GUIStyle();
    private bool game_start = false;       //game begin

    // Use this for initialization
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(1, 0, 1, 1);
        score_style.fontSize = 16;
        bold_style.normal.textColor = new Color(1, 0, 0);
        bold_style.fontSize = 16;
        over_style.normal.textColor = new Color(1, 1, 1);
        over_style.fontSize = 25;
    }

    void Update()
    {
        if(game_start && !action.GetGameover())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                action.Shoot();
            }
            float translationY = Input.GetAxis("Vertical");
            float translationX = Input.GetAxis("Horizontal");
            //move bow
            action.MoveBow(translationX, translationY);
        }
    }
    private void OnGUI()
    {
        if(game_start)
        {
            if (!action.GetGameover())
            {
                GUI.Label(new Rect(10, 5, 200, 50), "Score:", text_style);
                GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), score_style);

                GUI.Label(new Rect(Screen.width / 2 - 30, 8, 200, 50), "Aim Score:", text_style);
                GUI.Label(new Rect(Screen.width / 2 + 50, 8, 200, 50), action.GetTargetScore().ToString(), score_style);

                GUI.Label(new Rect(Screen.width - 170, 5, 50, 50), "Arrow:", text_style);
                for (int i = 0; i < action.GetResidueNum(); i++)
                {
                    GUI.Label(new Rect(Screen.width - 110 + 10 * i, 5, 50, 50), "I ", bold_style);
                }
                GUI.Label(new Rect(Screen.width - 170, 30, 200, 50), "Wind: ", text_style);
                GUI.Label(new Rect(Screen.width - 110, 30, 200, 50), action.GetWind(), text_style);
            }

            if (action.GetGameover())
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "GAME OVER", over_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "RESTRAT"))
                {
                    action.Restart();
                    return;
                }
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.width / 2 - 320, 100, 100), "Shoot the target!", over_style);
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.width / 2 - 220, 400, 100), "WSAD or arrow keys to move bow, left click to shoot", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "STRAT"))
            {
                game_start = true;
                action.BeginGame();
            }
        }
    }
}
