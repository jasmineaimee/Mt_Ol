using UnityEngine;

public class SimonSaysBtn : MonoBehaviour
{
    [Header("S I M O N  S A Y S  B T N")]
    [Header("Set In Inspector")]
    public int colourSoundNum; // the colour number for puzzle sequencing

    void OnTriggerEnter(Collider other)
    {
        // if the player has hit this with their hand send their answer to the puzzle, show the halo for half a second.
        if(other.gameObject.tag == "lHand" || other.gameObject.tag == "rHand")
        {
            SimonSaysPuzzle.Instance.UpdateAnswers(colourSoundNum);
            Component halo = gameObject.GetComponent("Halo");
            halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
            Invoke("HideHalo", 0.5f);
        }
    }

    private void HideHalo()
    {
        // hide halo
        Component halo = this.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
    }
}
