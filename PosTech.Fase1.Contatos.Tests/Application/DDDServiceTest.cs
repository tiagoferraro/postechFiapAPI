using AutoMapper;
using Moq;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Mappins;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Application.Services;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Domain.ObjectValue;
using PosTech.Fase1.Contatos.Infra.Interfaces;

namespace PosTech.Fase1.Contatos.Tests.Application;

public class DDDServiceTest
{
    private readonly IMapper _mapper;
    private readonly DDDDto _dddDto;

    public DDDServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DDDMapingProfile());
        });
        _mapper = mockMapper.CreateMapper();

        _dddDto = new DDDDto()
        {
            DddId = 11,
            Regiao = "São Paulo",
            UfNome = "São Paulo",
            UfSigla = "SP"
        };
    }
    
    [Fact]
    public async Task DDDService_Adiconar_SucessoAdicionarDDD()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        var dddService = new DDDService(dddRepository.Object, _mapper);
       
        //act
        var dddResult = await dddService.Adicionar(_dddDto);

        //assert
        Assert.True(dddResult.IsSuccess);
    }
    
    [Fact]
    public async Task DDDService_Adiconar_ErroAdicionarDDD()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        var ddd = _mapper.Map<DDD>(_dddDto);
        dddRepository
            .Setup(dddRepositoryMock => dddRepositoryMock.Obter(ddd.DddId))
            .Throws(new Exception()); 

        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Adicionar(_dddDto);

        //assert
        Assert.False(dddResult.IsSuccess);
        Assert.IsType<Exception>(dddResult.Error);
    }
    
    [Fact]
    public async Task DDDService_Adicionar_ErroDDDJaExiste()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        var ddd = _mapper.Map<DDD>(_dddDto);

        dddRepository
            .Setup(dddRepositoryMock => dddRepositoryMock.Obter(ddd.DddId))
            .ReturnsAsync(ddd);

        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Adicionar(_dddDto);

        //assert
        Assert.False(dddResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(dddResult.Error);
        Assert.Equal("DDD Já Existe", ex.Message);
    }
    
    [Fact]
    public async Task DDDService_Atualizar_SucessoAtualizadoDDD()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();

        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Atualizar(_dddDto);

        //assert
        Assert.True(dddResult.IsSuccess);
    }
    
    [Fact]
    public async Task DDDService_Atualizar_ErroAtualizarDDD()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();

        var mockMapper = new Mock<IMapper>();
        mockMapper
            .Setup(mockMapper => mockMapper.Map<DDD>(_dddDto))
            .Throws(new Exception());

        var dddService = new DDDService(dddRepository.Object, mockMapper.Object);

        //act
        var dddResult = await dddService.Atualizar(_dddDto);

        //assert
        Assert.False(dddResult.IsSuccess);
        Assert.IsType<Exception>(dddResult.Error);
    }

    [Fact]
    public async Task DDDService_Listar_SucessoListar()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Listar();

        //assert
        Assert.True(dddResult.IsSuccess);
    }

    [Fact]
    public async Task DDDService_Listar_ErroListar()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        dddRepository
            .Setup(dddRepository => dddRepository.Listar())
            .Throws(new Exception());
        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Listar();

        //assert
        Assert.False(dddResult.IsSuccess);
        Assert.IsType<Exception>(dddResult.Error);
    }

    [Fact]
    public async Task DDDService_Obter_SucessoObterDDD()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        var ddd = _mapper.Map<DDD>(_dddDto);

        dddRepository
            .Setup(dddRepositoryMock => dddRepositoryMock.Obter(ddd.DddId))
            .ReturnsAsync(ddd);

        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Obter(_dddDto.DddId);

        //assert
        Assert.True(dddResult.IsSuccess);
    }
 
    [Fact]
    public async Task DDDService_Obter_ErroObterDDD()
    {
        //arrange
        var dddRepository = new Mock<IDDDRepository>();
        var ddd = _mapper.Map<DDD>(_dddDto);

        dddRepository
            .Setup(dddRepositoryMock => dddRepositoryMock.Obter(ddd.DddId))
            .ReturnsAsync(ddd);

        
        dddRepository
            .Setup(dddRepositoryMock => dddRepositoryMock.Obter(ddd.DddId))
            .Throws(new Exception());

        var dddService = new DDDService(dddRepository.Object, _mapper);

        //act
        var dddResult = await dddService.Obter(_dddDto.DddId);

        //assert
        Assert.False(dddResult.IsSuccess);
        Assert.IsType<Exception>(dddResult.Error);
    }
 
}

