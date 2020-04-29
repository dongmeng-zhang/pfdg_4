using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootController : MonoBehaviour, ISceneController, IUserAction
{
    public ShootAction actionManager { get; set; }
    public DiskFactory1 factory { get; set; }
    public GameObject Arrow;
    public GameObject Bow;
    public Emit3 EmitDisk;
    public int score = 0;
    public int round = 0;
    public int game = 0;
    public int num = 0;
    public int time = 0;
    public Text GameText;
    public Text FinalText;
    public Text RoundText;
    public Text ScoreText;
    public Text WindText;
    private string Horizontal = "";
    private string Vertical = "";
    public static float directionX;
    public static float directionY;
    public int CoolTimes = 3; //starting time
    void Awake()
    
    {
        SSDirector director = SSDirector.getInstance();
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();
    }
    // Use this for initialization
    void Start () {
        round = 1;
        directionX = Random.Range(-1, 1);
        directionY = Random.Range(-1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        num++;
        RoundText.text = "Round:" + round.ToString();
        ScoreText.text = "Score:" + score.ToString();
        if(directionX > 0)
        {
            Horizontal = "EAST";
        }
        else if(directionX <= 0)
        {
            Horizontal = "WEST";
        }
        if (directionY > 0)
        {
            Vertical = "NORTH";
        }
        else if(directionY <= 0)
        {
            Vertical = "SOUTH";
        }
        WindText.text = "WindDirection: " + Vertical + " " + Horizontal;
        if(time == 9)
        {
            if(score > 35 * round)
            {
                round++;
                time = 0;
                num = 0;
            }
            else
            {
                GameOver();
                game = 2;
            }
        }
        if (num == 300)
        {
            num = 0;
            directionX = Random.Range(-1, 2);
            directionY = Random.Range(-1, 2);
            Debug.Log("111");
        }
	}
    public void GameOver()
    {
        FinalText.text = "Game Over!!!";
    }
    public void StartGame()
    {
        num = 0;
        if (game == 0)
        {
            game = 3;
            StartCoroutine(waitForOneSecond());
        }
    }
    public IEnumerator waitForOneSecond()
    {
        while (CoolTimes >= 0 && game == 3)
        {
            GameText.text = CoolTimes.ToString();
            print("remine" + CoolTimes);
            yield return new WaitForSeconds(1);
            CoolTimes--;
        }
        GameText.text = "";
        game = 1;
    }
    public void ReStart()
    {
        SceneManager.LoadScene("task2");
        game = 0;
    }
    public void ShowDetail()
    {
        GUI.Label(new Rect(220, 50, 350, 250), "Press Enter to Emit a Arrow，10 Arrow for each round.Press up,down,left,right to change your position.Try to get Good Grade！！！");
    }
    public void hit()
    {
        if(game == 1)
        {
            actionManager.playArrow();
            time++;
        }
    }
    public void LoadResources()
    {
        Instantiate(Resources.Load("prefabs/Target"));
        Arrow =  Instantiate(Resources.Load("prefabs/Arrow01"))as GameObject;
        Bow = Instantiate(Resources.Load("prefabs/Bow01")) as GameObject;
        Arrow.transform.parent = Bow.transform;
        Arrow.transform.localEulerAngles = new Vector3(90, 0, -90);
    }
}
