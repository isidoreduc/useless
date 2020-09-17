namespace Core.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {
        //id prop comes from BaseEntity
        public string ShortName { get; set; }
        public string DeliveryDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}