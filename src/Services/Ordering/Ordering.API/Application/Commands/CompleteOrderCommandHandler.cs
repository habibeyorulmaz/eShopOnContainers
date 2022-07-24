namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;

// Regular CommandHandler
public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public CompleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handler which processes the command when
    /// customer executes complete order from app
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<bool> Handle(CompleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetAsync(command.OrderNumber);
        if (orderToUpdate == null)
        {
            return false;
        }

        // Set CompletionDate to holds the time order completed
        orderToUpdate.SetOrderCompletionDate();

        //Set Status to Completed and Event is triggered when completion is finished
        orderToUpdate.SetCompletedStatus();

        
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}


// Use for Idempotency in Command process
public class CompleteOrderIdentifiedCommandHandler : IdentifiedCommandHandler<CompleteOrderCommand, bool>
{
    public CompleteOrderIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CompleteOrderCommand, bool>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override bool CreateResultForDuplicateRequest()
    {
        return true;                // Ignore duplicate requests for processing order.
    }
}