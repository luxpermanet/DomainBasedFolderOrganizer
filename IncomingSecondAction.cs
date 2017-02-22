using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBasedFolderOrganizer
{
    public enum IncomingSecondAction
    {
        [Description("Do Nothing")]
        DoNothing,
        [Description("Create search folder")]
        CreateSearchFolder,
        [Description("Create search folder and add to favorites")]
        CreateSearchFolderFavorite
    }
}
