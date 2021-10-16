﻿using CasaDoCodigo.Models;
using ECommerceMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CasaDoCodigo
{
    //MELHORIA: 8) dados do cadastro gravados na sessão
    public class HttpHelper : IHttpHelper
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<AppIdentityUser> userManager;

        public IConfiguration Configuration { get; }

        public HttpHelper(IHttpContextAccessor contextAccessor, IConfiguration configuration, UserManager<AppIdentityUser> userManager)
        {
            this.contextAccessor = contextAccessor;
            Configuration = configuration;
            this.userManager = userManager;
        }

        public int? GetPedidoId()
        {
            return contextAccessor.HttpContext.Session.GetInt32($"pedidoId_{GetClienteId()}");
        }

        private string GetClienteId()
        {
            return userManager.GetUserId(contextAccessor.HttpContext.User);
        }

        public void SetPedidoId(int pedidoId)
        {
            contextAccessor.HttpContext.Session.SetInt32($"pedidoId_{GetClienteId()}", pedidoId);
        }

        public void ResetPedidoId()
        {
            contextAccessor.HttpContext.Session.Remove($"pedidoId_{GetClienteId()}");
        }

        public void SetCadastro(Cadastro cadastro)
        {
            string json = JsonConvert.SerializeObject(cadastro.GetClone());
            contextAccessor.HttpContext.Session.SetString("cadastro", json);
        }

        public Cadastro GetCadastro()
        {
            string json = contextAccessor.HttpContext.Session.GetString("cadastro");
            if (string.IsNullOrWhiteSpace(json))
                return new Cadastro();

            return JsonConvert.DeserializeObject<Cadastro>(json);
        }
    }

}
