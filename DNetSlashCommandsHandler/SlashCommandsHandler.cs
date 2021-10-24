using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DNetSlashCommandsHandler.Commands;

namespace DNetSlashCommandsHandler
{
    public class SlashCommandsHandler
    {

        /// <summary>
        /// Returns all the ISlashCommandHandlers witin a namespace.
        /// </summary>
        /// <param name="_namespace">The namespace to search.</param>
        /// <param name="subNamespaces">Should namespaces within this namespace be searched.</param>
        /// <returns></returns>
        public static ISlashCommandHandler[] GetSlashCommandsInNamespace(string _namespace, bool subNamespaces = true)
        {
            List<ISlashCommandHandler> commands = new();

            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t =>
                {
                    Type cmdHnd = typeof(ISlashCommandHandler);

                    return (subNamespaces ? t.Namespace.StartsWith(_namespace) : t.Namespace == _namespace)
                    && t.IsAssignableTo(cmdHnd) && !string.Equals(t.Name, cmdHnd.Name);
                })
                .ToList()
                .ForEach(t =>
                {
                    // This is safe as the code above will only return classes that implement ISlashCommandHandler
                    commands.Add(Activator.CreateInstance(t) as ISlashCommandHandler);
                }); 

            return commands.ToArray();
        }

    }
}
