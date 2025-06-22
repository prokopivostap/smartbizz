using SmartBiz.Application.DTO;
public interface IFinancialRecordService
{
    void AddFinancialRecord(FinanceDTO record);
    FinanceDTO GetRecordById(int id);
    IEnumerable<FinanceDTO> GetAllRecords();
    void UpdateRecord(FinanceDTO record);
    void DeleteRecord(int id);
}