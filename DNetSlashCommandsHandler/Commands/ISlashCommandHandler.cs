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
    /// The root of all SlashCommands. Can contain Subcommands and SubcommandGroups
    /// </summary>
    public interface ISlashCommandHandler : ISlashBaseCommand
    {

        /// <summary>
        /// The properties of this SlashCommand. Contains Name, Description and available Options.
        /// </summary>
        public SlashCommandProperties CommandProperties { get; }

        /// <summary>
        /// A Dictionary of Subcommands within this SlashCommand.
        /// </summary>
        public Dictionary<string, ISlashSubcommandHandler> Subcommands { get; }

    }
}
