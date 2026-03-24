namespace BuildingBlock.CQRS;

public class ICommand
{
}


public interface ICommand<TResponse> : IRequest<TResponse>
{
}