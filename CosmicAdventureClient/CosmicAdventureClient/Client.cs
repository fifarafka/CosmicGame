using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmicAdventureClient.ServiceReference1;
using CosmicAdventureClient.ServiceReference2;



namespace CosmicAdventureClient
{
    class Client
    {

        private ServiceReference1.Service1Client cosmicAdventureClient;
        private ServiceReference2.Service1Client imperiumServiceClient;

        private List<Starship> _starships;
        private bool _anySystem;
        private int _gold;
        private int _imperiumMoneyAskCount;

        public Client()
        {
            cosmicAdventureClient = new ServiceReference1.Service1Client();
            imperiumServiceClient = new ServiceReference2.Service1Client();
            _starships = new List<Starship>();
            _anySystem = true;
            _gold = 1000;
            _imperiumMoneyAskCount = 4;
            cosmicAdventureClient.InitializeGame();
        }

        public void Menu()
        {
            Console.WriteLine("Gold: " + _gold);
            Console.WriteLine("Requests of gold: " + _imperiumMoneyAskCount);
            Console.WriteLine();
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("a - ask imperium for money");
            Console.WriteLine("b - buy starship");
            Console.WriteLine("c - send starship to system");
            Console.WriteLine("d - finish");
        }

        public void ExecuteCommand(String command)
        {
            if (command.Length > 1)
                Console.WriteLine("Bad command. Try again.");
            else
            {
                int p = (int)command[0];
                switch (p)
                {
                    case 97:
                        Console.Clear();
                        AskImperiumForMoney();
                        break;
                    case 98:
                        Console.Clear();
                        BuyStarship();
                        break;
                    case 99:
                        Console.Clear();
                        SendStarshipToSystem();
                        break;
                    case 100:
                        Console.Clear();
                        Finish();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Bad command. Try again.");
                        break;
                }
            }
        }

        private void AskImperiumForMoney()
        {
            if (_imperiumMoneyAskCount > 0)
            {
                int _gold = imperiumServiceClient.GetMoneyFromImperium();
                this._gold += _gold;
                _imperiumMoneyAskCount--;
                Console.WriteLine("You got {0} gold from imperium", _gold);
            }
            else
                Console.WriteLine("Imperium won't give you money");
        }

        private void BuyStarship()
        {
            Console.WriteLine("Your gold: {0} How much do you want to spend on new starship? ", _gold);
            string howMuchString = Console.ReadLine();
            int howMuch;
            if (int.TryParse(howMuchString, out howMuch) && howMuch <= _gold)
            {
                //if (howMuch<=_gold)
                _starships.Add(cosmicAdventureClient.GetStarship(howMuch));
            }
            else
                Console.WriteLine("You didn't enter a number or you don't have enough money");
        }

        private void SendStarshipToSystem()
        {
            SpaceSystem system = cosmicAdventureClient.GetSystem();
            if (!system.Equals(null) && _starships.Count > 0)
            {
                Console.WriteLine("System " + system.Name + " distance " + system.BaseDistance);
                Console.WriteLine("Ready starships: {0}.", _starships.Count);
                Console.WriteLine("Choose starship (or press 'e' to exit)");
                ShowStarships();
                Console.WriteLine();
                string which = Console.ReadLine();
                ChosenStarship(which, system);
            }
            else
            {
                Console.WriteLine("No systems or starship count = 0");
                _anySystem = false;
            }
        }

        private void ChosenStarship(String which, SpaceSystem system)
        {
            int Whichship;
            if (!which.Equals("e"))
            {
                if (int.TryParse(which, out Whichship))
                {
                    if (Whichship > 0 && Whichship <= _starships.Count)
                    {
                        Starship s = _starships.ElementAt(Whichship - 1);
                        _starships.Remove(s);
                        Starship returnedShip = cosmicAdventureClient.SendStarship(s, system.Name);

                        if (returnedShip.Gold != 0)
                        {
                            Console.WriteLine("You got {0} gold.", returnedShip.Gold);
                            this._gold += returnedShip.Gold;
                            returnedShip.Gold = 0;
                        }

                        if (returnedShip.Crew.Count() > 0)
                            _starships.Add(returnedShip);
                    }
                    else Console.WriteLine("There is no starship with that number");
                }

                else Console.WriteLine("You should enter number value");
            }
        }

        private void ShowStarships()
        {
            int count = 0;
            foreach (Starship s in _starships )
            {
                count++;
                Console.WriteLine(count+". "+s.ShipPower+", "+ getStringCrew(s));
            }
        }

        private String getStringCrew(Starship s)
        {
            String crew = "";
            foreach (Person p in s.Crew)
            {
                crew = crew + p.Name + " " + p.Nick + " " + p.Age + ", ";
            }
            return crew;
        }

        private void Finish()
        {
            if (_anySystem)
                Console.WriteLine("You've lost :(");
            else
                Console.WriteLine("Congratulations! You have won a game.");
        }

        static void Main(string[] args)
        {
            Client c = new Client();
            while (true)
            {
                c.Menu();
                string command = Console.ReadLine();
                c.ExecuteCommand(command);
            }
        }
    }
}
