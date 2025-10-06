namespace VendingMachine;

struct Coin
{
    public int Denomination { get; }

    public Coin(int denomination)
    {
        // В условии не указан максимальный номинал монеты, у меня будет 100
        if (denomination is 1 or 2 or 5 or 10 or 50 or 100) Denomination = denomination;

        else throw new ArgumentException("invalid denomination");
    }
}