﻿using PublicationSystem.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSystem.Delivery
{
    class Online : IDeliveryMethod
    {
        public static Online Instance { get; private set; } = new Online();

        public void SpecificMessage(int id)
        {
            DeliveryUtility.DeliveryPrint(id, "Online");
        }

        public void SpecificMessage(PersonalInformation info, int id)
        {
            DeliveryUtility.DeliveryPrint(info, id, "Online");
        }
    }
}
