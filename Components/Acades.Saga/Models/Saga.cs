using Acades.Saga.Models.Interfaces;

namespace Acades.Saga.Models
{
    public class Saga : ISaga
    {
        private SagaExecutionInfo info;
        private SagaExecutionState state;
        private SagaExecutionValues values;

        public Saga()
        {
        }

        public ISagaData Data { get; set; }

        public SagaExecutionInfo ExecutionInfo
        {
            get
            {
                info ??= new SagaExecutionInfo();
                return info;
            }
            set { info = value; }
        }

        public SagaExecutionState ExecutionState
        {
            get
            {
                state ??= new SagaExecutionState();
                return state;
            }
            set { state = value; }
        }

        public SagaExecutionValues ExecutionValues
        {
            get
            {
                values ??= new SagaExecutionValues();
                return values;
            }
            set { values = value; }
        }
    }
}
