namespace InvoiceManager
{
    public interface IInvoiceRepository
    {
        void Update(List<Invoice> balancedInvoices);
    }
}