using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_MGE
{
    class Variaveis_Globais
    {
        #region Atributos
        private static string vCliente;
        private static string vCidade;
        private static string vFase;
        private static string vTipo_Servico;
        private static string vClasse;
        private static string vGrupo;
        private static string vPosicao;
        private static string vUsuario;
        private static string vPerfilUsuario;
        private static string vUrl;
        #endregion

        #region Propriedade
        public static string Cliente
        {
            get { return vCliente; }
            set { vCliente = value; }
        }

        public static string Cidade
        {
            get { return vCidade; }
            set { vCidade = value; }
        }
        public static string Fase
        {
            get { return vFase; }
            set { vFase = value; }
        }
        public static string Tipo_Servico
        {
            get { return vTipo_Servico; }
            set { vTipo_Servico = value; }
        }
        public static string Classe
        {
            get { return vClasse; }
            set { vClasse = value; }
        }
        public static string Posicao
        {
            get { return vPosicao; }
            set { vPosicao = value; }
        }
        public static string Grupo
        {
            get { return vGrupo; }
            set { vGrupo = value; }
        }

        public static string Usuario
        {
            get { return vUsuario; }
            set { vUsuario = value; }
        }

        public static string PerfilUsuario
        {
            get { return vPerfilUsuario; }
            set { vPerfilUsuario = value; }
        }

        public static string Url
        {
            get { return vUrl; }
            set { vUrl = value; }
        }
        #endregion
    }
}