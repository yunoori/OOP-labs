namespace VendingMachine;

class Program
{
    public static void Main()
    {
        var cola = new Item("cola", 1, 100, 1);
        var cookie = new Item("cookie", 2, 50, 7);
        var kitkat = new Item("kitkat", 3, 70, 5);
        var machine = new Machine([cola, cookie, kitkat]);


        machine.InsertCoin(new Coin(100));
        machine.InsertCoin(new Coin(100));

        machine.CheckItems();

        machine.BuyItem(1);

        machine.BuyItem(1);
    }
}