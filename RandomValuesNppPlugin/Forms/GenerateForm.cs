using Kbg.NppPluginNET;
using Kbg.NppPluginNET.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace RandomValuesNppPlugin
{
    public partial class GenerateForm : Form
    {
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
            for (var i = 1; i < 10; i++)
            {
                String def = "";

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
            rdbtnMySQL.Checked = settings.SQLansi;
            rdbtnMSSQL.Checked = !settings.SQLansi;
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
            // add random example
            string[] examples = new string[] {
                "Password|String|XXXXYYYYZZZZ9999||case=mixed,mixmask=true,pwsafe=true",
                "Birth date|DateTime|dd-MM-yyyy|1970..2010|",
                "Sex|String||M,F|",
                "Length cm|Integer||140..200|empty=5",
                "Weight kg|Decimal||50.0..100.0|empty=5",
                "Postal code|String|9999XX||",
                "Follow-up date|DateTime|dd-MM-yyyy|2020-01..2020-03|",
                "Device unit price|Decimal||50.00..300.00|",
                "Medical device|String|||width=15"
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

            for (var i = 1; i < 10; i++)
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
            };

            // extra settings
            Main.settings.GenerateTablename = txtTablename.Text;
            Main.settings.SQLansi = (rdbtnMySQL.Checked);
            Main.settings.GenerateBatch = Convert.ToInt32(numBatch.Value);

            // save to ini file
            Main.settings.SaveToIniFile();
        }
    }
}
