using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace telegit.Controllers;

[Route("api/gitlab-webhook")]
[ApiController]
public class GitlabWebhookController: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] JObject? payload)
    {
        Console.WriteLine(payload);
        if (payload == null)
        {
            return await Task.FromResult<IActionResult>(BadRequest("Invalid JSON payload"));
        }
        
        var instance = JsonConvert.DeserializeObject<GitlabWebhookData>(payload.ToString());

        if (instance == null)
        {
            return await Task.FromResult<IActionResult>(BadRequest("Invalid JSON payload"));
        }

        var message = instance.event_type switch
        {
            "merge_request" => NotifyTextPreparer.PrepareMergeRequestMessage(instance),
            "note" => NotifyTextPreparer.PrepareCommentMessage(instance),
            _ => null
        };

        if (message != null)
        {
            await TelegramClient.TelegramClient.SendMessageAsync(message);
        }

        return await Task.FromResult<IActionResult>(Ok("Thanks!"));
    }
}
