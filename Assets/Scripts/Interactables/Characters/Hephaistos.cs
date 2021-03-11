public class Hephaistos : Character
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        missionActive = true;
        talkingPoints[0] = "I have nothing to give you. Zeus has other plans for me.";
        talkingPoints[1] = "I have to get back to my Blacksmithing work.";
        talkingPoints[2] = "Athena, being the Goddess of Warfare, is my best customer. I make all of her shields and weapons.";
        talkingPoints[3] = "Sometimes Hermes steals my tools. The little Thief finds it funny.";
    }
}
