using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBasedFolderOrganizer
{
    public enum OutgoingFirstAction
    {
        [Description("Do Nothing")]
        DoNothing,
        [Description("Create sent folder")]
        CreateSentFolder,
        [Description("Create sent folder and rule")]
        CreateSentFolderRule
    }
}
