namespace WEB_MGE
{
    public class Constantes
    {
        // Constantes para acesso ao Banco de Dados da aplicação
        // public const string ENDERECO_SERVIDOR   = "201.21.193.230";
        public const string ENDERECO_SERVIDOR   = "mgers.dyndns.org";
        public const string DATABASE            = "DB_WEBMGE";
        public const string USUARIO_DATABASE    = "SYSDBA";
        public const string SENHA_DATABASE      = "masterkey";
        public const string STRING_CONEXAO      = ("server=" + ENDERECO_SERVIDOR + 
                                                    "; User Id=" + USUARIO_DATABASE + 
                                                    "; database=" + DATABASE + 
                                                    "; password=" + SENHA_DATABASE );

        public const string DATABASE_TB_PAINEL = "GERAL_SGE";
        public const string STRING_CONEXAO_TB_PAINEL = ("server=" + ENDERECO_SERVIDOR +
                                                        "; User Id=" + USUARIO_DATABASE +
                                                        "; database=" + DATABASE_TB_PAINEL +
                                                        "; password=" + SENHA_DATABASE);

        public const string PROJETO = "projeto";
        public const string SERVICO = "servico";
        public const string AMOSTRA_RESIDENCIAL = "amostra_residencial";
        public const string AMOSTRA_COMERCIAL = "amostra_comercial";
        public const string AMOSTRA_INDUSTRIAL = "amostra_industrial";
        public const string AMOSTRA_PUBLICO = "amostra_publico";
        public const string AMOSTRA_RURAL = "amostra_rural";
        public const string AMOSTRA_TRAFO = "amostra_trafo";
        public const string VISITAS_INSTALACAO = "visitas_instalacao";
        public const string VISITAS_LEITURA = "visitas_leitura";
        public const string LEITURA_PERDIDA = "leitura_perdida";
        public const string DATA_HORA = "data_hora";
        public const string INSTALACAO_RESIDENCIAL = "instalacao_residencial";
        public const string INSTALACAO_COMERCIAL = "instalacao_comercial";
        public const string INSTALACAO_INDUSTRIAL = "instalacao_industrial";
        public const string INSTALACAO_PUBLICO = "instalacao_publico";
        public const string INSTALACAO_RURAL = "instalacao_rural";
        public const string INSTALACAO_TRAFO = "instalacao_trafo";
        public const string LEITURA_RESIDENCIAL = "leitura_residencial";
        public const string LEITURA_COMERCIAL = "leitura_comercial";
        public const string LEITURA_INDUSTRIAL = "leitura_industrial";
        public const string LEITURA_PUBLICO = "leitura_publico";
        public const string LEITURA_RURAL = "leitura_rural";
        public const string LEITURA_TRAFO = "leitura_trafo";
        public const string RETIRADA_RESIDENCIAL = "retirada_residencial";
        public const string RETIRADA_COMERCIAL = "retirada_comercial";
        public const string RETIRADA_INDUSTRIAL = "retirada_industrial";
        public const string RETIRADA_PUBLICO = "retirada_publico";
        public const string RETIRADA_RURAL = "retirada_rural";
        public const string RETIRADA_TRAFO = "retirada_trafo";
        public const string DEPURACAO_RESIDENCIAL = "depuracao_residencial";
        public const string DEPURACAO_COMERCIAL = "depuracao_comercial";
        public const string DEPURACAO_INDUSTRIAL = "depuracao_industrial";
        public const string DEPURACAO_PUBLICO = "depuracao_publico";
        public const string DEPURACAO_RURAL = "depuracao_rural";
        public const string DEPURACAO_TRAFO = "depuracao_trafo";
        public const string ENVIO_RESIDENCIAL = "envio_residencial";
        public const string ENVIO_COMERCIAL = "envio_comercial";
        public const string ENVIO_INDUSTRIAL = "envio_industrial";
        public const string ENVIO_PUBLICO = "envio_publico";
        public const string ENVIO_RURAL = "envio_rural";
        public const string ENVIO_TRAFO = "envio_trafo";

        public const string UC = "UC";
        public const string GRUPO = "GRUPO";
        public const string POSICAO = "POSICAO";
        public const string CLASSE = "CLASSE";
        public const string SUBCLASSE = "SUBCLASSE";
        public const string NOME_CONSUMIDOR = "NOME_CONSUMIDOR";
        public const string ENDERECO_CONSUMIDOR = "ENDERECO_CONSUMIDOR";
        public const string BAIRRO_CONSUMIDOR = "BAIRRO_CONSUMIDOR";
        public const string CIDADE_CONSUMIDOR = "CIDADE_CONSUMIDOR";
        public const string FASE_CONSUMIDOR = "FASE_CONSUMIDOR";
        public const string TIPO_EQUIPAMENTO = "TIPO_EQUIPAMENTO";
        public const string TIPO_SERVICO = "TIPO_SERVICO";
        public const string STATUS_INSTALACAO = "STATUS_INSTALACAO";
        public const string STATUS_REINSTALACAO = "STATUS_REINSTALACAO";
        public const string STATUS_RETIRADA = "STATUS_RETIRADA";
        public const string LATITUDE = "LATITUDE_CONSUMIDOR";
        public const string LONGITUDE = "LONGITUDE_CONSUMIDOR";
        public const string LATITUDE_INSTALACAO = "LATITUDE_CONSUMIDOR_INSTALACAO";
        public const string LONGITUDE_INSTALACAO = "LONGITUDE_CONSUMIDOR_INSTALACAO";

        public const string PROPRIETARIO_CELULAR = "PROPRIETARIO";
        public const string MEI_CELULAR = "MEI";
        public const string NUMERO_CELULAR = "NUMERO";
        public const string LATITUDE_ATUAL_CELULAR = "LATITUDE_ATUAL";
        public const string LONGITUDE_ATUAL_CELULAR = "LONGITUDE_ATUAL";
        public const string DATA_HORA_CELULAR = "DATA_HORA";
    }
}