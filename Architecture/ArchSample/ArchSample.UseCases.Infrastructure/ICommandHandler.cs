namespace ArchSample.UseCases.Infrastructure
{
    public interface ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand
        where TResponse : IResponse
    {
        TResponse Handle(TCommand command);
    }
}
