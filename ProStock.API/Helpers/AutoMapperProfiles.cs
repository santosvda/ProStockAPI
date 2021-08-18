using System.Linq;
using AutoMapper;
using ProStock.API.Dtos;
using ProStock.Domain;

namespace ProStock.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(dest => dest.Vendas, opt => {
                    opt.MapFrom(src => src.ProdutosVendas.Select(x => x.Venda).ToList());
                })
                .ReverseMap();

            CreateMap<Venda, VendaDto>()
                .ForMember(dest => dest.Produtos, opt => {
                    opt.MapFrom(src => src.ProdutosVendas.Select(x => x.Produto).ToList());
                })
                .ReverseMap();

            CreateMap<ProdutoVenda, ProdutoVendaDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Endereco, EnderecoDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
            CreateMap<Usuario, UsuarioGetDto>().ReverseMap();
            CreateMap<Estoque, EstoqueDto>().ReverseMap();
            CreateMap<Loja, LojaDto>().ReverseMap();
            CreateMap<Pessoa, PessoaDto>().ReverseMap();
        }
    }
}