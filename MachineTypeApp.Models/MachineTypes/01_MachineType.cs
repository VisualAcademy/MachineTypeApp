using System.ComponentModel.DataAnnotations;

namespace MachineTypeApp.Models.MachineTypes
{
    public class MachineType
    {
        public int Id { get; set; }

        // PM> Install-Package System.ComponentModel.Annotations
        [Required]
        public string Name { get; set; }
    }
}
