namespace SprinklerWinterizer.Models
{
    public class Person : BaseItem
    {
        public string fullName { get; set; }
        public string email { get; set; }
        public Device[] devices { get; set; }
    }
}
