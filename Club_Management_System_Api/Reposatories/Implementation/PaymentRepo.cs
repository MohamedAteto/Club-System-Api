using System.Collections.Immutable;
using Club_Management_System_Api.Data;
using Club_Management_System_Api.DTOs.PaymentDTOs;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class PaymentRepo : GenericRepo<Payment>, IPaymentRepo
    {
        private readonly AppDbContext _context;
        public PaymentRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PaymentStatisticsDTO>> GetAllPaymentsAsync()
        {
            var payments = await _context.Payments
                .Select(p => new PaymentStatisticsDTO
                {
                    MaxAmount = _context.Payments.Max(x => x.PaymentAmount),
                    MinAmount = _context.Payments.Min(x => x.PaymentAmount),
                    TotalSum = _context.Payments.Sum(x => x.PaymentAmount),
                    TotalPaymentsCount = _context.Payments.Count()
                }).ToListAsync();

            return payments;

        }
    }
}
