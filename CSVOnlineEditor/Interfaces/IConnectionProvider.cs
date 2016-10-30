using System.Data;
using System.Data.Common;

namespace CSVOnlineEditor.Interfaces
{
    public interface IConnectionProvider
    {
        DbConnection GetConnection();
    }
}
