using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;

namespace DNetSlashCommandsHandler.Commands
{
    public interface ISlashCommandGroup : ISlashSubcommandHandler
    {

        /// <summary>
        /// A Dictionary of Subcommands within this SlashCommandGroup.
        /// </summary>
        public Dictionary<string, ISlashSubcommandHandler> Subcommands { get; }

    }
}
