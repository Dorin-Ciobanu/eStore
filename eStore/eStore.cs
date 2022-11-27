using eStore.Services.DialogueProviders.Greeting;

namespace eStore;

internal class Store
{
    private readonly IGreetingDialogueProvider _greetingDialogueProvider;
    public Store(IGreetingDialogueProvider greetingDialogueProvider)
    {
        _greetingDialogueProvider = greetingDialogueProvider ?? throw new ArgumentNullException(nameof(greetingDialogueProvider));
    }

    internal void RunStore()
    {
        _greetingDialogueProvider.StartDialogue();
    }
}

