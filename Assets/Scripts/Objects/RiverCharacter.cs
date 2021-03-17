using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverCharacter : MonoBehaviour
{
    public Vector3 leftPosition;
    public Vector3 rightPosition;
    public int character;

    void Start()
    {
        if(character == 0)
        {
            PuzzleManager.Instance.death = this;
        }
        else if(character == 1)
        {
            PuzzleManager.Instance.life = this;
        }
        else if(character == 2)
        {
            PuzzleManager.Instance.human = this;
        }
        else if(character == 3)
        {
            PuzzleManager.Instance.boat = this;
        }
    }
}
