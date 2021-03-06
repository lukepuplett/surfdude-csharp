﻿namespace Evoq.Surfdude
{
    using Evoq.Surfdude.Hypertext;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IStep
    {
        string Name { get; }

        IHypertextResource Resource { get; }

        Task ExecuteAsync(IStep previous, CancellationToken cancellationToken);
    }
}