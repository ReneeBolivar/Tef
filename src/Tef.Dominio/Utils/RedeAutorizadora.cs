﻿using System.Collections.Generic;

namespace Tef.Dominio.Utils
{
    public static class RedeAutorizadora
    {
        private static Dictionary<string, string> Autorizadoras = new Dictionary<string, string>();

        static RedeAutorizadora()
        {
            Autorizadoras.Add("00000", "OUTRA, NÃO DEFINIDA");
            Autorizadoras.Add("00001", "TECBAN");
            Autorizadoras.Add("00002", "ITAÚ");
            Autorizadoras.Add("00003", "BRADESCO");
            Autorizadoras.Add("00004", "VISANET - ESPECIFICAÇÃO 200001");
            Autorizadoras.Add("00005", "REDECARD");
            Autorizadoras.Add("00006", "AMEX");
            Autorizadoras.Add("00007", "SOLLO");
            Autorizadoras.Add("00008", "E CAPTURE");
            Autorizadoras.Add("00009", "SERASA");
            Autorizadoras.Add("00010", "SPC BRASIL");
            Autorizadoras.Add("00011", "SERASA DETALHADO");
            Autorizadoras.Add("00012", "TELEDATA");
            Autorizadoras.Add("00013", "ACSP");
            Autorizadoras.Add("00014", "ACSP DETALHADO");
            Autorizadoras.Add("00015", "TECBIZ");
            Autorizadoras.Add("00016", "CDL DF");
            Autorizadoras.Add("00017", "REPOM");
            Autorizadoras.Add("00018", "STANDBY");
            Autorizadoras.Add("00019", "EDMCARD");
            Autorizadoras.Add("00020", "CREDICESTA");
            Autorizadoras.Add("00021", "BANRISUL");
            Autorizadoras.Add("00022", "ACC CARD");
            Autorizadoras.Add("00023", "CLUBCARD");
            Autorizadoras.Add("00024", "ACPR");
            Autorizadoras.Add("00025", "VIDALINK");
            Autorizadoras.Add("00026", "CCC_WEB");
            Autorizadoras.Add("00027", "EDIGUAY");
            Autorizadoras.Add("00028", "CARREFOUR");
            Autorizadoras.Add("00029", "SOFTWAY");
            Autorizadoras.Add("00030", "MULTICHEQUE");
            Autorizadoras.Add("00031", "TICKET COMBUSTÍVEL");
            Autorizadoras.Add("00032", "YAMADA");
            Autorizadoras.Add("00033", "CITIBANK");
            Autorizadoras.Add("00034", "INFOCARD");
            Autorizadoras.Add("00035", "BESC");
            Autorizadoras.Add("00036", "EMS");
            Autorizadoras.Add("00037", "CHEQUE CASH");
            Autorizadoras.Add("00038", "CENTRAL CARD");
            Autorizadoras.Add("00039", "DROGARAIA");
            Autorizadoras.Add("00040", "OUTRO SERVIÇO");
            Autorizadoras.Add("00041", "EDENRED");
            Autorizadoras.Add("00042", "EPAY GIFT");
            Autorizadoras.Add("00043", "PARATI");
            Autorizadoras.Add("00044", "TOKORO");
            Autorizadoras.Add("00045", "COOPERCRED");
            Autorizadoras.Add("00046", "SERVCEL");
            Autorizadoras.Add("00047", "SOROCRED");
            Autorizadoras.Add("00048", "VITAL");
            Autorizadoras.Add("00049", "SAX FINANCEIRA");
            Autorizadoras.Add("00050", "FORMOSA");
            Autorizadoras.Add("00051", "HIPERCARD");
            Autorizadoras.Add("00052", "TRICARD");
            Autorizadoras.Add("00053", "CHECK OK");
            Autorizadoras.Add("00054", "POLICARD");
            Autorizadoras.Add("00055", "CETELEM CARREFOUR");
            Autorizadoras.Add("00056", "LEADER");
            Autorizadoras.Add("00057", "CONSÓRCIO CREDICARD VENEZUELA");
            Autorizadoras.Add("00058", "GAZINCRED");
            Autorizadoras.Add("00059", "TELENET");
            Autorizadoras.Add("00060", "CHEQUE PRÉ");
            Autorizadoras.Add("00061", "BRASIL CARD");
            Autorizadoras.Add("00062", "EPHARMA");
            Autorizadoras.Add("00063", "TOTAL");
            Autorizadoras.Add("00064", "CONSÓRCIO AMEX VENEZUELA");
            Autorizadoras.Add("00065", "GAX");
            Autorizadoras.Add("00066", "PERALTA");
            Autorizadoras.Add("00067", "SERVIDOR PAGAMENTO");
            Autorizadoras.Add("00068", "BANESE");
            Autorizadoras.Add("00069", "RESOMAQ");
            Autorizadoras.Add("00070", "SYSDATA");
            Autorizadoras.Add("00071", "CDL POA");
            Autorizadoras.Add("00072", "BIGCARD");
            Autorizadoras.Add("00073", "DTRANSFER");
            Autorizadoras.Add("00074", "VIAVAREJO");
            Autorizadoras.Add("00075", "CHECK EXPRESS");
            Autorizadoras.Add("00076", "GIVEX");
            Autorizadoras.Add("00077", "VALECARD");
            Autorizadoras.Add("00078", "PORTAL CARD");
            Autorizadoras.Add("00079", "BANPARA");
            Autorizadoras.Add("00080", "SOFTNEX");
            Autorizadoras.Add("00081", "SUPERCARD");
            Autorizadoras.Add("00082", "GETNET");
            Autorizadoras.Add("00083", "PREVSAUDE");
            Autorizadoras.Add("00084", "BANCO POTTENCIAL");
            Autorizadoras.Add("00085", "SOPHUS");
            Autorizadoras.Add("00086", "MARISA 2");
            Autorizadoras.Add("00087", "MAXICRED");
            Autorizadoras.Add("00088", "BLACKHAWK");
            Autorizadoras.Add("00089", "EXPANSIVA");
            Autorizadoras.Add("00090", "SAS NT");
            Autorizadoras.Add("00091", "LEADER 2");
            Autorizadoras.Add("00092", "SOMAR");
            Autorizadoras.Add("00093", "CETELEM AURA");
            Autorizadoras.Add("00094", "CABAL");
            Autorizadoras.Add("00095", "CREDSYSTEM");
            Autorizadoras.Add("00096", "BANCO PROVINCIAL");
            Autorizadoras.Add("00097", "CARTESYS");
            Autorizadoras.Add("00098", "CISA");
            Autorizadoras.Add("00099", "TRNCENTRE");
            Autorizadoras.Add("00100", "ACPR D");
            Autorizadoras.Add("00101", "CARDCO");
            Autorizadoras.Add("00102", "CHECK CHECK");
            Autorizadoras.Add("00103", "CADASA");
            Autorizadoras.Add("00104", "PRIVATE BRADESCO");
            Autorizadoras.Add("00105", "CREDMAIS");
            Autorizadoras.Add("00106", "GWCEL");
            Autorizadoras.Add("00107", "CHECK EXPRESS 2");
            Autorizadoras.Add("00108", "GETNET PBM");
            Autorizadoras.Add("00109", "USECRED");
            Autorizadoras.Add("00110", "SERV VOUCHER");
            Autorizadoras.Add("00111", "TREDENEXX");
            Autorizadoras.Add("00112", "BONUS PRESENTE CARREFOUR");
            Autorizadoras.Add("00113", "CREDISHOP");
            Autorizadoras.Add("00114", "ESTAPAR");
            Autorizadoras.Add("00115", "BANCO IBI");
            Autorizadoras.Add("00116", "WORKERCARD");
            Autorizadoras.Add("00117", "TELECHEQUE");
            Autorizadoras.Add("00118", "OBOE");
            Autorizadoras.Add("00119", "PROTEGE");
            Autorizadoras.Add("00120", "SERASA CARDS");
            Autorizadoras.Add("00121", "HOTCARD");
            Autorizadoras.Add("00122", "BANCO PANAMERICANO");
            Autorizadoras.Add("00123", "BANCO MERCANTIL");
            Autorizadoras.Add("00124", "SIGACRED");
            Autorizadoras.Add("00125", "VISANET – ESPECIFICAÇÃO 4.1");
            Autorizadoras.Add("00126", "SPTRANS");
            Autorizadoras.Add("00127", "PRESENTE MARISA");
            Autorizadoras.Add("00128", "COOPLIFE");
            Autorizadoras.Add("00129", "BOD");
            Autorizadoras.Add("00130", "G CARD");
            Autorizadoras.Add("00131", "TCREDIT");
            Autorizadoras.Add("00132", "SISCRED");
            Autorizadoras.Add("00133", "FOXWINCARDS");
            Autorizadoras.Add("00134", "CONVCARD");
            Autorizadoras.Add("00135", "VOUCHER");
            Autorizadoras.Add("00136", "EXPAND CARDS");
            Autorizadoras.Add("00137", "ULTRAGAZ");
            Autorizadoras.Add("00138", "QUALICARD");
            Autorizadoras.Add("00139", "HSBC UK");
            Autorizadoras.Add("00140", "WAPPA");
            Autorizadoras.Add("00141", "SQCF");
            Autorizadoras.Add("00142", "INTELLISYS");
            Autorizadoras.Add("00143", "BOD DÉBITO");
            Autorizadoras.Add("00144", "ACCREDITO");
            Autorizadoras.Add("00145", "COMPROCARD");
            Autorizadoras.Add("00146", "ORGCARD");
            Autorizadoras.Add("00147", "MINASCRED");
            Autorizadoras.Add("00148", "FARMÁCIA POPULAR");
            Autorizadoras.Add("00149", "FIDELIDADE MAIS");
            Autorizadoras.Add("00150", "ITAÚ SHOPLINE");
            Autorizadoras.Add("00151", "CDL RIO");
            Autorizadoras.Add("00152", "FORTCARD");
            Autorizadoras.Add("00153", "PAGGO");
            Autorizadoras.Add("00154", "SMARTNET");
            Autorizadoras.Add("00155", "INTERFARMACIA");
            Autorizadoras.Add("00156", "VALECON");
            Autorizadoras.Add("00157", "CARTÃO EVANGÉLICO");
            Autorizadoras.Add("00158", "VEGASCARD");
            Autorizadoras.Add("00159", "SCCARD");
            Autorizadoras.Add("00160", "ORBITALL");
            Autorizadoras.Add("00161", "ICARDS");
            Autorizadoras.Add("00162", "FACILCARD");
            Autorizadoras.Add("00163", "FIDELIZE");
            Autorizadoras.Add("00164", "FINAMAX");
            Autorizadoras.Add("00165", "BANCO GE");
            Autorizadoras.Add("00166", "UNIK");
            Autorizadoras.Add("00167", "TIVIT");
            Autorizadoras.Add("00168", "VALIDATA");
            Autorizadoras.Add("00169", "BANESCARD");
            Autorizadoras.Add("00170", "CSU CARREFOUR");
            Autorizadoras.Add("00171", "VALESHOP");
            Autorizadoras.Add("00172", "SOMAR CARD");
            Autorizadoras.Add("00173", "OMNION");
            Autorizadoras.Add("00174", "CONDOR");
            Autorizadoras.Add("00175", "STANDBYDUP");
            Autorizadoras.Add("00176", "BPAG BOLDCRON");
            Autorizadoras.Add("00177", "MARISA SAX SYSIN");
            Autorizadoras.Add("00178", "STARFICHE");
            Autorizadoras.Add("00179", "ACE SEGUROS");
            Autorizadoras.Add("00180", "TOP CARD");
            Autorizadoras.Add("00181", "GETNET LAC");
            Autorizadoras.Add("00182", "UP SIGHT");
            Autorizadoras.Add("00183", "MAR");
            Autorizadoras.Add("00184", "FUNCIONAL CARD");
            Autorizadoras.Add("00185", "PHARMA SYSTEM");
            Autorizadoras.Add("00186", "MARKET PAY");
            Autorizadoras.Add("00187", "SICREDI");
            Autorizadoras.Add("00188", "ESCALENA");
            Autorizadoras.Add("00189", "N SERVIÇOS");
            Autorizadoras.Add("00190", "CSF CARREFOUR");
            Autorizadoras.Add("00191", "ATP");
            Autorizadoras.Add("00192", "AVST");
            Autorizadoras.Add("00193", "ALGORIX");
            Autorizadoras.Add("00194", "AMEX EMV");
            Autorizadoras.Add("00195", "COMPREMAX");
            Autorizadoras.Add("00196", "LIBERCARD");
            Autorizadoras.Add("00197", "SEICON");
            Autorizadoras.Add("00198", "SERASA AUTORIZ CRÉDITO");
            Autorizadoras.Add("00199", "SMARTN");
            Autorizadoras.Add("00200", "PLATCO");
            Autorizadoras.Add("00201", "SMARTNET EMV");
            Autorizadoras.Add("00202", "PROSA MÉXICO");
            Autorizadoras.Add("00203", "PEELA");
            Autorizadoras.Add("00204", "NUTRIK");
            Autorizadoras.Add("00205", "GOLDENFARMA PBM");
            Autorizadoras.Add("00206", "GLOBAL PAYMENTS");
            Autorizadoras.Add("00207", "ELAVON");
            Autorizadoras.Add("00208", "CTF");
            Autorizadoras.Add("00209", "BANESTIK");
            Autorizadoras.Add("00210", "VISA ARG");
            Autorizadoras.Add("00211", "AMEX ARG");
            Autorizadoras.Add("00212", "POSNET ARG");
            Autorizadoras.Add("00213", "AMEX MÉXICO");
            Autorizadoras.Add("00214", "ELETROZEMA");
            Autorizadoras.Add("00215", "BARIGUI");
            Autorizadoras.Add("00216", "SIMEC");
            Autorizadoras.Add("00217", "SGF");
            Autorizadoras.Add("00218", "HUG");
            Autorizadoras.Add("00219", "CARTÃO CONSIGNUM CARTÃO METTACARD");
            Autorizadoras.Add("00220", "DDTOTAL");
            Autorizadoras.Add("00221", "CARTÃO QUALIDADE");
            Autorizadoras.Add("00222", "REDECONV");
            Autorizadoras.Add("00223", "NUTRICARD");
            Autorizadoras.Add("00224", "DOTZ");
            Autorizadoras.Add("00225", "PREMIAÇÕES RAIZEN");
            Autorizadoras.Add("00226", "TROCO SOLIDÁRIO");
            Autorizadoras.Add("00227", "AMBEV SÓCIO TORCEDOR");
            Autorizadoras.Add("00228", "SEMPRE");
            Autorizadoras.Add("00229", "BIN");
            Autorizadoras.Add("00230", "COCIPA");
            Autorizadoras.Add("00231", "IBI MÉXICO");
            Autorizadoras.Add("00232", "SIANET");
            Autorizadoras.Add("00233", "SGCARDS");
            Autorizadoras.Add("00234", "CIAGROUP");
            Autorizadoras.Add("00235", "FILLIP");
            Autorizadoras.Add("00236", "CONDUCTOR");
            Autorizadoras.Add("00237", "LTM RAIZEN");
            Autorizadoras.Add("00238", "INCOMM");
            Autorizadoras.Add("00239", "VISA PASS FIRST");
            Autorizadoras.Add("00240", "CENCOSUD");
            Autorizadoras.Add("00241", "HIPERLIFE");
            Autorizadoras.Add("00242", "SITPOS");
            Autorizadoras.Add("00243", "AGT");
            Autorizadoras.Add("00244", "MIRA");
            Autorizadoras.Add("00245", "AMBEV 2 SÓCIO TORCEDOR");
            Autorizadoras.Add("00246", "JGV");
            Autorizadoras.Add("00247", "CREDSAT");
            Autorizadoras.Add("00248", "BRAZILIAN CARD");
            Autorizadoras.Add("00249", "RIACHUELO");
            Autorizadoras.Add("00250", "ITS RAIZEN");
            Autorizadoras.Add("00251", "SIMCRED");
            Autorizadoras.Add("00252", "BANCRED CARD");
            Autorizadoras.Add("00253", "CONEKTA");
            Autorizadoras.Add("00254", "SOFTCARD");
            Autorizadoras.Add("00255", "ECOPAG");
            Autorizadoras.Add("00256", "C&A AUTOMAÇÃO IBI");
            Autorizadoras.Add("00257", "C&A PARCERIAS BRADESCARD");
            Autorizadoras.Add("00258", "OGLOBA");
            Autorizadoras.Add("00259", "BANESE VOUCHER");
            Autorizadoras.Add("00260", "RAPP");
            Autorizadoras.Add("00261", "MONITORA POS");
            Autorizadoras.Add("00262", "SOLLUS");
            Autorizadoras.Add("00263", "FITCARD");
            Autorizadoras.Add("00264", "ADIANTI");
            Autorizadoras.Add("00265", "STONE");
            Autorizadoras.Add("00266", "DMCARD");
            Autorizadoras.Add("00267", "ICATU 2");
            Autorizadoras.Add("00268", "FARMASEG");
            Autorizadoras.Add("00269", "BIZ");
            Autorizadoras.Add("00270", "SEMPARAR RAIZEN");
            Autorizadoras.Add("00272", "PBM GLOBAL");
            Autorizadoras.Add("00271", "CARDSE");
            Autorizadoras.Add("00273", "PAYSMART");
            Autorizadoras.Add("00275", "ONEBOX");
            Autorizadoras.Add("00276", "CARTO");
            Autorizadoras.Add("00277", "WAYUP");
            Autorizadoras.Add("00296", "SAFRA");
            Autorizadoras.Add("00301", "CTF FROTA");
            Autorizadoras.Add("00303", "SIPAG");
        }

        public static string ObterAutorizadora(string chave)
        {
            Autorizadoras.TryGetValue(chave, out var valor);
            return valor;
        }
    }
}
