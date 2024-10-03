namespace WeddingPlanningReport.Models.ViewModel
{
    public class OrdersDetailsViewModel
    {

        List<CakeOrder> CakeOrders { get; set; }
        public List<MailViewModel> MailViewModels { get; set; }
        public int VenuesTotal { get; set; }
        public int CakesTotal { get; set; }
        public int DishesTotal { get; set; }
        public int CarsTotal { get; set; }
        public int VenueMonthOrdersTotal { get; set; }
        public int CakeMonthOrdersTotal { get; set; }
        public int DishesMonthOrdersTotal { get; set; }
        public int CarMonthOrdersTotal { get; set; }
        public int OrdersTotal { get; set; }
        public int MemberNumber { get; set; }
        public int preferC { get; set; }
        public int preferW { get; set; }
        public int lastTwoMonthMember { get; set; }
        public int lastMonthMember { get; set; }
        public int thisMonthMember { get; set; }

    }
}