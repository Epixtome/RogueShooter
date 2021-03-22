using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;/* openWhenEnemiesCleared;*/
    public GameObject[] doors;
    //public List<GameObject> enemies = new List<GameObject>();
    [HideInInspector]
    public bool roomActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if(enemies.Count > 0 && roomActive && openWhenEnemiesCleared)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if(enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                    closeWhenEntered = false;
                }
            }
        }*/
    }

    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            closeWhenEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            if (closeWhenEntered)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            roomActive = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
