﻿using System;
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

        [Description("Automatically apply syntax highlighting to SQL, XML or JSON result, only when it's smaller than this size. Prevent Notepad++ from freezing on large files."), Category("RandomGenerate"), DefaultValue(1024 * 1024)]
        public int AutoSyntaxLimit { get; set; }

        [Description("Generate SQL for database type mySQL, MS-SQL or PostgreSQL (0, 1 or 2)."), Category("RandomGenerate"), DefaultValue(0)]
        public int SQLtype { get; set; }

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

        [Description("The amount of random value records to generate."), Category("RandomGenerate"), DefaultValue(100)]
        public int GenerateAmount { get; set; }

        [Description("Generate random values, definition column 1"), Category("RandomGenerateCols"), DefaultValue("\"Order ID\" integer {1001..9999}")]
        public String GenerateCol01 { get; set; }

        [Description("Generate random values, definition column 2"), Category("RandomGenerateCols"), DefaultValue("\"Order date\" datetime(dd-MM-yyyy) {2023..2024}")]
        public String GenerateCol02 { get; set; }

        [Description("Generate random values, definition column 3"), Category("RandomGenerateCols"), DefaultValue("\"Order price\" decimal {10.0..99.9}")]
        public String GenerateCol03 { get; set; }

        [Description("Generate random values, definition column 4"), Category("RandomGenerateCols"), DefaultValue("\"Parts group\" string {ENGINE,ELECTRA,CARBODY,CHASSIS,INTERIO,CLIMATE}")]
        public String GenerateCol04 { get; set; }

        [Description("Generate random values, definition column 5"), Category("RandomGenerateCols"), DefaultValue("\"Order description\" string [width=15]")]
        public String GenerateCol05 { get; set; }

        [Description("Generate random values, definition column 6"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol06 { get; set; }

        [Description("Generate random values, definition column 7"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol07 { get; set; }

        [Description("Generate random values, definition column 8"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol08 { get; set; }

        [Description("Generate random values, definition column 9"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol09 { get; set; }

        [Description("Generate random values, definition column 10"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol10 { get; set; }

        [Description("Generate random values, definition column 11"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol11 { get; set; }

        [Description("Generate random values, definition column 12"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol12 { get; set; }

        [Description("Generate random values, definition column 13"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol13 { get; set; }

        [Description("Generate random values, definition column 14"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol14 { get; set; }

        [Description("Generate random values, definition column 15"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol15 { get; set; }

        [Description("Generate random values, definition column 16"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol16 { get; set; }

        [Description("Generate random values, definition column 17"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol17 { get; set; }

        [Description("Generate random values, definition column 18"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol18 { get; set; }

        [Description("Generate random values, definition column 19"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol19 { get; set; }

        [Description("Generate random values, definition column 20"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol20 { get; set; }

        [Description("Generate random values, definition column 21"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol21 { get; set; }

        [Description("Generate random values, definition column 22"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol22 { get; set; }

        [Description("Generate random values, definition column 23"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol23 { get; set; }

        [Description("Generate random values, definition column 24"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol24 { get; set; }

        [Description("Generate random values, definition column 25"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol25 { get; set; }

        [Description("Generate random values, definition column 26"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol26 { get; set; }

        [Description("Generate random values, definition column 27"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol27 { get; set; }

        [Description("Generate random values, definition column 28"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol28 { get; set; }

        [Description("Generate random values, definition column 29"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol29 { get; set; }

        [Description("Generate random values, definition column 30"), Category("RandomGenerateCols"), DefaultValue("")]
        public String GenerateCol30 { get; set; }
    }
}
