namespace ArchSample.UseCases.Infrastructure
{
    public interface ICommandBus
    {
        TResponse Execute<TCommand, TResponse>(TCommand command)
            where TCommand : ICommand
            where TResponse : IResponse;
    }
}
