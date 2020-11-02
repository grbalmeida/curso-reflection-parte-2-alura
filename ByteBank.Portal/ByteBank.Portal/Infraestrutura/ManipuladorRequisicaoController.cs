using ByteBank.Portal.Infraestrutura.Binding;
using ByteBank.Portal.Infraestrutura.Filtros;
using ByteBank.Portal.Infraestrutura.IoC;
using System;
using System.Net;
using System.Text;

namespace ByteBank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoController
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();
        private readonly FilterResolver _filterResolver = new FilterResolver();
        private readonly ControllerResolver _controllerResolver;

        public ManipuladorRequisicaoController(IContainer container)
        {
            _controllerResolver = new ControllerResolver(container);
        }

        public void Manipular(HttpListenerResponse resposta, string path)
        {
            var partes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];

            var controllerNomeCompleto = $"ByteBank.Portal.Controller.{controllerNome}Controller";

            //var controllerWrapper = Activator.CreateInstance("ByteBank.Portal", controllerNomeCompleto, new object[0]);
            //var controller = controllerWrapper.Unwrap();

            var controller = _controllerResolver.ObterController(controllerNomeCompleto);

            var actionBindInfo = _actionBinder.ObterActionBindInfo(controller, path);

            var filterResult = _filterResolver.VerificarFiltros(actionBindInfo);

            if (filterResult.PodeContinuar)
            {
                var resultadoAction = (string)actionBindInfo.Invoke(controller);

                var buffer = Encoding.UTF8.GetBytes(resultadoAction);

                resposta.StatusCode = (int)HttpStatusCode.OK;
                resposta.ContentType = "text/html; charset=utf-8";
                resposta.ContentLength64 = buffer.Length;

                resposta.OutputStream.Write(buffer, 0, buffer.Length);
                resposta.OutputStream.Close();
            }
            else
            {
                resposta.StatusCode = (int)HttpStatusCode.TemporaryRedirect;
                resposta.RedirectLocation = "/Erro/Inesperado";
                resposta.OutputStream.Close();
            }
        }
    }
}
