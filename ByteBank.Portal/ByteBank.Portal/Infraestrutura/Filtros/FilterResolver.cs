﻿using ByteBank.Portal.Filtros;
using ByteBank.Portal.Infraestrutura.Binding;
using System;

namespace ByteBank.Portal.Infraestrutura.Filtros
{
    public class FilterResolver
    {
        public FilterResult VerificarFiltros(ActionBindInfo actionBindInfo)
        {
            var methodInfo = actionBindInfo.MethodInfo;

            var atributos = methodInfo.GetCustomAttributes(typeof(FiltroAttribute), inherit: false);
        
            foreach (FiltroAttribute filtro in atributos)
            {
                if (!filtro.PodeContinuar())
                    return new FilterResult(false);
            }

            return new FilterResult(true);
        }
    }
}
