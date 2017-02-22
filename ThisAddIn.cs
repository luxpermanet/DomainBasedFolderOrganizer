using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace DomainBasedFolderOrganizer
{
    public partial class ThisAddIn
    {
        private Outlook.NameSpace defaultNamespace;
        private Outlook.Rules rules;
        private Outlook.MAPIFolder inbox;
        private Outlook.MAPIFolder sentbox;
        private Outlook.Folders searchFolders;

        public Outlook.NameSpace DefaultNamespace { get { return defaultNamespace; } }
        public Outlook.Rules RuleSet { get { return rules; } }
        public Outlook.MAPIFolder Inbox { get { return inbox; } }
        public Outlook.MAPIFolder Sentbox { get { return sentbox; } }
        public Outlook.Folders SearchFolders { get { return searchFolders; } }

        private const string IncomingRulePrefix = "dbfo-incoming-";
        private const string OutgoingRulePrefix = "dbfo-outgoing-";

        private const string SentboxFolderNamePrefix = "to ";

        private const string EventLogSource = "Outlook";
        
        private const string PR_SMTP_ADDRESS = "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";

        private static readonly Dictionary<char, string> ParentFolderMap = new Dictionary<char, string>()
        {
            { '0', "0..9" },
            { '1', "0..9" },
            { '2', "0..9" },
            { '3', "0..9" },
            { '4', "0..9" },
            { '5', "0..9" },
            { '6', "0..9" },
            { '7', "0..9" },
            { '8', "0..9" },
            { '9', "0..9" },
            { 'A', "A-B-C-D" },
            { 'B', "A-B-C-D" },
            { 'C', "A-B-C-D" },
            { 'D', "A-B-C-D" },
            { 'E', "E-F-G-H" },
            { 'F', "E-F-G-H" },
            { 'G', "E-F-G-H" },
            { 'H', "E-F-G-H" },
            { 'I', "I-J-K-L" },
            { 'J', "I-J-K-L" },
            { 'K', "I-J-K-L" },
            { 'L', "I-J-K-L" },
            { 'M', "M-N-O-P" },
            { 'N', "M-N-O-P" },
            { 'O', "M-N-O-P" },
            { 'P', "M-N-O-P" },
            { 'Q', "Q-R-S-T-U" },
            { 'R', "Q-R-S-T-U" },
            { 'S', "Q-R-S-T-U" },
            { 'T', "Q-R-S-T-U" },
            { 'U', "Q-R-S-T-U" },
            { 'V', "V-W-X-Y-Z" },
            { 'W', "V-W-X-Y-Z" },
            { 'X', "V-W-X-Y-Z" },
            { 'Y', "V-W-X-Y-Z" },
            { 'Z', "V-W-X-Y-Z" }
        };

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.AddInEnabled)
                {
                    EnableAddIn();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EventLogSource, ex.Message, EventLogEntryType.Error, 1);
                throw;
            }
        }

        private void EnableAddIn()
        {
            defaultNamespace = Application.GetNamespace("MAPI");
            rules = defaultNamespace.DefaultStore.GetRules();
            inbox = defaultNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            sentbox = defaultNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
            searchFolders = defaultNamespace.DefaultStore.GetSearchFolders();

            Application.NewMailEx += Application_NewMailEx;
            Application.ItemSend += Application_ItemSend;
            Application.AdvancedSearchComplete += Application_AdvancedSearchComplete;
        }

        private void Application_ItemSend(object Item, ref bool Cancel)
        {
            AfterSend(Item);
        }

        private void Application_NewMailEx(string EntryIDCollection)
        {
            var entryIDs = EntryIDCollection?.Split(',') ?? new string[] { };
            foreach (var entryID in entryIDs)
            {
                Outlook.MailItem mailItem = null;
                if (TryGetMailItem(entryID, out mailItem))
                {
                    AfterReceive(mailItem);
                }
            }
        }

        private bool TryGetMailItem(string entryID, out Outlook.MailItem mailItem)
        {
            try
            {
                mailItem = defaultNamespace.GetItemFromID(entryID, Inbox.StoreID) as Outlook.MailItem;
                return true;
            }
            catch // probably because it is not an email item
            {
                mailItem = null;
                return false;
            }
        }

        private void DisableAddIn()
        {
            Application.NewMailEx -= Application_NewMailEx;
            Application.ItemSend -= Application_ItemSend;
            Application.AdvancedSearchComplete -= Application_AdvancedSearchComplete;
            
            inbox = null;
            sentbox = null;
            rules = null;
            searchFolders = null;
            defaultNamespace = null;
        }

        private void AfterReceive(object Item)
        {
            try
            {
                AfterReceiveUnsafe(Item);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EventLogSource, ex.Message, EventLogEntryType.Error, 2);
            }
        }

        private void AfterReceiveUnsafe(object Item)
        {
            if (!CurrentSettings.AddInEnabled || CurrentSettings.IncomingFirstAction == IncomingFirstAction.DoNothing)
            {
                return;
            }

            Outlook.MailItem mailItem = Item as Outlook.MailItem;

            string senderAddress;
            if (!TryGetSenderAddress(mailItem, out senderAddress))
            {
                return;
            }

            string domain;
            if (!TryGetDomain(senderAddress, out domain))
            {
                return;
            }

            if (CurrentSettings.IncomingExceptions.Contains(domain))
            {
                return;
            }

            var ruleName = IncomingRulePrefix + domain;
            var initChar = char.ToUpper(domain.Take(1).First());
            var folderName = initChar.ToString() + domain.Substring(1);

            Outlook.MAPIFolder parentFolder = null;
            if (CurrentSettings.IncomingCreateParentFolders)
            {
                var parentFolderName = GetParentFolderName(initChar);
                if (!TryGetFolder(parentFolderName, Inbox, out parentFolder))
                {
                    parentFolder = CreateFolder(parentFolderName, Inbox);
                }
            }
            else
            {
                parentFolder = Inbox;
            }

            Outlook.MAPIFolder folder = null;
            if (!TryGetFolder(folderName, parentFolder, out folder))
            {
                folder = CreateFolder(folderName, parentFolder);
            }

            if (CurrentSettings.IncomingSecondAction != IncomingSecondAction.DoNothing)
            {
                RefreshSearchFolders();

                Outlook.MAPIFolder searchFolder = null;

                if (CurrentSettings.IncomingCreateParentFolders)
                {
                    searchFolder = parentFolder;
                }
                else
                {
                    searchFolder = folder;
                }

                if (!FolderExists(searchFolder.Name, SearchFolders))
                {
                    CreateSearchFolder(searchFolder);
                }
            }
            
            if (CurrentSettings.IncomingFirstAction == IncomingFirstAction.CreateInboxFolderRule)
            {
                RefreshRules();
                var ruleSet = RuleSet;
                if (!RuleExists(ruleSet, ruleName))
                {
                    CreateIncomingRule(ruleSet, ruleName, domain, folder, mailItem);
                    ruleSet.Save(false);
                }
            }
        }

        private void RefreshSearchFolders()
        {
            searchFolders = defaultNamespace.DefaultStore.GetSearchFolders();
        }

        private bool TryGetSenderAddress(Outlook.MailItem mailItem, out string senderAddress)
        {
            //var exchangeUser = mailItem?.Sender?.GetExchangeUser();
            //senderAddress = exchangeUser?.PrimarySmtpAddress ?? mailItem?.SenderEmailAddress;

            try
            {
                senderAddress = GetSenderAddress(mailItem);
            }
            catch
            {
                senderAddress = null;
            }
            
            return senderAddress != null;
        }

        private string GetSenderAddress(Outlook.MailItem mail)
        {
            if (mail == null)
            {
                return null;
            }

            if (mail.SenderEmailType == "EX")
            {
                Outlook.AddressEntry sender = mail.Sender;
                if (sender != null)
                {
                    //Now we have an AddressEntry representing the Sender
                    if (sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry || 
                        sender.AddressEntryUserType == Outlook.OlAddressEntryUserType.olExchangeRemoteUserAddressEntry)
                    {
                        //Use the ExchangeUser object PrimarySMTPAddress
                        Outlook.ExchangeUser exchUser =sender.GetExchangeUser();
                        if (exchUser != null)
                        {
                            return exchUser.PrimarySmtpAddress;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return sender.PropertyAccessor.GetProperty(PR_SMTP_ADDRESS) as string;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return mail.SenderEmailAddress;
            }
        }

        private bool TryGetDomain(string address, out string domain)
        {
            domain = null;
            if (!string.IsNullOrWhiteSpace(address) && address.IndexOf('@') >= 0 || address.Length > 1)
            {
                domain = address.Substring(address.IndexOf('@') + 1).ToLowerInvariant();
            }

            return domain != null;
        }

        private string GetParentFolderName(char initChar)
        {
            if (ParentFolderMap.ContainsKey(char.ToUpperInvariant(initChar)))
            {
                return ParentFolderMap[char.ToUpperInvariant(initChar)];
            }
            else
            {
                return ParentFolderMap['0'];
            }
        }

        private bool FolderExists(string folderName, Outlook.MAPIFolder parentFolder)
        {
            try
            {
                return FolderExists(folderName, parentFolder.Folders);
            }
            catch
            {
                // folder does not exist
            }
            return false;
        }

        private bool FolderExists(string folderName, Outlook.Folders foldersColl)
        {
            try
            {
                var foo = foldersColl[folderName] as Outlook.MAPIFolder;
                return true; // folder exists
            }
            catch
            {
                // folder does not exist
            }
            return false;
        }

        private Outlook.MAPIFolder CreateFolder(string folderName, Outlook.MAPIFolder parentFolder)
        {
            return parentFolder.Folders.Add(folderName) as Outlook.MAPIFolder;
        }

        private Outlook.MAPIFolder GetFolder(string folderName, Outlook.MAPIFolder parentFolder)
        {
            return parentFolder.Folders[folderName] as Outlook.MAPIFolder;
        }

        private bool TryGetFolder(string folderName, Outlook.MAPIFolder parentFolder, out Outlook.MAPIFolder folder)
        {
            try
            {
                folder = parentFolder.Folders[folderName] as Outlook.MAPIFolder;
                return true; // folder exists
            }
            catch
            {
                folder = null; // folder does not exist
                return false;
            }
        }

        private void CreateSearchFolder(Outlook.MAPIFolder folderToSearch)
        {
            Application.AdvancedSearch("\'" + folderToSearch.FolderPath + "\'", null, true, folderToSearch.Name);
        }

        private void Application_AdvancedSearchComplete(Outlook.Search SearchObject)
        {
            try
            {
                object searchFolder = SearchObject.Save(SearchObject.Tag);
                if (CurrentSettings.IncomingSecondAction == IncomingSecondAction.CreateSearchFolderFavorite)
                {
                    AddFolderToFavorites(searchFolder as Outlook.MAPIFolder);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EventLogSource, ex.Message, EventLogEntryType.Error, 3);
            }
        }

        private void AddFolderToFavorites(Outlook.MAPIFolder folder)
        {
            var pane = Application.ActiveExplorer().NavigationPane;
            var module = pane.Modules.GetNavigationModule(Outlook.OlNavigationModuleType.olModuleMail) as Outlook.MailModule;
            var navGroup = module.NavigationGroups.GetDefaultNavigationGroup(Outlook.OlGroupType.olFavoriteFoldersGroup);
            navGroup.NavigationFolders.Add(folder);
        }
        
        private bool RuleExists(Outlook.Rules ruleSet, string ruleName)
        {
            Outlook.Rule rule;
            try
            {
                rule = ruleSet[ruleName];
                return true; // rule exists
            }
            catch
            {
                // rule does not exist
            }
            return false;
        }

        private Outlook.Rule CreateIncomingRule(Outlook.Rules ruleSet, string ruleName, string domain, Outlook.MAPIFolder folder, Outlook.MailItem mailItem)
        {
            try
            {
                mailItem.Move(folder);
            }
            catch
            {
                // try to move, if it does not happen, ignore it
            }
            
            Outlook.Rule rule = ruleSet.Create(ruleName, Outlook.OlRuleType.olRuleReceive);

            // Rule Conditions
            // To condition
            rule.Conditions.SenderAddress.Address = new string[] { domain };
            rule.Conditions.SenderAddress.Enabled = true;

            // Rule Exceptions
            // nothing yet

            // Rule Actions
            rule.Actions.Stop.Enabled = true;
            rule.Actions.MoveToFolder.Folder = folder;
            rule.Actions.MoveToFolder.Enabled = true;

            rule.Enabled = true;

            return rule;
        }

        private void RefreshRules()
        {
            rules = defaultNamespace.DefaultStore.GetRules();
        }

        private void AfterSend(object Item)
        {
            try
            {
                AfterSendUnsafe(Item);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EventLogSource, ex.Message, EventLogEntryType.Error, 4);
            }
        }

        private void AfterSendUnsafe(object Item)
        {
            if (!CurrentSettings.AddInEnabled || CurrentSettings.OutgoingFirstAction == OutgoingFirstAction.DoNothing)
            {
                return;
            }

            Outlook.MailItem mailItem = Item as Outlook.MailItem;

            var domains = new HashSet<string>();
            foreach (Outlook.Recipient recipient in mailItem.Recipients)
            {
                string recipientAddress;
                if (!TryGetRecipientAddress(recipient, out recipientAddress))
                {
                    continue;
                }

                string domain;
                if (!TryGetDomain(recipientAddress, out domain))
                {
                    continue;
                }

                if (!domains.Contains(domain))
                {
                    domains.Add(domain);
                }
            }

            var domainsAllowed = domains.Except(CurrentSettings.OutgoingExceptions);
            
            Outlook.Rules ruleSet = null;
            var needsSave = false;

            if (domainsAllowed.Count() > 0 && CurrentSettings.OutgoingFirstAction == OutgoingFirstAction.CreateSentFolderRule)
            {
                RefreshRules();
                ruleSet = RuleSet;
            }

            foreach (var domain in domainsAllowed)
            {
                var ruleName = OutgoingRulePrefix + domain;
                var initChar = char.ToUpper(domain.Take(1).First());
                var folderName = SentboxFolderNamePrefix + initChar.ToString() + domain.Substring(1);

                Outlook.MAPIFolder parentFolder = null;
                if (CurrentSettings.OutgoingCreateParentFolders)
                {
                    var parentFolderName = GetParentFolderName(initChar);
                    if (!TryGetFolder(parentFolderName, Sentbox, out parentFolder))
                    {
                        parentFolder = CreateFolder(parentFolderName, Sentbox);
                    }
                }
                else
                {
                    parentFolder = Sentbox;
                }

                Outlook.MAPIFolder folder = null;
                if (!TryGetFolder(folderName, parentFolder, out folder))
                {
                    folder = CreateFolder(folderName, parentFolder);
                }

                if (CurrentSettings.OutgoingFirstAction == OutgoingFirstAction.CreateSentFolderRule)
                {
                    if (!RuleExists(ruleSet, ruleName))
                    {
                        CreateOutgoingRule(ruleSet, ruleName, domain, folder, mailItem);
                        needsSave = true;
                    }
                }
            }

            if (needsSave && ruleSet != null)
            {
                ruleSet.Save(false);
            }
        }

        private bool TryGetRecipientAddress(Outlook.Recipient recipient, out string recipientAddress)
        {
            try
            {
                recipientAddress = GetRecipientAddress(recipient);
            }
            catch
            {
                recipientAddress = null;
            }

            return recipientAddress != null;
        }

        private string GetRecipientAddress(Outlook.Recipient recipient)
        {
            Outlook.PropertyAccessor pa = recipient.PropertyAccessor;
            return pa.GetProperty(PR_SMTP_ADDRESS).ToString();
        }
        
        private Outlook.Rule CreateOutgoingRule(Outlook.Rules ruleSet, string ruleName, string domain, Outlook.MAPIFolder folder, Outlook.MailItem mailItem)
        {
            try
            {
                var copy = mailItem.Copy() as Outlook.MailItem;
                copy.Move(folder);
            }
            catch
            {
                // tried to move if it does not happen ignore
            }

            Outlook.Rule rule = ruleSet.Create(ruleName, Outlook.OlRuleType.olRuleSend);

            // Rule Conditions
            // To condition
            rule.Conditions.RecipientAddress.Address = new string[] { domain };
            rule.Conditions.RecipientAddress.Enabled = true;

            // Rule Exceptions
            // nothing yet

            // Rule Actions
            rule.Actions.CopyToFolder.Folder = folder;
            rule.Actions.CopyToFolder.Enabled = true;

            rule.Enabled = true;

            return rule;
        }

        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            var ribbon = new Ribbon();
            ribbon.OnEnableDisableAddIn += Ribbon_OnEnableDisableAddIn;
            ribbon.OnEditSettings += Ribbon_OnEditSettings;
            return ribbon;
        }

        private void Ribbon_OnEnableDisableAddIn(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.AddInEnabled)
                {
                    EnableAddIn();
                }
                else
                {
                    DisableAddIn();
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EventLogSource, ex.Message, EventLogEntryType.Error, 5);
            }
        }

        private void Ribbon_OnEditSettings(object sender, EventArgs e)
        {
            EditSettings settingsWindow = new EditSettings();
            IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
            Control mainWindow = Control.FromHandle(mainWindowHandle);
            settingsWindow.ShowDialog(mainWindow);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            try
            {
                DisableAddIn();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(EventLogSource, ex.Message, EventLogEntryType.Error, 6);
            }

            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}