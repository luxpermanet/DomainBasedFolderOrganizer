namespace DomainBasedFolderOrganizer
{
    partial class EditSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbIncomingFirstAction = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIncomingCreateParentFolders = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtIncomingException = new System.Windows.Forms.TextBox();
            this.btnRemoveIncomingException = new System.Windows.Forms.Button();
            this.lbIncomingExceptions = new System.Windows.Forms.ListBox();
            this.btnAddIncomingException = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbIncomingSecondAction = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkOutgoingCreateParentFolders = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtOutgoingException = new System.Windows.Forms.TextBox();
            this.btnRemoveOutgoingException = new System.Windows.Forms.Button();
            this.lbOutgoingExceptions = new System.Windows.Forms.ListBox();
            this.btnAddOutgoingException = new System.Windows.Forms.Button();
            this.cbOutgoingFirstAction = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbIncomingFirstAction
            // 
            this.cbIncomingFirstAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIncomingFirstAction.FormattingEnabled = true;
            this.cbIncomingFirstAction.Location = new System.Drawing.Point(6, 19);
            this.cbIncomingFirstAction.Name = "cbIncomingFirstAction";
            this.cbIncomingFirstAction.Size = new System.Drawing.Size(336, 21);
            this.cbIncomingFirstAction.TabIndex = 0;
            this.cbIncomingFirstAction.SelectedIndexChanged += new System.EventHandler(this.cbIncomingFirstAction_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIncomingCreateParentFolders);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbIncomingSecondAction);
            this.groupBox1.Controls.Add(this.cbIncomingFirstAction);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 304);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Incoming Messages";
            // 
            // chkIncomingCreateParentFolders
            // 
            this.chkIncomingCreateParentFolders.AutoSize = true;
            this.chkIncomingCreateParentFolders.Location = new System.Drawing.Point(6, 96);
            this.chkIncomingCreateParentFolders.Name = "chkIncomingCreateParentFolders";
            this.chkIncomingCreateParentFolders.Size = new System.Drawing.Size(267, 17);
            this.chkIncomingCreateParentFolders.TabIndex = 9;
            this.chkIncomingCreateParentFolders.Text = "Create folders under a logical parent (A-B-C-D, etc.)";
            this.chkIncomingCreateParentFolders.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtIncomingException);
            this.groupBox3.Controls.Add(this.btnRemoveIncomingException);
            this.groupBox3.Controls.Add(this.lbIncomingExceptions);
            this.groupBox3.Controls.Add(this.btnAddIncomingException);
            this.groupBox3.Location = new System.Drawing.Point(6, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 175);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Exceptions";
            // 
            // txtIncomingException
            // 
            this.txtIncomingException.Location = new System.Drawing.Point(6, 19);
            this.txtIncomingException.Name = "txtIncomingException";
            this.txtIncomingException.Size = new System.Drawing.Size(243, 20);
            this.txtIncomingException.TabIndex = 4;
            // 
            // btnRemoveIncomingException
            // 
            this.btnRemoveIncomingException.Location = new System.Drawing.Point(255, 45);
            this.btnRemoveIncomingException.Name = "btnRemoveIncomingException";
            this.btnRemoveIncomingException.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveIncomingException.TabIndex = 7;
            this.btnRemoveIncomingException.Text = "Remove";
            this.btnRemoveIncomingException.UseVisualStyleBackColor = true;
            this.btnRemoveIncomingException.Click += new System.EventHandler(this.btnRemoveIncomingException_Click);
            // 
            // lbIncomingExceptions
            // 
            this.lbIncomingExceptions.FormattingEnabled = true;
            this.lbIncomingExceptions.Location = new System.Drawing.Point(6, 45);
            this.lbIncomingExceptions.Name = "lbIncomingExceptions";
            this.lbIncomingExceptions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbIncomingExceptions.Size = new System.Drawing.Size(243, 121);
            this.lbIncomingExceptions.TabIndex = 6;
            // 
            // btnAddIncomingException
            // 
            this.btnAddIncomingException.Location = new System.Drawing.Point(255, 19);
            this.btnAddIncomingException.Name = "btnAddIncomingException";
            this.btnAddIncomingException.Size = new System.Drawing.Size(75, 23);
            this.btnAddIncomingException.TabIndex = 5;
            this.btnAddIncomingException.Text = "Add";
            this.btnAddIncomingException.UseVisualStyleBackColor = true;
            this.btnAddIncomingException.Click += new System.EventHandler(this.btnAddIncomingException_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Then";
            // 
            // cbIncomingSecondAction
            // 
            this.cbIncomingSecondAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIncomingSecondAction.FormattingEnabled = true;
            this.cbIncomingSecondAction.Location = new System.Drawing.Point(6, 67);
            this.cbIncomingSecondAction.Name = "cbIncomingSecondAction";
            this.cbIncomingSecondAction.Size = new System.Drawing.Size(336, 21);
            this.cbIncomingSecondAction.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkOutgoingCreateParentFolders);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.cbOutgoingFirstAction);
            this.groupBox2.Location = new System.Drawing.Point(12, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 288);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Outgoing Messages";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Rules for outgoing emails may cause outlook to respond slower!";
            // 
            // chkOutgoingCreateParentFolders
            // 
            this.chkOutgoingCreateParentFolders.AutoSize = true;
            this.chkOutgoingCreateParentFolders.Location = new System.Drawing.Point(6, 79);
            this.chkOutgoingCreateParentFolders.Name = "chkOutgoingCreateParentFolders";
            this.chkOutgoingCreateParentFolders.Size = new System.Drawing.Size(267, 17);
            this.chkOutgoingCreateParentFolders.TabIndex = 10;
            this.chkOutgoingCreateParentFolders.Text = "Create folders under a logical parent (A-B-C-D, etc.)";
            this.chkOutgoingCreateParentFolders.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtOutgoingException);
            this.groupBox5.Controls.Add(this.btnRemoveOutgoingException);
            this.groupBox5.Controls.Add(this.lbOutgoingExceptions);
            this.groupBox5.Controls.Add(this.btnAddOutgoingException);
            this.groupBox5.Location = new System.Drawing.Point(6, 105);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(336, 175);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Exceptions";
            // 
            // txtOutgoingException
            // 
            this.txtOutgoingException.Location = new System.Drawing.Point(6, 19);
            this.txtOutgoingException.Name = "txtOutgoingException";
            this.txtOutgoingException.Size = new System.Drawing.Size(243, 20);
            this.txtOutgoingException.TabIndex = 4;
            // 
            // btnRemoveOutgoingException
            // 
            this.btnRemoveOutgoingException.Location = new System.Drawing.Point(255, 45);
            this.btnRemoveOutgoingException.Name = "btnRemoveOutgoingException";
            this.btnRemoveOutgoingException.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveOutgoingException.TabIndex = 7;
            this.btnRemoveOutgoingException.Text = "Remove";
            this.btnRemoveOutgoingException.UseVisualStyleBackColor = true;
            this.btnRemoveOutgoingException.Click += new System.EventHandler(this.btnRemoveOutgoingException_Click);
            // 
            // lbOutgoingExceptions
            // 
            this.lbOutgoingExceptions.FormattingEnabled = true;
            this.lbOutgoingExceptions.Location = new System.Drawing.Point(6, 45);
            this.lbOutgoingExceptions.Name = "lbOutgoingExceptions";
            this.lbOutgoingExceptions.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbOutgoingExceptions.Size = new System.Drawing.Size(243, 121);
            this.lbOutgoingExceptions.TabIndex = 6;
            // 
            // btnAddOutgoingException
            // 
            this.btnAddOutgoingException.Location = new System.Drawing.Point(255, 19);
            this.btnAddOutgoingException.Name = "btnAddOutgoingException";
            this.btnAddOutgoingException.Size = new System.Drawing.Size(75, 23);
            this.btnAddOutgoingException.TabIndex = 5;
            this.btnAddOutgoingException.Text = "Add";
            this.btnAddOutgoingException.UseVisualStyleBackColor = true;
            this.btnAddOutgoingException.Click += new System.EventHandler(this.btnAddOutgoingException_Click);
            // 
            // cbOutgoingFirstAction
            // 
            this.cbOutgoingFirstAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutgoingFirstAction.FormattingEnabled = true;
            this.cbOutgoingFirstAction.Location = new System.Drawing.Point(6, 48);
            this.cbOutgoingFirstAction.Name = "cbOutgoingFirstAction";
            this.cbOutgoingFirstAction.Size = new System.Drawing.Size(300, 21);
            this.cbOutgoingFirstAction.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(285, 616);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(204, 616);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 648);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "EditSettings";
            this.Text = "Domain-based Folder Organizer Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIncomingFirstAction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbIncomingSecondAction;
        private System.Windows.Forms.Button btnRemoveIncomingException;
        private System.Windows.Forms.ListBox lbIncomingExceptions;
        private System.Windows.Forms.Button btnAddIncomingException;
        private System.Windows.Forms.TextBox txtIncomingException;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbOutgoingFirstAction;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtOutgoingException;
        private System.Windows.Forms.Button btnRemoveOutgoingException;
        private System.Windows.Forms.ListBox lbOutgoingExceptions;
        private System.Windows.Forms.Button btnAddOutgoingException;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkIncomingCreateParentFolders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOutgoingCreateParentFolders;
    }
}