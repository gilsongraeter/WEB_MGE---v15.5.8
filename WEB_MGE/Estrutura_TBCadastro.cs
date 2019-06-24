using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_MGE
{
    public class Estrutura_TBCadastro
    {
        #region Atributos
        private static string uc;
        private static string grupo;
        private static string posicao;
        private static string classe;
        private static string subclasse;
        private static string nome_consumidor;
        private static string endereco_consumidor;
        private static string bairro_consumidor;
        private static string cidade_consumidor;
        private static string fase_consumidor;
        private static string latitude;
        private static string longitude;
        private static string tipo_servico;
        private static string status_instalacao;
        private static string status_reinstalacao;
        private static string status_retirada;
        #endregion

        #region Propriedade
        public static string UC
        {
            get { return uc; }
            set { uc = value; }
        }

        public static string GRUPO
        {
            get { return grupo; }
            set { grupo = value; }
        }

        public static string POSICAO
        {
            get { return posicao; }
            set { posicao = value; }
        }

        public static string CLASSE
        {
            get { return classe; }
            set { classe = value; }
        }

        public static string SUBCLASSE
        {
            get { return subclasse; }
            set { subclasse = value; }
        }

        public static string NOME_CONSUMIDOR
        {
            get { return nome_consumidor; }
            set { nome_consumidor = value; }
        }

        public static string ENDERECO_CONSUMIDOR
        {
            get { return endereco_consumidor; }
            set { endereco_consumidor = value; }
        }

        public static string BAIRRO_CONSUMIDOR
        {
            get { return bairro_consumidor; }
            set { bairro_consumidor = value; }
        }

        public static string CIDADE_CONSUMIDOR
        {
            get { return cidade_consumidor; }
            set { cidade_consumidor = value; }
        }

        public static string FASE_CONSUMIDOR
        {
            get { return fase_consumidor; }
            set { fase_consumidor = value; }
        }

        public static string LATITUDE
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public static string LONGITUDE
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public static string TIPO_SERVICO
        {
            get { return tipo_servico; }
            set { tipo_servico = value; }
        }

        public static string STATUS_INSTALACAO
        {
            get { return status_instalacao; }
            set { status_instalacao = value; }
        }

        public static string STATUS_REINSTALACAO
        {
            get { return status_reinstalacao; }
            set { status_reinstalacao = value; }
        }

        public static string STATUS_RETIRADA
        {
            get { return status_retirada; }
            set { status_retirada = value; }
        }
        #endregion
    }
}