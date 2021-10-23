using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RandomValuesNppPlugin
{
    public enum RandomDataType
    {
        String,
        Integer,
        Decimal,
        DateTime,
        Guid
    }

    public class RandomValue
    {
        // note: use the same ONE Random object for all RandomValue instances
        // because if each has its own Random object then the random values will have the same "random" pattern (i.e. not random)
        // this is most apparent with two exact same settings, example remove "static" and define "integer 100..999" twice, then you get two columns with the same values
        private static Random RandObj = new Random();

        private static string Lorem = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui.";

        private string[] OptionCaseNames = new string[] { "(none)", "lower", "upper", "mixed", "initcap" };

        // initialise strings for masks, for example '9999XX' for random zipcodes etc.
        private const string VARCHAR_MASK_A = "AEIOU";                   // vowels
        private const string VARCHAR_MASK_B = "BCDFGHJKLMNPQRSTVWXYZ";   // consonants
        private const string VARCHAR_MASK_0 = "0123456789";              // digits
        private const string VARCHAR_MASK_F = "0123456789ABCDEF";        // hexdigits
        private const string VARCHAR_MASK_S = "!@#$%^&*+-";              // symbols

        private const string VARCHAR_MASK_X = VARCHAR_MASK_A + VARCHAR_MASK_B;
        private const string VARCHAR_MASK_Y = VARCHAR_MASK_A + VARCHAR_MASK_B + VARCHAR_MASK_0;
        private const string VARCHAR_MASK_Z = VARCHAR_MASK_A + VARCHAR_MASK_B + VARCHAR_MASK_0 + VARCHAR_MASK_S;

        // password safe characters, avoid confusion between chracters (for example 1 and l, or 0 and O etc.)
        private const string VARCHAR_MASK_PW_A = "aeu"; // i o removed
        private const string VARCHAR_MASK_PW_B = "bcdfhjkmnpqrtvwxy"; // g l s z removed
        private const string VARCHAR_MASK_PW_0 = "34678"; // 0 1 2 5 9 removed

        private const string VARCHAR_MASK_PW_X = VARCHAR_MASK_PW_A + VARCHAR_MASK_PW_B;
        private const string VARCHAR_MASK_PW_Y = VARCHAR_MASK_PW_A + VARCHAR_MASK_PW_B + VARCHAR_MASK_PW_0;
        private const string VARCHAR_MASK_PW_Z = VARCHAR_MASK_PW_A + VARCHAR_MASK_PW_B + VARCHAR_MASK_PW_0 + VARCHAR_MASK_S;

        // members
        public string Description = "test";
        private string DataType_old = "string";
        public RandomDataType DataType = RandomDataType.String;
        public string Mask = "";
        public bool MixMask = false;
        public bool PwSafe = false;
        public int CaseChar = 0;
        public string Range = "";
        private string RangeOperate = "";
        private bool RangeMinMax = false;
        private int RangeIntMin = 10;
        private int RangeIntMax = 99;
        private double RangeDblMin = 10.0;
        private double RangeDblMax = 99.9;
        private DateTime RangeDateMin;
        private bool RangeInclTime = false;
        public int EmptyPerc = 0;
        public int Width = 50; // actual used width
        public int _width = 0; // explicitly set by by user
        public int FloatDec = 1;
        private NumberFormatInfo FloatFormat = new NumberFormatInfo();
        private string Options = "";
        public List<string> ListRange;
        public List<string> ListOptions;

        public RandomValue()
        {
            Initialise("ValueName", "String", "", "", "");
        }
        public RandomValue(string args)
        {
            DeSerialise(args);
        }
        public RandomValue(string sName, string sDataType)
        {
            Initialise(sName, sDataType, "", "", "");
        }
        public RandomValue(string sName, string sDataType, string sMask)
        {
            Initialise(sName, sDataType, sMask, "", "");
        }
        public RandomValue(string sName, string sDataType, string sMask, string sRange)
        {
            Initialise(sName, sDataType, sMask, sRange, "");
        }
        public RandomValue(string sName, string sDataType, string sMask, string sRange, string sOptions)
        {
            Initialise(sName, sDataType, sMask, sRange, sOptions);
        }

        private void Initialise(string sName, string sDataType, string sMask, string sRange, string sOptions)
        {
            Description = sName;
            DataType_old = sDataType;
            DataType = (RandomDataType)Enum.Parse(typeof(RandomDataType), sDataType, true);
            Mask = sMask;
            Range = sRange;
            Options = sOptions;

            // common user mistake, enter range as "..." instead of  ".."
            if (Range.IndexOf("...") >= 0) Range = Range.Replace("...", "..");

            // common user mistake, mask for integer or decimal
            if ((DataType == RandomDataType.Integer) || (DataType == RandomDataType.Decimal)) Mask = ""; // mask not supported atm

            // common user mistake, no explicit range
            if (Range == "")
            {
                // add explicit range
                if (DataType == RandomDataType.Integer) Range = "10..99";
                if (DataType == RandomDataType.Decimal) Range = "10.0..99.9";
                if (DataType == RandomDataType.DateTime)
                {
                    int currentyear = DateTime.Now.Year;
                    Range = string.Format("{0}..{1}", currentyear-1, currentyear);
                }
            }

            FloatFormat.NumberDecimalSeparator = ".";

            // check range
            if (Range != "")
            {
                // operator at end, example {1,2,3}*6 -> operator = *6
                var pos = Range.IndexOf("}");
                if (pos >= 0) {
                    if (pos < Range.Length - 1)
                    {
                        RangeOperate = Range.Substring(pos + 1, Range.Length - pos - 1);
                        Range = Range.Substring(pos, Range.Length - pos);
                    }
                    Range = Range.Substring(1, Range.Length - 2);
                };

                // min/max range or individual values
                RangeMinMax = (Range.IndexOf("..") >= 0);
                var Sep = (RangeMinMax ? ".." : ",");

                // split to list
                ListRange = Range.Split(new string[] { Sep }, StringSplitOptions.None).ToList();

                // precalc string to int
                if ((RangeMinMax) && (DataType == RandomDataType.Integer))
                {
                    var bool1 = Int32.TryParse(ListRange[0], out RangeIntMin);
                    var bool2 = Int32.TryParse(ListRange[1], out RangeIntMax);

                    // check if any error in range
                    if (!bool1 || !bool2)
                    {
                        // overwrite with default value
                        Range = "10..99";
                        RangeIntMin = 10;
                        RangeIntMax = 99;
                    }

                    // fix min max error
                    if (RangeIntMin > RangeIntMax)
                    {
                        var tmp = RangeIntMin;
                        RangeIntMin = RangeIntMax;
                        RangeIntMax = tmp;
                    };
                }

                // precalc string to float
                if ((RangeMinMax) && (DataType == RandomDataType.Decimal))
                {

                    var bool1 = Double.TryParse(ListRange[0], NumberStyles.Any, CultureInfo.InvariantCulture, out RangeDblMin);
                    var bool2 = Double.TryParse(ListRange[1], NumberStyles.Any, CultureInfo.InvariantCulture, out RangeDblMax);

                    // check if any error in range
                    if (!bool1 || !bool2)
                    {
                        // overwrite with default value
                        Range = "10.0..99.9";
                        RangeDblMin = 10.0;
                        RangeDblMax = 99.9;
                    }

                    // fix min max error
                    if (RangeDblMin > RangeDblMax)
                    {
                        var tmp = RangeDblMin;
                        RangeDblMin = RangeDblMax;
                        RangeDblMax = tmp;
                    };

                    var dec = ListRange[0].IndexOf('.');
                    FloatDec = (dec >= 0 ? ListRange[0].Length - dec - 1 : 0);

                    FloatFormat.NumberDecimalDigits = FloatDec;
                    //Mask = "0.";
                    //Mask = Mask.PadRight(FloatDec + 2, '0');
                }

                // precalc string to date
                if ((RangeMinMax) && (DataType == RandomDataType.DateTime))
                {
                    // default mask
                    if (Mask == "") Mask = "yyyy-MM-dd";

                    var dt1 = DateRangeValue(ListRange[0], false);
                    var dt2 = DateRangeValue(ListRange[1], true);

                    if (dt1 > dt2)
                    {
                        var tmp = dt1;
                        dt1 = dt2;
                        dt2 = tmp;
                    };

                    // keep only start date and max days to add
                    RangeDateMin = dt1;
                    RangeIntMax = (dt2 - dt1).Days;

                    RangeInclTime = (Mask.IndexOf("HH") > 0 || Mask.IndexOf(":mm") > 0 || Mask.IndexOf(":ss") > 0 ? true : false);
                }
            }

            // check options
            if (Options != "")
            {
                // dictionary list of options
                var dictOptions = new Dictionary<string, string>();

                //string options = "da:,de:,en: Henkell Brut Vintage,fr:,nl:,sv:";
                string[] commaSplit = Options.Split(',');

                // proces each option
                foreach (string split in commaSplit)
                {
                    // split each key=value pair
                    string[] eqSplit = split.Split('=');
                    dictOptions.Add(eqSplit[0], eqSplit[1]);
                }
                
                // Valid options are:
                // * width = 50
                // * case = upper / lower / initcap / mix
                // * mixmask = true (for passwords)
                // * empty = 10 (percent)
                // * padd zeroes integer
                // * padd zeroes decimals

                // get additional options
                dictOptions.TryGetValue("width",   out var wid);
                dictOptions.TryGetValue("mixmask", out var mix);
                dictOptions.TryGetValue("pwsafe",  out var pws);
                dictOptions.TryGetValue("case",    out var cas);
                dictOptions.TryGetValue("empty",   out var emp);

                cas = (cas ?? "").ToLower();
                string[] testcase = new string[] { "(none)", "lower", "upper", "mixed", "initcap" };

                // set variables
                if (Int32.TryParse(wid, out int iwid))
                {
                    _width = iwid;
                    Width = iwid;
                }
                CaseChar = Array.IndexOf(testcase, cas);
                if (Int32.TryParse(emp, out int iemp)) EmptyPerc = iemp;
                if ((EmptyPerc < 0) || (EmptyPerc >= 100)) EmptyPerc = 0; // must be between 0..100
                MixMask = (mix == "true");
                PwSafe = (pws == "true");
            }

            Console.WriteLine("name: {0}\ndatatype: {1}\nmask: {2}\nrange: {3}\noptions: {4}", Description, DataType.ToString(), Mask, Range, Options);
        }

        public string GetOptionsAsString()
        {
            var res = "";

            if (_width > 0)                        res += "width=" + _width.ToString() + ",";
            if (EmptyPerc > 0)                     res += "empty=" + EmptyPerc.ToString() + ",";
            if ((CaseChar > 0) && (CaseChar <= 4)) res +=  "case=" + OptionCaseNames[CaseChar] + ",";

            if (MixMask) res += "mixmask=true,";
            if (PwSafe)  res +=  "pwsafe=true,";

            // remove last comma
            //res = res.Remove(res.Length - 1);
            res = res.TrimEnd(',');

            return res;
        }

        //   (\".*\")\s+(int|integer|string|decimal|datetime|date|time|guid)(\(.*\))?(\].*\])?
        //   (\".*\")\s+(int|integer|string|decimal|datetime|date|time|guid)((\(.*\))?)\s+((\{.*\})?)\s+((\[.*\])?)
        //   (\".*\")\s+(int|integer|string|decimal|datetime|date|time|guid)(\(.*\)\s*)?(.*[^\]])\s+(\[.*\])?
        //   (?<filename>\".*\")\s+(?<datatype>int|integer|string|decimal|datetime|date|time|guid)(?<mask>\(.*\)\s*)?(?<range>.*[^\]])\s+(?<options>\[.*\])?

        //   Group 1) match between quotes for "name"
        //   Group 2) match datatype (string int etc.)
        //   Group 3) optionally, match mask
        //   Group 4) match range
        //   Group 5) optionally, match between breackets for [any,argumenst=extra]

        // original regex:  (?<description>\".*\")\s+(?<datatype>string|int|integer|decimal|datetime|date|time|guid)(?<mask>\(.*\))?(?<range>.+?(?=\[|$))?(?<options>\[.*\])?
        // Use verbatim string, only replace single " with "" see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/verbatim
        private static string pattern2 = @"(?<description>\"".*\"")\s+(?<datatype>string|integer|decimal|datetime|date|time|guid)(?<mask>\(.*\))?(?<range>.+?(?=\[|$))?(?<options>\[.*\])?";

        private Regex regex = new Regex(pattern2,
               RegexOptions.Singleline
               | RegexOptions.ExplicitCapture
               | RegexOptions.CultureInvariant
               | RegexOptions.IgnorePatternWhitespace
               | RegexOptions.Compiled
               );

        public void DeSerialise(string args)
        {
            Console.WriteLine("text: {0}", args);
            Match m = regex.Match(args);

            if (m.Success)
            {
                // set local variables
                var sName     = m.Groups["description"].ToString();
                var sDataType = m.Groups["datatype"].ToString();
                var sMask     = m.Groups["mask"].ToString().Trim();
                var sRange    = m.Groups["range"].ToString().Trim();
                var sOptions  = m.Groups["options"].ToString();

                // remove quotes
                sName = sName.Substring(1, sName.Length - 2);

                // remove round brackets
                if (sMask != "")
                {
                    sMask = sMask.Substring(1, sMask.Length - 2);
                }
                // remove square brackets
                if (sOptions != "")
                {
                    sOptions = sOptions.Substring(1, sOptions.Length - 2);
                }

                Initialise(sName, sDataType, sMask, sRange, sOptions);
            }
            else
            {
                // error default
                Initialise("Value Description", "string", "", "", "");
            }
        }

        private DateTime DateRangeValue(string dt, bool max)
        {
            // date range, should always generates date values up-to-and-including,
            // so if only year is given add a year, if only year-month then add month, or for full date add 1 day
            // example 2022       -> max range = '2023-01-01' (max generated date effectively '2022-12-31')
            // example 2022-04    -> max range = '2022-05-01' (max generated date effectively '2022-04-30')
            // example 2022-12-31 -> max range = '2023-01-01' (max generated date effectively '2022-12-31')
            var addday   = (max && dt.Length > 7 ? 1 : 0);
            var addmonth = (max && dt.Length > 4 && dt.Length <= 7 ? 1 : 0);
            var addyear  = (max && dt.Length == 4 ? 1 : 0);

            dt += (dt.Length > 4 ? (dt.Length > 7 ? "" : "-01") : "-01-01");

            DateTime res;

            try
            {
                res = DateTime.ParseExact(dt, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).AddYears(addyear).AddMonths(addmonth).AddDays(addday);
            }
            catch
            {
                if (max)
                {
                    res = DateTime.Now;
                }
                else
                {
                    res = new DateTime(1970, 1, 1);
                }
            }

            return res;
        }

        public string NextValue()
        {
            var res = "";
            var emptyvalue = false;

            // empty values
            if (EmptyPerc > 0)
            {
                var rnd = RandObj.Next(1, 101); // max = exclusive upper bound, so 1..101, not including max, returns values from 1..100
                emptyvalue = (rnd <= EmptyPerc);
            }

            // skip for empty values
            if (!emptyvalue)
            {
                // get next value based on datatype
                switch (DataType)
                {
                    case RandomDataType.String:
                        res = GetRandomVarchar();
                        break;
                    case RandomDataType.Integer:
                        res = GetRandomInteger();
                        break;
                    case RandomDataType.Decimal:
                        res = GetRandomDecimal();
                        break;
                    case RandomDataType.DateTime:
                        res = GetRandomDate();
                        break;
                    case RandomDataType.Guid:
                        res = GetRandomGUID();
                        break;
                }
            }

            return res;
        }
        public string GetRandomVarchar()
        {

            string res = "";

            if (Mask != "")
            {
                res = GetRandomMaskValue();
            }
            else if (ListRange != null)
            {
                res = GetRandomListValue(ListRange);
            }
            else
            {
                res = GetRandomLorem();
            }

            if (CaseChar > 0) res = AdjustCapitalization(res);

            return res;
        }

        public string GetRandomInteger()
        {
            int res = (RangeIntMin + RandObj.Next(0, RangeIntMax - RangeIntMin + 1));

            return res.ToString();
        }

        public string GetRandomDecimal()
        {
            double res = RandObj.NextDouble() * (RangeDblMax - RangeDblMin) + RangeDblMin;

            return string.Format(FloatFormat, "{0:N}", res);
        }

        public string GetRandomDate()
        {
            // random date
            var res = RangeDateMin.AddDays(RandObj.Next(RangeIntMax));

            // also random time?
            if (RangeInclTime)
            {
                res = res.AddSeconds(RandObj.Next((24 * 60 * 60)));
            };

            return res.ToString(Mask, CultureInfo.InvariantCulture);
        }

        private string GetRandomGUID()
        {
            // simpler uuid generator, found at https://gist.github.com/jcxplorer/)
            var res = "";

            for (var i = 0; i < 32; i++)
            {

                int b = RandObj.Next(0, 16); // 16 = exclusive upper bound, so 0..15, not including 16

                if (i == 8 || i == 12 || i == 16 || i == 20) res += "-";

                b = (i == 12 ? 4 : (i == 16 ? (b & 3 | 8) : b));

                res += b.ToString("x");
            };

            if (CaseChar > 0) res = AdjustCapitalization(res);

            return res;
        }

        private string GetRandomListValue(List<string> list)
        {
            var max = list.Count;

            int idx = RandObj.Next(0, max); // max = exclusive upper bound, so 0..max-1, not including max

            return list[idx];
        }

        private string GetRandomLorem()
        {
            string res = "";

            // TODO: LoremArr should be global static?
            var LoremArr = Lorem.Split(' ');

            // start at random point and take random amount of words
            int x = RandObj.Next(0, LoremArr.Length);
            int i = x;
            int max = RandObj.Next(10, Width);

            // add random snippets from 'Lorem ipsum' text
            while ((res.Length < max) && (i < LoremArr.Length))
            {
                res = res + LoremArr[i] + ' ';
                i++;
            };

            // trim and max width
            res = res.Trim();
            return (res.Length <= Width ? res : res.Substring(0, Width));
        }

        public void RandomizeMask()
        {
            if (Mask != "")
            {
                // get Mask string info
                var info = new StringInfo(Mask);
                var indices = Enumerable.Range(0, info.LengthInTextElements).ToArray();

                // determine randomized order
                for (var i = indices.Length; i-- > 1;)
                {
                    var j = RandObj.Next(i + 1);
                    if (i != j)
                    {
                        var temp = indices[i];
                        indices[i] = indices[j];
                        indices[j] = temp;
                    }
                }

                // rearrange mask string in random order
                var builder = new StringBuilder(Mask.Length);
                foreach (var index in indices)
                {
                    builder.Append(info.SubstringByTextElements(index, 1));
                }
                Mask = builder.ToString();
            }
        }

        private string AdjustCapitalization(string strinput)
        {
            var res = "";

            // all lower case
            if (CaseChar == 1) res = strinput.ToLower();

            // all upper case
            if (CaseChar == 2) res = strinput.ToUpper();

            // mix or initcap
            if ((CaseChar == 3) || (CaseChar == 4))
            {
                // make entire inpput lower case
                strinput = strinput.ToLower();
                var prevspc = true;

                // adjust character for character
                for (var i = 0; i < strinput.Length; i++)
                {
                    string c = strinput[i].ToString();
                    bool upper = false;

                    if (CaseChar == 3)
                    {
                        // mixed/random capitalization
                        var rnd = RandObj.Next(1, 101); // max = exclusive upper bound, so 1..101, not including max, returns values from 1..100
                        upper = (rnd <= 25); // 25% uppercase
                    }
                    else
                    {
                        // was previous character a space?
                        upper = prevspc;

                        // is current character a space?
                        prevspc = (c == " " || c == "." || c == ",");
                    }

                    // add to result
                    if (upper) c = c.ToUpper();
                    res += c;
                }
            }

            return res;
        }

        // random varchar/text value based on mask, example '9999XX' for random zipcode
        private string GetRandomMaskValue()
        {
            // variables
            var res = "";

            // randomize mask for passwords
            if (MixMask) RandomizeMask();

            // parse input mask, example 'ababab99' for random password
            for (var i = 0; i < Mask.Length; i++)
            {
                var CharList = "";
                char ch = Mask[i];
                switch (ch)
                {
                    case 'A':
                        CharList = (PwSafe ? VARCHAR_MASK_PW_A : VARCHAR_MASK_A);
                        break;
                    case 'B':
                        CharList = (PwSafe ? VARCHAR_MASK_PW_B : VARCHAR_MASK_B);
                        break;
                    case '9':
                        CharList = (PwSafe ? VARCHAR_MASK_PW_0 : VARCHAR_MASK_0);
                        break;
                    case 'F':
                        CharList = VARCHAR_MASK_F;
                        break;
                    case '@':
                        CharList = VARCHAR_MASK_S;
                        break;
                    case 'X':
                        CharList = (PwSafe ? VARCHAR_MASK_PW_X : VARCHAR_MASK_X);
                        break;
                    case 'Y':
                        CharList = (PwSafe ? VARCHAR_MASK_PW_Y : VARCHAR_MASK_Y);
                        break;
                    case 'Z':
                        CharList = (PwSafe ? VARCHAR_MASK_PW_Z : VARCHAR_MASK_Z);
                        break;
                    default: // not a valid mask character, just copy it into password
                        res = res + Mask[i];
                        break;
                }
                // is valid
                if (CharList != "")
                {
                    var pos = RandObj.Next(0, CharList.Length);
                    res += CharList[pos];
                }
            }

            // return random varchar value
            return res;
        }
    }
}
