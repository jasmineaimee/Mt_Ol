using UnityEngine;

public class MazePuzzle : MonoBehaviour
{
    public static MazePuzzle Instance; // only want one MazePuzzle

    // TODO: mazeLocation, mazePrefabs, set hasLost if teleporting back from start
    // TODO: GameManager Teleport to maze via Teleport's place
    [Header("M A Z E  P U Z Z L E")]
    [Header("Set In Inspector")]
    public GameObject[] mazePrefabs; // all the maze prefabs from easy to hard
    public Vector3 mazeLocation; // This is where the maze is placed in the world
    public int difficulty = 0;
    
    [Header("Set Dynamically")]
    public bool hasLost = false; // if player quit the maze
    public bool hasWon = false; // if the player has won
    public Teleport toMaze; // the teleport that is taking you to the maze
    public Vector3 endOfMaze; // this is the end of the maze (probably a teleport here)
    public Vector3 startOfMaze; // this is the start of the maze (probably a teleport here)
    public Vector3 rotationOfMaze;
    public Vector3 returnFromMaze; // the position in the room to teleport the player back to
    public Vector3 rotationFromMaze;
    public Teleport endMaze; // the teleport that takes you back from the maze end
    public Teleport startMaze; // the teleport that takes you back from the maze start
    public bool inMaze = false; // if the player is in the maze

    // Private vars
    private int mazeNum = -1; // the number maze loaded
    private GameObject maze; // the maze


    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        // if player is playing maze, if they press B, teleport them back to start.
        if(inMaze)
        {
             if((Input.GetKeyDown(KeyCode.B) || OVRInput.Get(OVRInput.Touch.Two)))
            {
                GameManager.Instance.StartTeleport(startMaze.place);
                ResetMaze();
            }
        }
        if(hasWon)
        {
            toMaze.gameObject.SetActive(false);
            InventoryManager.Instance.roomCollectable.SetActive(true);
        }
    }

    public void SetMaze()
    {
        // set maze to random prefab with chosen difficulty.
        if(difficulty == 0)
        {
            mazeNum = Random.Range(0,3);
        }
        else if(difficulty == 1)
        {
            mazeNum = Random.Range(3,6);
        }
        else if(difficulty == 2)
        {
            mazeNum = Random.Range(6,9);
        }
        else
        {
            mazeNum = 0;
        }
        // set teleports/teleport locations/ instantiate maze
        //toMaze = teleport;
        maze = Instantiate(mazePrefabs[mazeNum], mazeLocation, Quaternion.identity);
        startMaze = maze.transform.Find("StartingTeleport").GetComponent<Teleport>();
        endMaze = maze.transform.Find("EndingTeleport").GetComponent<Teleport>();
        startOfMaze = startMaze.transform.position;
        startOfMaze.y += GameManager.Instance.playerStartY;
        rotationOfMaze = startMaze.transform.rotation.eulerAngles;
        endOfMaze = endMaze.transform.position;
        returnFromMaze = toMaze.transform.position;
        rotationFromMaze = toMaze.transform.rotation.eulerAngles;
        returnFromMaze.y = GameManager.Instance.playerStartY;
    }

    private void ResetMaze()
    {
        // reset all dynamic variables
        startMaze = null;
        endMaze = null;
        startOfMaze = Vector3.zero;
        rotationOfMaze = Vector3.zero;
        endOfMaze = Vector3.zero;
        toMaze.gameObject.SetActive(false);
        toMaze = null;
        returnFromMaze = Vector3.zero;
        rotationFromMaze = Vector3.zero;
        mazeNum = -1;
        inMaze = false;
        Destroy(maze);
    }
}
