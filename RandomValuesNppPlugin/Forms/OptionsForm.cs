using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RandomValuesNppPlugin
{
    public partial class OptionsForm : Form
    {
        public Dictionary<string, string> dictOptions;

        public OptionsForm()
        {
            InitializeComponent();
        }

        public void InitialiseOptions(string descript, string datatype, string options)
        {
            this.Text += " - " + descript;

            // dictionary list of options
            dictOptions = new Dictionary<string, string>();

            if (options != "")
            {
                //string options = "da:,de:,en: Henkell Brut Vintage,fr:,nl:,sv:";
                string[] commaSplit = options.Split(',');

                // proces each option
                foreach (string split in commaSplit)
                {
                    // split each key=value pair
                    string[] eqSplit = split.Split('=');
                    dictOptions.Add(eqSplit[0], eqSplit[1]);
                }
            }

            // enable/disable certian based on datatype
            datatype = datatype.ToLower();

            checkMixMask.Enabled = (datatype == "string");
            checkPwSafe.Enabled = (datatype == "string");
            checkPwSafe.Enabled = (datatype == "string");
            comboboxCase.Enabled = ((datatype != "integer") && (datatype != "decimal"));

            // set existing options
            dictOptions.TryGetValue("width", out var wid);
            dictOptions.TryGetValue("mixmask", out var mix);
            dictOptions.TryGetValue("pwsafe", out var pws);
            dictOptions.TryGetValue("case", out var cas);
            dictOptions.TryGetValue("empty", out var emp);

            cas = (cas ?? "").ToLower();
            string[] testcase = new string[] { "", "lower", "upper", "mixed", "initcap" };
            var casidx = Array.IndexOf(testcase, cas);

            // set controls
            textWidth.Text = wid;
            comboboxCase.SelectedIndex = casidx;
            checkMixMask.Checked = (mix == "true");
            checkPwSafe.Checked = (pws == "true");
            textEmptyPerc.Text = emp;
        }

        public string getCurrentOptions()
        {
            string res = "";

            // set controls
            var wid = textWidth.Text;
            var cas = (comboboxCase.SelectedItem ?? "").ToString().ToLower();
            var mix = checkMixMask.Checked;
            var pws = checkPwSafe.Checked;
            var emp = textEmptyPerc.Text;

            if (cas == "(none)") cas = "";

            // set existing options
            if (wid != "") res += ("width=" + wid + ",");
            if (cas != "") res += ("case=" + cas + ",");
            if (mix)       res += ("mixmask=true,");
            if (pws)       res += ("pwsafe=true,");
            if (emp != "") res += ("empty=" + emp + ",");

            // remove last comma
            //res = res.Remove(res.Length - 1);
            res = res.TrimEnd(',');

            return res;
        }
    }
}
