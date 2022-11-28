using eStore.Services.DialogueProviders.ChooseProducts;

namespace eStore.Services.DialogueProviders.Greeting
{
    public class GreetingDialogueProvider : DialogueProviderBase, IGreetingDialogueProvider
    {
        private const string ValidAnswerYes = "Y";
        private const string ValidAnswerNo = "N";

        private readonly IChooseProductsDialogueProvider _chooseProductsDialogueProvider;
        public GreetingDialogueProvider(IChooseProductsDialogueProvider chooseProductsDialogueProvider)
        {
            _chooseProductsDialogueProvider = chooseProductsDialogueProvider ?? throw new ArgumentNullException(nameof(chooseProductsDialogueProvider));
        }

        public override void StartDialogue()
        {
            Console.Clear();

            Console.WriteLine($"Welcome! You have now entered the eStore. We have some great campaigns running at the moment.{Environment.NewLine}Would you like to learn more about them? Type in Y for \"Yes\" or N for \"No\":");

            var answer = EnsureValidAnswer(Console.ReadLine());

            if (answer.Equals(ValidAnswerYes))
            {
                Console.WriteLine($"{Environment.NewLine}Coming soon...");
                Thread.Sleep(1500);

                Console.WriteLine($"{Environment.NewLine}But you can still browse the shop.");
                Thread.Sleep(2000);

                _chooseProductsDialogueProvider.StartDialogue();
            }
            else if (answer.Equals(ValidAnswerNo))
            {
                _chooseProductsDialogueProvider.StartDialogue();
            }
            else
            {
                throw new NotImplementedException($"The \"{answer}\" option is not implemented in {nameof(GreetingDialogueProvider)}.{nameof(StartDialogue)} method.");
            }
        }

        protected override string EnsureValidAnswer(string input)
        {
            var validAnswers = new string[2] { ValidAnswerYes, ValidAnswerNo };

            while (!validAnswers.Contains(input))
            {
                Console.WriteLine($"Invalid answer. Please answer with \"{ValidAnswerYes}\" or \"{ValidAnswerNo}\".");
                input = Console.ReadLine();
            }

            return input;
        }
    }
}
