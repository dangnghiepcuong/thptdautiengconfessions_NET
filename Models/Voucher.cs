namespace THPTDTCFS.Models
{
    public class Voucher
    {
        public string VoucherCode { get; set; }
        public int ShopId { get; set; } 
        public string VoucherDescription { get; set;}
        public int Redemption { get; set;}
        public bool ActiveStatus { get; set; }
    }
}
