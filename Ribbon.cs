using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


namespace DomainBasedFolderOrganizer
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        private int extrospectiveButtonClickCount = 0;
        private Dictionary<int, string> extrospectiveButtonMessages = new Dictionary<int, string>()
        {
            { 0, "Apparently you will not stop hitting that button but I am done with my warnings and I dunno what else I can do to stop you hitting that darn button :(" },
            { 1, "It says don't click. I will keep it as a secret for this time but don't click again, deal?" },
            { 2, "C'mon I thought we had a deal. You won't do that again, will you?" },
            { 3, "And you did it again, applauses to you >:(" },
            { 4, "No more games dude!" },
            { 5, "Bona sera, bona sera you don't even call me godfather. -Godfather" },
            { 6, "Frankly, my dear, I don't give a dam. -Gone With the Wind" },
            { 8, "I am gonna make you an offer you can't refuse -Godfather" },
            { 9, "Go ahead, make my day. -Sudden Impact" },
            { 10, "You talking to me? -Taxi Driver" },
            { 11, "What we-ve got here is failure to communicate. -Cool Hand Luke" },
            { 12, "I'm as mad as hell, and I'm not going to take this anymore! -Network" },
            { 13, "You can't handle the truth! -A Few Good Men" },
            { 14, "" },
            { 15, "" },
            { 16, "" }
        };

        public event EventHandler OnEnableDisableAddIn;
        public event EventHandler OnEditSettings;

        public Ribbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("DomainBasedFolderOrganizer.Ribbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226
        
        public string GetLabelEnableDisableButton(Office.IRibbonControl control)
        {
            if (Properties.Settings.Default.AddInEnabled)
            {
                return "Disable Add-In";
            }
            else
            {
                return "Enable Add-In";
            }
        }

        public void OnSettingsButton(Office.IRibbonControl control)
        {
            OnEditSettings?.Invoke(null, null);
        }

        public void OnExtrospectiveButton(Office.IRibbonControl control)
        {
            string message = extrospectiveButtonMessages[0];
            extrospectiveButtonClickCount++;
            if (extrospectiveButtonMessages.ContainsKey(extrospectiveButtonClickCount))
            {
                message = extrospectiveButtonMessages[extrospectiveButtonClickCount];
            }

            MessageBox.Show(message, "Extrospective button says..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public void OnEnableDisableButton(Office.IRibbonControl control)
        {
            Properties.Settings.Default.AddInEnabled = !Properties.Settings.Default.AddInEnabled;
            Properties.Settings.Default.Save();
            ribbon.InvalidateControl(control.Id);
            OnEnableDisableAddIn?.Invoke(null, null);
        }
        
        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
