var publisher = new EventPublisher();
var consumer = new SqsConsumer();

while (true)
{
  Console.WriteLine("\nChoose an option:");
  Console.WriteLine("1. Publish event");
  Console.WriteLine("2. Poll messages from SQS");
  Console.WriteLine("3. Exit");
  Console.Write("Enter your choice: ");

  var input = Console.ReadLine();

  switch (input)
  {
    case "1":
      Console.Write("Enter name to include in the event: ");
      var name = Console.ReadLine();
      await publisher.PublishEventAsync(name);
      break;

    case "2":
      Console.Write("Enter SQS Queue Name: ");
      var queueName = Console.ReadLine();
      await consumer.PollMessagesAsync(queueName);
      break;

    case "3":
      Console.WriteLine("Exiting...");
      return;

    default:
      Console.WriteLine("Invalid choice. Please try again.");
      break;
  }
}
