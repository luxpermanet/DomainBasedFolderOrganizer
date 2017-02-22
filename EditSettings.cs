using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DomainBasedFolderOrganizer
{
    public partial class EditSettings : Form
    {
        public EditSettings()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            PopulateComboBoxes();
            LoadSavedSettings();

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private void PopulateComboBoxes()
        {
            PopulateComboIncomingFirstAction();
            PopulateComboIncomingSecondAction();
            PopulateComboOutgoingFirstAction();
        }

        private void PopulateComboIncomingFirstAction()
        {
            foreach (Enum enumVal in Enum.GetValues(typeof(IncomingFirstAction)))
            {
                cbIncomingFirstAction.Items.Add(enumVal.GetDescription<DescriptionAttribute>());
            }
        }

        private void PopulateComboIncomingSecondAction()
        {
            foreach (Enum enumVal in Enum.GetValues(typeof(IncomingSecondAction)))
            {
                cbIncomingSecondAction.Items.Add(enumVal.GetDescription<DescriptionAttribute>());
            }
        }

        private void PopulateComboOutgoingFirstAction()
        {
            foreach (Enum enumVal in Enum.GetValues(typeof(OutgoingFirstAction)))
            {
                cbOutgoingFirstAction.Items.Add(enumVal.GetDescription<DescriptionAttribute>());
            }
        }

        private void LoadSavedSettings()
        {
            LoadIncomingFirstAction();
            LoadIncomingSecondAction();
            LoadOutgoingFirstAction();
            LoadIncomingExceptions();
            LoadOutgoingExceptions();
            chkIncomingCreateParentFolders.Checked = Properties.Settings.Default.IncomingCreateParentFolders;
            chkOutgoingCreateParentFolders.Checked = Properties.Settings.Default.OutgoingCreateParentFolders;
        }

        private void LoadIncomingFirstAction()
        {
            IncomingFirstAction enumVal = IncomingFirstAction.DoNothing;
            if (!Enum.TryParse(Properties.Settings.Default.IncomingFirstAction, out enumVal))
            {
                enumVal = IncomingFirstAction.DoNothing;
            }

            cbIncomingFirstAction.SelectedIndex = (int)enumVal;
        }

        private void LoadIncomingSecondAction()
        {
            IncomingSecondAction enumVal = IncomingSecondAction.DoNothing;
            if (!Enum.TryParse(Properties.Settings.Default.IncomingSecondAction, out enumVal))
            {
                enumVal = IncomingSecondAction.DoNothing;
            }

            cbIncomingSecondAction.SelectedIndex = (int)enumVal;
        }

        private void LoadOutgoingFirstAction()
        {
            OutgoingFirstAction enumVal = OutgoingFirstAction.DoNothing;
            if (!Enum.TryParse(Properties.Settings.Default.OutgoingFirstAction, out enumVal))
            {
                enumVal = OutgoingFirstAction.DoNothing;
            }

            cbOutgoingFirstAction.SelectedIndex = (int)enumVal;
        }

        private void LoadIncomingExceptions()
        {
            foreach (var exception in Properties.Settings.Default.IncomingExceptions)
            {
                lbIncomingExceptions.Items.Add(exception);
            }
        }

        private void LoadOutgoingExceptions()
        {
            foreach (var exception in Properties.Settings.Default.OutgoingExceptions)
            {
                lbOutgoingExceptions.Items.Add(exception);
            }
        }

        private void cbIncomingFirstAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIncomingSecondAction.Enabled = cbIncomingFirstAction.SelectedIndex != 0;
        }

        private void btnAddIncomingException_Click(object sender, EventArgs e)
        {
            var exceptionCandid = txtIncomingException.Text;
            if (string.IsNullOrWhiteSpace(exceptionCandid))
            {
                MessageBox.Show("Enter a valid domain", "Domain Empty", MessageBoxButtons.OK);
                return;
            }
            if (lbIncomingExceptions.Items.Contains(exceptionCandid))
            {
                MessageBox.Show("Domain already exists", "Existent Domain", MessageBoxButtons.OK);
                return;
            }

            lbIncomingExceptions.Items.Insert(0, exceptionCandid);
        }

        private void btnAddOutgoingException_Click(object sender, EventArgs e)
        {
            var exceptionCandid = txtOutgoingException.Text;
            if (string.IsNullOrWhiteSpace(exceptionCandid))
            {
                MessageBox.Show("Enter a valid domain", "Domain Empty", MessageBoxButtons.OK);
                return;
            }
            if (lbOutgoingExceptions.Items.Contains(exceptionCandid))
            {
                MessageBox.Show("Domain already exists", "Existent Domain", MessageBoxButtons.OK);
                return;
            }

            lbOutgoingExceptions.Items.Insert(0, exceptionCandid);
        }

        private void btnRemoveIncomingException_Click(object sender, EventArgs e)
        {
            var selIndices = lbIncomingExceptions.SelectedIndices;
            foreach (int selIndex in selIndices)
            {
                lbIncomingExceptions.Items.RemoveAt(selIndex);
            }
        }

        private void btnRemoveOutgoingException_Click(object sender, EventArgs e)
        {
            var selIndices = lbOutgoingExceptions.SelectedIndices;
            foreach (int selIndex in selIndices)
            {
                lbOutgoingExceptions.Items.RemoveAt(selIndex);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.IncomingFirstAction = ((IncomingFirstAction)cbIncomingFirstAction.SelectedIndex).ToString();
            Properties.Settings.Default.IncomingSecondAction = ((IncomingSecondAction)cbIncomingSecondAction.SelectedIndex).ToString();
            Properties.Settings.Default.OutgoingFirstAction = ((OutgoingFirstAction)cbOutgoingFirstAction.SelectedIndex).ToString();

            Properties.Settings.Default.IncomingExceptions.Clear();
            foreach (var exception in lbIncomingExceptions.Items)
            {
                Properties.Settings.Default.IncomingExceptions.Add(exception as string);
            }

            Properties.Settings.Default.OutgoingExceptions.Clear();
            foreach (var exception in lbOutgoingExceptions.Items)
            {
                Properties.Settings.Default.OutgoingExceptions.Add(exception as string);
            }

            Properties.Settings.Default.IncomingCreateParentFolders = chkIncomingCreateParentFolders.Checked;
            Properties.Settings.Default.OutgoingCreateParentFolders = chkOutgoingCreateParentFolders.Checked;

            Properties.Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
        }
    }
}
