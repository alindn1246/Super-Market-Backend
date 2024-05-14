namespace HussieniSuperMarket.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        

        public string UserId { get; set; }

      

        public decimal TotalAmount { get; set; } = 0;
    }
}
