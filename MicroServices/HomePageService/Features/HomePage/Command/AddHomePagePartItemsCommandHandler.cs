using HomPageServices.Models.Dtos;
using HomPageServices.Services;
using MediatR;

namespace HomPageServices.Features.HomePage.Command
{
    public class AddHomePagePartItemsCommandHandler : IRequestHandler<AddHomePagePartItemsCommand, bool>
    {
        private readonly IHomePageServices homePageServices;
        public AddHomePagePartItemsCommandHandler(IHomePageServices homePageServices)
        {
            this.homePageServices = homePageServices;
        }

        public Task<bool> Handle(AddHomePagePartItemsCommand request, CancellationToken cancellationToken)
        {
            var result = homePageServices.AddHomePageParts(request.HomePagePartDto);
            return result;
        }
    }
}
