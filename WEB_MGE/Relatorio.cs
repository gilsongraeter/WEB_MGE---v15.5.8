using System.Collections.Generic;
using System.Xml;

namespace WEB_MGE
{
    public class Relatorio
    {
        #region Atributos
        private string vCliente;
        private string vServico;
        private string vAmostraRes;
        private string vAmostraCom;
        private string vAmostraInd;
        private string vAmostraPub;
        private string vAmostraRur;
        private string vAmostraTra;
        private string vInsVisitas;
        private string vInsReinstalados;
        private string vLeiPerdida;
        private string vDHAtualiza;
        private string vInsRes;
        private string vInsResAmostra;
        private string vInsCom;
        private string vInsComAmostra;
        private string vInsInd;
        private string vInsIndAmostra;
        private string vInsPub;
        private string vInsPubAmostra;
        private string vInsRur;
        private string vInsRurAmostra;
        private string vInsTra;
        private string vInsTraAmostra;
        private string vLeiRes;
        private string vLeiResAmostra;
        private string vLeiCom;
        private string vLeiComAmostra;
        private string vLeiInd;
        private string vLeiIndAmostra;
        private string vLeiPub;
        private string vLeiPubAmostra;
        private string vLeiRur;
        private string vLeiRurAmostra;
        private string vLeiTra;
        private string vLeiTraAmostra;
        private string vRetRes;
        private string vRetResAmostra;
        private string vRetCom;
        private string vRetComAmostra;
        private string vRetInd;
        private string vRetIndAmostra;
        private string vRetPub;
        private string vRetPubAmostra;
        private string vRetRur;
        private string vRetRurAmostra;
        private string vRetTra;
        private string vRetTraAmostra;
        private string vDepRes;
        private string vDepResAmostra;
        private string vDepCom;
        private string vDepComAmostra;
        private string vDepInd;
        private string vDepIndAmostra;
        private string vDepPub;
        private string vDepPubAmostra;
        private string vDepRur;
        private string vDepRurAmostra;
        private string vDepTra;
        private string vDepTraAmostra;
        private string vEnvRes;
        private string vEnvResAmostra;
        private string vEnvCom;
        private string vEnvComAmostra;
        private string vEnvInd;
        private string vEnvIndAmostra;
        private string vEnvPub;
        private string vEnvPubAmostra;
        private string vEnvRur;
        private string vEnvRurAmostra;
        private string vEnvTra;
        private string vEnvTraAmostra;
        #endregion


        #region Propriedades       
        public string VCliente
        {
            get
            {
                return vCliente;
            }

            set
            {
                vCliente = value;
            }
        }

        public string VServico
        {
            get
            {
                return vServico;
            }

            set
            {
                vServico = value;
            }
        }

        public string VAmostraRes
        {
            get
            {
                return vAmostraRes;
            }

            set
            {
                vAmostraRes = value;
            }
        }

        public string VAmostraCom
        {
            get
            {
                return vAmostraCom;
            }

            set
            {
                vAmostraCom = value;
            }
        }

        public string VAmostraInd
        {
            get
            {
                return vAmostraInd;
            }

            set
            {
                vAmostraInd = value;
            }
        }

        public string VAmostraPub
        {
            get
            {
                return vAmostraPub;
            }

            set
            {
                vAmostraPub = value;
            }
        }

        public string VAmostraRur
        {
            get
            {
                return vAmostraRur;
            }

            set
            {
                vAmostraRur = value;
            }
        }

        public string VAmostraTra
        {
            get
            {
                return vAmostraTra;
            }

            set
            {
                vAmostraTra = value;
            }
        }

        public string VInsVisitas
        {
            get
            {
                return vInsVisitas;
            }

            set
            {
                vInsVisitas = value;
            }
        }

        public string VInsReinstalados
        {
            get
            {
                return vInsReinstalados;
            }

            set
            {
                vInsReinstalados = value;
            }
        }

        public string VLeiPerdida
        {
            get
            {
                return vLeiPerdida;
            }

            set
            {
                vLeiPerdida = value;
            }
        }

        public string VDHAtualiza
        {
            get
            {
                return vDHAtualiza;
            }

            set
            {
                vDHAtualiza = value;
            }
        }

        public string VInsRes
        {
            get
            {
                return vInsRes;
            }

            set
            {
                vInsRes = value;
            }
        }

