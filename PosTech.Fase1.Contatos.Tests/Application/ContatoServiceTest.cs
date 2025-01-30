using AutoMapper;
using Moq;
using PosTech.Fase1.Contatos.Application.DTO;
using PosTech.Fase1.Contatos.Application.Mappins;
using PosTech.Fase1.Contatos.Application.Model;
using PosTech.Fase1.Contatos.Application.Services;
using PosTech.Fase1.Contatos.Domain.Entities;
using PosTech.Fase1.Contatos.Infra.Interfaces;

namespace PosTech.Fase1.Contatos.Tests.Application;

public class ContatoServiceTest
{
    private readonly IMapper _mapper;
    private readonly ContatoDto _contatoDto;
    private readonly Contato _contato;
    private readonly DDD _ddd;
    private readonly ContatoDto _contatoListaDto;
    private readonly Mock<IContatoRepository> contatoRepository;
    private readonly Mock<IDDDRepository> dddRepository;
    private readonly Mock<IContatoAddFila> contatoAddFila;
    private readonly Mock<IContatoUpdateFila> contatoUpdateFila;
    private readonly Mock<IContatoDeleteFila> contatoDeletarFila;

    public ContatoServiceTest()
    {
        var configMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ContatoMapingProfile());
            cfg.AddProfile(new DDDMapingProfile());
        });
        _ddd = new DDD(71, "BA", "Salvador");
        _mapper = configMapper.CreateMapper();
        _contatoDto = new ContatoDto()
        {
            ContatoId = Guid.NewGuid(),
            Nome = "Mario",
            Telefone = "7198875566",
            Email = "mario.silveira@gmail.com",
            Ativo = true,
            DddId = 71
        };
        
        _contatoListaDto = new ContatoDto()
        {
            ContatoId = Guid.NewGuid(),
            Nome = "Mario",
            Telefone = "7198875566",
            Email = "mario.silveira@gmail.com",
            Ativo = true,
            DddId = 71,
            Ddd = _mapper.Map<DDDDto>(_ddd)
        };
        

        _contato = _mapper.Map<Contato>(_contatoDto);

        contatoRepository = new Mock<IContatoRepository>();
        dddRepository = new Mock<IDDDRepository>();
        contatoAddFila = new Mock<IContatoAddFila>();
        contatoUpdateFila = new Mock<IContatoUpdateFila>();
        contatoDeletarFila = new Mock<IContatoDeleteFila>();
    }
    [Fact]
    public async Task ContatoService_Adiconar_ComSucesso()
    {
        //arrange

  
        dddRepository
            .Setup(x => x.Obter(_contato.DddId))
            .ReturnsAsync(_ddd);

        var ContatoService =
            new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object,contatoDeletarFila.Object);

        //act
        var ContatoResult = await ContatoService.Adicionar(_contatoDto);

        //assert
        Assert.True(ContatoResult.IsSuccess);
    }

    [Fact]
    public async Task ContatoService_Adiconar_ComErroDDDNaoExiste()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_ddd.DddId));


        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object,contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Adicionar(_contatoDto);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("DDD não existe", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Adiconar_ComErroContatoJaExistente()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_contato.DddId))
            .ReturnsAsync(_ddd);

        contatoRepository
            .Setup(x => x.Existe(It.IsAny<Contato>()))
            .ReturnsAsync(true);

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object,contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Adicionar(_contatoDto);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("Cadastro de contato ja existe", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Adicionar_ComErro()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_contatoDto.DddId))
            .Throws(new Exception());

        var contatoService = new  ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Adicionar(_contatoDto);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }



    [Fact]
    public async Task ContatoService_Atualizar_ContatoServiceAtualizadoComSucesso()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_ddd.DddId))
            .ReturnsAsync(_ddd);

        contatoRepository
            .Setup(x => x.Obter(_contato.ContatoId!.Value))
            .ReturnsAsync(_contato);

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Atualizar(_contatoDto);

        //assert
        Assert.True(contatoResult.IsSuccess);
    }

    [Fact]
    public async Task ContatoService_Atualizar_ComErroDDDNaoExiste()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_ddd.DddId));
        
        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Atualizar(_contatoDto);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("DDD não existe", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Atualizar_ComErroContatoNaoExiste()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_ddd.DddId))
            .ReturnsAsync(_ddd);

        contatoRepository
            .Setup(x => x.Obter(_contato.ContatoId!.Value));

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Atualizar(_contatoDto);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("Contato não existe", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Atualizar_AtualizadoComErro()
    {
        //arrange
        dddRepository
            .Setup(x => x.Obter(_contatoDto.DddId))
            .Throws(new Exception());

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Atualizar(_contatoDto);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }

    [Fact]
    public async Task ContatoService_Excluir_ComSucesso()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Obter(_contato.ContatoId!.Value))
            .ReturnsAsync(_contato);

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Excluir(_contatoDto.ContatoId!.Value);

        //assert
        Assert.True(contatoResult.IsSuccess);
    }
    [Fact]
    public async Task ContatoService_Excluir_ComErroRegistroNaoExiste()
    {
        //arrange
        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Excluir(_contatoDto.ContatoId!.Value);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<ValidacaoException>(contatoResult.Error);
    }

    [Fact]
    public async Task ContatoService_Excluir_ComErro()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Obter(_contato.ContatoId!.Value))
            .Throws(new Exception());

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Excluir(_contatoDto.ContatoId!.Value);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }

    [Fact]
    public async Task ContatoService_Listar_ComSucesso()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Listar())
            .ReturnsAsync(new List<Contato>());

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Listar();

        //assert
        Assert.True(contatoResult.IsSuccess);
    }

    [Fact]
    public async Task ContatoService_Listar_ComErro()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Listar())
            .Throws(new Exception());

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Listar();

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }



    [Fact]
    public async Task ContatoService_ListarComDDD_ComSucesso()
    {
        //arrange
        contatoRepository
            .Setup(x => x.ListarComDDD(_contatoDto.DddId))
            .ReturnsAsync(new List<Contato>() );

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.ListarComDdd(_contatoDto.DddId);

        //assert
        Assert.True(contatoResult.IsSuccess);
  
    }

    [Fact]
    public async Task ContatoService_ListarComDDD_ListadoComErro()
    {
        //arrange
        contatoRepository
            .Setup(x => x.ListarComDDD(_contato.DddId))
            .Throws(new Exception());

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.ListarComDdd(_contatoDto.DddId);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }



    [Fact]
    public async Task ContatoService_Obter_ComSucesso()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Obter(_contatoListaDto.ContatoId!.Value))
            .ReturnsAsync(_contato);

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Obter(_contatoListaDto.ContatoId!.Value);

        //assert
        Assert.True(contatoResult.IsSuccess);
    }

    [Fact]
    public async Task ContatoService_Obter_ComErro()
    {
        //arrange
        contatoRepository
            .Setup(x => x.Obter(_contatoDto.ContatoId!.Value))
            .Throws(new Exception());

        var contatoService = new ContatoService(contatoRepository.Object, _mapper, dddRepository.Object, contatoAddFila.Object, contatoUpdateFila.Object, contatoDeletarFila.Object);

        //act
        var contatoResult = await contatoService.Obter(_contatoDto.ContatoId!.Value);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }
}

