namespace WeddingPlanningReport.Models.ViewModel
{
    public class OrdersDetailsViewModel
    {
        List<CakeOrder> CakeOrders { get; set; }

        public int VenuesTotal { get; set; }
        public int CakesTotal { get; set; }
        public int DishesTotal { get; set; }
        public int CarsTotal { get; set; }
        public int VenueOrdersTotal { get; set; }
        public int CakeOrdersTotal { get; set; }
        public int DishesOrdersTotal { get; set; }
        public int CarOrdersTotal { get; set; }
        public int OtherOrdersTotal { get; set; }
        public int OrdersTotal { get; set; }
        public int MemberNumber { get; set; }

        public int preferC { get; set; }
        public int preferW { get; set; }

        public int lastTwoMonthMember { get; set; }
        public int lastMonthMember { get; set; }
        public int thisMonthMember { get; set; }

    }
}