using Acades.Saga.Models;
using Acades.Saga.Models.History;
using Acades.Saga.ModelsSaga.Actions.Interfaces;
using Acades.Saga.ModelsSaga.Steps;
using Acades.Saga.ModelsSaga.Steps.Interfaces;
using Acades.Saga.Utils;
using System;

namespace Acades.Saga.ModelsSaga.Actions
{
    internal static class NextStepCalculator
    {
        public static ISagaStep GetNextStepToExecute(
            this ISagaAction sagaAction,
            ISagaStep step,
            SagaExecutionState sagaState)
        {
            NextStepInfo nextStepForWhile = GetNextStepForWhile(sagaAction, step, sagaState);
            if (nextStepForWhile != null) { return nextStepForWhile.NextStep; }

            NextStepInfo nextStepForIf = GetNextStepForIf(sagaAction, step, sagaState);
            if (nextStepForIf != null) { return nextStepForIf.NextStep; }

            NextStepInfo nextChildStep = GetChildNextStep(step);
            if (nextChildStep != null) { return nextChildStep.NextStep; }

            return getNextStep(sagaAction, step, sagaState);
        }
        static ISagaStep getNextStep(
            ISagaAction sagaAction,
            ISagaStep step,
            SagaExecutionState sagaState)
        {
            ISagaStep nextStepForWhile = GetNextStepForWhile(sagaAction, step);
            if (nextStepForWhile != null) { return nextStepForWhile; }

            // Buscando o próximo passo 
            ISagaStep nextStep = GetNextStepElsewhere(
                sagaAction.ChildSteps,
                step);

            // Verifique se o pai to ELSE
            nextStep = GetNextStepForElse(
                sagaAction,
                nextStep,
                sagaState);

            return nextStep;
        }
        static NextStepInfo GetChildNextStep(
            ISagaStep step)
        {
            if (step.ChildSteps.Any()) { return new NextStepInfo { NextStep = step.ChildSteps.GetFirstStep() }; }
            return null;
        }
        static ISagaStep GetNextStepForWhile(
            ISagaAction sagaAction,
            ISagaStep step)
        {
            ISagaStep nextStep = GetNextStepSameLevel(
                sagaAction.ChildSteps,
                step);

            if (nextStep == null && step.ParentStep.Is<ISagaStepForWhile>()) { return step.ParentStep; }
            return null;
        }
        static NextStepInfo GetNextStepForIf(
            ISagaAction sagaAction,
            ISagaStep step,
            SagaExecutionState sagaState)
        {
            if (step.Is<ISagaStepForIf>())
            {
                IStepData currentStepData = sagaState.History.
                    GetLatestByStepName(step.StepName);

                if (currentStepData?.ExecutionData?.ConditionResult == true)
                {
                    return new NextStepInfo { NextStep = step.ChildSteps.GetFirstStep() };
                }
                else
                {
                    return new NextStepInfo { NextStep = GetNextStepElsewhere(sagaAction.ChildSteps, step) };
                }
            }
            return null;
        }
        static NextStepInfo GetNextStepForWhile(
            ISagaAction sagaAction,
            ISagaStep step,
            SagaExecutionState sagaState)
        {
            if (step.Is<ISagaStepForWhile>())
            {
                IStepData currentStepData = sagaState.History.
                    GetLatestByStepName(step.StepName);

                if (currentStepData?.ExecutionData?.ConditionResult == true)
                {
                    return new NextStepInfo { NextStep = step.ChildSteps.GetFirstStep() };
                }
                else
                {
                    return new NextStepInfo { NextStep = GetNextStepElsewhere(sagaAction.ChildSteps, step) };
                }
            }
            return null;
        }
        static ISagaStep GetNextStepForElse(
            ISagaAction sagaAction,
            ISagaStep step,
            SagaExecutionState sagaState)
        {
            if (step.Is<ISagaStepForElse>())
            {
                bool ifElseResult = GetResultFromPrevIfElse(
                    sagaAction.ChildSteps,
                    step,
                    sagaState);

                // if-else é verdadeiro, não o inserimos mais, ou seja,
                // procuramos o próximo passo além de if-else
                if (ifElseResult)
                {
                    ISagaStep nextStepAfterIfElse = GetNextStepAfterIfElse(
                        sagaAction.ChildSteps,
                        step);

                    if (nextStepAfterIfElse != null) { return nextStepAfterIfElse; }
                }

                return getNextStep(sagaAction, step, sagaState);
            }
            return step;
        }
        static ISagaStep GetNextStepAfterIfElse(
            SagaSteps childSteps,
            ISagaStep step)
        {
            while (true)
            {
                ISagaStep nextStep = GetNextStepSameLevel(childSteps, step);
                if (nextStep.Is<ISagaStepForIf>() && nextStep.Is<ISagaStepForElse>())
                {
                    step = nextStep;
                }
                else if (!nextStep.Is<ISagaStepForIf>() && nextStep.Is<ISagaStepForElse>())
                {
                    step = nextStep;
                }
                else
                {
                    return nextStep;
                }
            }
        }
        static bool GetResultFromPrevIfElse(
            SagaSteps childSteps,
            ISagaStep step,
            SagaExecutionState sagaState)
        {
            while (true)
            {
                ISagaStep prevStepIf = GetPrevStepSameLevel(childSteps, step.ParentStep, step);
                if (prevStepIf.Is<ISagaStepForIf>())
                {
                    IStepData stepDataIf = sagaState.History.
                        GetLatestByStepName(prevStepIf.StepName);

                    if (stepDataIf?.ExecutionData?.ConditionResult == true) { return true; }

                    if (!prevStepIf.Is<ISagaStepForElse>()) { return false; }

                    step = prevStepIf;
                }
                else
                {
                    return false;
                }
            }
        }
        static ISagaStep GetNextStepSameLevel(
            SagaSteps SagaSteps, ISagaStep stepToFind)
        {
            return GetNextStepElsewhere(SagaSteps, stepToFind, true);
        }
        static ISagaStep GetNextStepElsewhere(
            SagaSteps SagaSteps, ISagaStep stepToFind,
            Boolean onTheSameLevel = false)
        {
            ISagaStep parentStep = stepToFind?.ParentStep;

            bool stepFound = false;
            if (parentStep == null)
            {
                foreach (ISagaStep childStep in SagaSteps)
                {
                    if (stepFound) { return childStep; }

                    if (childStep == stepToFind) { stepFound = true; }
                }
                return null;
            }
            else
            {
                foreach (ISagaStep childStep in parentStep.ChildSteps)
                {
                    if (stepFound) { return childStep; }

                    if (childStep == stepToFind) { stepFound = true; }
                }

                if (stepFound && onTheSameLevel) { return null; }

                return GetNextStepElsewhere(SagaSteps, stepToFind.ParentStep, onTheSameLevel);
            }
        }
        static ISagaStep GetPrevStepSameLevel(
            SagaSteps SagaSteps, ISagaStep parentStep, ISagaStep stepToFind)
        {
            ISagaStep prevStep = null;
            if (parentStep == null)
            {
                foreach (ISagaStep childStep in SagaSteps)
                {
                    if (childStep == stepToFind)
                    {
                        return prevStep;
                    }
                    prevStep = childStep;
                }
                return null;
            }
            else
            {
                foreach (ISagaStep childStep in parentStep.ChildSteps)
                {
                    if (childStep == stepToFind)
                    {
                        return prevStep;
                    }
                    prevStep = childStep;
                }

                return GetPrevStepSameLevel(SagaSteps, stepToFind.ParentStep.ParentStep, stepToFind.ParentStep);
            }
        }
    }
    internal class NextStepInfo
    {
        public ISagaStep NextStep;
    }
}
