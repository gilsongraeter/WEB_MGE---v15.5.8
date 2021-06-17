using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_MGE
{
    class Variaveis_Globais
    {
        #region Atributos
        private static string vNROOP;
        private static string vQtdOP;
        private static string vCodModelo;
        private static string vModelo;
        private static string vQtdMontagem;
        private static string vQtdTenAplicada;
        private static string vQtdExatidao;
        private static string vQtdMarcaLaser;
        private static string vQtdTesteFinal;

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
        private static string vHost;
        private static string vProjetoAtual;
        private static string vUltimoProjeto;
        private static string vUltimoMapa;
        private static string vDiretorioRaiz;

        private static string vDataHora;

        private static bool vServidor;
        private static bool vTrocaProjeto;
        #endregion

        #region Propriedade
        public static string NroOP
        {
            get { return vNROOP; }
            set { vNROOP = value; }
        }

        public static string QtdOP
        {
            get { return vQtdOP; }
            set { vQtdOP = value; }
        }

        public static string CodModelo
        {
            get { return vCodModelo; }
            set { vCodModelo = value; }
        }

        public static string Modelo
        {
            get { return vModelo; }
            set { vModelo = value; }
        }

        public static string QtdMontagem
        {
            get { return vQtdMontagem; }
            set { vQtdMontagem = value; }
        }

        public static string QtdTenAplicada
        {
            get { return vQtdTenAplicada; }
            set { vQtdTenAplicada = value; }
        }

        public static string QtdExatidao
        {
            get { return vQtdExatidao; }
            set { vQtdExatidao = value; }
        }

        public static string QtdMarcaLaser
        { 
            get { return vQtdMarcaLaser; }
            set { vQtdMarcaLaser = value; }
        }

        public static string QtdTesteFinal
        {
            get { return vQtdTesteFinal; }
            set { vQtdTesteFinal = value; }
        }
        
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

        public static string Host
        {
            get { return vHost; }
            set { vHost = value; }
        }

        public static string ProjetoAtual
        {
            get { return vProjetoAtual; }
            set { vProjetoAtual = value; }
        }

        public static string UltimoProjeto
        {
            get { return vUltimoProjeto; }
            set { vUltimoProjeto = value; }
        }

        public static string UltimoMapa
        {
            get { return vUltimoMapa; }
            set { vUltimoMapa = value; }
        }

        public static string DiretorioRaiz
        {
            get { return vDiretorioRaiz; }
            set { vDiretorioRaiz = value; }
        }

        public static string DataHora
        {
            get { return vDataHora; }
            set { vDataHora = value; }
        }

        public static bool Servidor
        {
            get { return vServidor; }
            set { vServidor = value; }
        }

        public static bool TrocaProjeto
        {
            get { return vTrocaProjeto; }
            set { vTrocaProjeto = value; }
        }
        #endregion
    }
}