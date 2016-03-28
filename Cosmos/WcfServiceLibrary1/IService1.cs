using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using CosmicAdventureDTO;

namespace Service1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Starship SendStarship(Starship starship, string systemName);

        [OperationContract]
        void InitializeGame();

        [OperationContract]
        SpaceSystem GetSystem();

        [OperationContract]
        Starship GetStarship(int money);

    }
}