        public string VInsResAmostra
        {
            get
            {
                return vInsResAmostra;
            }

            set
            {
                vInsResAmostra = value;
            }
        }

        public string VInsCom
        {
            get
            {
                return vInsCom;
            }

            set
            {
                vInsCom = value;
            }
        }

        public string VInsComAmostra
        {
            get
            {
                return vInsComAmostra;
            }

            set
            {
                vInsComAmostra = value;
            }
        }

        public string VInsInd
        {
            get
            {
                return vInsInd;
            }

            set
            {
                vInsInd = value;
            }
        }

        public string VInsIndAmostra
        {
            get
            {
                return vInsIndAmostra;
            }

            set
            {
                vInsIndAmostra = value;
            }
        }

        public string VInsPub
        {
            get
            {
                return vInsPub;
            }

            set
            {
                vInsPub = value;
            }
        }

        public string VInsPubAmostra
        {
            get
            {
                return vInsPubAmostra;
            }

            set
            {
                vInsPubAmostra = value;
            }
        }

        public string VInsRur
        {
            get
            {
                return vInsRur;
            }

            set
            {
                vInsRur = value;
            }
        }

        public string VInsRurAmostra
        {
            get
            {
                return vInsRurAmostra;
            }

            set
            {
                vInsRurAmostra = value;
            }
        }

        public string VInsTra
        {
            get
            {
                return vInsTra;
            }

            set
            {
                vInsTra = value;
            }
        }

        public string VInsTraAmostra
        {
            get
            {
                return vInsTraAmostra;
            }

            set
            {
                vInsTraAmostra = value;
            }
        }

        public string VLeiRes
        {
            get
            {
                return vLeiRes;
            }

            set
            {
                vLeiRes = value;
            }
        }

        public string VLeiResAmostra
        {
            get
            {
                return vLeiResAmostra;
            }

            set
            {
                vLeiResAmostra = value;
            }
        }

        public string VLeiCom
        {
            get
            {
                return vLeiCom;
            }

            set
            {
                vLeiCom = value;
            }
        }

        public string VLeiComAmostra
        {
            get
            {
                return vLeiComAmostra;
            }

            set
            {
                vLeiComAmostra = value;
            }
        }

        public string VLeiInd
        {
            get
            {
                return vLeiInd;
            }

            set
            {
                vLeiInd = value;
            }
        }

        public string VLeiIndAmostra
        {
            get
            {
                return vLeiIndAmostra;
            }

            set
            {
                vLeiIndAmostra = value;
            }
        }

        public string VLeiPub
        {
            get
            {
                return vLeiPub;
            }

            set
            {
                vLeiPub = value;
            }
        }

        public string VLeiPubAmostra
        {
            get
            {
                return vLeiPubAmostra;
            }

            set
            {
                vLeiPubAmostra = value;
            }
        }

        public string VLeiRur
        {
            get
            {
                return vLeiRur;
            }

            set
            {
                vLeiRur = value;
            }
        }

        public string VLeiRurAmostra
        {
            get
            {
                return vLeiRurAmostra;
            }

            set
            {
                vLeiRurAmostra = value;
            }
        }

        public string VLeiTra
        {
            get
            {
                return vLeiTra;
            }

            set
            {
                vLeiTra = value;
            }
        }

        public string VLeiTraAmostra
        {
            get
            {
                return vLeiTraAmostra;
            }

            set
            {
                vLeiTraAmostra = value;
            }
        }

        public string VRetRes
        {
            get
            {
                return vRetRes;
            }

            set
            {
                vRetRes = value;
            }
        }

        public string VRetResAmostra
        {
            get
            {
                return vRetResAmostra;
            }

            set
            {
                vRetResAmostra = value;
            }
        }

        public string VRetCom
        {
            get
            {
                return vRetCom;
            }

            set
            {
                vRetCom = value;
            }
        }

        public string VRetComAmostra
        {
            get
            {
                return vRetComAmostra;
            }

            set
            {
                vRetComAmostra = value;
            }
        }

        public string VRetInd
        {
            get
            {
                return vRetInd;
            }

            set
            {
                vRetInd = value;
            }
        }

        public string VRetIndAmostra
        {
            get
            {
                return vRetIndAmostra;
            }

            set
            {
                vRetIndAmostra = value;
            }
        }

        public string VRetPub
        {
            get
            {
                return vRetPub;
            }

            set
            {
                vRetPub = value;
            }
        }

