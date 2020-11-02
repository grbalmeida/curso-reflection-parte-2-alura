using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteBank.Portal.Infraestrutura.IoC
{
    public class ContainerSimples : IContainer
    {
        private readonly Dictionary<Type, Type> _mapaDeTipos = new Dictionary<Type, Type>();

        // Registrar(typeof(ICambioService), typeof(CambioServiceTeste))
        // Recuperar(typeof(ICambioService))
        // Deve retornar para nós uma instância do tipo CambioServiceTeste
        public void Registrar(Type tipoOrigem, Type tipoDestino)
        {
            if (_mapaDeTipos.ContainsKey(tipoOrigem))
                throw new InvalidOperationException("Tipo já mapeado!");

            VerificarHierarquiaOuLancarExcecao(tipoOrigem, tipoDestino);

            _mapaDeTipos.Add(tipoOrigem, tipoDestino);
        }

        public object Recuperar(Type tipoOrigem)
        {
            throw new NotImplementedException();
        }

        private void VerificarHierarquiaOuLancarExcecao(Type tipoOrigem, Type tipoDestino)
        {
            // Verificar se tipoDestino herda ou implementa tipoOrigem
        
            if (tipoOrigem.IsInterface)
            {
                var tipoDestinoImplementaInterface =
                    tipoDestino
                        .GetInterfaces()
                        .Any(tipoInterface => tipoInterface == tipoOrigem);

                if (!tipoDestinoImplementaInterface)
                    throw new InvalidOperationException("O tipo destino não implementa o tipo de origem");
            }
            else
            {
                var tipoDestinoHerdaTipoOrigem = tipoDestino.IsSubclassOf(tipoOrigem);

                if (!tipoDestinoHerdaTipoOrigem)
                    throw new InvalidOperationException("O tipo destino não herda o tipo de origem");
            }
        }
    }
}
