using ByteBank.Portal.Infraestrutura;
using ByteBank.Portal.Model;
using ByteBank.Service;
using ByteBank.Service.Cambio;

namespace ByteBank.Portal.Controller
{
    public class CambioController : ControllerBase
    {
        private readonly ICambioService _cambioService;

        public CambioController()
        {
            _cambioService = new CambioTesteService();
        }

        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            var modelo = new CambioBrasilModel
            {
                ValorEmReais = valorFinal
            };

            return View(modelo);
        }

        public string USD()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            var modelo = new CambioBrasilModel
            {
                ValorEmReais = valorFinal
            };

            return View(modelo);
        }

        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorFinal = _cambioService.Calcular(moedaOrigem, moedaDestino, valor);

            var modelo = new CalculoCambioModel
            {
                MoedaDestino = moedaDestino,
                ValorDestino = valorFinal,
                MoedaOrigem = moedaOrigem,
                ValorOrigem = valor
            };

            return View(modelo);
        }

        public string Calculo(string moedaDestino, decimal valor) =>
            Calculo("BRL", moedaDestino, valor);

        public string Calculo(string moedaDestino) =>
            Calculo("BRL", moedaDestino, 1);
    }
}
