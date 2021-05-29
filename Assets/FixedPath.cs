using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPath : MonoBehaviour
{
    public GameObject controller; //是否固定路径

    private Vector3 origin;
    public Transform target; //固定路径时的起始位置与末端位置

    private GameObject player; //玩家

    private bool playerIn;

    public float pathCoefficient; //轨迹系数，控制轨迹
    public float cruiseTime; //飞行时间
    // Start is called before the first frame update
    void Start()
    {
        playerIn = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (!controller.GetComponent<Steam>().fixedPath)
        {
            Destroy(this); //正常模式
        }
        else
        {
            //路径固定模式
            Destroy(gameObject.GetComponent<AreaEffector2D>());
        }
    }

    float GetY(float x) //轨迹方程
    {
        float x1 = origin.x;
        float x2 = target.transform.position.x;
        float y1 = origin.y;
        float y2 = target.transform.position.y;

        float b = (y1 - y2 - pathCoefficient * (x1 * x1 - x2 * x2)) / (x1 - x2);
        float c = (y1 - pathCoefficient * x1 * x1 - b * x1);


        return (pathCoefficient * x * x + b * x + c);
    }


    float GetDY(float x) //轨迹方程的一阶导数，使速度从kinematic到dynamic后连续
    {
        float x1 = origin.x;
        float x2 = target.transform.position.x;
        float y1 = origin.y;
        float y2 = target.transform.position.y;

        float b = (y1 - y2 - pathCoefficient * (x1 * x1 - x2 * x2)) / (x1 - x2);


        return (2*pathCoefficient*x + b);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        origin = player.transform.position;
        if(other.gameObject.tag == "Player")
        {
            player.transform.rotation = new Quaternion(0, 0, 0, 0);//初始旋转角
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;//质心速度清零
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;//角速度清零
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            StartCoroutine("StartMove");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isPlayerIn()
    {
        return playerIn;
    }

    IEnumerator StartMove()
    {
        player.GetComponent<LabMov>().enabled = false;
        playerIn = true;
        for (float schedule = 0; schedule <= cruiseTime; schedule = schedule + Time.deltaTime)
        {
            player.transform.position = new Vector3(origin.x + (target.position.x - origin.x) * schedule / cruiseTime, GetY(origin.x + (target.position.x - origin.x) * schedule / cruiseTime));
            yield return 0;
        }
        player.transform.position = target.position;
        player.GetComponent<Rigidbody2D>().velocity = new Vector3((target.transform.position.x - origin.x) / cruiseTime, GetDY(player.transform.position.x) * (target.transform.position.x - origin.x) / cruiseTime);
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        playerIn = false;
        player.GetComponent<LabMov>().enabled = true;
        this.gameObject.SetActive(false);
        yield break;
    }
}
