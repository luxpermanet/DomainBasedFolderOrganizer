using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBasedFolderOrganizer
{
    public static class CurrentSettings
    {
        public static IncomingFirstAction IncomingFirstAction
        {
            get
            {
                IncomingFirstAction enumVal = IncomingFirstAction.DoNothing;
                if (!Enum.TryParse(Properties.Settings.Default.IncomingFirstAction, out enumVal))
                {
                    enumVal = IncomingFirstAction.DoNothing;
                }
                return enumVal;
            }
        }

        public static IncomingSecondAction IncomingSecondAction
        {
            get
            {
                IncomingSecondAction enumVal = IncomingSecondAction.DoNothing;
                if (!Enum.TryParse(Properties.Settings.Default.IncomingSecondAction, out enumVal))
                {
                    enumVal = IncomingSecondAction.DoNothing;
                }
                return enumVal;
            }
        }

        public static OutgoingFirstAction OutgoingFirstAction
        {
            get
            {
                OutgoingFirstAction enumVal = OutgoingFirstAction.DoNothing;
                if (!Enum.TryParse(Properties.Settings.Default.OutgoingFirstAction, out enumVal))
                {
                    enumVal = OutgoingFirstAction.DoNothing;
                }
                return enumVal;
            }
        }

        public static IEnumerable<string> IncomingExceptions
        {
            get
            {
                foreach (var exception in Properties.Settings.Default.IncomingExceptions)
                {
                    if (exception.StartsWith("@"))
                        yield return exception.Substring(1).ToLowerInvariant();
                    else
                        yield return exception.ToLowerInvariant();
                }
            }
        }

        public static IEnumerable<string> OutgoingExceptions
        {
            get
            {
                foreach (var exception in Properties.Settings.Default.OutgoingExceptions)
                {
                    if (exception.StartsWith("@"))
                        yield return exception.Substring(1).ToLowerInvariant();
                    else
                        yield return exception.ToLowerInvariant();
                }
            }
        }

        public static bool IncomingCreateParentFolders
        {
            get
            {
                return Properties.Settings.Default.IncomingCreateParentFolders;
            }
        }

        public static bool OutgoingCreateParentFolders
        {
            get
            {
                return Properties.Settings.Default.OutgoingCreateParentFolders;
            }
        }

        public static bool AddInEnabled
        {
            get
            {
                return Properties.Settings.Default.AddInEnabled;
            }
        }
    }
}
