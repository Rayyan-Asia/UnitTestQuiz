namespace InvoiceManager;
public class InvoiceService
{
    private readonly IInvoiceRepository _invoiceRepo;
    public InvoiceService(IInvoiceRepository invoiceRepo)
    {
        _invoiceRepo = invoiceRepo;

    }

    public int UpdateInvoices(List<Invoice> invoices)
    {
        var balancedInvoices = invoices.Where(i => i.IsBalanced).ToList();
        _invoiceRepo.Update(balancedInvoices);
        return balancedInvoices.Count();
    }
}