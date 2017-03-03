using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBasedFolderOrganizer
{
    public enum IncomingFirstAction
    {
        [Description("Do Nothing")]
        DoNothing,
        [Description("Create inbox folder and move")]
        CreateInboxFolderMove,
        [Description("Create inbox folder and rule")]
        CreateInboxFolderRule
    }
}
