using Moq;

namespace InvoiceManager.Tests;

public class InvoiceTest
{
    private Mock<IInvoiceRepository> _mockInvoiceRepository;
    private InvoiceService _invoiceService;
    private List<Invoice> _invoiceList; 
    public InvoiceTest()
    {
        _mockInvoiceRepository = new Mock<IInvoiceRepository>();
        _invoiceService = new InvoiceService(_mockInvoiceRepository.Object);
        _invoiceList = new List<Invoice>()
        {
            new Invoice() {IsBalanced = true},
            new Invoice() {IsBalanced = false},
            new Invoice() {IsBalanced = true},
            new Invoice() {IsBalanced = false},
        };
    }

    [Fact]
    public void UpdateInvoices_EmptyList_ReturnsZero()
    {
        //Act
        var count = _invoiceService.UpdateInvoices(new List<Invoice>());
        //Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void UpdateInvoices_CheckCount_ReturnsTwo()
    {
        //Act
        var count = _invoiceService.UpdateInvoices(_invoiceList);
        //Assert
        Assert.Equal(2, count);
    }

    [Fact]
    public void UpdateInvoices_UsingCorrectList_ShouldRunOnce()
    {
        //Arrange
        List<Invoice> actual = new();
        var balancedList = _invoiceList.Select(i => i).Where(i => i.IsBalanced == true).ToList();
        //Act
        _invoiceService.UpdateInvoices(_invoiceList);
        //Assert
        _mockInvoiceRepository.Verify(t =>t.Update(balancedList));
    }

    [Fact]
    public void UpdateInvoices_UsingInCorrectList_ShouldRunZeroTimes()
    {
        //Arrange
        List<Invoice> actual = new();
        var balancedList = _invoiceList.Select(i => i).Where(i => i.IsBalanced == true).ToList();
        //Act
        _invoiceService.UpdateInvoices(_invoiceList);
        //Assert
        _mockInvoiceRepository.Verify(t => t.Update(_invoiceList),Times.Never);
    }

    [Fact]
    public void UpdateInvoices_UsingCorrectListWithCallBack_ShouldReturnTrue()
    {
        //Arrange
        var balancedList = _invoiceList.Where(i => i.IsBalanced == true).ToList();
        List<Invoice> actual = null;
        _mockInvoiceRepository.Setup(s => s.Update(It.IsAny<List<Invoice>>()))
            .Callback<List<Invoice>>(i => actual = i);

        //Act
        _invoiceService.UpdateInvoices(_invoiceList);

        //Assert
        Assert.Equal(balancedList, actual);
    }

}