namespace lab02
{
    public class Job
    {
        public string Company { get; set; }
        public string Post { get; set; }
        public string Experience { get; set; }
        
        public Job()
        {
            
        }
        public Job(string Company,string Post,string Experience)
        {
            this.Company = Company;
            this.Post = Post;
            this.Experience = Experience;
        }
        public override string ToString()
        {
            string str = "";
            str = $"Компания:{Company}\nДолжность:{Post}\nОпыт:{Experience}";
            return str;
        }
    }
}