        public string VRetPubAmostra
        {
            get
            {
                return vRetPubAmostra;
            }

            set
            {
                vRetPubAmostra = value;
            }
        }

        public string VRetRur
        {
            get
            {
                return vRetRur;
            }

            set
            {
                vRetRur = value;
            }
        }

        public string VRetRurAmostra
        {
            get
            {
                return vRetRurAmostra;
            }

            set
            {
                vRetRurAmostra = value;
            }
        }

        public string VRetTra
        {
            get
            {
                return vRetTra;
            }

            set
            {
                vRetTra = value;
            }
        }

        public string VRetTraAmostra
        {
            get
            {
                return vRetTraAmostra;
            }

            set
            {
                vRetTraAmostra = value;
            }
        }

        public string VDepRes
        {
            get
            {
                return vDepRes;
            }

            set
            {
                vDepRes = value;
            }
        }

        public string VDepResAmostra
        {
            get
            {
                return vDepResAmostra;
            }

            set
            {
                vDepResAmostra = value;
            }
        }

        public string VDepCom
        {
            get
            {
                return vDepCom;
            }

            set
            {
                vDepCom = value;
            }
        }

        public string VDepComAmostra
        {
            get
            {
                return vDepComAmostra;
            }

            set
            {
                vDepComAmostra = value;
            }
        }

        public string VDepInd
        {
            get
            {
                return vDepInd;
            }

            set
            {
                vDepInd = value;
            }
        }

        public string VDepIndAmostra
        {
            get
            {
                return vDepIndAmostra;
            }

            set
            {
                vDepIndAmostra = value;
            }
        }

        public string VDepPub
        {
            get
            {
                return vDepPub;
            }

            set
            {
                vDepPub = value;
            }
        }

        public string VDepPubAmostra
        {
            get
            {
                return vDepPubAmostra;
            }

            set
            {
                vDepPubAmostra = value;
            }
        }

        public string VDepRur
        {
            get
            {
                return vDepRur;
            }

            set
            {
                vDepRur = value;
            }
        }

        public string VDepRurAmostra
        {
            get
            {
                return vDepRurAmostra;
            }

            set
            {
                vDepRurAmostra = value;
            }
        }

        public string VDepTra
        {
            get
            {
                return vDepTra;
            }

            set
            {
                vDepTra = value;
            }
        }

        public string VDepTraAmostra
        {
            get
            {
                return vDepTraAmostra;
            }

            set
            {
                vDepTraAmostra = value;
            }
        }

        public string VEnvRes
        {
            get
            {
                return vEnvRes;
            }

            set
            {
                vEnvRes = value;
            }
        }

        public string VEnvResAmostra
        {
            get
            {
                return vEnvResAmostra;
            }

            set
            {
                vEnvResAmostra = value;
            }
        }

        public string VEnvCom
        {
            get
            {
                return vEnvCom;
            }

            set
            {
                vEnvCom = value;
            }
        }

        public string VEnvComAmostra
        {
            get
            {
                return vEnvComAmostra;
            }

            set
            {
                vEnvComAmostra = value;
            }
        }

        public string VEnvInd
        {
            get
            {
                return vEnvInd;
            }

            set
            {
                vEnvInd = value;
            }
        }

        public string VEnvIndAmostra
        {
            get
            {
                return vEnvIndAmostra;
            }

            set
            {
                vEnvIndAmostra = value;
            }
        }

        public string VEnvPub
        {
            get
            {
                return vEnvPub;
            }

            set
            {
                vEnvPub = value;
            }
        }

        public string VEnvPubAmostra
        {
            get
            {
                return vEnvPubAmostra;
            }

            set
            {
                vEnvPubAmostra = value;
            }
        }

        public string VEnvRur
        {
            get
            {
                return vEnvRur;
            }

            set
            {
                vEnvRur = value;
            }
        }

        public string VEnvRurAmostra
        {
            get
            {
                return vEnvRurAmostra;
            }

            set
            {
                vEnvRurAmostra = value;
            }
        }

        public string VEnvTra
        {
            get
            {
                return vEnvTra;
            }

            set
            {
                vEnvTra = value;
            }
        }

        public string VEnvTraAmostra
        {
            get
            {
                return vEnvTraAmostra;
            }

            set
            {
                vEnvTraAmostra = value;
            }
        }
        #endregion        


