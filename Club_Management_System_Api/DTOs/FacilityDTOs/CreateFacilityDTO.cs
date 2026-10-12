namespace Club_Management_System_Api.DTOs.FacilityDTOs
{
    public class CreateFacilityDTO
    {
        public string FacilityName { get; set; }
        public int FacilityCapacity { get; set; }
        public decimal FacilityHourlyPrice { get; set; }
    }
}
