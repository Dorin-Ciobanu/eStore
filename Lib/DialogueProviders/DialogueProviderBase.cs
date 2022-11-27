
namespace eStore.Services.DialogueProviders;
public abstract class DialogueProviderBase : IDialogueProvider
{
    public abstract void StartDialogue();

    protected abstract string EnsureValidAnswer(string input);
}
