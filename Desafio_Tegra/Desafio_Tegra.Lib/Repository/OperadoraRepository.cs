using Desafio_Tegra.Lib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Desafio_Tegra.Lib.Repository
{
    public static class OperadoraRepository
    {
        private static List<Voo> GetVooFromCSVFIle()
        {          
            // faz a leitura do .csv, e converte para stream.
            List<Voo> ListVoo = new List<Voo>();
            StreamReader stream = new StreamReader(@"Assets\uberair.csv");
            string linha = null;

            // Realiza a leitura do linha a linha do arquivo
            while ((linha = stream.ReadLine()) != null)
            {
                // Pega a linha em questão e faz o split(‘,’) para separar os dados de cada coluna
                string[] c = linha.Split(',');
                
                // condição para não pegar a primeira linha onde se encontra o cabeçalho
                if (c[0] != "numero_voo")

                // Cria um novo objeto Voo onde será adicionado os dado vindo da linha em questão do arquivo e adiciona na lista que será retornada 
                    ListVoo.Add(new Voo()
                    {
                        voo = c[0],
                        Origem = c[1],
                        Destino = c[2],
                        DataSaida = Convert.ToDateTime(c[3]),
                        Saida = Convert.ToDateTime(c[4]).TimeOfDay,
                        Chegada = Convert.ToDateTime(c[5]).TimeOfDay,
                        Preco = Convert.ToDecimal(c[6]),
                        idOperadora = 2

                    });
            }

            return ListVoo;
        }
       
        #region Read Json Files
        private static List<Voo> GetVooFromJsonFile()
        {
            // faz a leitura do json, e converte para array.
            List<Voo> ListVoo = new List<Voo>();
            var json = File.ReadAllText(@"Assets\99planes.json");
            dynamic array = JsonConvert.DeserializeObject(json);

            // percorre a array, criando o objeto de retorno.
            foreach (var item in array)
            {
                ListVoo.Add(new Voo()
                {
                    voo = item.voo,
                    Origem = item.origem,
                    Destino = item.destino,
                    DataSaida = item.data_saida,
                    Saida = item.saida,
                    Chegada = item.chegada,
                    Preco = item.valor,
                    idOperadora = 1


                });
            }
            return ListVoo;
        }

        private static List<Aeroporto> GetAirportsFromJsonFile()
        {
            // faz a leitura do json, e converte para array.
            List<Aeroporto> ListAeroporto = new List<Aeroporto>();
            var json = File.ReadAllText(@"Assets\aeroportos.json");
            dynamic array = JsonConvert.DeserializeObject(json);

            // percorre a array, criando o objeto de retorno.
            foreach (var item in array)
            {
                ListAeroporto.Add(new Aeroporto()
                {
                    nome = item.nome,
                    aeroporto = item.aeroporto,
                    cidade = item.cidade

                });
            }
            return ListAeroporto;
        }
        #endregion

        public static List<Voo> GetFlyData()
        {
            // lista de voos
            List<Voo> ret = new List<Voo>();

            // adiciona na lista, os voos tanto da 99plane como da UberAirLine
            ret.AddRange(GetVooFromCSVFIle());
            ret.AddRange(GetVooFromJsonFile());

            // retorna a lista completa.
            return ret;
        }

        public static List<Aeroporto> GetAirportData() => GetAirportsFromJsonFile();
    }
}
