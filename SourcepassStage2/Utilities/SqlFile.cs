namespace SourcepassStage2.Utilities
{
    public class SqlFile
    {
        public String _filePath = String.Empty;
        public String _query = String.Empty;


        public String sqlQuery
        {
            get 
            {
                return this._query;
            }
            set
            {
                try
                {
                    this._filePath = value;

                    string startupPath = Environment.CurrentDirectory;
                    //var path = startupPath.Remove(startupPath.Length - 14, 14);

                    String filePath = Path.Combine(startupPath, this._filePath.Replace("/","\\"));
                    if (File.Exists(filePath))
                    {
                        this._query = File.ReadAllText(filePath);
                    }
                    else
                    {
                        this.sqlQuery = "Blank";
                    }
                }
                catch (Exception ex)
                { 
                
                }
            }
        
        }


        public void setParameter(String parameter, String value)
        {
            this._query = this._query.Replace("@" + parameter, value);
        }

       

    }
}
