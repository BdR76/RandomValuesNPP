using Kbg.NppPluginNET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace RandomValuesNppPlugin
{
    class RandomValues
    {
        public static String Generate()
        {
            // get settings
            var list = GetRandomValueColumns();
            var outputtype = Main.settings.GenerateType;
            var amount = Main.settings.GenerateAmount;

            // generate values
            StringBuilder sb = new StringBuilder();

            switch (outputtype)
            {
                case 0: // Comma separated(CSV)
                    GenerateCSV(sb, list, amount, ',');
                    break;
                case 1: // Tab separated
                    GenerateCSV(sb, list, amount, '\t');
                    break;
                case 2: // Semi - colon separated
                    GenerateCSV(sb, list, amount, ';');
                    break;
                case 3: // SQL
                    GenerateSQL(sb, list, amount);
                    break;
                case 4: // XML
                    GenerateXML(sb, list, amount);
                    break;
                case 5: // JSON
                    GenerateJSON(sb, list, amount);
                    break;
            }

            return sb.ToString();
        }

        private static List<RandomValue> GetRandomValueColumns()
        {
            var list = new List<RandomValue>();
            for (var i = 1; i < 10; i++)
            {
                String def = "";

                if (i == 1) def = Main.settings.GenerateCol01;
                if (i == 2) def = Main.settings.GenerateCol02;
                if (i == 3) def = Main.settings.GenerateCol03;
                if (i == 4) def = Main.settings.GenerateCol04;
                if (i == 5) def = Main.settings.GenerateCol05;
                if (i == 6) def = Main.settings.GenerateCol06;
                if (i == 7) def = Main.settings.GenerateCol07;
                if (i == 8) def = Main.settings.GenerateCol08;
                if (i == 9) def = Main.settings.GenerateCol09;
                if (i == 10) def = Main.settings.GenerateCol10;

                if (def != "")
                {
                    list.Add(new RandomValue(def));
                }
            };
            return list;
        }

        private static void GenerateCSV(StringBuilder sb, List<RandomValue> list, int amount, char sep)
        {
            // column headers
            for (var r = 0; r < list.Count; r++)
            {
                // add column separator
                if (r > 0) sb.Append(sep);

                // next value
                var val = list[r].Description;

                // add quotes when value contains separator
                if (val.IndexOf(sep) >= 0) val = string.Format("\"{0}\"", val);

                sb.Append(val);
            }
            sb.Append("\r\n");

            // add values
            for (var i = 0; i < amount; i++)
            {
                for (var r = 0; r < list.Count; r++)
                {
                    // add column separator
                    if (r > 0)
                    {
                        sb.Append(sep);
                    };

                    // next value
                    var val = list[r].NextValue();

                    // add quotes when value contains separator
                    if (val.IndexOf(sep) >= 0) val = string.Format("\"{0}\"", val);

                    sb.Append(val);
                }
                sb.Append("\r\n");
            };
        }
        private static void GenerateSQL(StringBuilder sb, List<RandomValue> list, int amount)
        {
            bool SQLansi = Main.settings.SQLansi;
            var TableName = Main.settings.GenerateTablename;
            string recidname = "_record_number";

            sb.Append("-- -------------------------------------\r\n");
            sb.Append(string.Format("-- Notepad++ Random Values plug-in v{0}\r\n", Main.GetVersion()));
            sb.Append(string.Format("-- Date: {0}\r\n", DateTime.Now.ToString("dd-MMM-yyyy HH:mm")));
            sb.Append(string.Format("-- Records: {0}\r\n", amount));
            sb.Append(string.Format("-- SQL ANSI: {0}\r\n", (Main.settings.SQLansi ? "mySQL" : "MS-SQL")));
            sb.Append("-- -------------------------------------\r\n");
            sb.Append(string.Format("CREATE TABLE {0}(\r\n\t", TableName));

            if (Main.settings.SQLansi)
                sb.Append(string.Format("`{0}` int AUTO_INCREMENT NOT NULL,\r\n\t", recidname)); // mySQL
            else
                sb.Append(string.Format("[{0}] int IDENTITY(1,1) PRIMARY KEY,\r\n\t", recidname)); // MS-SQL
            var cols = "\t";

            for (var r = 0; r < list.Count; r++)
            {
                // determine sql column name
                string sqlname = string.Format((Main.settings.SQLansi ? "`{0}`" : "[{0}]"), list[r].Description);

                // determine sql datatype
                var sqltype = "varchar";
                if (list[r].DataType == RandomDataType.Integer) sqltype = "integer";
                if (list[r].DataType == RandomDataType.DateTime) sqltype = "datetime";
                if (list[r].DataType == RandomDataType.Guid) sqltype = "varchar(36)";
                if (list[r].DataType == RandomDataType.Decimal)
                {
                    sqltype = string.Format("numeric({0},{1})", list[r].Width, list[r].FloatDec);
                }
                // for SQL varchar format needs width
                if (list[r].DataType == RandomDataType.String)
                {
                    sqltype = string.Format("varchar({0})", list[r].Width);
                }
                // for SQL numeric format always use point decimals
                if (list[r].DataType == RandomDataType.Decimal)
                {
                    list[r].FloatFormat.NumberDecimalSeparator = ".";
                }
                // for SQL date format always needs to be ISO format
                if (list[r].DataType == RandomDataType.DateTime)
                {
                    string masknew = "";
                    if (list[r].Mask.IndexOf("yy") >= 0) masknew += "yyyy-MM-dd";
                    if (list[r].Mask.IndexOf("H") >= 0) masknew += " HH:mm";
                    if (list[r].Mask.IndexOf("s") >= 0) masknew += ":ss";
                    if (list[r].Mask.IndexOf("f") >= 0) masknew += ".fff";

                    list[r].Mask = masknew.Trim();
                }

                sb.Append(String.Format("{0} {1}", sqlname, sqltype));
                cols += sqlname;
                if (r < list.Count-1)
                {
                    sb.Append(",\r\n\t");
                    cols += ",\r\n\t";
                };
            };

            // primary key definition for mySQL
            if (Main.settings.SQLansi) sb.Append(string.Format(",\r\n\tprimary key(`{0}`)", recidname));

            sb.Append("\r\n);\r\n\r\n");

            var maxrec = 0;
            var MaxSQLrows = Main.settings.GenerateBatch;

            for (var i = 0; i < amount; i++)
            {
                if (i % MaxSQLrows == 0)
                {
                    maxrec = i + MaxSQLrows;
                    maxrec = (maxrec > amount ? amount : maxrec);

                    sb.Append("-- -------------------------------------\r\n");
                    sb.Append(String.Format("-- insert records {0} - {1}\r\n", (i+1), maxrec));
                    sb.Append("-- -------------------------------------\r\n");
                    sb.Append(string.Format("insert into {0}(\r\n", TableName));
                    sb.Append(cols);
                    sb.Append("\r\n) values");
                }

                sb.Append("\r\n(");
                for (var r = 0; r < list.Count; r++)
                {
                    if (r > 0)
                    {
                        sb.Append(", ");
                    };

                    // format next value, quotes for varchar and datetime
                    var str = list[r].NextValue();
                    if (str == "")
                    {
                        str = "NULL";
                    }
                    else
                    {
                        // quoted strings
                        if (   (list[r].DataType == RandomDataType.String)
                            || (list[r].DataType == RandomDataType.DateTime)
                            || (list[r].DataType == RandomDataType.Guid) ) str = string.Format("'{0}'", str);
                    }
                    sb.Append(str);
                };
                sb.Append(")");
                if (i < maxrec-1)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.Append(";\r\n\r\n");
                };
            };
        }

        private static string GetValidXMLTagName(string xmlkey)
        {
            // replace invalid XML characters with space
            var validTagName = "";
            foreach (char ch in xmlkey)
            {
                validTagName += (XmlConvert.IsNCNameChar(ch) ? ch : ' ');
            }

            // replace all spaces (also double/multiple spaces) with single underscore
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{1,}", options);
            validTagName = regex.Replace(validTagName, "_");

            // if starts with digit, add a '_' at start
            if (validTagName == "" || !XmlConvert.IsStartNCNameChar(validTagName[0]))
                validTagName = "_" + validTagName;

            return validTagName;
        }

        private static string GetValidXMLValue(String strval)
        {

            strval = strval.Replace("&", "&amp;");
            strval = strval.Replace(">", "&gt;");
            strval = strval.Replace("<", "&lt;");
            strval = strval.Replace("<", "&lt;");

            return strval;
        }

        private static void GenerateXML(StringBuilder sb, List<RandomValue> list, int amount)
        {
            // change to valid tagnames, outside of the amount-loop because it only needs to be done once per column description
            for (var r = 0; r < list.Count; r++)
            {
                list[r].Description = GetValidXMLTagName(list[r].Description);
            }

            sb.Append("<RandomValues>\r\n");

            sb.Append(string.Format("\t<!-- {0} random node objects -->\r\n", amount));
            for (var i = 0; i < amount; i++)
            {
                sb.Append(string.Format("\t<{0}>\r\n", Main.settings.GenerateTablename));
                for (var r = 0; r < list.Count; r++)
                {
                    var val = GetValidXMLValue(list[r].NextValue());

                    var str = string.Format("\t\t<{0}>{1}</{0}>\r\n", list[r].Description, val);
                    sb.Append(str);
                }
                sb.Append(string.Format("\t</{0}>\r\n", Main.settings.GenerateTablename));
            };
            sb.Append("</RandomValues>\r\n");
        }
        private static void GenerateJSON(StringBuilder sb, List<RandomValue> list, int amount)
        {

            // for JSON numeric format always use point decimals
            for (var r = 0; r < list.Count; r++)
            {
                if (list[r].DataType == RandomDataType.Decimal)
                {
                    list[r].FloatFormat.NumberDecimalSeparator = ".";
                }
            }

            // generate JSON header
            sb.Append("{\r\n");
            sb.Append("\t\"RandomDataGenerator\":{\r\n");
            sb.Append(string.Format("\t\t\"{0}\":[\r\n", Main.settings.GenerateTablename));

            for (var i = 0; i < amount; i++)
            {
                sb.Append("\t\t\t{");
                for (var r = 0; r < list.Count; r++)
                {
                    // format next value, not quotes for numbers
                    var str = list[r].NextValue();
                    if ((str == "") || ((list[r].DataType != RandomDataType.Decimal) && (list[r].DataType != RandomDataType.Integer))) str = string.Format("\"{0}\"", str);

                    // apend and comma 
                    var str2 = string.Format("\r\n\t\t\t\t\"{0}\": {1}", list[r].Description, str);
                    sb.Append(str2);
                    if (r < list.Count - 1)
                    {
                        sb.Append(",");
                    };
                }
                sb.Append("\r\n\t\t\t}");
                if (i < amount-1)
                {
                    sb.Append(",");
                };
                sb.Append("\r\n");
            };
            sb.Append("\t\t]\r\n");
            sb.Append("\t}\r\n");
            sb.Append("}\r\n");
        }
    }
}
