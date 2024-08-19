using System.Data;

namespace ProjectMS.Common
{
    public static class CommonFunc
    {
        public static DataTable ConvertListToDataTable<T>(this IEnumerable<T> data)
        {
            if (data == null)
                return new DataTable();

            var properties = typeof(T).GetProperties();
            var table = new DataTable(typeof(T).Name);

            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item, null);
                }
                table.Rows.Add(row);

            }

            return table;
        }
    }
}
