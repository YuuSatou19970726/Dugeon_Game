using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    int index = 0;
    Vector2 position;
    void Start()
    {
        foreach (GameObject gob in players)
        {
            gob.SetActive(false);
        }

        players[index].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AutoSpawnNextPlayer();
        }
    }

    void SpawnPlayer(int id)
    {
        GameObject player = Instantiate(players[id]);
    }
    void AutoSpawnNextPlayer()
    {

        players[index].SetActive(false);
        position = players[index].transform.position;
        index++;
        if (index >= players.Count) { index = 0; }
        players[index].transform.position = position;
        players[index].SetActive(true);

    }
}
