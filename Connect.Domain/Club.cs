namespace Connect.Domain
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Event> Events { get; set; } = new List<Event>();
    }
}