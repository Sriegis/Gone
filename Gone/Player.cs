namespace Gone
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public IStrategy Strategy { get; set; }
    }
}
