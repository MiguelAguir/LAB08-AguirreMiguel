namespace LAB09_AguirreMiguel.DTOs
{
    public class ClientOrdersDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<OrderDto> Orders { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

    public class ClientTotalProductsDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int TotalProducts { get; set; }
    }

    public class ClientSalesDto
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public decimal TotalSales { get; set; }
    }
}