using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mystefy.DTOs;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryService;

        public DeliveryController(IDeliveryRepository deliveryService)
        {
            _deliveryService = deliveryService;
        }

        // POST: api/Delivery
        [HttpPost]
        public async Task<ActionResult<DeliveryDTO>> CreateDelivery([FromBody] Delivery delivery)
        {
            var createdDelivery = await _deliveryService.CreateDeliveryAsync(delivery);
            return CreatedAtAction(nameof(GetDeliveryById), new { deliveryId = createdDelivery.DeliveryID }, MapToDTO(createdDelivery));
        }

        // GET: api/Delivery/{deliveryId}
        [HttpGet("{deliveryId}")]
        public async Task<ActionResult<DeliveryDTO>> GetDeliveryById(int deliveryId)
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(deliveryId);
            if (delivery == null)
            {
                return NotFound("Delivery not found");
            }
            return Ok(MapToDTO(delivery));
        }

        // GET: api/Delivery
        [HttpGet]
        public async Task<ActionResult<List<DeliveryDTO>>> GetAllDeliveries()
        {
            var deliveries = await _deliveryService.GetAllDeliveriesAsync();
            var deliveryDtos = deliveries.Select(d => MapToDTO(d)).ToList();
            return Ok(deliveryDtos);
        }

        // PUT: api/Delivery/{deliveryId}
        [HttpPut("{deliveryId}")]
        public async Task<ActionResult<DeliveryDTO>> UpdateDelivery(int deliveryId, [FromBody] Delivery delivery)
        {
            if (deliveryId != delivery.DeliveryID)
                return BadRequest("ID mismatch");

            var updatedDelivery = await _deliveryService.UpdateDeliveryAsync(delivery);
            if (updatedDelivery == null)
                return NotFound("Delivery not found");

            return Ok(MapToDTO(updatedDelivery));
        }

        // DELETE: api/Delivery/{deliveryId}
        [HttpDelete("{deliveryId}")]
        public async Task<IActionResult> DeleteDelivery(int deliveryId)
        {
            bool success = await _deliveryService.DeleteDeliveryAsync(deliveryId);
            if (!success)
                return NotFound("Delivery not found");
            return NoContent();
        }

        // Helper method: Maps a Delivery entity to its DTO.
        private DeliveryDTO MapToDTO(Delivery delivery)
        {
            return new DeliveryDTO
            {
                DeliveryID = delivery.DeliveryID,
                SupplierID = delivery.SupplierID,
                DeliveryDateArrived = delivery.DeliveryDateArrived,
                DeliveryDateOrdered = delivery.DeliveryDateOrdered,
                DeliveryCost = delivery.DeliveryCost,
                WarehouseID = delivery.WarehouseID
            };
        }
    }
}

