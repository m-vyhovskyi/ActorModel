using System;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

using Akka.Actor;
using Akka.Event;
using Akka.TestKit.Xunit2;

using Xunit;
using Xunit.Abstractions;

namespace ActorModel.Tests
{
    public class SupervisoryTests : TestKit
    {
        public ITestOutputHelper Output { get; set; }

        public SupervisoryTests(ITestOutputHelper output)
        {
            Output = output;
        }

        [Fact(DisplayName = "The child actor should be initialized properly")]
        public void TheChildActorInitializationTest()
        {
            EventFilter
                .Info("Creating a child actor").And
                .Info("Child actor is created").And
                .Info("Init command was received from the child").And
                .Info("Configuration event is sent to the child back")
                .Expect(4, () =>
                {
                    var parent = ActorOf(Props.Create(() => new ParentActor(Output)));
                });
        }

        [Fact(DisplayName = "The child actor should be reinitialized after restarting")]
        public void TheChildActorRestartingTest()
        {
            var parent = ActorOf(Props.Create(()=>new ParentActor(Output)));
            Thread.Sleep(100);
            EventFilter
                .Info("Supervision strategy in action").And
                .Info("Child actor is created").And
                .Info("Init command was received from the child").And
                .Info("Configuration event is sent to the child back")
                .Expect(4, () =>
                {
                    Output.WriteLine("Sending a simulation request");
                    parent.Tell("Simulate");
                });
        }
    }

    public class TestActor : ReceiveActor
    {
        private readonly ILoggingAdapter log = Context.GetLogger();
        private readonly ITestOutputHelper output;

        public TestActor(ITestOutputHelper output)
        {
            this.output = output;
        }

        protected void Log(string message)
        {
            log.Info(message);
            output.WriteLine(message);
        }
    }
    public class ParentActor : TestActor
    {
        private IActorRef _child;
        public ParentActor(ITestOutputHelper output):base(output)
        {
            Log("Parent: Creating a child actor");
            _child = Context.ActorOf(Props.Create(() => new ChildActor(Context.Self, output)));
            Receive<string>(s => Handle(s));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy((e) =>
            {                
                Log("Parent: Supervision strategy in action");
                return Directive.Restart;
            });
        }

        private void Handle(string command)
        {
            switch (command)
            {
                case "Init":
                    Log("Parent: Init command was received from the child");
                    Sender.Tell("Init");
                    break;
                case "Simulate":
                    _child.Forward(command);
                    break;
            }
        }
    }

    public class ChildActor : TestActor
    {
        public static int Counter = 0;
        private readonly IActorRef supervisor;

        private int Id { get; set; }

        public ChildActor(IActorRef supervisor, ITestOutputHelper output):base(output)
        {
            this.supervisor = supervisor;
            Id = ++Counter;
            Receive<string>(s => Handle(s));
            supervisor.Tell("Init");
            Log("Child " + Id + ": Created with "+supervisor);
        }

        private void Handle(string message)
        {
            switch (message)
            {
                case "Init":
                    Log("Child " + Id + ": Configuration event is sent to the child back");
                    break;
                case "Simulate":
                    Log("Child " + Id + ": Throwinf simulate exception");
                    throw new Exception("Simulated exception gets thrown");
            }
            
        }
    }

}