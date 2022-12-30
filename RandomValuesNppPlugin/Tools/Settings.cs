using RandomValuesNppPlugin.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace Kbg.NppPluginNET.Tools
{
    /// <summary>
    /// Manages application settings
    /// </summary>
    public class Settings : SettingsBase
    {
        [Description("Random value add a line feed, set to false for no line feed"), Category("General"), DefaultValue(true)]
        public bool LineFeed { get; set; }

        [Description("Toolbar icon repeats the last random value, set to false and toolbar icon will open the Generate Values window. Close and restart Notepad++ when changing this settings."), Category("General"), DefaultValue(true)]
        public bool ToolbarRepeatLast { get; set; }

        [Description("Random value settings for menu item 1. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Password (strong)\" string(XXXXYYYYZZZZ9999) [case=mixed,mixmask=true,pwsafe=true]")]
        public String MenuItem1 { get; set; }

        [Description("Random value settings for menu item 2. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Password (easy)\" string(BABABA99) [case=lower,pwsafe=true]")]
        public String MenuItem2 { get; set; }

        [Description("Random value settings for menu item 3. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Random guid\" guid")]
        public String MenuItem3 { get; set; }

        [Description("Random value settings for menu item 4. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Dice throw\" integer {1..6}")]
        public String MenuItem4 { get; set; }

        [Description("Random value settings for menu item 5. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Random color value\" string(#FFFFFF)")]
        public String MenuItem5 { get; set; }

        //[Description("Generate settings records"), Category("Generate"), DefaultValue("Col")]
        //public List<string> GenerateCols{ get; set; }

        // TODO: generate columns are limited to 10 because at the moment, a settings item cannot be of type TList<string>

        [Description("Generate SQL for database type mySQL, MS-SQL or PostgreSQL (0, 1 or 2)."), Category("RandomGenerate"), DefaultValue(0)]
        public int SQLansi { get; set; }

        [Description("Generate tablename or recordname, for SQL, XML and JSON"), Category("RandomGenerate"), DefaultValue("Tablename")]
        public String GenerateTablename { get; set; }

        private int _generateBatch;

        [Description("Generate SQL maximum records per insert batch, minimum batch size is 10."), Category("RandomGenerate"), DefaultValue(1000)]
        public int GenerateBatch
        {
            get { return _generateBatch; }
            set
            {
                _generateBatch = Math.Max(value, 10);
            }
        }

        [Description("The output type for the random values to generate (0..6)."), Category("RandomGenerate"), DefaultValue(1)]
        public int GenerateType { get; set; }

        [Description("The amount of random value records to generate."), Category("RandomGenerate"), DefaultValue(25)]
        public int GenerateAmount { get; set; }

        [Description("Generate random values, definition column 1"), Category("RandomGenerate"), DefaultValue("\"Order ID\" integer {1001..9999}")]
        public String GenerateCol01 { get; set; }

        [Description("Generate random values, definition column 2"), Category("RandomGenerate"), DefaultValue("\"Order date\" datetime(dd-MM-yyyy) {2020..2021}")]
        public String GenerateCol02 { get; set; }

        [Description("Generate random values, definition column 3"), Category("RandomGenerate"), DefaultValue("\"Order price\" decimal {10.0..99.9}")]
        public String GenerateCol03 { get; set; }

        [Description("Generate random values, definition column 4"), Category("RandomGenerate"), DefaultValue("\"Parts group\" string {ENGINE,ELECTRA,CARBODY,CHASSIS,INTERIO,CLIMATE}")]
        public String GenerateCol04 { get; set; }

        [Description("Generate random values, definition column 5"), Category("RandomGenerate"), DefaultValue("\"Order description\" string [width=15]")]
        public String GenerateCol05 { get; set; }

        [Description("Generate random values, definition column 6"), Category("RandomGenerate"), DefaultValue("")]
        public String GenerateCol06 { get; set; }

        [Description("Generate random values, definition column 7"), Category("RandomGenerate"), DefaultValue("")]
        public String GenerateCol07 { get; set; }

        [Description("Generate random values, definition column 8"), Category("RandomGenerate"), DefaultValue("")]
        public String GenerateCol08 { get; set; }

        [Description("Generate random values, definition column 9"), Category("RandomGenerate"), DefaultValue("")]
        public String GenerateCol09 { get; set; }

        [Description("Generate random values, definition column 10"), Category("RandomGenerate"), DefaultValue("")]
        public String GenerateCol10 { get; set; }
    }
}
