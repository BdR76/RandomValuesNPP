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
        static Bitmap tbBmp = RandomValuesNppPlugin.Properties.Resources.random_icon;
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
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
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
            List<RandomValueListItem> list = genform.GetRandomValuesList();
            var outtype = genform.GetOutputType();
            var amount = genform.GetAmount();
            genform.Dispose();

            if (res == DialogResult.OK)
            {
                // transform settings to actual random nr generator
                List<RandomValue> listrnd = new List<RandomValue>();
                foreach (var r in list)
                {
                    listrnd.Add(new RandomValue(r.Description, r.DataType.ToString(), r.Mask, r.Range, r.Options));
                }

                // OK save 'cleaned' settings, i.e. validated in new RandomValue()
                saveGenerateValues(outtype, amount, listrnd);

                // generate values
                var s = RandomValues.Generate(listrnd, outtype, amount);
                editor.ReplaceSel(s);
                if (settings.LineFeed) editor.NewLine();
            };
        }

        static void saveGenerateValues(int outtyp, int amount, List<RandomValue> list)
        {
            settings.GenerateType = outtyp;
            settings.GenerateAmount = amount;

            for (var i = 1; i < 10; i++)
            {
                var def = "";
                if (i <= list.Count)
                {
                    // mask, range optional
                    var msk = list[i - 1].Mask;
                    var rng = list[i - 1].Range;
                    var opt = list[i - 1].GetOptionsAsString();

                    msk = (msk != "" ? "(" + msk + ")" : "");
                    rng = (rng != "" ? " {" + rng + "}" : "");
                    opt = (opt != "" ? " [" + opt + "]" : "");
                    def = String.Format("\"{0}\" {1}{2}{3}{4}", list[i - 1].Description, list[i - 1].DataType.ToString().ToLower(), msk, rng, opt);
                }

                if (i == 1)  settings.GenerateCol01 = def;
                if (i == 2)  settings.GenerateCol02 = def;
                if (i == 3)  settings.GenerateCol03 = def;
                if (i == 4)  settings.GenerateCol04 = def;
                if (i == 5)  settings.GenerateCol05 = def;
                if (i == 6)  settings.GenerateCol06 = def;
                if (i == 7)  settings.GenerateCol07 = def;
                if (i == 8)  settings.GenerateCol08 = def;
                if (i == 9)  settings.GenerateCol09 = def;
                if (i == 10) settings.GenerateCol10 = def;
            };
            settings.SaveToIniFile();
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