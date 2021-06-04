namespace Tef.Dominio.Enums
{
    public enum Comandos
    {
        /// <summary>
        /// [0] Está devolvendo um valor para, se desejado, ser armazenado pela automação.
        /// </summary>
        StoreValue = 0,

        /// <summary>
        /// [1] Mensagem para o visor do operador.
        /// </summary>
        DisplayOperatorMessage = 1,

        /// <summary>
        /// [2] Mensagem para o visor do cliente
        /// </summary>
        DisplayCustomerMessage = 2,

        /// <summary>
        /// [3] Mensagem para os dois visores
        /// </summary>
        DisplayBothMessage = 3,

        /// <summary>
        /// [4] Texto que deverá ser utilizado como título na apresentação do menu ( vide comando 21)
        /// </summary>
        DisplayMenuHeader = 4,

        /// <summary>
        /// [11] Deve remover a mensagem apresentada no visor do operador (comando 1)
        /// </summary>
        ClearOperatorMessage = 11,

        /// <summary>
        /// [12] Deve remover a mensagem apresentada no visor do cliente (comando 2)
        /// </summary>
        ClearCustomerMessage = 12,

        /// <summary>
        /// [13] Deve remover mensagem apresentada no visor do operador e do cliente (comando 3)
        /// </summary>
        ClearBothMessage = 13,

        /// <summary>
        /// [14] Deve limpar o texto utilizado como título na apresentação do menu (comando 4)
        /// </summary>
        ClearMenuHeader = 14,

        /// <summary>
        /// [15] Cabeçalho a ser apresentado pela aplicação. 
        /// Refere-se a exibição de informações adicionais que algumas transações necessitam mostrar na tela.
        /// </summary>
        DisplayHeader = 15,

        /// <summary>
        /// [16] Deve remover o cabeçalho apresentado pelo comando 15.
        /// </summary>
        ClearHeader = 16,

        /// <summary>
        /// [20] Deve apresentar o texto em Buffer, e obter uma resposta do tipo SIM/NÃO.
        /// No retorno o primeiro caráter presente em Buffer deve conter 0 se confirma e 1 se cancela
        /// </summary>
        DisplayConfirm = 20,

        /// <summary>
        /// [21] Deve apresentar um menu de opções e permitir que o usuário selecione uma delas.
        /// </summary>
        DisplayMenuOptions = 21,

        /// <summary>
        /// [22] Deve apresentar a mensagem em Buffer, e aguardar uma tecla do operador. 
        /// É utilizada quando se deseja que o operador seja avisado de alguma mensagem apresentada na tela.
        /// </summary>
        WaitAnyKey = 22,

        /// <summary>
        /// [23] Este comando indica que a rotina está perguntando para a aplicação se ele deseja interromper o 
        /// processo de coleta de dados ou não.
        /// </summary>
        CancelPinPadOperation = 23,

        /// <summary>
        /// [29] Análogo ao comando 30, porém deve ser coletado um campo que não requer intervenção do
        /// operador de caixa, ou seja, não precisa que seja digitado/mostrado na tela, e sim passado 
        /// diretamente para a biblioteca pela automação.
        /// </summary>
        ParameterInformedWithoutIntervention = 29,

        /// <summary>
        /// [30] Deve ser lido um campo cujo tamanho está entre TamMinimo e TamMaximo. 
        /// O campo lido deve ser devolvido em Buffer.
        /// </summary>
        TextInputNeeded = 30,

        /// <summary>
        /// [31] Deve ser lido o número de um cheque. 
        /// A coleta pode ser feita via leitura de CMC-7, digitação do CMC-7 ou pela digitação da primeira linha do cheque. 
        /// </summary>
        BankCheckInputNeeded = 31,

        /// <summary>
        /// [34] Deve ser lido um campo monetário ou seja, aceita o delimitador de centavos e devolvido no parâmetro Buffer.
        /// </summary>
        MoneyInputNeeded = 34,

        /// <summary>
        /// [35] Deve ser lido um código em barras ou o mesmo deve ser coletado manualmente
        /// </summary>
        BarcodeInputNeeded = 35,

        /// <summary>
        /// [41] Análogo ao Comando 30, porém o campo deve ser coletado de forma mascarada.
        /// </summary>
        PasswordTextInputNeeded = 41,

        /// <summary>
        /// [42] Menu identificado. Deve apresentar um menu de opções e permitir que o usuário selecione uma delas. 
        /// </summary>
        DisplayIdentifiedMenuOptions = 42,

        /// <summary>
        /// [50] A automação comercial deve exibir o QRCode na tela. 
        /// Para tanto, neste mesmo comando, será devolvida a string do QRCode com a identificação de campo 584.
        /// </summary>
        DisplayQRCode = 50,

        /// <summary>
        /// [51] A automação comercial deve remover da tela o QRCode exibido anteriormente, 
        /// pois o SiTef já devolveu uma resposta à CliSiTef.
        /// </summary>
        ClearQRCode = 51
    }

    public enum Retornos
    {
        /// <summary>
        /// [0] Sucesso na execução da função.
        /// </summary>
        Success = 0,

        /// <summary>
        /// [10000] Deve ser chamada a rotina de continuidade do processo.
        /// </summary>
        Continue = 10_000,

        /// <summary>
        /// [outro valor positivo] Negada pelo autorizador.
        /// </summary>
        DeniedByAuthor,

        /// <summary>
        /// [-1] Módulo não inicializado. 
        /// O PDV tentou chamar alguma rotina sem antes executar a função configura.
        /// </summary>
        Uninitialized = -1,

        /// <summary>
        /// [-2] Operação cancelada pelo operador.
        /// </summary>
        CanceledByOperator = -2,

        /// <summary>
        /// [-3] O parâmetro função / modalidade é inexistente/inválido.
        /// </summary>
        InvalidFunctionParameter = -3,

        /// <summary>
        /// [-4]  Falta de memória no PDV.
        /// </summary>
        PDVWithoutMemory = -4,

        /// <summary>
        /// [-5] Sem comunicação com o SiTef.
        /// </summary>
        SitefWithoutCommunication = -5,

        /// <summary>
        /// [-6] Operação cancelada pelo usuário (no pinpad).
        /// </summary>
        CanceledByUser = -6,

        /// <summary>
        /// [-7] Reservado
        /// </summary>
        ReservedI = -7,

        /// <summary>
        /// [-42] Reservado
        /// </summary>
        ReservedII = -42,

        /// <summary>
        /// [-8] A CliSiTef não possui a implementação da função necessária, provavelmente está desatualizada (a CliSiTefI é mais recente).
        /// </summary>
        NotImplemented = -8,

        /// <summary>
        /// [-9] A automação chamou a rotina ContinuaFuncaoSiTefInterativo sem antes iniciar uma função iterativa.
        /// </summary>
        IterativeFunctionUncalled = -9,

        /// <summary>
        /// [-10] Algum parâmetro obrigatório não foi passado pela automação comercial.
        /// </summary>
        MissingRequiredParameter = -10,

        /// <summary>
        /// [-12] Erro na execução da rotina iterativa. 
        /// Provavelmente o processo iterativo anterior não foi executado até o final (enquanto o retorno for igual a 10000).
        /// </summary>
        IterativeRoutineExecutionError = -12,

        /// <summary>
        /// [-13] Documento fiscal não encontrado nos registros da CliSiTef. 
        /// Retornado em funções de consulta tais como ObtemQuantidadeTransaçõesPendentes.
        /// </summary>
        FiscalDocumentsNotFound = -13,

        /// <summary>
        /// [-15] Operação cancelada pela automação comercial.
        /// </summary>
        CanceledOperationByAutomation = -15,

        /// <summary>
        /// [-20] Parâmetro inválido passado para a função.
        /// </summary>
        InvalidParameter = -20,

        /// <summary>
        /// [-21] Utilizada uma palavra proibida, por exemplo SENHA, para coletar dados em aberto no pinpad. 
        /// Por exemplo na função ObtemDadoPinpadDiretoEx.
        /// </summary>
        ForbiddenWord = -21,

        /// <summary>
        /// -22 Carteira Digital não habilitada
        /// </summary>
        DigitalWalletDisabled = -22,

        /// <summary>
        /// [-25] Erro no Correspondente Bancário: Deve realizar sangria.
        /// </summary>
        BankCorrespondentError = -25,

        /// <summary>
        /// [-30] Erro de acesso ao arquivo. 
        /// Certifique-se que o usuário que roda a aplicação tem direitos de leitura/escrita.
        /// </summary>
        FileAccessError = -30,

        /// <summary>
        /// [-40] Transação negada pelo servidor SiTef.
        /// </summary>
        TransactionDeniedByServer = -40,

        /// <summary>
        /// [-41] Dados inválidos.
        /// </summary>
        InvalidData = -41,

        /// <summary>
        /// [-43] Problema na execução de alguma das rotinas no pinpad.
        /// </summary>
        RoutineExecutionError = -43,

        /// <summary>
        /// [-50] Transação não segura.
        /// </summary>
        UnsafeTransaction = -50,

        /// <summary>
        /// [-100] Erro interno do módulo.
        /// </summary>
        InternalError = -100,

        /// <summary>
        /// [outro valor negativo] Erros detectados internamente pela rotina
        /// </summary>
        InternalErrorDetectedByRoutine
    }
}
