namespace BillProtocol.Models.RequestModel
{
    public class IndexRequestFormModel
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? invoiceType { get; set; }
        public int? currency { get; set; }
        public Guid? wallet { get; set; }
        public int? status { get; set; }

    }
}
