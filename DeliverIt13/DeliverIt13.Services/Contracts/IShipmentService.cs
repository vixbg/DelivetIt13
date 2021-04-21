using DeliverIt13.Services.Models.ShipmentDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt13.Services.Contracts
{
    public interface IShipmentService
    {
        ShipmentGetDTO GetStatus(int id);

        ShipmentCreateDTO GetNextToArrive();

        List<ShipmentCreateDTO> GetAll();
        int GetCount();

        ShipmentCreateDTO Create(ShipmentCreateDTO shipment);

        bool Delete(int id);

        ShipmentUpdateDTO Update(int id, ShipmentUpdateDTO shipmentDTO);
    }
}
