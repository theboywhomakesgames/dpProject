﻿using PublicationSystem.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationSystem.CommandProcessing
{
    public class PublicationCommandExecuter : ICommandExecuter
    {
        private static PublicationCommandExecuter instance;

        public static PublicationCommandExecuter Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PublicationCommandExecuter();
                }

                return instance;
            }
        }

        private const string PrintInfoCommand = "printInfo";
        private const string CreateModelCommand = "createModel";

        private List<PublicationBuilder> m_Builders = new List<PublicationBuilder>();

        public void AddPublicationBuilder(PublicationBuilder builder)
        {
            m_Builders.Add(builder);
        }

        public void RemovePublicationBuilder(PublicationBuilder builder)
        {
            m_Builders.Remove(builder);
        }

        public bool Execute(string command, string[] args)
        {
            if(command == PrintInfoCommand)
            {
                PublicationProvider.Instance.PrintInfo();
                return true;
            }
            else if (args != null && args.Length > 0)
            {
                if (command == CreateModelCommand)
                {
                    return CreatePublication(args[0]);
                }
                else
                {
                    Publication publication = GetPublication(args[0]);
                    if (publication != null)
                    {
                        return publication.Execute(command, args);
                    }
                }               
            }

            return false;
        }

        private bool CreatePublication(string publicationName)
        {
            for (int i = 0; i < m_Builders.Count; i++)
            {
                if (publicationName == m_Builders[i].PublicationCommandName)
                {
                    PublicationProvider.Instance.CreatePublication(m_Builders[i]);
                    return true;
                }
            }

            return false;
        }

        private Publication GetPublication(string publicationName)
        {
            try
            {
                return PublicationProvider.Instance.GetPublication(publicationName);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        public List<string> GetCommandList()
        {
            List<string> commands = new List<string>(2);
            commands.Add(PrintInfoCommand);
            commands.Add(CreateModelCommand);
            AddStateCommands(commands, new Editing());
            AddStateCommands(commands, new Printing());
            AddStateCommands(commands, new Publishing());
            return commands;
        }

        private void AddStateCommands(List<string> commands, PublicationState state)
        {
            string[] stateCommands = state.GetCommandsList();
            for(int i = 0; i < stateCommands.Length; i++)
            {
                commands.Add(stateCommands[i]);
            }
        }
    }
}
