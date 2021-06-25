using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Basics
{
    public class Tasks
    {
        [Fact]
        public async Task MultipleAwaits_OfSameTask()
        {
            var instance = new TaskHolder();
            var task = instance.Update();
            task.IsCompleted.Should().BeFalse();
            var res1 = await task;
            task.IsCompleted.Should().BeTrue();
            var res2 = await task;
            new[] { res1, res2, instance.Value }.Should().BeEquivalentTo(new[] { 1, 1, 1 });
        }
    }

    public class TaskHolder
    {
        public int Value = 0;

        public async Task<int> Update()
        {
            Value++;
            await Task.Delay(100);
            return Value;
        }
    }
}
