namespace FreelancerModStudio.Data.INI
{
    public class IniOption
    {
        public string Value;
        public string Parent; // used to save nested options in correct order
        public int Index; // used to load nested options in correct order
    }
}