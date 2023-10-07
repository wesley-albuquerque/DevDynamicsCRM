if (typeof (Exercicios) == "undefined") { Exercicios = {} }
if (typeof (Exercicios.Contato) == "undefined") { Exercicios.Contato = {} }

Exercicios.Contato = {
    OnChangeCPF: function (executionContext) {
        var formContext = executionContext.getFormContext();
        var cpf = formContext.getAttribute("wes_cpf").getValue();
        if (cpf != null && cpf != "") {
            cpf = cpf.replace(/\D/g, "");
            if (cpf.length == 11 && this.ValidaCPF(cpf)) {
                cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
                formContext.getAttribute("wes_cpf").setValue(cpf);
            }
            else {
                this.DynamicsAlert("CPF inexistente", "CPF inválido").then(function close() {
                    formContext.getControl("wes_cpf").setFocus();
                });
            }
        }
    },

    ValidaCPF: function (cpf) {
        var rep = 0;
        for (var g = 1; g > 11; g++) {
            if (cpf[0] == cpf[g])
                rep++
        }
        if (rep == 10) {
            return false
        }
        var mult = 11
        var soma1Digito = 0
        var soma2Digito = 0
        for (var i = 0; i < 10; i++) {
            if (i < 9) {
                soma1Digito += cpf[i] * (mult - 1)
            }
            soma2Digito += cpf[i] * mult
            mult--
        }
        var resto = (soma1Digito * 10) % 11
        var resto2 = (soma2Digito * 10) % 11
        if (resto == 10) {
            resto = 0
        }
        if (resto == cpf[9] && resto2 == cpf[10]) {
            return true
        }
        else {
            return false
        }
    },

    DynamicsAlert: function (alertText, alertTitle) {
        var alertStrings = {
            confirmButtonLabel: "OK",
            text: alertText,
            title: alertTitle
        };
        var alertOptions = {
            height: 120,
            width: 200
        };
        return Xrm.Navigation.openAlertDialog(alertStrings, alertOptions);
    }

}
