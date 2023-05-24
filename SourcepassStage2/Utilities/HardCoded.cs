namespace SourcepassStage2.Utilities
{
    public class HardCoded
    {
        public String iPAddress;
        public String connectionString;


        public HardCoded()
        {
            iPAddress = "localhost";
            connectionString = "Data Source=" + iPAddress + ";Database=Sourcepass;User ID=sa;Password=sa";
        }


    }
}
