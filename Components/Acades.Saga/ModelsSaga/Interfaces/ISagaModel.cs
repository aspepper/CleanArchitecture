using System;
using Acades.Saga.ModelsSaga.Actions.Interfaces;

namespace Acades.Saga.ModelsSaga.Interfaces
{
    public interface ISagaModel
    {
        ISagaActions Actions { get; }
        Type SagaStateType { get; }
        string Name { get; set; }
        ESagaResumePolicy ResumePolicy { get; set; }
    }
}
