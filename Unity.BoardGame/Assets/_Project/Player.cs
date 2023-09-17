namespace Assets._Project
{
    public class Player
    {
        public int SequencePosition { get; }
        public string Name { get; }
        public string CharacterID { get; }

        public Player(int sequencePosition, string name, string characterID)
        {
            SequencePosition = sequencePosition;
            Name = name;
            CharacterID = characterID;
        }
    }
}
