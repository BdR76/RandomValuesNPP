using System;
using System.Collections.Generic;
using System.Text;

namespace RandomValuesNppPlugin
{
    class RandomValues
    {
        private const int MAX_SQL_ROWS = 1000;
        private const string TABLE_NAME = "TableName";

        public static String Generate(List<RandomValue> list, int outputtype, int amount)
        {

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

        private static void GenerateCSV(StringBuilder sb, List<RandomValue> list, int amount, char sep)
        {
            for (var i = 0; i < amount; i++)
            {
                for (var r = 0; r < list.Count; r++)
                {
                    if (r > 0)
                    {
                        sb.Append(sep);
                    };
                    sb.Append(list[r].NextValue());
                }
                sb.Append("\r\n");
            };
        }
        private static void GenerateSQL(StringBuilder sb, List<RandomValue> list, int amount)
        {

            sb.Append("-- -------------------------------------\r\n");
            sb.Append("-- Random Values plug-in for Notepad++ v0.1\r\n");
            sb.Append("-- -------------------------------------\r\n");
            sb.Append(string.Format("CREATE TABLE {0}(\r\n\t", TABLE_NAME));
            var cols = "\t";

            for (var r = 0; r < list.Count; r++)
            {
                // determine sql column name
                var sqlname = '[' + list[r].Description + ']';

                // determine sql datatype
                var sqltype = "varchar";
                if (list[r].DataType == RandomDataType.Integer) sqltype = "integer";
                if (list[r].DataType == RandomDataType.DateTime) sqltype = "datetime";
                if (list[r].DataType == RandomDataType.Guid) sqltype = "varchar(36)";
                if (list[r].DataType == RandomDataType.Decimal)
                {
                    sqltype = string.Format("numeric({0},{1})", list[r].Width, list[r].FloatDec);
                }
                // for SQL date format always needs to be ISO format
                if (list[r].DataType == RandomDataType.String)
                {
                    sqltype = string.Format("varchar({0})", list[r].Width);
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

            sb.Append("\r\n)");
            sb.Append("\r\ngo\r\n");

            var maxrec = MAX_SQL_ROWS;

            for (var i = 0; i < amount; i++)
            {
                if (i % MAX_SQL_ROWS == 0)
                {
                    maxrec = i + MAX_SQL_ROWS;
                    maxrec = (maxrec > amount ? amount : maxrec);

                    sb.Append("-- -------------------------------------\r\n");
                    sb.Append(String.Format("-- insert records {0} - {1}\r\n", (i+1), maxrec));
                    sb.Append("-- -------------------------------------\r\n");
                    sb.Append(string.Format("insert into {0}(\r\n", TABLE_NAME));
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
                    sb.Append("\r\ngo\r\n");
                };
            };
        }
        private static void GenerateXML(StringBuilder sb, List<RandomValue> list, int amount)
        {
            sb.Append("<RandomValues>\r\n");
            for (var i = 0; i < amount; i++)
            {
                sb.Append(string.Format("\t<{0}>\r\n", TABLE_NAME));
                for (var r = 0; r < list.Count; r++)
                {
                    var str = string.Format("\t\t<{0}>{1}</{0}>\r\n", list[r].Description, list[r].NextValue());
                    sb.Append(str);
                }
                sb.Append(string.Format("\t</{0}>\r\n", TABLE_NAME));
            };
            sb.Append("</RandomValues>\r\n");
        }
        private static void GenerateJSON(StringBuilder sb, List<RandomValue> list, int amount)
        {
            sb.Append("{\r\n");
            sb.Append("\t\"RandomDataGenerator\":{\r\n");
            sb.Append(string.Format("\t\t\"{0}\":[\r\n", TABLE_NAME));

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
