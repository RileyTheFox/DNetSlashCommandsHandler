using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;

namespace DNetSlashCommandsHandler.Commands
{
    public static class SlashCommandExtensions
    {

        /// <summary>
        /// Adds a subcommand to the SlashCommandBuilder, and inserts it into the Parent's Subcommands.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="subcommandHandler">The subcommand to add.</param>
        /// <returns>The modified SlashCommandBuilder.</returns>
        public static SlashCommandBuilder AddSubcommand(this SlashCommandBuilder builder, ISlashSubcommandHandler subcommandHandler)
        {
            // Retrieve the OptionBuilder from the subcommand and apply the SubCommand type.
            SlashCommandOptionBuilder optionBuilder = subcommandHandler.CommandProperties;
            optionBuilder.WithType(ApplicationCommandOptionType.SubCommand);

            if (subcommandHandler.Group != null)
                XConsole.WriteSuccessLine($"{subcommandHandler.ParentCommand.Name} - Added Subcommand: {subcommandHandler.Name} (Group: {subcommandHandler.Group.Name}");
            else XConsole.WriteSuccessLine($"{subcommandHandler.ParentCommand.Name} - Added Subcommand: {subcommandHandler.Name}");

            // Add to the parent subcommands.
            // This cast is a bit ugly, but this can only be called using a SlashCommandBuilder.
            // Which only exists within the ISlashCommandHandler and ParentCommand returns ISlashBaseCommand
            ((ISlashCommandHandler) subcommandHandler.ParentCommand).Subcommands.Add(subcommandHandler.Name, subcommandHandler);
            return builder.AddOption(optionBuilder);
        }

        /// <summary>
        /// Adds a Subcommand Group to the SlashCommandBuilder, and inserts it into the Parent's Subcommands.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="subcommandGroup">The subcommand group to add.</param>
        /// <returns>The modified SlashCommandBuilder.</returns>
        public static SlashCommandBuilder AddSubcommandGroup(this SlashCommandBuilder builder, ISlashSubcommandHandler subcommandGroup)
        {
            // Set to type SubCommandGroup here.
            SlashCommandOptionBuilder optionBuilder = subcommandGroup.CommandProperties;
            optionBuilder.WithType(ApplicationCommandOptionType.SubCommandGroup);

            XConsole.WriteSuccessLine($"{subcommandGroup.ParentCommand.Name} - Added Subcommand Group: {subcommandGroup.Name}");
            subcommandGroup.CommandProperties.Options.ForEach(option =>
            {
                XConsole.WriteSuccessLine($"{subcommandGroup.ParentCommand.Name} - {subcommandGroup.Name} - Added Subcommand: {option.Name} ");
            });

            // Adding to parent's subcommands. Same ugly casting but it works.
            ((ISlashCommandHandler) subcommandGroup.ParentCommand).Subcommands.Add(subcommandGroup.Name, subcommandGroup);
            return builder.AddOption(optionBuilder);
        }

        /// <summary>
        /// Adds a subcommand/group to the SlashCommandOptionBuilder, and inserts it into the specified Group's Subcommands.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="group">The group to add the subcommand to.</param>
        /// <param name="subcommandHandler">The subcommand to add.</param>
        /// <param name="type">The type of subcommand we're adding.</param>
        /// <returns>The modified SlashCommandOptionBuilder.</returns>
        public static SlashCommandOptionBuilder AddSubcommand(this SlashCommandOptionBuilder builder, ISlashCommandGroup group, ISlashSubcommandHandler subcommandHandler, ApplicationCommandOptionType type)
        {
            SlashCommandOptionBuilder optionBuilder = subcommandHandler.CommandProperties;
            optionBuilder.WithType(type);

            if (type == ApplicationCommandOptionType.SubCommandGroup)
            {
                subcommandHandler.CommandProperties.Options.ForEach(option =>
                {
                    XConsole.WriteSuccessLine($"{subcommandHandler.ParentCommand.Name} - {subcommandHandler.Group.Name} - Added Subcommand: {option.Name}");
                });
            }

            // The group must be specified so we don't have to do casting here.
            group.Subcommands.Add(subcommandHandler.Name, subcommandHandler);
            return builder.AddOption(optionBuilder);
        }

    }
}
