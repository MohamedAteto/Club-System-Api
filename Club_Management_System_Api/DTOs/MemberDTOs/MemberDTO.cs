using Club_Management_System_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Club_Management_System_Api.DTOs.MemberDTOs
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string MemberFullName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public DateTime MemberBirthDate { get; set; }

    }
}
