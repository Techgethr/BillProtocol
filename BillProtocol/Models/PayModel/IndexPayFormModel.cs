namespace BillProtocol.Models.PayModel
{
    public class IndexPayFormModel
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? invoiceType { get; set; }
        public int? currency { get; set; }
        public int? status { get; set; }
    }
}
