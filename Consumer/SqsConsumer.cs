using Amazon.SQS;
using Amazon.SQS.Model;

public class SqsConsumer
{
  // SQS client instance for interacting with AWS SQS.
  private readonly AmazonSQSClient _sqsClient = new();

  public async Task PollMessagesAsync(string queueName)
  {
    try{
        // Step 1: Get the Queue URL
        var urlResponse = await _sqsClient.GetQueueUrlAsync(new GetQueueUrlRequest
        {
          QueueName = queueName
        });

        string queueUrl = urlResponse.QueueUrl;

        // Step 2: Create a request to receive up to 5 messages, waiting up to 5 seconds.
        var request = new ReceiveMessageRequest
        {
          QueueUrl = queueUrl,
          MaxNumberOfMessages = 5,
          WaitTimeSeconds = 5
        };

        // Step 3: Poll the queue for messages.
        var response = await _sqsClient.ReceiveMessageAsync(request);
        if (response.Messages.Count == 0)
        {
          Console.WriteLine("No messages received.");
        }
        else
        {
          foreach (var msg in response.Messages)
          {
            Console.WriteLine($"Received: {msg.Body}");
          }
        }
    }
    catch (AmazonSQSException ex)
    {
      Console.WriteLine($"AWS SQS error: {ex.Message}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"General error while polling SQS: {ex.Message}");
    }
  }
}
