using System.Data;

namespace ProjectMS.Service
{
    public class CommonFuncService : ICommonFuncService
    {
        public DataTable ConvertListToDataTable<T>(IEnumerable<T> data)
        {
            if (data == null)
                return new DataTable();

            var properties = typeof(T).GetProperties();
            var dataTable = new DataTable(typeof(T).Name);

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var item in data)
            {
                var row = dataTable.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item, null);
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}