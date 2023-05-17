using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject boardMove, boardJump, boardHoldJump;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("VisibleBoardMove", 1.45f);
    }

    private void Update()
    {
        
    }

    void VisibleBoardMove()
    {
        boardMove.SetActive(true);
    }

    public void CheckTransform (Transform transform)
    {
        if (transform.position.x >= -18f && transform.position.x <= -17f && !boardJump.activeSelf)
        {
            boardMove.SetActive(false);
            boardJump.SetActive(true);
        } else if (transform.position.x >= -10f && transform.position.x <= -9f && !boardHoldJump.activeSelf)
        {
            boardJump.SetActive(false);
            boardHoldJump.SetActive(true);
        }
    }
}
