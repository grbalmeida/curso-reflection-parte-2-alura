using ByteBank.Portal.Infraestrutura;
using ByteBank.Service;
using ByteBank.Portal.Filtros;

namespace ByteBank.Portal.Controller
{
    public class CambioController : ControllerBase
    {
        private readonly ICambioService _cambioService;
        private readonly ICartaoService _cartaoService;

        public CambioController(ICambioService cambioService, ICartaoService cartaoService)
        {
            _cambioService = cambioService;
            _cartaoService = cartaoService;
        }

        [ApenasHorarioComercialFiltro]
        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            return View(new { ValorEmReais = valorFinal });
        }

        [ApenasHorarioComercialFiltro]
        public string USD()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            return View(new { ValorEmReais = valorFinal });
        }

        [ApenasHorarioComercialFiltro]
        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorFinal = _cambioService.Calcular(moedaOrigem, moedaDestino, valor);
            var cartaoPromocao = _cartaoService.ObterCartaoDeCreditoDeDestaque();

            var modelo = new
            {
                MoedaDestino = moedaDestino,
                ValorDestino = valorFinal,
                MoedaOrigem = moedaOrigem,
                ValorOrigem = valor,
                CartaoPromocao = cartaoPromocao
            };

            return View(modelo);
        }

        [ApenasHorarioComercialFiltro]
        public string Calculo(string moedaDestino, decimal valor) =>
            Calculo("BRL", moedaDestino, valor);

        [ApenasHorarioComercialFiltro]
        public string Calculo(string moedaDestino) =>
            Calculo("BRL", moedaDestino, 1);
    }
}
