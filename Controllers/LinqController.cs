using Microsoft.AspNetCore.Mvc;
using LAB08_AguirreMiguel.Repositories;
using LAB08_AguirreMiguel.Data;
using System.Linq;

[ApiController]
[Route("api/linq")]
public class LinqController : ControllerBase
{
    private readonly IRepository<Client> _clientRepo;
    private readonly IRepository<Product> _productRepo;
    private readonly IRepository<Order> _orderRepo;
    private readonly IRepository<OrderDetail> _orderDetailRepo;

    public LinqController(
        IRepository<Client> clientRepo,
        IRepository<Product> productRepo,
        IRepository<Order> orderRepo,
        IRepository<OrderDetail> orderDetailRepo)
    {
        _clientRepo = clientRepo;
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _orderDetailRepo = orderDetailRepo;
    }
    [HttpGet("clients")]
    public IActionResult GetClientsByName(string name)
    {
        var clients = _clientRepo.GetAll()
            .Where(c => c.Name.StartsWith(name))
            .ToList();
        return Ok(clients);
    }
    [HttpGet("products")]
    public IActionResult GetProductsByPrice(decimal price)
    {
        var products = _productRepo.GetAll()
            .Where(p => p.Price > price)
            .ToList();
        return Ok(products);
    }
    [HttpGet("order-details")]
    public IActionResult GetOrderDetails(int orderId)
    {
        var details = _orderDetailRepo.GetAll()
            .Where(od => od.OrderId == orderId)
            .Select(od => new { ProductName = od.Product.Name, od.Quantity })
            .ToList();
        return Ok(details);
    }
    [HttpGet("order-quantity")]
    public IActionResult GetOrderTotalQuantity(int orderId)
    {
        var total = _orderDetailRepo.GetAll()
            .Where(od => od.OrderId == orderId)
            .Sum(od => od.Quantity);
        return Ok(total);
    }
    [HttpGet("most-expensive-product")]
    public IActionResult GetMostExpensiveProduct()
    {
        var product = _productRepo.GetAll()
            .OrderByDescending(p => p.Price)
            .FirstOrDefault();
        return Ok(product);
    }
    [HttpGet("orders-after-date")]
    public IActionResult GetOrdersAfterDate(DateTime date)
    {
        var orders = _orderRepo.GetAll()
            .Where(o => o.OrderDate > date)
            .ToList();
        return Ok(orders);
    }
    [HttpGet("average-product-price")]
    public IActionResult GetAverageProductPrice()
    {
        var average = _productRepo.GetAll()
            .Average(p => p.Price);
        return Ok(average);
    }
    [HttpGet("products-without-description")]
    public IActionResult GetProductsWithoutDescription()
    {
        var products = _productRepo.GetAll()
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToList();
        return Ok(products);
    }
    [HttpGet("client-with-most-orders")]
    public IActionResult GetClientWithMostOrders()
    {
        var clientOrders = _orderRepo.GetAll()
            .GroupBy(o => o.ClientId)
            .Select(g => new { ClientId = g.Key, Count = g.Count() })
            .ToList();
        var maxOrders = clientOrders.Max(co => co.Count);
        var topClients = clientOrders
            .Where(co => co.Count == maxOrders)
            .Select(co => new
            {
                Client = _clientRepo.GetById(co.ClientId),
                OrdersCount = co.Count
            })
            .ToList();

        return Ok(new { MaxOrders = maxOrders, TopClients = topClients });
    }
    [HttpGet("all-orders-details")]
    public IActionResult GetAllOrdersDetails()
    {
        var details = _orderDetailRepo.GetAll()
            .Select(od => new { OrderId = od.OrderId, ProductName = od.Product.Name, Quantity = od.Quantity })
            .ToList();
        return Ok(details);
    }
    [HttpGet("products-by-client")]
    public IActionResult GetProductsByClient(int clientId)
    {
        var products = _orderRepo.GetAll()
            .Where(o => o.ClientId == clientId)
            .SelectMany(o => o.OrderDetails)
            .Select(od => od.Product.Name)
            .Distinct()
            .ToList();
        return Ok(products);
    }
    [HttpGet("clients-by-product")]
    public IActionResult GetClientsByProduct(int productId)
    {
        var clients = _orderDetailRepo.GetAll()
            .Where(od => od.ProductId == productId)
            .Select(od => od.Order.Client.Name)
            .Distinct()
            .ToList();
        return Ok(clients);
    }
}
