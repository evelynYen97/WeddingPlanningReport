namespace WeddingPlanningReport.Models.ViewModel
{
    internal class OrdersDetailsViewModel
    {
        List<CakeOrder> CakeOrders { get; set; }


        public int? VenueOrdersTotal { get; set; }
        public int? CakeOrdersTotal { get; set; }
        public int? DishesOrdersTotal { get; set; }
        public int? CarOrdersTotal { get; set; }
        public int? OtherOrdersTotal { get; set; }
        public int? OrdersTotal { get; set; }
    }
}