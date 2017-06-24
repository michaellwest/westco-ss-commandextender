using System.Collections.Generic;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Reflection;
using Sitecore.SharedSource.CommandExtender.Extensions;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.HtmlControls;

namespace Westco.SS.Foundation.CommandExtender.Commands
{
    public class ItemNew : Sitecore.Shell.Framework.Commands.ItemNew
    {
        protected virtual IEnumerable<Command> GetNewItemCommands()
        {
            var typeNames = new List<Command>();

            var xmlNode = Factory.GetConfigNode($"commands/command[@name='{Name}']/menucommands");
            if (xmlNode == null || !xmlNode.HasChildNodes) return typeNames;

            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                if (!childNode.Name.Is("menucommand")) continue;

                var typeName = childNode.Attributes?["type"].Value;
                if (string.IsNullOrEmpty(typeName)) continue;

                var command = ReflectionUtil.CreateObject(typeName);
                if (command is Sitecore.Shell.Framework.Commands.ItemNew)
                {
                    typeNames.Add((Sitecore.Shell.Framework.Commands.ItemNew)command);
                }
            }

            return typeNames;
        }

        public override Control[] GetSubmenuItems(CommandContext context)
        {
            var controls = base.GetSubmenuItems(context);

            if (controls == null || context.Items.Length != 1 || context.Items[0] == null)
            {
                return controls;
            }

            var menuItems = new List<Control>();

            var baseMenuItems = new HashSet<string>();
            foreach (var control in controls)
            {
                if (control is MenuItem)
                {
                    baseMenuItems.Add(((MenuItem)control).GetFormattedHeader());
                }
            }

            foreach (var command in GetNewItemCommands())
            {
                var submenuItems = command.GetSubmenuItems(context);
                if (submenuItems == null) continue;
                var addDivider = false;
                foreach (var submenuItem in submenuItems)
                {
                    var menu = submenuItem as MenuItem;
                    if (menu != null && !baseMenuItems.Contains(menu.GetFormattedHeader()))
                    {
                        addDivider = true;
                        menuItems.Add(submenuItem);
                    }
                }

                if (addDivider)
                {
                    menuItems.Add(new MenuDivider());
                }
            }

            menuItems.AddRange(controls);

            return menuItems.ToArray();
        }
    }
}