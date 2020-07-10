﻿using PublicationSystem.StylishPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSystem.CommandProcessing
{
    public class PrintingCommandExecuter : ICommandExecuter
    {
        private static PrintingCommandExecuter instance;

        public static PrintingCommandExecuter Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PrintingCommandExecuter();
                }

                return instance;
            }
        }


        private List<PrintingStyle> m_Styles = new List<PrintingStyle>();

        private const string AddCommand = "add";
        private const string RemoveCommand = "remove";

        public void AddStyleCommand(PrintingStyle style)
        {
            m_Styles.Add(style);
        }

        public void RemoveStyleCommand(PrintingStyle style)
        {
            m_Styles.Remove(style);
        }

        public bool Execute(string command, string[] args)
        {
            bool addExits = command.StartsWith(AddCommand);
            if (addExits)
            {
                command = command.Remove(0, AddCommand.Length);
                for(int i = 0; i < m_Styles.Count; i++)
                {
                    if(command == m_Styles[i].CommandSuffix)
                    {
                        StylishPrinter.AddStyle(m_Styles[i]);
                        return true;
                    }
                }
            }
            else
            {
                bool removeExists = command.StartsWith(RemoveCommand);
                if (removeExists)
                {
                    command = command.Remove(0, RemoveCommand.Length);
                    for (int i = 0; i < m_Styles.Count; i++)
                    {
                        if (command == m_Styles[i].CommandSuffix)
                        {
                            StylishPrinter.RemoveStyle(m_Styles[i]);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public List<string> GetCommandList()
        {
            List<string> commands = new List<string>(2);
            for(int i = 0; i < m_Styles.Count; i++)
            {
                commands.Add(AddCommand + m_Styles[i].CommandSuffix);
                commands.Add(RemoveCommand + m_Styles[i].CommandSuffix);
            }
            return commands;
        }
    }
}
