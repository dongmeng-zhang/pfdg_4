using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlyActionManager : SSActionManager
{

    private ArrowFlyAction fly;                                //arrow flying
    public FirstSceneController scene_controller;              

    protected void Start()
    {
        scene_controller = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;

        scene_controller.action_manager = this;
    }
    
    public void ArrowFly(GameObject arrow,Vector3 wind)
    {
        fly = ArrowFlyAction.GetSSAction(wind);
        this.RunAction(arrow, fly, this);
    }
}
