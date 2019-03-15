using Desafio_Tegra.Lib.Models;
using System.Collections.Generic;
using Desafio_Tegra.Lib.Repository;
using System;
using System.Linq;

namespace Desafio_Tegra.Lib.Business
{
    public static class OperadoraBusiness
    {
        /// <summary>
        /// Retorna os dados de todos os aeroportos
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Aeroporto> GetAllAirports() => OperadoraRepository.GetAirportData();

        /// <summary>
        /// Retorna os dados de todos os voos e suas escalas 
        /// </summary>
        /// <param name="origem">Origem do vôo</param>
        /// <param name="destino">Destino do vôo</param>
        /// <param name="data">Data de partida do vôo</param>
        /// <returns></returns>
        public static Voo GetFlyWithScale(string origem, string destino, DateTime data)
        {           
            // obtem apenas as horas da data.            
            string hora = data.ToString("HH:mm:ss");
            
            // separa pelo caracter, e obtem o array de horario.
		    string[] horas = hora.Split(':');	

            // cria o horas minutos
            TimeSpan horasMinutos = new TimeSpan(Convert.ToInt32(horas[0]),Convert.ToInt32(hora[1]),Convert.ToInt32(hora[2]));

            // adiciona 12 horas.
            TimeSpan proximasHoras = horasMinutos.Add( new TimeSpan(12,0,0));
                    		    
            // formata a data para o padrao.
            string diaSaida = data.ToString("yyyy-MM-dd");
            
            // busca todos os voos
            List<Voo> ListaVoos = OperadoraRepository.GetFlyData();                        
                           
            // busca os voo com mesma origem de saida, no mesmo dia, nas proximas 12horas
            List<Voo> OrigemSimilar = ListaVoos.Where(v => v.Origem == origem && v.DataSaida.ToString("yyyy-MM-dd") == diaSaida && v.Saida <= proximasHoras).ToList();

            // lista de escalas.
            List<Voo> escalas = new List<Voo>();

            // objeto de ultima escala.
            Voo UltimaEscala;

            foreach(Voo voo in OrigemSimilar){
                // caso na lista de voos, haja alguem que saia do seu destino, e o destino seja o destino final, encontrou a 2 e ultima escala.                
                UltimaEscala = ListaVoos.Where(v => v.Origem == voo.Destino && v.Destino == destino)?.FirstOrDefault();

                // se encontrou a ultima escala, termina o loop, limitando os Voo a apenas 2 escalas.
                if (UltimaEscala != null){   

                    // salva a primeira escala
                    escalas.Add(voo);

                    // salva a ultima escala
                    escalas.Add(UltimaEscala);
                    break;
                }
            }

            // ternario para setar nome das operadoras aereas.                                                        
            escalas.Select(c => c.Operadora = c.idOperadora == 1? "99planes" : "UberAir" ).ToList();

             Voo retorno = new Voo{                
                Origem = origem,
                Destino = destino,
                DataSaida = Convert.ToDateTime(diaSaida),
                Saida = escalas.First().Saida,
                Chegada = escalas.Last().Chegada,
                trechos = escalas
            };
            
            return retorno;
        }        
    }
}
