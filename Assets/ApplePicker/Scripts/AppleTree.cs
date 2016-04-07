using UnityEngine;
using System.Collections;

public class AppleTree : MonoBehaviour {
    public GameObject applePrefab;
    public float speed = 1f;
    public float leftAndRightEdge;
    public float chanceToChangeDirections = 0.5f;
    public float secondsBetweenAppleDrops = 1f;
    public bool drop = false;
    // Use this for initialization
    void Start () {
        //Dropping apples every scond
        if (drop == true)
        {
            InvokeRepeating("DropApple", 2f, secondsBetweenAppleDrops);
        }
        Vector3 a = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)); //added for bounds; gets the camera tagged main. make sure its ortho
        leftAndRightEdge = a.x;
    }

    void DropApple() {
        GameObject apple = Instantiate(applePrefab) as GameObject;
        apple.transform.position = transform.position;
    }


	// Update is called once per frame
	void Update () {
        //Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        //Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge)//move right
        {
            speed = -Mathf.Abs(speed);//move left
        }
       
        
	}
    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;//Change Direction randomly
        }
    }

}
