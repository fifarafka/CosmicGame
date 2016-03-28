using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CosmicAdventureDTO;

namespace Service1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        List<SpaceSystem> _systems = new List<SpaceSystem>();

        public Starship SendStarship(Starship starship, string systemName)
        {
            SpaceSystem system = FindStarShip(systemName);
            if (!system.Equals(null))
            {
                AddAgeFormula(starship, system);
                FindGold(starship, system);
            }
            else
                RemoveCrew(starship);
            return starship;
        }

        private void RemoveCrew(Starship starship)
        {
            foreach (Person p in starship.Crew)
            {
                starship.Crew.Remove(p);
            }
        }

        private void FindGold(Starship starship, SpaceSystem system)
        {
            if (starship.ShipPower >= system.MinShipPower)
            {
                starship.Gold = system.Gold;
                _systems.Remove(system);
            }
        }

        private void AddAgeFormula(Starship starship, SpaceSystem system)
        {
            int ShipPower = starship.ShipPower;
            if (ShipPower <= 20)
                AddAge(starship, system, 12);

            else if (ShipPower > 20 && ShipPower <= 30)
                AddAge(starship, system, 6);
            else if (ShipPower > 30)
                AddAge(starship, system, 4);
        }

        private void AddAge(Starship starship, SpaceSystem system, int intensity)
        {
            foreach (Person p in starship.Crew)
            {
                p.Age += (2 * system.BaseDistance) / intensity;
                if (p.Age > 90)
                {
                    starship.Crew.Remove(p);
                }
            }
        }

        private SpaceSystem FindStarShip(string Name)
        {
            foreach (SpaceSystem s in _systems)
            {
                if (s.Name.Equals(Name))
                    return s;
            }
            return null;
        }

        public void InitializeGame()
        {
            Random rnd1 = new Random();
            _systems.Add(new SpaceSystem("Space1", rnd1.Next(10, 40), rnd1.Next(20, 120), rnd1.Next(3000, 7000)));
            _systems.Add(new SpaceSystem("Space2", rnd1.Next(10, 40), rnd1.Next(20, 120), rnd1.Next(3000, 7000)));
            _systems.Add(new SpaceSystem("Space3", rnd1.Next(10, 40), rnd1.Next(20, 120), rnd1.Next(3000, 7000)));
            _systems.Add(new SpaceSystem("Space4", rnd1.Next(10, 40), rnd1.Next(20, 120), rnd1.Next(3000, 7000)));

        }

        public SpaceSystem GetSystem()
        {
            return _systems.First();
        }

        public Starship GetStarship(int money)
        {
            List<Person> Crew = new List<Person>();
            Crew.Add(new Person("Person1", "Yolo", 20));
            Crew.Add(new Person("Person2", "Yolo2", 20));
            Crew.Add(new Person("Person3", "Yolo3", 20));
            Crew.Add(new Person("Person4", "Yolo4", 20));
            return new Starship(Crew, 0, getShipPower(money));

        }

        private int getShipPower(int money)
        {
            Random rnd1 = new Random();
            int ShipPower = 0;
            if (money > 1000 && money <= 3000)
                ShipPower = rnd1.Next(10, 25);
            else if (money > 3001 && money <= 10000)
                ShipPower = rnd1.Next(20, 35);
            else if (money > 10000)
                ShipPower = rnd1.Next(35, 60);
            return ShipPower;
        }
    }
}
