using Eventify.Models;

namespace Eventify.Services;

public class RecommendationService
{
    public IEnumerable<EventItem> Recommend(IEnumerable<EventItem> events, IEnumerable<string> interests)
    {
        var interestSet = interests.ToHashSet(StringComparer.OrdinalIgnoreCase);
        return events
            .Where(e => interestSet.Contains(e.Category))
            .OrderBy(e => e.Date);
    }
}
