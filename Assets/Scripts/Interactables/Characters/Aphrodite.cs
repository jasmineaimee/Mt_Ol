public class Aphrodite : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        if(InventoryManager.Instance.isInInventory(InventoryManager.Instance.roomCollectable.GetComponent<Collectable>().type))
        {
            missionActive = true;
        }
        missionStatement = "Philotes, I heard that Zeus wants a symbol of grace. I'll give you mine, but I need you to complete this maze I've made. Just Press A on the teleportation spot by the chest to head to the maze.";
        talkingPoints[0] = "I have another maze for you to test if you wish, but I have nothing else to give you.";
        talkingPoints[1] = "Is it true that Zeus wants to create a human? Actually, don't answer that.\n\n I've made another maze for you to complete. Although, I have nothing else to give you.";
        talkingPoints[2] = "Why is Zeus making a human? Oh! Philotes! I didn't see you there.\n\n There's another maze for you to complete. Although, I have nothing else to give you.";
        talkingPoints[3] = "I have a bad feeling about this human thing. Oh! Philotes! I didn't see you there.\n\n There's another maze for you to complete. I have nothing else to give you, though.";
    }
}
