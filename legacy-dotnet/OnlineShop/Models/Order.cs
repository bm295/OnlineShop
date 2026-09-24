namespace OnlineShop.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    Completed = 1,
    Cancelled = 2
}
