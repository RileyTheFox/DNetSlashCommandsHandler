using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace DNetSlashCommandsHandler.Commands
{
    public interface ISlashSubcommandHandler : ISlashBaseCommand
    {
        /// <summary>
        /// The Group this ISlashSubcommandHandler belongs to.
        /// Null if not part of a group.
        /// </summary>
        public ISlashCommandGroup Group { get; set; }

        /// <summary>
        /// The properties of this Subcommand. Contains Name, Description and available Options.
        /// </summary>
        public SlashCommandOptionBuilder CommandProperties { get; }

    }
}
