using System.ComponentModel.DataAnnotations;

namespace WebApplication10.Modal
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employes> Employes { get; set; }

    }
}
