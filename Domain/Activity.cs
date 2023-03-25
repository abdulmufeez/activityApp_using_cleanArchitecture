namespace Domain
{
    public class Activity
    {
        public Guid Id { get; set; }   
        public string Title { get; set; }     
        public DateTime Date { get; set; }
        public string Desc { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
    }
}