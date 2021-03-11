using UnityEngine;
public class Hades : Character
{
    [Header("Set in Inspector")]
    [Header("H A D E S")]
    public GameObject riverPuzzleLeft;
    public GameObject riverPuzzleRight;
    protected override void Start()
    {
        base.Start();
        if(InventoryManager.Instance.isInInventory(Collectables.Wovens))
        {
            missionActive = true;
            riddleSpot.SetActive(false);
            riverPuzzleLeft.SetActive(false);
            riverPuzzleLeft.SetActive(false);
        }
        missionStatement = "Welcome to the Underworld, Philotes. If you would like the Wovens that Zeus has asked for, you need to solve the puzzle by the river. Just Press A on the spot by the chest.";
        talkingPoints[0] = "Thank you for bringing my wife, Persephone, down here. It was getting quite dull, just me and the wisps.";
        talkingPoints[1] = "Zeus thought my being God of the Souls of the Dead would be a punishment. But Persphone and I quite like it.";
        talkingPoints[2] = "If Zeus is creating a human woman, that can only mean one thing. And it's not good.";
        talkingPoints[3] = "Persephone tells me zeus mentioned a box? That is disturbing news indeed.";
    }
}
