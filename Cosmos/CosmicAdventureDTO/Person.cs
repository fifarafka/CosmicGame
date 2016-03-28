using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CosmicAdventureDTO
{

    [DataContract]
    public class Person
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]

        public String Nick { get; set; }
        [DataMember]
        public float Age { get; set; }
        public Person(String Name, String Nick, float Age)
        {
            this.Name = Name;
            this.Nick = Nick;
            this.Age = Age;
        }
    }
}
