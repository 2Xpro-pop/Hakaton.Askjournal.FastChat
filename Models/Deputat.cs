namespace FastChat.Models;

public class Deputat
{
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string PhotoUrl
    {
        get; set;
    }
    public Party Party
    {
        get; set;
    }
    public string AnalyticKg
    {
        get; set;
    }
    public string AnalyticRu
    {
        get; set;
    }
    public IList<VoteVm> Votes
    {
        get; set;
    } = new List<VoteVm>();
}
