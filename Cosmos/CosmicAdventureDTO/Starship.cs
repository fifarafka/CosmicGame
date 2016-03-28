using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CosmicAdventureDTO
{
    [DataContract]
    public class Starship
    {
        [DataMember]
        public List<Person> Crew { get; set; }

        [DataMember]
        public int Gold { get; set; }

        [DataMember]
        public int ShipPower { get; set; }

        public String getStringCrew()
        {
            String crew = "";
            foreach (Person p in Crew)
            {
                crew = crew + p.Name + " " + p.Nick + " " + p.Age + ", ";
            }
            return crew;
        }

        public Starship(List<Person> Crew, int Gold, int ShipPower)
        {
            this.Crew = Crew;
            this.Gold = Gold;
            this.ShipPower = ShipPower;
        }
    }
}
