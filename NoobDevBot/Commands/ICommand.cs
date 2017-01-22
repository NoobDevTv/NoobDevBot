using System;

namespace NoobDevBot.Commands
{
    public delegate void FinishEventHandler<TParameter>(object sender, TParameter e);
    public delegate void CommandEventHandler<TParameter, TOut>(object sender, Func<TParameter, TOut> method);
    public interface ICommand<TParameter, TOut>
    {
        Func<TParameter, TOut> NextFunction { get; }

        bool Finished { get; }

        event FinishEventHandler<TParameter> FinishEvent;

        TOut Dispatch(TParameter parameter);
        
    }
}