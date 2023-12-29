using System.Text;
using telegit.Controllers;

namespace telegit;

public static class NotifyTextPreparer
{
    public static string PrepareMergeRequestMessage(GitlabWebhookData? mergeRequest)
    {
        if (mergeRequest == null) throw new Exception("Merge request is null.");
        if (mergeRequest.object_attributes.action != "open") throw new Exception("Close event triggered.");
        var reviewersString = ReviewersToString(mergeRequest.reviewers);
        var message = $"<b>Новый Merge Request на {mergeRequest.project.name}</b> \nОт: <b>{mergeRequest.user.name}</b> \nОткуда: <i>{mergeRequest.object_attributes.source_branch}</i>\nКуда: <i>{mergeRequest.object_attributes.target_branch}</i> \nНазначены: {reviewersString ?? "Неизвестно."} \n\n \n {mergeRequest.object_attributes.url}";

        return message;
    }
    
    public static string PrepareCommentMessage(GitlabWebhookData? comment)
    {
        if (comment == null) throw new Exception("Comment is null.");
        string message;
        if (comment.object_attributes.noteable_type == "MergeRequest")
        {
            message = comment.object_attributes.line_code != null ? $"<b>Новый комментарий на код по MR {comment.merge_request.url}</b> \nОт: <b>{comment.user.name}</b> \nКомментарий: {comment.object_attributes.note}\n \n\n\n{comment.object_attributes.url}" : $"<b>Новый комментарий по MR {comment.merge_request.url}</b> \nОт: <b>{comment.user.name}</b> \nКомментарий: {comment.object_attributes.note}\n \n\n\n{comment.object_attributes.url}";
        }
        else
        {
            throw new Exception("Unknown comment type.");
        }

        return message;
    }
    
    private static string? ReviewersToString(List<Reviewer>? reviewers)
    {
        if (reviewers == null || reviewers.Count == 0)
            return null;

        var reviewersString = new StringBuilder();
        foreach (var reviewer in reviewers)
        {
            reviewersString.Append(reviewer.name + " ");
        }

        return reviewersString.ToString();
    }
}