using Atlas.UI.Domain;
using Atlas.UI.Models;
using AutoMapper;

namespace Atlas.UI.Infra
{
    public class ContatoProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Contato,ContatoViewModel>();

            CreateMap<ContatoViewModel, Contato>();
        }
    }
}