﻿using PublicationSystem.CommandProcessing;
using PublicationSystem.Info;
using PublicationSystem.StylishPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSystem 
{ 
    class Program
    {
        static void Main(string[] args)
        {
            //object creation
            CommandProcessor cp = CommandProcessor.Instance;
            PrintingCommandExecuter.Instance.AddStyleCommand(new StarStyle());
            PrintingCommandExecuter.Instance.AddStyleCommand(new QuestionStyle());
            PrintingCommandExecuter.Instance.AddStyleCommand(new UpperCaseStyle());
            PublicationCommandExecuter.Instance.AddPublicationBuilder(new SportBuilder());
            PublicationCommandExecuter.Instance.AddPublicationBuilder(new PoliticalBuilder());
            PublicationCommandExecuter.Instance.AddPublicationBuilder(new EconomicalBuilder());

            //command subscription
            cp.Subscribe(UserManager.Instance);
            cp.Subscribe(PublicationCommandExecuter.Instance);
            cp.Subscribe(PrintingCommandExecuter.Instance);

            while (true)
            {
                string command = Console.ReadLine();
                cp.ParseCommand(command);
            }
        }
    }
}
