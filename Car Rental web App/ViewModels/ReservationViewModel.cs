using System.ComponentModel.DataAnnotations;

namespace Car_Rental_web_App.ViewModels
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Car Id is required.")]

        public int CarId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

    }
}
