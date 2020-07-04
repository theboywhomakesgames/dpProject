﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSystem.Delivery
{
    class Courier : IDeliveryMethod
    {
        private static Courier instance;

        public static Courier Instance
        {
            get;
            set;
        }

        public void SpecificMessage(int id)
        {
            StylishPrinting.StylishPrinter.PrintLine("Courier: Character " + id);
        }
    }
}
