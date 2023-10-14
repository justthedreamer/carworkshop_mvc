using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshopService.Commands
{
    public class CreateCarworkshopServiceCommand : CarWorkshopServiceDto, IRequest
    {
        public string CarWorkshopEncodedName { get; set; }
    }
}
