using System;
using System.ComponentModel;

namespace Kbg.NppPluginNET.Tools
{
    /// <summary>
    /// Manages application settings
    /// </summary>
    public class Settings : SettingsBase
    {
        [Description("Random value settings for menu item 1. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Password (strong)\" string(XXXYYYZZZ999) [case=mixed,mixmask=true,pwsafe=true]")]
        public String MenuItem1 { get; set; }

        [Description("Random value settings for menu item 2. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Password (easy)\" string(BABABA99) [case=lower,pwsafe=true]")]
        public String MenuItem2 { get; set; }

        [Description("Random value settings for menu item 3. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Random guid\" guid")]
        public String MenuItem3 { get; set; }

        [Description("Random value settings for menu item 4. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Dice throw\" integer {1..6}")]
        public String MenuItem4 { get; set; }

        [Description("Random value settings for menu item 5. Close and restart Notepad++ when changing menu items."), Category("Menu items"), DefaultValue("\"Random color (hex)\" string(#FFFFFF)")]
        public String MenuItem5 { get; set; }

        [Description("Random value add a line feed, set to false for no line feed"), Category("General"), DefaultValue(true)]
        public bool LineFeed { get; set; }

        [Description("Toolbar icon repeats last random value, set to false and toolbar icon will open the Generate Values window"), Category("General"), DefaultValue(true)]
        public bool IconAddValue { get; set; }

        //[Description("Generate settings records"), Category("Generate"), DefaultValue("Col")]
        //public List<string> GenerateCols{ get; set; }

        // TODO: generate columns are limited to 10 because at the moment, a settings item cannot be of type TList<string>

        [Description("The output type for the random values to generate (0..6)."), Category("GenerateValues"), DefaultValue(1)]
        public int GenerateType { get; set; }

        [Description("The amount of random value records to generate."), Category("GenerateValues"), DefaultValue(25)]
        public int GenerateAmount { get; set; }

        [Description("Generate random values, definition column 1"), Category("GenerateValues"), DefaultValue("\"Order ID\" integer {1001..9999}")]
        public String GenerateCol01 { get; set; }

        [Description("Generate random values, definition column 2"), Category("GenerateValues"), DefaultValue("\"Order date\" datetime(dd-MM-yyyy) {2020..2021}")]
        public String GenerateCol02 { get; set; }

        [Description("Generate random values, definition column 3"), Category("GenerateValues"), DefaultValue("\"Order price\" decimal {10.0..99.9}")]
        public String GenerateCol03 { get; set; }

        [Description("Generate random values, definition column 4"), Category("GenerateValues"), DefaultValue("\"Parts group\" string {ENGINE,ELECTRA,CARBODY,CHASSIS,INTERIO,CLIMATE}")]
        public String GenerateCol04 { get; set; }

        [Description("Generate random values, definition column 5"), Category("GenerateValues"), DefaultValue("\"Order description\" string [width=15]")]
        public String GenerateCol05 { get; set; }

        [Description("Generate random values, definition column 6"), Category("GenerateValues"), DefaultValue("")]
        public String GenerateCol06 { get; set; }

        [Description("Generate random values, definition column 7"), Category("GenerateValues"), DefaultValue("")]
        public String GenerateCol07 { get; set; }

        [Description("Generate random values, definition column 8"), Category("GenerateValues"), DefaultValue("")]
        public String GenerateCol08 { get; set; }

        [Description("Generate random values, definition column 9"), Category("GenerateValues"), DefaultValue("")]
        public String GenerateCol09 { get; set; }

        [Description("Generate random values, definition column 10"), Category("GenerateValues"), DefaultValue("")]
        public String GenerateCol10 { get; set; }
    }
}
