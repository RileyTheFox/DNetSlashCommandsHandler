# DNetSlashCommandsHandler

This library is designed to be a more organised, structured and simpler way of creating SlashCommands in the Discord.Net.Labs library.

Runs on .NET 5 and requires the [latest version](https://github.com/Discord-Net-Labs/Discord.Net-Labs) of the Discord.Net.Labs nuget package.

# Creating SlashCommands

To create a SlashCommand, create a class that implements the ISlashCommandHandler interface. Implement all the required Properties and Functions then fill in your command info as necessary.

Don't forget to create private fields for your Property getters, for example:

    private SlashCommandProperties _commandProperties;
    public SlashCommandProperties CommandProperties { get => _commandProperties; }

You must initialize the CommandProperties in the constructor of the class, giving the name, description and any options you would like to add, for example:

    public TestCommandHandler()
	{
	    _commandProperties = new SlashCommandBuilder()
	    .WithName(Name)
	    .WithDescription(Description)
	    .AddSubcommand(new TestSubcommandHandler(this))
	    .AddSubcommandGroup(new TestSubcommandGroup(this))
	    .Build();
	}

# Creating Subcommands

Subcommands are stored within a SlashCommand. They require a parent command and can optionally be part of a SlashCommand Group. The interface for a Subcommand is similar to that of a Slashcommand, but the constructor must have some parameters or else things will not work.

    public TestSubcommandHandler(ISlashCommandHandler _parentCommand, ISlashCommandGroup _group = null)
	{
	    ParentCommand = _parentCommand;
	    Group = _group;

	    _commandProperties = new SlashCommandOptionBuilder()
	    .WithName(Name)
	    .WithDescription(Description);
	}

### Subcommand Groups
Subcommand groups work in the same way as Subcommands (Groups derive from Subcommands), except these implement a Dictionary of Subcommands within that Group. 

All your files and folders are presented as a tree in the file explorer. You can switch from one to another by clicking a file in the tree.

# SlashCommand Extension Methods

DNet.Labs uses 2 different builders for slash commands, SlashCommandBuilder and SlashCommandOptionBuilder. The former is used only within the root SlashCommand, whereas the latter is used for Options within a SlashCommand (Subcommands, Subcommand Groups). 
I have made extension methods to these which let you easily create a Subcommand or Subcommand Group within a SlashCommand.
These are the `AddSubcommand()` and `AddSubcommandGroup()` functions.

# Auto Registering all SlashCommands.

In the `SlashCommandsHandler` class is the `GetSlashCommandsInNamespace()` function which will return all the classes that implement ISlashCommandHandler within a provided namespace.
For example, if your commands are located in the `MyBot.Commands` namespace, you would pass into this function the `MyBot.Commands` namespace, with the option to choose if you would like to recursively search for commands in namespaces within this namespace (such as, `MyBot.Commands.PingCommand`)

With the returned array of ISlashCommandHandlers, you can interate over this array and register them as either guild or global commands:

    foreach (ISlashCommandHandler command in GetSlashCommandsInNamespace("MyBot.Commands"))
	{
	    await testGuild.CreateApplicationCommandAsync(command.CommandProperties);
	    CommandHandlers.Add(command.Name, command);
	}
The CommandHandlers Dictionary is important, as it will be used to execute the command when a SlashCommandExecuted event is fired.

# Executing Commands
Command execution is simple and automatic for each command.
This is the SlashCommandExecuted event in DNet.Labs. Using the CommandHandler's Dictionary, you can lookup the command name and retrieve the handler instance, then execute the Handle function. 

    private async Task Client_SlashCommandExecuted(SocketSlashCommand arg)
	{
		await CommandHandlers[arg.CommandName].Handle(arg);
	}

### Executing Subcommands
In the SocketSlashCommand function is the Data field, and inside that is the Options field. Refer to [these docs](https://discord-net-labs.com/guides/interactions/application-commands/slash-commands/06-subcommands.html) to learn how to get subcommands in DNet.Labs. You will put the code for the subcommand in the parent command's Handle function (whether that be the root command, or a Subcommand Group)

This is a small example of executing subcommand's within a Subcommand Group, but also applies to ISlashCommandHandlers.

    public async Task Handle(SocketSlashCommand command)
	{
	    var fieldName = command.Data.Options.First().Options.First();
	    await Subcommands[fieldName.Name].Handle(command);
	}


# Help
If you require help, message me on Discord @ RileyTheFox#3621, or mention me in the Discord.Net.Labs discord when referring to this library