namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToCompletedIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string OrderStatus { get; }
    public string BuyerName { get; }
    public DateTime CompletionDate { get; }

    public OrderStatusChangedToCompletedIntegrationEvent(int orderId, string orderStatus, string buyerName, DateTime completionDate)
    {
        OrderId = orderId;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
        CompletionDate = completionDate;
    }
}