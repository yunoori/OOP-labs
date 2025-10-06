using System.Data.Common;

namespace VendingMachine;

class Item
{
    public Item(string name, int id, int price, int amount)
    {
        Name = name;
        Price = price;
        Amount = amount;
        Id = id;
    }

    public string Name { get; }

    public int Id { get; }

    public int Price { get; }
    
    public int Amount { get; set; }

}