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
    public class TestesRepositorio
    {
        #region Configuration

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(TestContext.Properties["TestSettingsPath"].ToString())
                .AddJsonFile("testSettings.json", optional: false, reloadOnChange: true);

            builder = builder.AddEnvironmentVariables();
            return builder.Build();
        }

        #endregion

        private TestContext m_testContext;

        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        IConfiguration config;


        [TestInitialize()]
        public void TestesRepositorioInitialize()
        {
            // Path para o arquivo de configuração
            TestContext.Properties.Add("TestSettingsPath", @"C:\Users\Leonardo\source\repos\MonitoringApi\Repository.Tests");

            config = GetConfiguration();
            TestContext.Properties.Add("NovaCidade", new City { Name = "Curitiba" });
            TestContext.Properties.Add("CidadeDuplicada", new City { Name = "Curitiba" });
            TestContext.Properties.Add("CidadeInexistente", new City { Id = 9999, Name = "Porco Alegre" });
            TestContext.Properties.Add("TemperaturaSemCidade", new Temperature { Degrees = 22, Date=DateTime.Now });
            TestContext.Properties.Add("TemperaturaComCidadeInvalida", new Temperature { CityId = 9999, Degrees = 22, Date = DateTime.Now });
            TestContext.Properties.Add("TemperaturaComDataInvalida", new Temperature { Degrees = 22, Date = DateTime.MinValue });
        }


        /// <summary>
        /// Teste de cadastro de cidade para monitoramento
        /// </summary>
        [TestMethod()]
        public void CadastrarCidade()
        {
            try
            {
                var repo = new CityRepository(config);
                var city = (City)TestContext.Properties["NovaCidade"];
                repo.Add(city);
            }
            catch (Exception) { Assert.Fail(); }
        }

        /// <summary>
        /// teste de cadastro de cidade duplicada
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void CadastrarCidadeDuplicada()
        {
            var repo = new CityRepository(config);
            var city = (City)TestContext.Properties["CidadeDuplicada"];
            repo.Add(city);

            // Assert - Expects exception
        }

        /// <summary>
        /// teste de listagem de cidades cadastradas
        /// </summary>
        [TestMethod()]
        public void ListarCidades()
        {
            try
            {
                var repo = new CityRepository(config);
                var result = repo.GetAll();
            }
            catch (Exception) { Assert.Fail(); }
        }

        /// <summary>
        /// Teste de obter cidade por nome
        /// </summary>
        [TestMethod()]
        public void ObterCidadePorNome()
        {
            try
            {
                var repo = new CityRepository(config);
                var result = repo.GetByName((TestContext.Properties["NovaCidade"] as City).Name);
            }
            catch (Exception) { Assert.Fail(); }
        }

        /// <summary>
        /// Teste de obter cidade não cadastrada por nome
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void ObterCidadeNaoExistente()
        {
            var repo = new CityRepository(config);
            var result = repo.GetByName((TestContext.Properties["CidadeInexistente"] as City).Name);

            // Assert - Expects exception
        }

        /// <summary>
        /// Teste de cadastro de temperaturas sem uma cidade relacionada
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void CadastrarTemperaturaSemCidade()
        {
            var repo = new TemperatureRepository(config);
            repo.Add((TestContext.Properties["TemperaturaSemCidade"] as Temperature));

            // Assert - Expects exception
        }

        /// <summary>
        /// Teste de cadastro de temperatura com cidade não inválida
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void CadastrarTemperaturaDataInvalida()
        {
            var repo = new TemperatureRepository(config);
            repo.Add((TestContext.Properties["TemperaturaComDataInvalida"] as Temperature));

            // Assert - Expects exception
        }

        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void ObterHistoricoTemperaturasCidadesInexistente()
        {
            var repo = new TemperatureRepository(config);
            repo.GetByDate((TestContext.Properties["TemperaturaSemCidade"] as Temperature), DateTime.Now);
        }

        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void RemoverTemperaturaSemCidade()
        {
            var repo = new TemperatureRepository(config);
            repo.Remove((TestContext.Properties["TemperaturaSemCidade"] as Temperature));

            // Assert - Expects exception
        }

        [TestMethod()]
        public void RemoverCidade()
        {
            try
            {
                var repo = new CityRepository(config);
                var cidadeRemover = repo.GetByName((TestContext.Properties["NovaCidade"] as City).Name);

                repo.Remove(cidadeRemover);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(CustomValidationException))]
        public void RemoverCidadeInexistente()
        {
            var repo = new CityRepository(config);
            var cidadeRemover = (City)TestContext.Properties["CidadeInexistente"];

            repo.Remove(cidadeRemover);

            // Assert - Expects exception
        }
    }
}
