
namespace RandomValuesNppPlugin
{
    public class RandomValueListItem
    {
        public string Description { get; set; }
        public RandomDataType DataType { get; set; }
        public string Mask { get; set; }
        public string Range { get; set; }
        public string Options { get; set; }

        public RandomValueListItem(string description, string datatype, string mask, string range, string options)
        {
            Description = description;
            datatype = datatype.ToLower();
            DataType = (datatype == "integer"  ? RandomDataType.Integer  :
                       (datatype == "decimal"  ? RandomDataType.Decimal  :
                       (datatype == "datetime" ? RandomDataType.DateTime :
                       (datatype == "guid"     ? RandomDataType.Guid     : RandomDataType.String))));
            Mask = mask;
            Range = range;
            Options = options;
        }
    }
}
