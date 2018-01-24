using DesafioStone.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using Monitoring.Repository;
using MonitoringApi.dto;
using System;
using Monitoring.Repository.Exceptions;
using System.Linq;

namespace Repository.Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region Configuration

        IConfiguration config;

        public static IConfigurationRoot GetConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();
            return builder.Build();
        }

        #endregion

        [TestMethod()]
        public void CadastrarCidade()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var city = new City { Name = "Nova Friburgo" };
                repo.Add(city);
            }
            catch (Exception) { Assert.Fail(); }
        }

        [TestMethod()]
        public void CadastrarCidadeDuplicada()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var city = new City { Name = "Porto Alegre" };
                repo.Add(city);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CustomValidationException", ex.GetType().Name);

            }
        }

        [TestMethod()]
        public void ListarCidades()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var result = repo.GetAll();
            }
            catch (Exception) { Assert.Fail(); }
        }

        [TestMethod()]
        public void ObterCidadePorNome()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var result = repo.GetByName("Porto Alegre");
            }
            catch (Exception) { Assert.Fail(); }
        }

        [TestMethod()]
        public void ObterCidadeNaoExistente()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var result = repo.GetByName("Porco Alegre");
            }
            catch (Exception ex)
            { Assert.AreEqual("CustomValidationException", ex.GetType().Name); }
        }

        [TestMethod()]
        public void RemoverCidade()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var cidadeRemover = repo.GetByName("Porto Alegre");

                repo.Remove(cidadeRemover);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void RemoverCidadeInexistente()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new CityRepository(config);
                var cidadeRemover = repo.GetByName("Por Alegre");

                repo.Remove(cidadeRemover);
            }
            catch (Exception ex)
            { Assert.AreEqual("CustomValidationException", ex.GetType().Name); }
        }

        [TestMethod()]
        public void CadastrarTemperaturaSemCidade()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new TemperatureRepository(config);
                repo.Add(new Temperature { Date = DateTime.Now, Degrees = 18 });
            }
            catch (Exception ex)
            { Assert.AreEqual("CustomValidationException", ex.GetType().Name); }
        }

        [TestMethod()]
        public void CadastrarTemperaturaIdCidadeInvalidos()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new TemperatureRepository(config);
                repo.GetAll(4566);
            }
            catch (Exception ex)
            { Assert.AreEqual("CustomValidationException", ex.GetType().Name); }
        }

        [TestMethod()]
        public void ObterHistoricoTemperaturasCidadesInexistente()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new TemperatureRepository(config);
                var result = new Temperature { CityId = 1000 };
                repo.GetByDate(result, DateTime.Now.AddMinutes(-6));
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CustomValidationException", ex.GetType().Name);
            }
        }

        [TestMethod()]
        public void RemoverTemperaturaSemCidade()
        {
            try
            {
                config = GetConfiguration(Directory.GetCurrentDirectory());
                var repo = new TemperatureRepository(config);
                var result = new Temperature { Id = 1, Date = DateTime.Now };
                repo.Remove(result);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CustomValidationException", ex.GetType().Name);
            }
        }
    }
}
