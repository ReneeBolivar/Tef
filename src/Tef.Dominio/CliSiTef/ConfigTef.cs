namespace Tef.Dominio.CliSiTef
{
    public class ConfigTef : IConfigTef
    {
        public string PathArquivos { get; set; }
        public string IP { get; set; }
        public string Empresa { get; set; }
        public string CnpjEmpresa { get; set; }
        public string Terminal { get; set; }
        public string CnpjSoftwareHouse { get; set; }
        public string PinPadPorta { get; set; }
        public string PinPadMensagem { get; set; }
        public bool PinPadVerificar { get; set; }

        public ConfigTef(string pathArquivos,
                         string ip,
                         string empresa,
                         string cnpjEmpresa,
                         string terminal,
                         string cnpjSoftwareHouse,
                         string pinPadPorta,
                         string pinPadMensagem,
                         bool pinPadVerificar)
        {
            PathArquivos = pathArquivos;
            IP = ip;
            Empresa = empresa;
            CnpjEmpresa = cnpjEmpresa;
            Terminal = terminal;
            CnpjSoftwareHouse = cnpjSoftwareHouse;
            PinPadPorta = pinPadPorta;
            PinPadMensagem = pinPadMensagem;
            PinPadVerificar = pinPadVerificar;
        }
    }
}
