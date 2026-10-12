using Club_Management_System_Api.DTOs.PaymentDTOs;
using Club_Management_System_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IPaymentRepo : IGenericRepo<Payment> 
    {
        Task<IEnumerable<PaymentStatisticsDTO>> GetAllPaymentsAsync();

    }
}
