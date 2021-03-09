using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeHitBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        MazePuzzle.Instance.hasWon = true;
    }
}
