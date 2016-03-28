using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ImperiumService
{
    public class Service1 : IService1
    {
        public int GetMoneyFromImperium()
        {
            return new Random().Next(3000, 5000);
        }
    }
}