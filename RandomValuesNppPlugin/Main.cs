using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;
using Kbg.NppPluginNET.Tools;
using RandomValuesNppPlugin;

namespace Kbg.NppPluginNET
{
    class Main
    {
        internal const string PluginName = "Random values";
        static int idMyDlg = 6;

        // toolbar icons
        static Bitmap tbBmp = RandomValuesNppPlugin.Properties.Resources.random_icon;
        static Icon tbIco = RandomValuesNppPlugin.Properties.Resources.dice_black_32;
        static Icon tbIcoDM = RandomValuesNppPlugin.Properties.Resources.dice_white_32;

        static IScintillaGateway editor = new ScintillaGateway(PluginBase.GetCurrentScintilla());

        #region " Variables "
        public static Settings settings = new Settings();
        static RandomValue rnd1;
        static RandomValue rnd2;
        static RandomValue rnd3;
        static RandomValue rnd4;
        static RandomValue rnd5;
        static int iRepeatLast = 1;
        #endregion

        public static void OnNotification(ScNotification notification)
        {
            // This method is invoked whenever something is happening in notepad++
            // use eg. as
            // if (notification.Header.Code == (uint)NppMsg.NPPN_xxx)
            // { ... }
            // or
            //
            // if (notification.Header.Code == (uint)SciMsg.SCNxxx)
            // { ... }
        }

        internal static void CommandMenuInit()
        {
            // get the parameter value from plugin config
            rnd1 = new RandomValue(settings.MenuItem1);
            rnd2 = new RandomValue(settings.MenuItem2);
            rnd3 = new RandomValue(settings.MenuItem3);
            rnd4 = new RandomValue(settings.MenuItem4);
            rnd5 = new RandomValue(settings.MenuItem5);

            PluginBase.SetCommand(0, rnd1.Description, randomValue1); // , new ShortcutKey(false, true, false, Keys.R));
            PluginBase.SetCommand(1, rnd2.Description, randomValue2);
            PluginBase.SetCommand(2, rnd3.Description, randomValue3);
            PluginBase.SetCommand(3, rnd4.Description, randomValue4);
            PluginBase.SetCommand(4, rnd5.Description, randomValue5);

            // Here you insert a separator
            PluginBase.SetCommand(5, "---", null);

            PluginBase.SetCommand(6, "Repeat random value", randomValueRepeat);
            PluginBase.SetCommand(7, "Generate random values", dialogGenerateValues);

            // Here you insert a separator
            PluginBase.SetCommand(8, "---", null);

            // Shortcut :
            PluginBase.SetCommand(9, "Settings", dialogSettings);
            PluginBase.SetCommand(10, "About", dialogAbout);

            // toolbar icon function
            idMyDlg = (settings.ToolbarRepeatLast ? 6 : 7); // 6 = Repeat, 7 = Generate window
        }

        internal static void SetToolBarIcon()
        {
            // create struct
            toolbarIcons tbIcons = new toolbarIcons();

            // add bmp icon
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            tbIcons.hToolbarIcon = tbIco.Handle;            // icon with black lines
            tbIcons.hToolbarIconDarkMode = tbIcoDM.Handle;  // icon with light grey lines

            // convert to c++ pointer
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);

            // call Notepad++ api
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_ADDTOOLBARICON_FORDARKMODE, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);

            // release pointer
            Marshal.FreeHGlobal(pTbIcons);
        }

        internal static void PluginCleanUp()
        {
            // this method gets called when Notepad++ shuts down
        }

        public static string GetVersion()
        {
            // version for example "1.3.0.0"
            String ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            // if 4 version digits, remove last two ".0" if any, example  "1.3.0.0" ->  "1.3" or  "2.0.0.0" ->  "2.0"
            while ((ver.Length > 4) && (ver.Substring(ver.Length - 2, 2) == ".0"))
            {
                ver = ver.Substring(0, ver.Length - 2);
            }
            return ver;
        }

        #region " Menu functions "
        static void randomValue1()
        {
            InsertRandomValue(rnd1);
            iRepeatLast = 1;
        }
        static void randomValue2()
        {
            InsertRandomValue(rnd2);
            iRepeatLast = 2;
        }
        static void randomValue3()
        {
            InsertRandomValue(rnd3);
            iRepeatLast = 3;
        }
        static void randomValue4()
        {
            InsertRandomValue(rnd4);
            iRepeatLast = 4;
        }
        static void randomValue5()
        {
            InsertRandomValue(rnd5);
            iRepeatLast = 5;
        }
        static void InsertRandomValue(RandomValue rnd)
        {
            // insert into text
            var str = rnd.NextValue() + (settings.LineFeed ? "\r\n" : "");
            editor.ReplaceSel(str);
        }

        static void randomValueRepeat()
        {
            switch (iRepeatLast)
            {
                case 2:
                    randomValue2();
                    break;
                case 3:
                    randomValue3();
                    break;
                case 4:
                    randomValue4();
                    break;
                case 5:
                    randomValue5();
                    break;
                default:
                    randomValue1();
                    break;
            }
        }

        static void dialogGenerateValues()
        {
            var genform = new GenerateForm(settings);
            DialogResult res = genform.ShowDialog();

            genform.Dispose();

            if (res == DialogResult.OK)
            {
                // generate values
                var s = RandomValues.Generate();

                // create new file
                INotepadPPGateway notepad = new NotepadPPGateway();
                notepad.FileNew();

                // set new text
                editor.SetText(s);
                if (s.Length < Main.settings.AutoSyntaxLimit)
                {
                    //if (Main.settings.GenerateType == 0) ?; // Comma separated(CSV)
                    //if (Main.settings.GenerateType == 1) ?; // Tab separated
                    //if (Main.settings.GenerateType == 2) ?; // Semi - colon separated
                    if (Main.settings.GenerateType == 3) notepad.SetCurrentLanguage(LangType.L_SQL); // SQL
                    if (Main.settings.GenerateType == 4) notepad.SetCurrentLanguage(LangType.L_XML); // XML
                    if (Main.settings.GenerateType == 5) notepad.SetCurrentLanguage(LangType.L_JSON); // JSON
                }
            };
        }

        static void dialogSettings()
        {
            settings.ShowDialog();
        }

        internal static void dialogAbout()
        {
            var about = new AboutForm();
            about.ShowDialog();
            about.Dispose();
        }
        #endregion
    }
}