using Newtonsoft.Json;

namespace telegit.Controllers;

[method: JsonConstructor]
public class User(string name)
{
    public string name { get; } = name;
}

[method: JsonConstructor]
public class Project(string name)
{
    public string name { get; } = name;
}

[method: JsonConstructor]
public class Reviewer(string name)
{
    public string name { get; } = name;
}

[method: JsonConstructor]
public class ObjectAttributes(
    string action,
    string sourceBranch,
    string targetBranch,
    string url,
    int? lineCode,
    string note,
    string noteableType)
{
    public string action { get; } = action;
    public string source_branch { get; } = sourceBranch;
    public string target_branch { get; } = targetBranch;
    public string url { get; } = url;
    public int? line_code { get; } = lineCode;
    public string note { get; } = note;
    public string noteable_type { get; } = noteableType;
}

[method: JsonConstructor]
public class MergeRequest(string url)
{
    public string url { get; } = url;
}

[method: JsonConstructor]
public class GitlabWebhookData(string event_type, User user, ObjectAttributes object_attributes, MergeRequest mergeRequest, List<Reviewer> reviewers, Project project)
{
    public string event_type { get; } = event_type;
    public User user { get; } = user;
    public ObjectAttributes object_attributes { get; } = object_attributes;

    public MergeRequest merge_request { get; } = mergeRequest;

    public List<Reviewer> reviewers { get; } = reviewers;

    public Project project { get; } = project;
}
