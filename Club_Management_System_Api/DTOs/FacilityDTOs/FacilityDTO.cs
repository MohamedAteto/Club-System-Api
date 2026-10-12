using System.ComponentModel.DataAnnotations;

namespace Club_Management_System_Api.DTOs.FacilityDTOs
{
    public class FacilityDTO
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public int FacilityCapacity { get; set; }
        public decimal FacilityHourlyPrice { get; set; }

    }
}
