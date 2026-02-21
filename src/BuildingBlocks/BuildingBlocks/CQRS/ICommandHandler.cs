using MediatR;

namespace BuildingBlocks.CQRS
{
    //If there is no response form cmd then this interface will work
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {

    }

    public interface ICommandHandler<in TCommand, TResposne> : IRequestHandler<TCommand, TResposne>
        where TCommand : ICommand<TResposne>
        where TResposne : notnull
    {
    }
}
