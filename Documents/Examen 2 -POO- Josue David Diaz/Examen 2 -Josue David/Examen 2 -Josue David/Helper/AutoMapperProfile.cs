using AutoMapper;
using Examen_2__Josue_David.DataBase.DTO.Clientes;
using Examen_2__Josue_David.Entity;


namespace Proyecto_Poo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForOrders();
            
        }


        private void MapsForOrders()
        {
            CreateMap<Cliente, ClienteDto>(); 
            CreateMap<Cliente, ClienteCreate>();
        }
    }

    
}

