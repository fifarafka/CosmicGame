using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CosmicAdventureDTO
{
    [DataContract]
    public class SpaceSystem
    {
        [DataMember]
        public String Name { get; set; }

        private int _MinShipPower;

        public int MinShipPower
        {
            get { return this._MinShipPower; }
            set { this._MinShipPower = value; }
        }

        [DataMember]
        public int BaseDistance { get; set; }

        private int _Gold;

        public int Gold
        {
            get { return this._Gold; }
            set { this._Gold = value; }
        }

        public SpaceSystem()
        {

        }

        public SpaceSystem(String Name, int MinShipPower, int BaseDistance, int Gold)
        {
            this.Name = Name;
            this.MinShipPower = MinShipPower;
            this.BaseDistance = BaseDistance;
            this.Gold = Gold;
        }
    }
}
