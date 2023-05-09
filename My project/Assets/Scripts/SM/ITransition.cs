using System;

namespace SM
{
    public interface ITransition
    {
        Func<bool> Condition { get; }
        IState To { get; }
    }
}
