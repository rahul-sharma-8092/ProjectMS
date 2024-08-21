using System.Data;

namespace ProjectMS.Service
{
    public interface ICommonFuncService
    {
        DataTable ConvertListToDataTable<T>(IEnumerable<T> data);
    }
}
