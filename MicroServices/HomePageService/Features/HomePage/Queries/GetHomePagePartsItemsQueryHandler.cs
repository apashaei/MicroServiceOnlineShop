using HomPageServices.Context;
using HomPageServices.Services;
using MediatR;

namespace HomPageServices.Features.HomePage.Queries
{
    public class GetHomePagePartsItemsQueryHandler : IRequestHandler<GetHomePagePartsItemsQuery, List<PartGroupDto>>
    {
        private readonly IHomePageServices homePageServices;
        public GetHomePagePartsItemsQueryHandler(IHomePageServices homePageServices)
        {
            this.homePageServices = homePageServices;
        }
        public async Task<List<PartGroupDto>> Handle(GetHomePagePartsItemsQuery request, CancellationToken cancellationToken)
        {
            var result = await homePageServices.GetHomePageParts();
            return result;
        }
    }
}
