namespace BillProtocol.Models
{
    public class Constants
    {
        // Invoice Types
        public const int RegularInvoice = 1;
        public const int PaymentToDateInvoice = 2;
        public const int EscrowInvoice = 3;

        // Invoice Status
        public const int CreatedInvoiceStatus = 1;
        public const int RejectedInvoiceStatus = 2;
        public const int ApprovedInvoiceStatus = 3;
        public const int PayedInvoiceStatus = 4;

        // Currencies
        public const int XRPCurrency = 1;
        public const int USDCurrency = 2;
        public const int EURCurrency = 3;
        public const int GBPCurrency = 4;
    }
}
