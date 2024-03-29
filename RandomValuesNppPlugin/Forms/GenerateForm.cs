﻿using Kbg.NppPluginNET;
using Kbg.NppPluginNET.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace RandomValuesNppPlugin
{
    public partial class GenerateForm : Form
    {
        private const int MAX_COLUMNS = 30;

        public List<RandomValueListItem> listRandomValues;

        private BindingSource listSource;
        private int idxAddExample = 0;

        public GenerateForm(Settings settings)
        {
            InitializeComponent();

            // keep column order set at design-time 
            gridRandomValues.AutoGenerateColumns = false;

            // grid data source
            listSource = new BindingSource();

            // !! TESTING !! just add some
            //listRandomValues = new List<RandomValueListItem> {
            //    new RandomValueListItem("password (strong)", "string", "XXXYYYZZ99", "", "case=mix,mask=random"),
            //    new RandomValueListItem("password (esay)", "string", "ABABAB99", "", "case=low"),
            //    new RandomValueListItem("random code", "string", "", "abc,def,ghi", "empty=5"),
            //    new RandomValueListItem("integer", "integer", "", "100..999", ""),
            //    new RandomValueListItem("float", "decimal", "", "8.5..12.5", ""),
            //    new RandomValueListItem("random date month", "datetime", "dd-MM-yyyy", "2021-04..2021-05", "")
            //};

            listRandomValues = new List<RandomValueListItem>();
            for (var i = 1; i <= MAX_COLUMNS; i++)
            {
                String def = "";

                // I know this code is ugly, please submit PR if you know how to store a List<string> as a settings-list using the current Settings/SettingsBase
                if (i == 1)  def = settings.GenerateCol01;
                if (i == 2)  def = settings.GenerateCol02;
                if (i == 3)  def = settings.GenerateCol03;
                if (i == 4)  def = settings.GenerateCol04;
                if (i == 5)  def = settings.GenerateCol05;
                if (i == 6)  def = settings.GenerateCol06;
                if (i == 7)  def = settings.GenerateCol07;
                if (i == 8)  def = settings.GenerateCol08;
                if (i == 9)  def = settings.GenerateCol09;
                if (i == 10) def = settings.GenerateCol10;

                if (i == 11) def = settings.GenerateCol11;
                if (i == 12) def = settings.GenerateCol12;
                if (i == 13) def = settings.GenerateCol13;
                if (i == 14) def = settings.GenerateCol14;
                if (i == 15) def = settings.GenerateCol15;
                if (i == 16) def = settings.GenerateCol16;
                if (i == 17) def = settings.GenerateCol17;
                if (i == 18) def = settings.GenerateCol18;
                if (i == 19) def = settings.GenerateCol19;
                if (i == 20) def = settings.GenerateCol20;

                if (i == 21) def = settings.GenerateCol21;
                if (i == 22) def = settings.GenerateCol22;
                if (i == 23) def = settings.GenerateCol23;
                if (i == 24) def = settings.GenerateCol24;
                if (i == 25) def = settings.GenerateCol25;
                if (i == 26) def = settings.GenerateCol26;
                if (i == 27) def = settings.GenerateCol27;
                if (i == 28) def = settings.GenerateCol28;
                if (i == 29) def = settings.GenerateCol29;
                if (i == 30) def = settings.GenerateCol30;

                if (def != "")
                {
                    var tmp = new RandomValue(def);
                    if (def != "") listRandomValues.Add(new RandomValueListItem(tmp.Description, tmp.DataType.ToString().ToLower(), tmp.Mask, tmp.Range, tmp.GetOptionsAsString())); // tmp.ListOptions?
                }
            };

            listSource.DataSource = listRandomValues;
            gridRandomValues.DataSource = listSource;

            //gridRandomValues.Columns[1].DataSource = Enum.GetValues(typeof(RandomDataType));

            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)gridRandomValues.Columns["colDataType"];
            
            col.DataSource = Enum.GetValues(typeof(RandomDataType));
            col.ValueType = typeof(RandomDataType);


            // settings
            cmbOutputType.SelectedIndex = (settings.GenerateType >= 0 && settings.GenerateType < cmbOutputType.Items.Count ? settings.GenerateType : 0);
            numAmount.Value = settings.GenerateAmount;

            // extra settings
            txtTablename.Text = settings.GenerateTablename;

            var idx = Main.settings.SQLtype;
            cmbSQLtype.SelectedIndex = (idx >= 0 && idx <= 2 ? idx : 0);

            numBatch.Value = settings.GenerateBatch;
        }

        private void txtNumeric_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!int.TryParse(tb.Text, out _))
            {
                MessageBox.Show(tb.Text + " is not a numeric value.");
                tb.Undo();
                e.Cancel = true;
            }
        }

        private void menuitemMoveRow_Click(object sender, EventArgs e)
        {
            // todo
            var updown = ((ToolStripMenuItem)sender).Tag.ToString();

            int idx = gridRandomValues.CurrentCell.RowIndex;

            var idx1 = gridRandomValues.CurrentCell.RowIndex;
            var idx2 = (updown == "1" ? idx1 + 1 : idx1 - 1); // move up or down

            // cannot move items to outside the gridlist
            if ((idx2 >= 0) && (idx2 < gridRandomValues.Rows.Count))
            {
                // switch items
                var tmp = listRandomValues[idx1];
                listRandomValues[idx1] = listRandomValues[idx2];
                listRandomValues[idx2] = tmp;

                //listRandomValues.Add(new RandomValueListItem("description add", "mask add", "range add"));
                listSource.ResetBindings(false);

                gridRandomValues.ClearSelection();
                gridRandomValues.CurrentCell = gridRandomValues.Rows[idx2].Cells[0];
            }
        }

        private void menuitemAddRow_Click(object sender, EventArgs e)
        {
            // Prevent users from adding more than the ten supported rows
            if (listRandomValues.Count >= MAX_COLUMNS) return;

            // example based on current year
            var yr = DateTime.Now.Year;

            // add random example
            string[] examples = new string[] {
                "Password|String|XXXXYYYYZZZZ9999||case=mixed,mixmask=true,pwsafe=true",
                string.Format("Birth date|DateTime|dd-MM-yyyy|{0}..{1}|", yr-65, yr-18),
                "Sex|String||Male,Female|",
                "Length cm|Integer||140..200|empty=5",
                "Weight kg|Decimal||50.0..100.0|empty=5",
                "Postal code|String|9999XX||",
                string.Format("Follow-up date|DateTime|M/d/yyyy|{0}-01..{0}-05|", yr),
                "Glucose BL mmol/l|Decimal||3,9..5,6|",
                "Lab verified 75perc|String||Yes,Yes,Yes,No|",
                "Remarks free text|String|||width=50"
            };

            // next example
            if (idxAddExample >= examples.Length) idxAddExample = 0;
            var strargs = examples[idxAddExample];
            idxAddExample++;

            // add row
            int idx = 0;
            if (gridRandomValues.CurrentCell != null)
            {
                idx = gridRandomValues.CurrentCell.RowIndex + 1;
            };

            // get all arguments
            var args = strargs.Split('|');
            var desc = String.Format("{0} ({1})", args[0], (idx + 1));

            // add row
            listRandomValues.Insert(idx, new RandomValueListItem(desc, args[1], args[2], args[3], args[4]));

            //listRandomValues.Add(new RandomValueListItem("description add", "mask add", "range add"));
            listSource.ResetBindings(false);

            //gridRandomValues.Rows[idx].Selected = true;

            gridRandomValues.ClearSelection();
            gridRandomValues.CurrentCell = gridRandomValues.Rows[idx].Cells[0];
        }

        private void menuitemDelRow_Click(object sender, EventArgs e)
        {
            //if (listRandomValues.Count > 0)
            if (gridRandomValues.CurrentCell != null)
            {
                int idx = gridRandomValues.CurrentCell.RowIndex;

                gridRandomValues.Rows.RemoveAt(idx);
            };
        }

        private void gridRandomValues_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var rowidx = senderGrid.CurrentCell.RowIndex;

                // get current data type and options
                string descript = gridRandomValues.Rows[rowidx].Cells[0].Value.ToString(); // description column 
                string datatype = gridRandomValues.Rows[rowidx].Cells[1].Value.ToString(); // datatype column 
                string options = (gridRandomValues.Rows[rowidx].Cells[4].Value ?? "").ToString(); // Options (hidden) column

                // options form
                var frm = new OptionsForm();
                frm.InitialiseOptions(descript, datatype, options);
                DialogResult r = frm.ShowDialog();
                if (r == DialogResult.OK)
                {
                    // options are possibly updated
                    options = frm.getCurrentOptions();

                    gridRandomValues.Rows[rowidx].Cells[4].Value = options; // Options (hidden) column
                    gridRandomValues.UpdateCellValue(4, rowidx);
                }
                frm.Dispose();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            menuitemMoveRow_Click(menuitemMoveUp, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            menuitemAddRow_Click(menuitemAddRow, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            menuitemDelRow_Click(menuitemDelRow, e);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            menuitemMoveRow_Click(menuitemMoveDown, e);
        }

        private void cmbOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idx = cmbOutputType.SelectedIndex;

            lblTablename.Visible = (idx >= 3); // 3=sql, 4=xml, 5=json
            txtTablename.Visible = (idx >= 3);
            pnlSQL.Visible       = (idx == 3); // 3=sql
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Main.settings.GenerateType = cmbOutputType.SelectedIndex;
            Main.settings.GenerateAmount = Convert.ToInt32(numAmount.Value);

            for (var i = 1; i <= MAX_COLUMNS; i++)
            {
                var def = "";
                if (i <= listRandomValues.Count)
                {
                    // mask, range optional
                    var msk = listRandomValues[i - 1].Mask;
                    var rng = listRandomValues[i - 1].Range;
                    var opt = listRandomValues[i - 1].Options;

                    msk = (msk != "" ? "(" + msk + ")" : "");
                    rng = (rng != "" ? " {" + rng + "}" : "");
                    opt = (opt != "" ? " [" + opt + "]" : "");
                    def = String.Format("\"{0}\" {1}{2}{3}{4}", listRandomValues[i - 1].Description, listRandomValues[i - 1].DataType.ToString().ToLower(), msk, rng, opt);
                }

                // I know this code is ugly, please submit PR if you know how to store a List<string> as a settings-list using the current Settings/SettingsBase
                if (i == 1) Main.settings.GenerateCol01 = def;
                if (i == 2) Main.settings.GenerateCol02 = def;
                if (i == 3) Main.settings.GenerateCol03 = def;
                if (i == 4) Main.settings.GenerateCol04 = def;
                if (i == 5) Main.settings.GenerateCol05 = def;
                if (i == 6) Main.settings.GenerateCol06 = def;
                if (i == 7) Main.settings.GenerateCol07 = def;
                if (i == 8) Main.settings.GenerateCol08 = def;
                if (i == 9) Main.settings.GenerateCol09 = def;
                if (i == 10) Main.settings.GenerateCol10 = def;

                if (i == 11) Main.settings.GenerateCol11 = def;
                if (i == 12) Main.settings.GenerateCol12 = def;
                if (i == 13) Main.settings.GenerateCol13 = def;
                if (i == 14) Main.settings.GenerateCol14 = def;
                if (i == 15) Main.settings.GenerateCol15 = def;
                if (i == 16) Main.settings.GenerateCol16 = def;
                if (i == 17) Main.settings.GenerateCol17 = def;
                if (i == 18) Main.settings.GenerateCol18 = def;
                if (i == 19) Main.settings.GenerateCol19 = def;
                if (i == 20) Main.settings.GenerateCol20 = def;

                if (i == 21) Main.settings.GenerateCol21 = def;
                if (i == 22) Main.settings.GenerateCol22 = def;
                if (i == 23) Main.settings.GenerateCol23 = def;
                if (i == 24) Main.settings.GenerateCol24 = def;
                if (i == 25) Main.settings.GenerateCol25 = def;
                if (i == 26) Main.settings.GenerateCol26 = def;
                if (i == 27) Main.settings.GenerateCol27 = def;
                if (i == 28) Main.settings.GenerateCol28 = def;
                if (i == 29) Main.settings.GenerateCol29 = def;
                if (i == 30) Main.settings.GenerateCol30 = def;
            };

            // extra settings
            Main.settings.GenerateTablename = txtTablename.Text;
            Main.settings.SQLtype = cmbSQLtype.SelectedIndex;
            Main.settings.GenerateBatch = Convert.ToInt32(numBatch.Value);

            // save to ini file
            Main.settings.SaveToIniFile();
        }
    }
}
