using SourcepassStage2.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace SourcepassStage2.Repository
{
    public abstract class BaseRepository
    {
        public SqlFile queryFile = new SqlFile();


        public BaseRepository()
        {
            Dapper.SqlMapper.Settings.CommandTimeout = 0;
        }


        IDbConnection _connection;
        internal abstract string Tablename { get; }


        internal IDbConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    var source = new HardCoded();
                    var connectionString = source.connectionString;
                    _connection = new SqlConnection(connectionString);

                   
                }

                return _connection;
            }

        }

    }
}
