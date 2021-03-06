﻿using PublicationSystem.CommandProcessing;
using PublicationSystem.State;
using PublicationSystem.StylishPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSystem
{
    public class Publication
    {
        private PublicationState m_State = new Editing();
        private List<int> m_SubscriberIDs = new List<int>();
        private Delivery.IDeliveryMethod deliveryMethod = new Delivery.Courier();

        public Delivery.IDeliveryMethod PubDeliveryMethod
        {
            get
            {
                return deliveryMethod;
            }

            set
            {
                deliveryMethod = value;
            }
        }
        public List<int> SubsIDs
        {
            get
            {
                return m_SubscriberIDs;
            }
        }
        public string Name { get; set; }
        public DateTime EstablishDate { get; set; }
        public int RegisterationNumber { get; set; }
        public string HeadName { get; set; }

        public override string ToString()
        {
            string message = "Name: " + Name + "\n";
            message += "Date: " + EstablishDate.ToString() + "\n";
            message += "Hashcode: " + GetHashCode().ToString() + "\n";
            message += "Head: " + HeadName;
            return message;
        }

        public void SetState(PublicationState newState)
        {
            m_State = newState;
        }

        public void PrintInfo()
        {
            StylishPrinter.PrintLine(ToString());
        }

        public bool Execute(string command, string[] args)
        {
            return m_State.Execute(command, args, this);
        }

        public void Subscribe(int userID)
        {
            bool subsribedBefore = m_SubscriberIDs.Contains(userID);
            if (!subsribedBefore)
            {
                m_SubscriberIDs.Add(userID);
            }
        }

        public void Unsubcribe(int userID)
        {
            m_SubscriberIDs.Remove(userID);
        }

        public int GetUserID(int index)
        {
            return m_SubscriberIDs[index];
        }
    }
}
