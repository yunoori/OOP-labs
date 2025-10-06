namespace VendingMachine
{
    class Machine
    {
        public enum Role { Admin, User }

        private Role _role;

        private Dictionary<int, Item> items;

        public List<Coin> coinsGiven;

        private readonly int[] denominations = { 100, 50, 10, 5, 2, 1 };

        // Сумма всех заработанных монет не считая запаса который был изначально для сдачи
        private int sumOfAllCoinsEarned;

        // Сумма монет вставленных последним (действующим) пользователем
        private int sumOfLastCoinsInserted;

        public Machine(Item[] itemsGiven)
        {
            items = new Dictionary<int, Item>();
            foreach (var item in itemsGiven)
            {
                items.Add(item.Id, item);
            }
            coinsGiven = new List<Coin>();
            sumOfAllCoinsEarned = 0;
            _role = Role.User;
        }


        public void CheckItems()
        {
            foreach (var item in items)
            {
                System.Console.WriteLine("Name: {0}, Id: {1}, Price: {2} Amount: {3}", item.Value.Name, item.Key, item.Value.Price, item.Value.Amount);
            }
        }

        public void InsertCoin(Coin coin)
        {
            coinsGiven.Add(coin);
            sumOfAllCoinsEarned += coin.Denomination;
            sumOfLastCoinsInserted += coin.Denomination;

        }

        public void BuyItem(int itemId)
        {
            if (!items.ContainsKey(itemId))
            {
                System.Console.WriteLine("\nIncorrect Id, we don't have such item");
                return;
            }

            if (sumOfLastCoinsInserted < items[itemId].Price)
            {
                System.Console.WriteLine("\ninsufficient funds were deposited");
                return;
            }

            System.Console.WriteLine("Are you sure?(Yes/No)");
            string response = Console.ReadLine();
            if (response != "Yes")
            {
                if (response != "No") System.Console.WriteLine("Incorrect input");
                System.Console.WriteLine("Operation canceled");
                ReturnCoins(sumOfLastCoinsInserted);
                return;
            }
            sumOfLastCoinsInserted -= items[itemId].Price;
            System.Console.WriteLine("Take your {0}!", items[itemId].Name);

            items[itemId].Amount -= 1;

            if (items[itemId].Amount == 0) items.Remove(itemId);


            if (sumOfLastCoinsInserted != 0) GiveChange(sumOfLastCoinsInserted);
        }

        private void ReturnCoins(int coinsInserted)
        {
            Console.WriteLine("Take your coins:");
            foreach (var coin in coinsGiven)
            {
                Console.Write(coin.Denomination + " ");
            }
            coinsGiven.Clear();
            sumOfLastCoinsInserted = 0;
        }

        private void GiveChange(decimal change)
        {
            //чтобы всегда можно было вернуть монеты, есть некий бесконечный запас монет (в условии не сказано что может не быть монет для сдачи)
            System.Console.WriteLine("Here is your change: ");
            while (change > 0)
            {
                foreach (var coin in denominations)
                {
                    if (change >= coin)
                    {
                        System.Console.Write(coin + " ");
                        change -= coin;
                        break;
                    }
                }

            }
        }

        public void ChangeRoleToAdmin()
        {
            _role =  Role.Admin;
            System.Console.WriteLine("You are an admin now!");
        }

        public void ChangeRoleToUser()
        {
            _role = Role.User;
            System.Console.WriteLine("You are a user now!");
        }

        public void AddItem(Item item)
        {
            if (_role is not Role.Admin)
            {
                System.Console.WriteLine("Permission denied, you are not an admin");
                return;
            }
            if (items.ContainsKey(item.Id)) items[item.Id].Amount ++;
            else items[item.Id] = item;
            System.Console.WriteLine("You added an item successfully!");
        }

        public void TakingMoney()
        {
            if (_role != Role.Admin)
            {
                System.Console.WriteLine("Permission denied, you are not an admin");
                return;
            }
            System.Console.WriteLine("Take your money ({0})", sumOfAllCoinsEarned);
            sumOfAllCoinsEarned = 0;
        }

        public void GetRole()
        {
            System.Console.WriteLine(_role);
        }
    }
}