using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance; // only want one Player

    //[Header("Set In Inspector")]
    //[Header("P L A Y E R")]
    //[Header("Set Dynamically")]
    

    void Awake()
    {
        GameManager.Instance.ovrPlayer = this.gameObject;
        GameManager.Instance.cameraRig = this.transform.Find("OVRCameraRig").gameObject;
        GameManager.Instance.StartAt();
        if(GameManager.Instance.playerInRoom == 10)
        {
            InventoryManager.Instance.SetInventoryRoom();
            if(GameManager.Instance.numCollectables >= 6)
            {
                InventoryManager.Instance.SetWinCondition();
            }
        }
    }
}
