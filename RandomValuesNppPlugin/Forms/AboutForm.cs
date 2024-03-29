﻿using Kbg.NppPluginNET;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RandomValuesNppPlugin
{
    public partial class AboutForm : Form
    {
        private readonly ToolTip helperTip = new ToolTip();
        private int ForceEasterEgg = 0;

        public AboutForm()
        {
            InitializeComponent();

            lblTitle.Text += Main.GetVersion();

            // tooltip initialization
            helperTip.SetToolTip(btnDonate, "Support this plug-in by buying me a coffee!");
            helperTip.SetToolTip(lnkInfo, "Send comments or suggestions (right-click to copy e-mail address)");
            helperTip.SetToolTip(lnkGithub, "Open the Random Values plug-in GitHub page (right-click to copy url)");

            DisplayEasterEgg();
        }

        private int IsEaster(DateTime dt)
        {
            int month = dt.Month;
            if ((month == 3) || (month == 4)) // always march or april anyway
            {
                int year = dt.Year;
                int g = year % 19;
                int c = year / 100;
                int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
                int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

                int day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
                month = 3;

                if (day > 31)
                {
                    month++;
                    day -= 31;
                }

                // also check for second Easter day
                var dt2 = dt.AddDays(-1);

                // return 1st (sunday) or 2nd (monday)
                if ((month == dt.Month) && (day == dt.Day)) return 1; // 1st, easter sunday
                if ((month == dt2.Month) && (day == dt2.Day)) return 2; // 2nd, easter monday
            }

            return 0; // not Easter day
        }
        private void DisplayEasterEgg()
        {
            // display easter egg icon on certain dates
            DateTime today = DateTime.Now.AddHours(-3); // day 'starts' in the morning and lasts after midnight (especially for new years eve etc.)
            //DateTime today = new DateTime(2023, 12, 31); // testing

            string msg = "";
            string obj = "";
            Image img = RandomValuesNppPlugin.Properties.Resources.easteregg;

            int easter = IsEaster(today);
            if (easter > 0)
            {
                // March/April ?th, varies
                msg = string.Format("Easter {0}day", (easter == 1 ? "Sun" : "Mon"));
                obj = "an Easter egg";
                img = RandomValuesNppPlugin.Properties.Resources.easteregg;
            }
            else
            {
                int daymonth = today.Month * 100 + today.Day;

                switch (daymonth)
                {
                    case 317: // March 17th
                        msg = "St. Patrick's Day";
                        obj = "a four-leaf clover";
                        img = RandomValuesNppPlugin.Properties.Resources.clover;
                        break;
                    case 1031: // October 31st
                        msg = "Halloween";
                        obj = "a spooky pumpkin";
                        img = RandomValuesNppPlugin.Properties.Resources.pumpkin;
                        break;
                    case 101: // January 1st
                    case 1231: // December 31th
                        msg = "New Year" + (daymonth == 1231 ? "'s Eve" : "");
                        obj = "an oliebol";
                        img = RandomValuesNppPlugin.Properties.Resources.oliebol;
                        break;
                }
            };

            // initialization easter egg
            if (msg != "")
            {
                string tip = string.Format("Happy {0}! You've found {1} ;)", msg, obj);
                helperTip.SetToolTip(picEasterEgg, tip);
                picEasterEgg.Image = img;
                picEasterEgg.Visible = true;
                picEasterEgg.Tag = "1"; // today is day with actual easter egg
            }
        }
        //private void displayEasterEgg_old()
        //{
        //    // tooltip initialization easter eggs
        //    helperTip.SetToolTip(picClover, "Happy St. Patricks day! You've found a four-leaf clover ;)");
        //    helperTip.SetToolTip(picEasterEgg, "Happy Easter Day! You've found an Easter egg ;)");
        //    helperTip.SetToolTip(picPumpkin, "Happy Halloween! You've found a spooky pumpkin ;)");
        //    helperTip.SetToolTip(picOliebol, "Happy New Year! You've found an oliebol ;)");
        //
        //    // display easter egg icon on certain dates
        //    DateTime today = DateTime.Now.AddHours(-3); // day 'starts' in the morning and lasts after midnight (especially for new years eve etc.)
        //
        //    if (IsEaster(today))
        //    {
        //        picEasterEgg.Visible = true;
        //    }
        //    else
        //    {
        //        int daymonth = (today.Month * 100 + today.Day);
        //
        //        if (daymonth == 317)  picClover.Visible = true;  // March 17th
        //        if (daymonth == 1031) picPumpkin.Visible = true; // October 31st
        //        if (daymonth == 1231) picOliebol.Visible = true; // December 31th
        //        if (daymonth == 101) picOliebol.Visible = true;  // January 1st
        //    };
        //}
        private void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lbl = sender as LinkLabel;
            string url = lbl.Text;
            string urlcopy = url;
            if ((string)lbl.Tag == "1")
            {
                urlcopy = "Bas de Reuver <bdr1976@gmail.com>";
                url = string.Format("mailto:{0}?subject={1} ({2}bit)", urlcopy, lblTitle.Text, (IntPtr.Size * 8));
                url = url.Replace(" ", "%20");
            }

            if (e.Button == MouseButtons.Right)
            {
                Clipboard.SetText(urlcopy); // urlcopy is e-mai address without "mailto:.."
            } else {
                // Change the color of the link text by setting LinkVisited to true.
                lbl.LinkVisited = true;

                // Call the Process.Start method to open the default browser with a URL:
                System.Diagnostics.Process.Start(url);
            }
        }

        private void picEasterEgg_Click(object sender, EventArgs e)
        {
            // click to reveal easter egg, but only on days without actual easter egg
            if (picEasterEgg.Tag.ToString() == "0") {
                ForceEasterEgg++;

                if (ForceEasterEgg % 5 == 0)
                {
                    // show image
                    if (ForceEasterEgg      <=  5) picEasterEgg.Image = RandomValuesNppPlugin.Properties.Resources.easteregg; // click 5 times
                    else if (ForceEasterEgg <= 10) picEasterEgg.Image = RandomValuesNppPlugin.Properties.Resources.clover;    // click 5 more times etc.
                    else if (ForceEasterEgg <= 15) picEasterEgg.Image = RandomValuesNppPlugin.Properties.Resources.pumpkin;
                    else picEasterEgg.Image                           = RandomValuesNppPlugin.Properties.Resources.oliebol;

                    // update help text + counter
                    helperTip.SetToolTip(picEasterEgg, "On certain days in the year you'll find an easter egg here ;)");
                    if (ForceEasterEgg >= 20) ForceEasterEgg = 0; // cycle back to first
                }
            }
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            // Call the Process.Start method to open the default browser with a URL:
            var url = "https://www.paypal.com/donate/?hosted_button_id=BX57KU8MFTDHU";
            System.Diagnostics.Process.Start(url);
        }
    }
}
