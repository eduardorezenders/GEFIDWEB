// Aqui adicionamos os metodos respeitando os nomes gerado em nossa classe atributo
// os nomes devem estar em caixa baixa tanto na integração como aqui no JavaScript (Nome atributo, parametro)
$.validator.unobtrusive.adapters.addSingleVal('horabrasil', 'params');

//Aqui temos a funcao que Valida do lado do cliente as horas digitadas utilizando uma expressão regular
$.validator.addMethod('horabrasil',
    function (value, element, params) {


        var parametros = params.split(',');


        // Valida caso o valor não tenha sido preenchido
        if (value.length == 0) {
            if (parametros[0].toString() == "False") {
                return true;
            }
        }

        // Atribui expressão
        var expReg = "";

        if (parametros[1].toString() == "True") {
            expReg = /^([0-1][0-9]|[2][0-3]):([0-5][0-9])$/;
        }
        else {
            expReg = /^([0-9][0-9][0-9]|[0-9][0-9]|[0-9]):([0-5][0-9])$/;
        }

        // Valida Expressao
        var result = value.match(expReg);

        // Retorna false caso a expressao nao seja valida
        if (result == null) {
            return false;
        }

        else {
            if (parametros[1].toString() == "True") {
                return true;
            }

        }

        // Caso Hora24=false e HoraRequerida=true valida se o valor não é nulo

        expReg = /^([0][0][0]|[0][0]|[0]):([0][0])$/;

        result = value.match(expReg);

        if (result == null) {
            return true;
        }

        return false;
    });
