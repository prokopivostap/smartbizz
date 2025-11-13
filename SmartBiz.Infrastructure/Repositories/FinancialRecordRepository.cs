using System; 
using System.Collections.Generic;
using System.Linq;
using SmartBiz.Domain;
using SmartBiz.Application;
using SmartBiz.Application.DTO;

namespace SmartBiz.Infrastructure.Repositories
{
    public class FinancialRecordRepository : IFinancialRecordService
    {
        private readonly ApplicationDbContext _context;

        public FinancialRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFinancialRecord(FinanceDTO recordDto)
        {
            var record = new FinancialRecord
            {
                Data = DateTime.SpecifyKind(recordDto.Data, DateTimeKind.Utc),
                Income = (int)recordDto.Income,
                Type = recordDto.Type,
                Records = recordDto.Records,
                Currency = recordDto.Currency,
                Purpose = recordDto.Purpose,
                Sum = 0 
            };

            _context.FinancialRecords.Add(record);
            _context.SaveChanges();

            
            UpdateSumsByPurpose();
        }

        public FinanceDTO GetRecordById(int id)
        {
            var record = _context.FinancialRecords.Find(id);
            if (record == null) return null;

            return new FinanceDTO
            {
                Id = record.Id, 
                Data = record.Data,
                Income = record.Income,
                Type = record.Type,
                Records = record.Records,
                Currency = record.Currency,
                Purpose = record.Purpose,
                Sum = record.Sum 
            };
        }

        public IEnumerable<FinanceDTO> GetAllRecords()
        {
            
            UpdateSumsByPurpose();

            return _context.FinancialRecords.Select(record => new FinanceDTO
            {
                Id = record.Id,
                Data = record.Data,
                Income = record.Income,
                Type = record.Type,
                Records = record.Records,
                Currency = record.Currency,
                Purpose = record.Purpose,
                Sum = record.Sum 
            }).ToList();
        }

        public void DeleteRecord(int id)
        {
            var record = _context.FinancialRecords.Find(id);
            if (record != null)
            {
                _context.FinancialRecords.Remove(record);
                _context.SaveChanges();

                
                UpdateSumsByPurpose();
            }
        }

        public void UpdateRecord(FinanceDTO item)
        {
            var record = _context.FinancialRecords.FirstOrDefault(t => t.Id == item.Id);
            if (record is null) return;

            record.Data = DateTime.SpecifyKind(item.Data, DateTimeKind.Utc);
            record.Income = (int)item.Income;
            record.Type = item.Type;
            record.Records = item.Records;
            record.Currency = item.Currency;
            record.Purpose = item.Purpose;

            _context.SaveChanges();

            
            UpdateSumsByPurpose();
        }

        
        private void UpdateSumsByPurpose()
        {
            var grouped = _context.FinancialRecords
                .GroupBy(r => r.Purpose)
                .Select(g => new
                {
                    Purpose = g.Key,
                    TotalSum = g.Sum(r => (decimal)r.Income)
                })
                .ToList();

            foreach (var group in grouped)
            {
                var records = _context.FinancialRecords.Where(r => r.Purpose == group.Purpose).ToList();
                foreach (var record in records)
                {
                    record.Sum = group.TotalSum;
                }
            }

            _context.SaveChanges();
        }
    }
}