        #region Métodos
        public static List<Relatorio> lerRelatorioXML(string Projeto)
        {
            // Instancia a lista que armazenará as informações
            List<Relatorio> listaDeInformacoes = new List<Relatorio>();

            // Objeto provisório para popular a lista de retorno
            Relatorio itemLista = new Relatorio();

            // Nome do arquivo que será aberto
            string nomeArquivo = ("C:\\FTP\\WEB_MGE\\relatorio_" + Projeto + ".xml");

            // Abre o arquivo XML dentro do objeto arquivoRelatorio
            XmlDocument arquivoRelatorio = new XmlDocument();

            arquivoRelatorio.Load(nomeArquivo.ToUpper());

            // Tenta ler o arquivo XML
            try
            {
                // Pega o nó de dados
                XmlNode noDados = arquivoRelatorio.SelectSingleNode("dados");

                // Pega o nó de valores
                XmlNode noValores = noDados.SelectSingleNode("valores");

                // Pega as informações de cada nó de valores
                itemLista.vCliente = noValores.SelectSingleNode("vCliente").InnerText;
                itemLista.vServico = noValores.SelectSingleNode("vServico").InnerText;
                itemLista.vAmostraRes = noValores.SelectSingleNode("vAmostraRes").InnerText;
                itemLista.vAmostraCom = noValores.SelectSingleNode("vAmostraCom").InnerText;
                itemLista.vAmostraInd = noValores.SelectSingleNode("vAmostraInd").InnerText;
                itemLista.vAmostraPub = noValores.SelectSingleNode("vAmostraPub").InnerText;
                itemLista.vAmostraRur = noValores.SelectSingleNode("vAmostraRur").InnerText;
                itemLista.vAmostraTra = noValores.SelectSingleNode("vAmostraTra").InnerText;
                itemLista.vInsVisitas = noValores.SelectSingleNode("vInsVisitas").InnerText;
                itemLista.vInsReinstalados = noValores.SelectSingleNode("vInsReinstalados").InnerText;
                itemLista.vLeiPerdida = noValores.SelectSingleNode("vLeiPerdida").InnerText;
                itemLista.vDHAtualiza = noValores.SelectSingleNode("vDHAtualiza").InnerText;
                itemLista.vInsRes = noValores.SelectSingleNode("vInsRes").InnerText;
                itemLista.vInsResAmostra = noValores.SelectSingleNode("vInsResAmostra").InnerText;
                itemLista.vInsCom = noValores.SelectSingleNode("vInsCom").InnerText;
                itemLista.vInsComAmostra = noValores.SelectSingleNode("vInsComAmostra").InnerText;
                itemLista.vInsInd = noValores.SelectSingleNode("vInsInd").InnerText;
                itemLista.vInsIndAmostra = noValores.SelectSingleNode("vInsIndAmostra").InnerText;
                itemLista.vInsPub = noValores.SelectSingleNode("vInsPub").InnerText;
                itemLista.vInsPubAmostra = noValores.SelectSingleNode("vInsPubAmostra").InnerText;
                itemLista.vInsRur = noValores.SelectSingleNode("vInsRur").InnerText;
                itemLista.vInsRurAmostra = noValores.SelectSingleNode("vInsRurAmostra").InnerText;
                itemLista.vInsTra = noValores.SelectSingleNode("vInsTra").InnerText;
                itemLista.vInsTraAmostra = noValores.SelectSingleNode("vInsTraAmostra").InnerText;
                itemLista.vLeiRes = noValores.SelectSingleNode("vLeiRes").InnerText;
                itemLista.vLeiResAmostra = noValores.SelectSingleNode("vLeiResAmostra").InnerText;
                itemLista.vLeiCom = noValores.SelectSingleNode("vLeiCom").InnerText;
                itemLista.vLeiComAmostra = noValores.SelectSingleNode("vLeiComAmostra").InnerText;
                itemLista.vLeiInd = noValores.SelectSingleNode("vLeiInd").InnerText;
                itemLista.vLeiIndAmostra = noValores.SelectSingleNode("vLeiIndAmostra").InnerText;
                itemLista.vLeiPub = noValores.SelectSingleNode("vLeiPub").InnerText;
                itemLista.vLeiPubAmostra = noValores.SelectSingleNode("vLeiPubAmostra").InnerText;
                itemLista.vLeiRur = noValores.SelectSingleNode("vLeiRur").InnerText;
                itemLista.vLeiRurAmostra = noValores.SelectSingleNode("vLeiRurAmostra").InnerText;
                itemLista.vLeiTra = noValores.SelectSingleNode("vLeiTra").InnerText;
                itemLista.vLeiTraAmostra = noValores.SelectSingleNode("vLeiTraAmostra").InnerText;
                itemLista.vRetRes = noValores.SelectSingleNode("vRetRes").InnerText;
                itemLista.vRetResAmostra = noValores.SelectSingleNode("vRetResAmostra").InnerText;
                itemLista.vRetCom = noValores.SelectSingleNode("vRetCom").InnerText;
                itemLista.vRetComAmostra = noValores.SelectSingleNode("vRetComAmostra").InnerText;
                itemLista.vRetInd = noValores.SelectSingleNode("vRetInd").InnerText;
                itemLista.vRetIndAmostra = noValores.SelectSingleNode("vRetIndAmostra").InnerText;
                itemLista.vRetPub = noValores.SelectSingleNode("vRetPub").InnerText;
                itemLista.vRetPubAmostra = noValores.SelectSingleNode("vRetPubAmostra").InnerText;
                itemLista.vRetRur = noValores.SelectSingleNode("vRetRur").InnerText;
                itemLista.vRetRurAmostra = noValores.SelectSingleNode("vRetRurAmostra").InnerText;
                itemLista.vRetTra = noValores.SelectSingleNode("vRetTra").InnerText;
                itemLista.vRetTraAmostra = noValores.SelectSingleNode("vRetTraAmostra").InnerText;
                itemLista.vDepRes = noValores.SelectSingleNode("vDepRes").InnerText;
                itemLista.vDepResAmostra = noValores.SelectSingleNode("vDepResAmostra").InnerText;
                itemLista.vDepCom = noValores.SelectSingleNode("vDepCom").InnerText;
                itemLista.vDepComAmostra = noValores.SelectSingleNode("vDepComAmostra").InnerText;
                itemLista.vDepInd = noValores.SelectSingleNode("vDepInd").InnerText;
                itemLista.vDepIndAmostra = noValores.SelectSingleNode("vDepIndAmostra").InnerText;
                itemLista.vDepPub = noValores.SelectSingleNode("vDepPub").InnerText;
                itemLista.vDepPubAmostra = noValores.SelectSingleNode("vDepPubAmostra").InnerText;
                itemLista.vDepRur = noValores.SelectSingleNode("vDepRur").InnerText;
                itemLista.vDepRurAmostra = noValores.SelectSingleNode("vDepRurAmostra").InnerText;
                itemLista.vDepTra = noValores.SelectSingleNode("vDepTra").InnerText;
                itemLista.vDepTraAmostra = noValores.SelectSingleNode("vDepTraAmostra").InnerText;
                itemLista.vEnvRes = noValores.SelectSingleNode("vEnvRes").InnerText;
                itemLista.vEnvResAmostra = noValores.SelectSingleNode("vEnvResAmostra").InnerText;
                itemLista.vEnvCom = noValores.SelectSingleNode("vEnvCom").InnerText;
                itemLista.vEnvComAmostra = noValores.SelectSingleNode("vEnvComAmostra").InnerText;
                itemLista.vEnvInd = noValores.SelectSingleNode("vEnvInd").InnerText;
                itemLista.vEnvIndAmostra = noValores.SelectSingleNode("vEnvIndAmostra").InnerText;
                itemLista.vEnvPub = noValores.SelectSingleNode("vEnvPub").InnerText;
                itemLista.vEnvPubAmostra = noValores.SelectSingleNode("vEnvPubAmostra").InnerText;
                itemLista.vEnvRur = noValores.SelectSingleNode("vEnvRur").InnerText;
                itemLista.vEnvRurAmostra = noValores.SelectSingleNode("vEnvRurAmostra").InnerText;
                itemLista.vEnvTra = noValores.SelectSingleNode("vEnvTra").InnerText;
                itemLista.vEnvTraAmostra = noValores.SelectSingleNode("vEnvTraAmostra").InnerText;

                // Inclui o objeto temporário na lista
                listaDeInformacoes.Add(itemLista);
            }

            // Verifica se ocorreu alguma exceção na leitura do arquivo XML
            catch (XmlException)
            {
                // Caso tenha ocorrido, retorna a lista nula
                listaDeInformacoes = null;
            }

            // Retorna a lista de informações capturada a partir do arquivo XML
            return listaDeInformacoes;
        }
        #endregion
    }
}