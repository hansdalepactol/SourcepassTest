using Dapper;
using SourcepassStage2.Models;

namespace SourcepassStage2.Repository
{
    public class UserRepository : BaseRepository
    {
        internal override string Tablename => "tblUser";

        public User GetUserByID(int userID)
        {
            queryFile.sqlQuery = "SqlScripts/GetUser.sql";
            queryFile.setParameter("_userID",$"{userID}");

            return connection.Query<User>(queryFile.sqlQuery).FirstOrDefault();
        }

    }
}
