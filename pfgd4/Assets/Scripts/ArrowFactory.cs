using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour {

    public GameObject arrow = null;                             
    private List<GameObject> used = new List<GameObject>();     //the arrow be used
    private Queue<GameObject> free = new Queue<GameObject>();   
    public FirstSceneController sceneControler;                 
    


    public GameObject GetArrow()
    {
        if (free.Count == 0)
        {
            arrow = Instantiate(Resources.Load<GameObject>("Prefabs/arrow"));
        }
        else
        {
            arrow = free.Dequeue();
            //the arrow be shot
            if(arrow.tag == "hit")

            {
                arrow.GetComponent<Rigidbody>().isKinematic = false;
                //visible
                arrow.transform.GetChild(0).gameObject.SetActive(true);
                arrow.tag = "arrow";
            }

            arrow.gameObject.SetActive(true);

        }

        sceneControler = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;
        Transform temp = sceneControler.bow.transform.GetChild(2);
        

        arrow.transform.position = temp.transform.position;
        arrow.transform.parent = sceneControler.bow.transform;
        used.Add(arrow);
        return arrow;
    }

    //collect used arrow
    public void FreeArrow(GameObject arrow)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (arrow.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Enqueue(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}
