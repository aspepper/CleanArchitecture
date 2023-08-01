using Acades.Saga.MessageBus;
using Acades.Saga.MessageBus.Interfaces;
using Acades.Saga.Models;
using Acades.Saga.Models.Interfaces;
using Acades.Saga.ValueObjects;

namespace Acades.Saga.Messages
{
    internal class StateChangedMessage : IInternalMessage
    {
        public StateChangedMessage(string currentState, string currentStep, bool isCompensating, ISaga saga)
        {
            CurrentState = currentState;
            CurrentStep = currentStep;
            IsCompensating = isCompensating;
            Saga = saga;
        }

        /// <summary>
        ///     Correlation ID
        /// </summary>
        public ISaga Saga { get; }

        public string CurrentState { get; }
        public string CurrentStep { get; }
        public bool IsCompensating { get; }
    }

}