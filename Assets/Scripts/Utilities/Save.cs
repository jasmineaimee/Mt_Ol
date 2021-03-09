using System.Collections.Generic;

[System.Serializable]
public class Save
{
    public List<Collectables> inventory = new List<Collectables>(); // player's collectables if any
    public int[,] puzzleStatus = new int[4,2]; // the answers player have given thus far.
    public bool hasSeenZeus = false;
}
