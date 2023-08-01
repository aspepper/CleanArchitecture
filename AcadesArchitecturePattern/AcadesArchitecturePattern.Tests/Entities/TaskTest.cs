using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Shared.Enums;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Entities
{
    public class TaskTest
    {
        [Fact]
        public void WhenInitializedShouldBeCompleted()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.Done;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();

            // Act:
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);
            task.MarkAsDone();

            // Assert:
            Assert.True(task.Done);
        }

        [Fact]
        public void WhenInitializedShouldNotBeCompleted()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.ToDo;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();

            // Act:
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);
            task.MarkAsDone();

            // Assert:
            Assert.False(task.Done);
        }

        [Fact]
        public void MarkAsDoneShouldSetDoneToTrue()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.Done;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);

            // Act:
            task.MarkAsDone();

            // Assert:
            Assert.True(task.Done);
        }

        [Fact]
        public void UpdateTaskShouldUpdateProperties()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.ToDo;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);

            var updatedTaskName = "Updated Task";
            var updatedDescription = "This is an updated task description";
            var updatedPriority = EnTaskPriorityLevel.Low;
            var updatedStatus = EnStatusTask.Done;
            var updatedReminder = DateTime.Now.AddDays(1);

            // Act:
            task.UpdateTask(updatedTaskName, updatedDescription, updatedPriority, updatedStatus, updatedReminder);

            // Assert:
            Assert.Equal(updatedTaskName, task.Name);
            Assert.Equal(updatedDescription, task.Description);
            Assert.Equal(updatedPriority, task.Priority);
            Assert.Equal(updatedStatus, task.Status);
            Assert.Equal(updatedReminder, task.Reminder);
        }

        [Fact]
        public void MarkAsDoneShouldNotSetDoneToTrueWhenStatusIsToDo()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.ToDo;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);

            // Act:
            task.MarkAsDone();

            // Assert:
            Assert.False(task.Done);
        }

        [Fact]
        public void MarkAsDoneShouldNotAddDuplicateEventWhenAlreadyDone()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.Done;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);
            var initialDomainEvents = task.GetDomainEvents();

            // Act:
            task.MarkAsDone();

            // Assert:
            Assert.Equal(initialDomainEvents, task.GetDomainEvents());
        }

        [Fact]
        public void UpdateTaskShouldNotUpdatePropertiesWhenInvalid()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.ToDo;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);

            var updatedTaskName = "Updated Task";
            var updatedDescription = "This is an updated task description";
            var updatedPriority = EnTaskPriorityLevel.Low;
            var updatedStatus = EnStatusTask.Done;
            var updatedReminder = DateTime.Now.AddDays(1);

            // Add invalid state to the task
            task.AddNotification("Priority", "The task priority is invalid");

            // Act:
            task.UpdateTask(updatedTaskName, updatedDescription, updatedPriority, updatedStatus, updatedReminder);

            // Assert:
            Assert.NotEqual(updatedTaskName, task.Name);
            Assert.NotEqual(updatedDescription, task.Description);
            Assert.NotEqual(updatedPriority, task.Priority);
            Assert.NotEqual(updatedStatus, task.Status);
            Assert.NotEqual(updatedReminder, task.Reminder);
        }

        [Fact]
        public void UpdateTaskShouldNotUpdatePropertiesWhenAllNull()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.ToDo;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);
            var initialTaskName = task.Name;
            var initialDescription = task.Description;
            var initialPriority = task.Priority;
            var initialStatus = task.Status;
            var initialReminder = task.Reminder;

            // Act:
            task.UpdateTask();

            // Assert:
            Assert.Equal(initialTaskName, task.Name);
            Assert.Equal(initialDescription, task.Description);
            Assert.Equal(initialPriority, task.Priority);
            Assert.Equal(initialStatus, task.Status);
            Assert.Equal(initialReminder, task.Reminder);
        }

        [Fact]
        public void AddDomainEventShouldAddEventToList()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.Done;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);
            var taskEvent = new TaskEvent(task);

            // Set the Done property to true
            task.Done = true;

            // Act:
            task.AddDomainEvent(taskEvent);

            // Assert:
            Assert.Contains(taskEvent, task.GetDomainEvents());
        }


        [Fact]
        public void GetDomainEventsShouldReturnEmptyListWhenNoEventsAdded()
        {
            // Arrange:
            var taskName = "Test Task";
            var taskDescription = "This is a test task";
            var taskPriority = EnTaskPriorityLevel.High;
            var taskStatus = EnStatusTask.Done;
            var taskReminder = DateTime.Now;
            var idList = Guid.NewGuid();
            var task = new Domain.Entities.ToDoTask(taskName, taskDescription, taskPriority, taskStatus, taskReminder, idList);

            // Assert:
            Assert.Empty(task.GetDomainEvents());
        }
    }
}
