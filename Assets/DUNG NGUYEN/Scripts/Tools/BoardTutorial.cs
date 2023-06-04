using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTutorial : MonoBehaviour
{
    DataManager dataManager;

    [SerializeField]
    GameObject boardMove, boardJump, boardHoldJump;

    // Start is called before the first frame update
    void Start()
    {
        boardMove.SetActive(false);
        boardJump.SetActive(false);
        boardHoldJump.SetActive(false);

        dataManager = gameObject.AddComponent<DataManager>();
        Invoke("VisibleBoardMove", 1.45f);
    }

    void VisibleBoardMove()
    {
        if (dataManager.GetCheckPoint() == -1)
            boardMove.SetActive(true);
    }

    public void CheckTransform (Transform transform)
    {
        if (dataManager.GetCheckPoint() == -1)
        {
            if (transform.position.x >= -18f && transform.position.x <= -17f && boardMove.activeSelf && !boardJump.activeSelf)
            {
                boardMove.SetActive(false);
                boardJump.SetActive(true);
            }
            else if (transform.position.x >= -10f && transform.position.x <= -9f && boardJump.activeSelf && !boardHoldJump.activeSelf)
            {
                boardJump.SetActive(false);
                boardHoldJump.SetActive(true);
            }
        } else
        {
            boardHoldJump.SetActive(false);
        }
    }
}
