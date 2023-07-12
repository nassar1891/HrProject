using HrProject.Models;
using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
    public class VacationsViewModel
    {
        [Required(ErrorMessage = "please Enter the name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "please Enter the Date")]
        [DataType(DataType.Date, ErrorMessage = "please Enter the Right Date")]
        public DateTime? Date  { get; set; }

        public List<Vacations>? offvac { get; set; }
    }
}
