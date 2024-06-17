using Application.Medicines.Commands.CreateMedicine;
using Application.MedicineStocks.Commands.CreateMedicineStock;
using Application.MedicineStocks.Commands.DeleteMedicineStock;
using Application.MedicineStocks.Commands.UpdateMedicineStock;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddMedicineToDispenser([FromBody] CreateMedicineStockCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicineFromDispenser(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteMedicineStockCommand { Id = id }, cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateMedicineQuantityInDispenser([FromBody] UpdateMedicineStockCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
