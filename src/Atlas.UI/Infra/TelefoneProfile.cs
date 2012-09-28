using Atlas.UI.Domain;
using Atlas.UI.Models;
using AutoMapper;

namespace Atlas.UI.Infra
{
    public class TelefoneProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Telefone, TelefoneViewModel>();

            CreateMap<TelefoneViewModel, Telefone>();
        }
    }
}