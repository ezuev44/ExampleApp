using Entities;

namespace Interfaces
{
    public interface ISearchResponseMapperService <TItem>
    {
        SearchResponse Map(TItem item);
    }
}
