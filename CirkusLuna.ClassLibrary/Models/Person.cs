// Ansvarlig: Onur
namespace CirkusLuna.ClassLibrary.Models
{
    // abstrakt så man ikke kan oprette en Person direkte
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
