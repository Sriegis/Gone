namespace Gone
{
    public interface IStrategy
    {
        Transaction Turn(MyCell[] myCells);
    }
}
