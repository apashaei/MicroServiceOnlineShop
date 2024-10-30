using HomPageServices.Models.Dtos;
using MediatR;

namespace HomPageServices.Features.HomePage.Command
{
    public class AddHomePagePartItemsCommand:IRequest<bool>
    {
        public HomePagePartDto HomePagePartDto { get; set; }

        public AddHomePagePartItemsCommand(HomePagePartDto homePagePartDto)
        {
            this.HomePagePartDto = homePagePartDto;
        }
    }
}
