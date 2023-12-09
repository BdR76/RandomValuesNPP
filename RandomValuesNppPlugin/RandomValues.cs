using Kbg.NppPluginNET;
using System;
using System.Collections.Generic;
using System.Linq;
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
            for (var i = 1; i <= 10; i++)
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

        public static List<String> ScriptInfo(int amount)
        {
            List<String> list = new List<String>();

            list.Add(string.Format("Notepad++ Random Values plug-in v{0}", Main.GetVersion()));
            list.Add(string.Format("Generate records: {0}", amount));
            list.Add(string.Format("Date: {0}", DateTime.Now.ToString("dd-MMM-yyyy HH:mm")));

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
            int SQLansi = Main.settings.SQLtype;
            var TABLE_NAME = Main.settings.GenerateTablename;
            string recidname = "_record_number";
            string SQL_TYPE = (Main.settings.SQLtype <= 1 ? (Main.settings.SQLtype == 0 ? "mySQL" : "MS-SQL") : "PostgreSQL");

            // default comment
            sb.Append("-- -------------------------------------\r\n");
            List<String> comment = ScriptInfo(amount);
            foreach (var str in comment) sb.Append(string.Format("-- {0}\r\n", str));
            sb.Append(string.Format("-- SQL type: {0}\r\n", SQL_TYPE));
            sb.Append("-- -------------------------------------\r\n");
            sb.Append(string.Format("CREATE TABLE {0}(\r\n\t", TABLE_NAME));

            switch (Main.settings.SQLtype)
            {
                case 1:
                    sb.Append(string.Format("[{0}] int IDENTITY(1,1) PRIMARY KEY,\r\n\t", recidname)); // MS-SQL
                    break;
                case 2:
                    sb.Append(string.Format("\"{0}\" SERIAL PRIMARY KEY,\r\n\t", recidname)); // PostgreSQL
                    break;
                default: // 0=mySQL
                    sb.Append(string.Format("`{0}` int AUTO_INCREMENT NOT NULL,\r\n\t", recidname)); // mySQL
                    break;
            }
            var cols = "\t";
            var enumcols1 = "";
            var enumcols2 = "";

            for (var r = 0; r < list.Count; r++)
            {
                // determine sql column name -> mySQL = `colname`, MS-SQL = [colname], PostgreSQL = "colname"
                string sqlname = SQLSafeName(list[r].Description);

                // determine sql datatype
                var sqltype = "varchar";
                if (list[r].DataType == RandomDataType.Integer) sqltype = "integer";
                if (list[r].DataType == RandomDataType.DateTime) sqltype = (Main.settings.SQLtype < 2 ? "datetime" : "timestamp"); // mySQL/MS-SQL = datetime, PostgreSQL=timestamp
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

                // build SQL for Enum columns
                if ( (list[r].DataType == RandomDataType.String) && (list[r].ListRange != null) )
                {
                    var enumvals = string.Join("\", \"", list[r].ListRange.Distinct()).Replace("'", "''");
                    switch (Main.settings.SQLtype)
                    {
                        case 1: // MS-SQL
                                //case 2: // PostgreSQL
                                // constrains name is based on column name, for example "[test]" -> "[CHK_test]"
                            var chkname = SQLSafeName("CHK_" + list[r].Description);
                            var mscolate = "";
                            // Constrains for string or integer values
                            if (list[r].DataType == RandomDataType.String)
                            {
                                enumvals = string.Format("'{0}'", enumvals.Replace("\"", "'")); // use quotes
                                if (Main.settings.SQLtype == 1) mscolate = " COLLATE Latin1_General_CS_AS"; // MS-SQL case-sensitive
                            }
                            else
                            {
                                enumvals = enumvals.Replace("\"", ""); // no quotes
                            };
                            enumcols1 += string.Format("ALTER TABLE {0} ADD CONSTRAINT {1} CHECK({2}{3} IN ({4}));\r\n", TABLE_NAME, chkname, sqlname, mscolate, enumvals);
                            break;
                        // NOTE: Could also use CONSTRAINT..CHECK for both MS-SQL and PostgreSQL,
                        // even though PostgreSQL supports custom enum TYPE but it is less flexible than just CONSTRAINT..CHECK
                        case 2: // PostgreSQL
                            var postenum = SQLSafeName("enum_" + list[r].Description);
                            enumcols1 += string.Format("CREATE TYPE {0} AS ENUM ('{1}');\r\n", postenum, enumvals.Replace("\"", "'"));
                            enumcols2 += string.Format("ALTER TABLE {0} ALTER COLUMN {1} TYPE {2} USING ({1}::text)::{2};\r\n", TABLE_NAME, sqlname, postenum);
                            // for PostgreSQL, insert statements on ENUM column must always use quotes, so ('0', '1', '2') instead of (0, 1, 2)
                            if (list[r].DataType == RandomDataType.Integer) list[r].DataType = RandomDataType.String;
                            break;
                        default: // 0=mySQL
                            enumvals = enumvals.Replace("\"", "'"); // ENUM on mySQL is always treated as string value
                            enumcols1 += string.Format("ALTER TABLE {0} MODIFY COLUMN {1} ENUM('{2}');\r\n", TABLE_NAME, sqlname, enumvals);
                            // for mySQL, insert statements on ENUM column must always use quotes, so ('0', '1', '2') instead of (0, 1, 2)
                            if (list[r].DataType == RandomDataType.Integer) list[r].DataType = RandomDataType.String;
                            break;
                    }
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
            if (Main.settings.SQLtype == 0) sb.Append(string.Format(",\r\n\tprimary key(`{0}`)", recidname));

            sb.Append("\r\n);\r\n");

            // add enumeration columns
            if (enumcols1 != "") sb.Append(string.Format("-- Enumeration columns (optional)\r\n/*\r\n{0}{1}*/\r\n", enumcols1, enumcols2));

            // add comment table
            var tabcomment = string.Join("\r\n", comment).Replace("'", "''");
            sb.Append("-- Table comment\r\n");
            switch (Main.settings.SQLtype)
            {
                case 1:
                    sb.Append(string.Format("EXEC sp_addextendedproperty 'Comment', N'{1}', N'SCHEMA', DBO, N'TABLE', {0}\r\nGO\r\n", TABLE_NAME, tabcomment)); // MS-SQL
                    //sb.Append(string.Format("EXEC sys.sp_addextendedproperty @name = N'comment', @value = N'{0}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'{1}'\r\nGO\r\n", coment, tablemmaf)); // MS-SQL alt
                    break;
                case 2:
                    sb.Append(string.Format("COMMENT ON TABLE {0} IS '{1}';\r\n", TABLE_NAME, tabcomment)); // PostgreSQL
                    break;
                default: // 0=mySQL
                    sb.Append(string.Format("ALTER TABLE {0} COMMENT '{1}';\r\n", TABLE_NAME, tabcomment)); // mySQL
                    break;
            }

            var maxrec = 0;
            var MaxSQLrows = Main.settings.GenerateBatch;

            for (var i = 0; i < amount; i++)
            {
                if (i % MaxSQLrows == 0)
                {
                    maxrec = i + MaxSQLrows;
                    maxrec = (maxrec > amount ? amount : maxrec);

                    sb.Append("\r\n-- -------------------------------------\r\n");
                    sb.Append(String.Format("-- insert records {0} - {1}\r\n", (i+1), maxrec));
                    sb.Append("-- -------------------------------------\r\n");
                    sb.Append(string.Format("INSERT INTO {0}(\r\n", TABLE_NAME));
                    sb.Append(cols);
                    sb.Append("\r\n) VALUES");
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

        private static string SQLSafeName(string sqlname)
        {
            var res = sqlname;

            // use brackets or quotes only when absolutely necessary
            if (res.Contains(" ") || res.Contains("'"))
            {
                if (Main.settings.SQLtype == 1) // MS-SQL
                    res = string.Format("[{0}]", res);
                else
                    res = string.Format("{1}{0}{1}", res, (Main.settings.SQLtype == 0 ? "`" : "\""));
            }

            return res;
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

            // XML root node
            sb.Append("<RandomValues>\r\n");

            // default comment
            sb.Append("\t<!--\r\n");
            List<String> comment = ScriptInfo(amount);
            foreach (var str in comment) sb.Append(string.Format("\t{0}\r\n", str));
            sb.Append("\t-->\r\n");

            // random XML values
            for (var i = 0; i < amount; i++)
            {
                sb.Append(string.Format("\t<{0}>\r\n", Main.settings.GenerateTablename));
                for (var r = 0; r < list.Count; r++)
                {
                    var val = GetValidXMLValue(list[r].NextValue());

                    if (val == "")
                        sb.Append(string.Format("\t\t<{0}/>\r\n", list[r].Description));
                    else
                        sb.Append(string.Format("\t\t<{0}>{1}</{0}>\r\n", list[r].Description, val));
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
