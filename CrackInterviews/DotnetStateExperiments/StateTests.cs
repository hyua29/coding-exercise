using Stateless;
using Stateless.Graph;

namespace DotnetStateExperiments;

public class Tests
{
    private event EventHandler<bool> MyEvent;
    

    [SetUp]
    public void Setup()
    {
        MyEvent += (o, b) => { };
        MyEvent.Invoke(this, false);

    }

    [Test]
    public void Test1()
    {
        var phoneCall = new StateMachine<State, Trigger>(State.OffHook);

        phoneCall.Configure(State.OffHook)
            .Permit(Trigger.CallDialled, State.Ringing);
        
        phoneCall.Configure(State.Ringing)
            .Permit(Trigger.CallPickedUp, State.Connected);

        phoneCall.Configure(State.Connected)
            .OnEntry(t => StartCallTimer())
            .OnExit(t => StopCallTimer())
            .InternalTransition(Trigger.MuteMicrophone, t => OnMute())
            .InternalTransitionAsync(Trigger.UnmuteMicrophone, t =>
            {
                OnUnmute();
                return Task.CompletedTask;
            })
            .Permit(Trigger.LeftMessage, State.OffHook)
            .Permit(Trigger.PlacedOnHold, State.OnHold);

        phoneCall.Fire(Trigger.CallDialled);
        phoneCall.Fire(Trigger.CallPickedUp);
        phoneCall.FireAsync(Trigger.UnmuteMicrophone);
    }

    private void OnUnmute()
    {
        Console.WriteLine(nameof(OnUnmute));
    }

    private void OnMute()
    {
        Console.WriteLine(nameof(OnMute));
    }

    private void StopCallTimer()
    {
        Console.WriteLine(nameof(StopCallTimer));
    }

    private void StartCallTimer()
    {
        Console.WriteLine(nameof(StartCallTimer));
    }

    public enum State
    {
        Connected,
        Ringing,
        OffHook,
        OnHold
    }

    public enum Trigger
    {
        CallDialled,
        PlacedOnHold,
        LeftMessage,
        UnmuteMicrophone,
        MuteMicrophone,
        CallPickedUp
    }
}