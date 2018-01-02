namespace Gone
{
    public interface IPlayer
    {
        string Name { get; set; }
        IStrategy Strategy { get; set; }
    }
}