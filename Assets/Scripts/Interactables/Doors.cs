using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Header("D O O R S")]
    public int toRoomNum;
    public Vector3 playerLoadLocation = new Vector3(0f,0f,0f);
    public Vector3 playerLoadRotation = new Vector3(0f,0f,0f);
    [Header("Set Dynamically")]
    public bool isColliding = false;

    // Private Vars
    

    void Start()
    {
        GameManager.Instance.doors.Add(this);
        playerLoadLocation.y = GameManager.Instance.playerStartY;
    }

    void Update()
    {
        if(isColliding)
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.5f || OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.5f)
            {
                GameManager.Instance.ChangeSceneTo(toRoomNum, playerLoadLocation, playerLoadRotation);
                isColliding = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "rHand" || other.tag == "lHand")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "rHand" || other.tag == "lHand")
        {
            isColliding = false;
        }
    }
}
