using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AdaptiveAudioTrigger : MonoBehaviour {
    [Header("Set In Inspector")]
    [Header("A D A P T I V E  A U D I O  T R I G G E R")]
    public int triggerLevel; // what level area this is
    
    void OnDrawGizmosSelected()
    {
        // color box colliders in scene; not game tho
        Gizmos.color = GetGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, GetComponent<BoxCollider>().size);        
    }

    private Color GetGizmoColor()
    {
        switch (triggerLevel)
        {
            case 0:
            case 2:
                return Color.black;  //default is black

            case 1:
                return Color.green;

            case 3:
                return Color.yellow;

            case 4:
                return new Color(.4f, .1f, .6f); //purple

            case 5:
                return Color.magenta;

            case 6:
                return Color.blue;

            case 7:
                return Color.cyan;
        }
        return Color.black;
    }

    void OnTriggerEnter(Collider collider)
    {
        // change bg music when entering new area
        AdaptiveAudioManager.Instance.AdjustAudioLevel(triggerLevel);
    }



    void OnTriggerExit(Collider collider)
    {
        // if not in area, play default.
        AdaptiveAudioManager.Instance.AdjustAudioLevel(2);
    }    
}
