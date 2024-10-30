using HomPageServices.Services;
using MediatR;

namespace HomPageServices.Features.HomePage.Queries
{
    public class GetHomePagePartsItemsQuery:IRequest<List<PartGroupDto>>
    {
        public GetHomePagePartsItemsQuery()
        {
            
        }
    }
}
