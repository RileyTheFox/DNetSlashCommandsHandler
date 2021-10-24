using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace DNetSlashCommandsHandler.Commands
{
    /// <summary>
    /// The base of all SlashCommands, Subcommands and Subcommand Groups.
    /// </summary>
    public interface ISlashBaseCommand
    {

        /// <summary>
        /// The name of the Command (Required)
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The description of the Command (Required)
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The Parent to this Command. 
        /// Null if the implementing type is ISlashCommandHandler, as that is the root of the Command.
        /// </summary>
        public ISlashBaseCommand ParentCommand { get; }

        /// <summary>
        /// Handles the command when an Interaction is received for this command. 
        /// This should be defined async in the class implementation.
        /// </summary>
        /// <param name="command">The command that was executed in the Interaction.</param>
        /// <returns>A Task.</returns>
        public Task Handle(SocketSlashCommand command);

    }
}
