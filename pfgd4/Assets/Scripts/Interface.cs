using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    void LoadResources();
}

public interface IUserAction
{
    //get the kewborad
    void MoveBow(float offsetX, float offsetY);
    void Shoot();
    int GetScore();
    int GetTargetScore();
    int GetResidueNum();
    void Restart(); 
    bool GetGameover();  
    string GetWind();   
    void BeginGame();
}

public enum SSActionEventType : int { Started, Competeted }
public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, GameObject arrow = null);
}